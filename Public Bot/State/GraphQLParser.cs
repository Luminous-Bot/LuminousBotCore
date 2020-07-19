using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot
{
    public class GraphQLName : Attribute
    {
        public string Name { get; set; }
        public GraphQLName(string name)
        {
            Name = name;
        }
    }
    public class GraphQLProp : Attribute { }
    public class GraphQLObj : Attribute { }
    public class GraphQLSVar : Attribute { }
    public class GraphQLSObj : Attribute { }
    
    public class GraphQLParser
    {
        //public static string JoinQueries(params string[] Queries)
        //    => string.Join(',', Queries);

        private static Dictionary<Type, Func<object, string>> Parser = new Dictionary<Type, Func<object, string>>()
        {
            {typeof(ulong), (object val) => $"\"{val}\"" },
            {typeof(DateTime), (object val) => { var dt = (DateTime)val; return $"\"{dt.ToString("o")}\""; } },
            {typeof(string), (object val) => $"\"{val}\"" },
            {typeof(bool), (object val) => { bool vl = (bool)val; return vl ? "true" : "false"; } },
            {typeof(Action), (object val) => $"\"{val.ToString()}\"" }
        };
        public class gqlBase
        {
            public string operationName { get; set; }
            public object variables { get; set; }
            public string query { get; set; }
        }
        public static string GenerateGQLMutation<T>(string opname, bool hasVars, T obj, string varName = "", string varType = "", params KeyValuePair<string, object>[] Params)
        {
            Logger.Write($"Making Mutation for method {opname}", Logger.Severity.State);
            var classtype = typeof(T);
            var typeVars = classtype.GetProperties().Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLSVar) || x.AttributeType == typeof(GraphQLSObj)));
            List<string> vars = new List<string>();
            foreach(var tv in typeVars)
            {
                string name = tv.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLName)) ? ((GraphQLName)tv.GetCustomAttribute(typeof(GraphQLName))).Name : tv.Name;
                if(tv.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLSObj)))
                {
                    var tvT = tv.PropertyType;
                    var l = RecurseMutateVars(tvT, tv.GetValue(obj));
                    vars.AddRange(l);
                }
                else
                {
                    string val = "";
                    if (tv.PropertyType.Name.Contains("List"))
                        val = JsonConvert.SerializeObject(tv.GetValue(obj));
                    else
                        val = Parser.ContainsKey(tv.PropertyType) ? Parser[tv.PropertyType](tv.GetValue(obj)) : tv.GetValue(obj).ToString();
                    vars.Add($"\"{name}\": {val}");
                }
            }
            string parms = "";
            foreach (var p in Params)
                parms += $"{p.Key}: \\\"{p.Value.ToString()}\\\" ";
            string query = $"{{\"operationName\": \"{opname}\"," +
                           $"\"variables\": {(hasVars ? $"{{ \"{varName}\": {{ {string.Join(", ", vars)} }} }}" : "{ }")}," +
                           $"\"query\": \"mutation {opname}{(hasVars ? $"(${varName}: {varType})" : "")} {{ {opname}({( hasVars ? $"{varName}: ${varName}, {parms}" : parms)}) {genProps(typeof(T))} }}\" }}";

            return query;
        }
        public static List<string> RecurseMutateVars(Type type, object obj)
        {
            var typeVars = type.GetProperties().Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLSVar) || x.AttributeType == typeof(GraphQLSObj)));
            List<string> vars = new List<string>();
            foreach (var tv in typeVars)
            {
                string name = tv.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLName)) ? ((GraphQLName)tv.GetCustomAttribute(typeof(GraphQLName))).Name : tv.Name;
                if (tv.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLSObj)) && !tv.PropertyType.Name.Contains("List"))
                {
                    var tvT = tv.PropertyType;
                    var l = RecurseMutateVars(tvT, tv.GetValue(obj));
                    vars.AddRange(l);
                }
                else
                {
                    string val = "";
                    if (tv.PropertyType.Name.Contains("List"))
                        val = JsonConvert.SerializeObject(tv.GetValue(obj));
                    else
                        val = Parser.ContainsKey(tv.PropertyType) ? Parser[tv.PropertyType](tv.GetValue(obj)) : tv.GetValue(obj).ToString();
                    vars.Add($"\"{name}\": {val}");
                }
            }
            return vars;
        }
        public static string GenerateGQLQuery<T>(string method, params KeyValuePair<string, object>[] Params)
        {
            Logger.Write($"Making Query for method {method}", Logger.Severity.State);
            string parms = "";
            foreach (var p in Params)
                parms += $"{p.Key}: \"{p.Value.ToString()}\" ";
            string query = $"{{ {method}{(Params.Length > 0 ? $"({parms})" : " ")} {{ ";
            var classType = typeof(T);

            var attall = classType.GetProperties().Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLName) || x.AttributeType == typeof(GraphQLProp) || x.AttributeType == typeof(GraphQLObj))).ToList();

            var attprops = attall.Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLProp))).ToList();
            var attobj = attall.Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLObj))).ToList();
            foreach(var prop in attprops)
                query += $"{(prop.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLName)) ? ((GraphQLName)prop.GetCustomAttribute(typeof(GraphQLName))).Name : prop.Name)} ";
            foreach (var obj in attobj)
                query += $"{(obj.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLName)) ? ((GraphQLName)obj.GetCustomAttribute(typeof(GraphQLName))).Name : obj.Name)} " + genProps(obj.PropertyType);
            var f = new gqlBase()
            {
                operationName = null,
                query = query + "} }",
                variables = { }
            };
            return JsonConvert.SerializeObject(f);
        }
        private static string genProps(Type inf)
        {
            //schm = { props } 
            string query = "{ ";
            var type = inf;
            if (type.Name.Contains("List"))
            {
                type = type.GenericTypeArguments[0];
            }
            var attall = type.GetProperties().Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLName) || x.AttributeType == typeof(GraphQLProp) || x.AttributeType == typeof(GraphQLObj))).ToList();
            var attprops = attall.Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLProp))).ToList();
            var attobj = attall.Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLObj))).ToList();
            foreach (var prop in attprops)
                query += $"{(prop.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLName)) ? ((GraphQLName)prop.GetCustomAttribute(typeof(GraphQLName))).Name : prop.Name)} ";
            foreach (var obj in attobj)
                query += $"{(obj.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLName)) ? ((GraphQLName)obj.GetCustomAttribute(typeof(GraphQLName))).Name : obj.Name)} {genProps(obj.PropertyType)}";
            return query + "} ";
        }
    }
}
