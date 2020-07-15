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
    
    public class GraphQLParser
    {
        private static Dictionary<Type, Func<object, string>> Parser = new Dictionary<Type, Func<object, string>>()
        {
            {typeof(ulong), (object val) => $"\"{val}\"" },
            {typeof(DateTime), (object val) => { var dt = (DateTime)val; return $"\"{dt.ToString("o")}\""; } },
            {typeof(string), (object val) => $"\"{val}\"" }
        };
        public class gqlBase
        {
            public string operationName { get; set; }
            public object variables { get; set; }
            public string query { get; set; }
        }
        public static string GenerateGQLMutation<T>(string opname, bool hasVars, T obj, string varName = "", params KeyValuePair<string, object>[] Params)
        {
            var classtype = typeof(T);
            var typeVars = classtype.GetProperties().Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLSVar)));
            List<string> vars = new List<string>();
            foreach(var tv in typeVars)
            {
                string name = tv.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLName)) ? ((GraphQLName)tv.GetCustomAttribute(typeof(GraphQLName))).Name : tv.Name;
                string val = Parser.ContainsKey(tv.PropertyType) ? Parser[tv.PropertyType](tv.GetValue(obj)) : tv.GetValue(obj).ToString();
                vars.Add($"\"{name}\": {val}");
            }
            string parms = "";
            foreach (var p in Params)
                parms += $"{p.Key}: \"{p.Value.ToString()}\" ";
            string query = $"{{\"operationName\": \"{opname}\"," +
                           $"\"variables\": {(hasVars ? $"{{ \"data\": {{ {string.Join(", ", vars)} }} }}" : "{ }")}," +
                           $"\"query\": \"mutation {opname}({(hasVars ? $"$data: {varName} {parms}" : parms)}) {{ {opname}(data: $data) {genProps(typeof(T))} }}\" }}";

            return query;
        }
        public static string GenerateGQLQuery<T>(string method, params KeyValuePair<string, object>[] Params)
        {
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
