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

    public class MutationBucket<T>
    {
        private List<string> Mutations { get; set; } = new List<string>();
        public int Count
            => Mutations.Count;
        private string opname { get; set; }
        public void Add(T obj, params (string, object)[] Params)
        {
            string parms = "";
            foreach (var p in Params)
                parms += $"{p.Item1}: {p.Item2.ToString()} ";

            Mutations.Add($"_{Mutations.Count}: {opname}({parms}) {GraphQLParser.genProps(typeof(T))}");
        }
        public MutationBucket(string opname)
        {
            this.opname = opname;
        }   
        public string Build()
        {
            string query = $"{{\"operationName\": \"{opname}\"," +
                           $"\"variables\": {{}}, " +
                           $"\"query\": \"mutation {opname} {{ {string.Join(" ", Mutations)} }}\" }}";

            return query;
        }
    }
    public class VaredMutationBucket<T>
    {
        public int Count
            => Mutations.Count;
        public List<string> Mutations { get; set; } = new List<string>();
        private List<string> Vars { get; set; } = new List<string>();
        private string mV = "";

        private string opname { get; set; }
        private bool hasVars { get; set; }
        private string varType { get; set; }
        private string varName { get; set; }
        public void Add(T obj, params (string, object)[] t)
        {
            string parms = "";
            foreach (var p in t)
                parms += $"{p.Item1}: {p.Item2.ToString()} ";
            if (hasVars)
            {
                List<string> vars = new List<string>();
                var typeVars = typeof(T).GetProperties().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(GraphQLSVar) || y.AttributeType == typeof(GraphQLSObj)));
                foreach (var tv in typeVars)
                {
                    string name = tv.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLName)) ? ((GraphQLName)tv.GetCustomAttribute(typeof(GraphQLName))).Name : tv.Name;
                    if (tv.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLSObj)))
                    {
                        var tvT = tv.PropertyType;
                        var l = GraphQLParser.RecurseMutateVars(tvT, tv.GetValue(obj));
                        vars.AddRange(l);
                    }
                    else
                    {
                        string val = "";
                        if (tv.PropertyType.Name.Contains("List"))
                            val = JsonConvert.SerializeObject(tv.GetValue(obj));
                        else
                            val = GraphQLParser.Parser.ContainsKey(tv.PropertyType) ? GraphQLParser.Parser[tv.PropertyType](tv.GetValue(obj)) : tv.GetValue(obj).ToString();
                        vars.Add($"\"{name}\": {val}");
                    }
                }
                mV += $"$_{Mutations.Count}: {varType} ";
                Vars.Add($"\"_{Mutations.Count}\": {{ {string.Join(", ", vars)} }}");
            }
            Mutations.Add($"_{Mutations.Count}: {opname}({(hasVars ? $"{varName}: $_{Mutations.Count} " : "")}{parms}) {GraphQLParser.genProps(typeof(T))}");
        }
        public VaredMutationBucket(string opname, bool hasvars, string varName = "", string varType = "")
        {
            this.opname = opname;
            this.hasVars = hasvars;
            this.varType = varType;
            this.varName = varName;
        }
        public string Build()
        {

            string query = $"{{\"operationName\": \"{opname}\"," +
                           $"\"variables\": {{ {(hasVars ? $"{string.Join(", ", Vars)}" : "")} }}, " +
                           $"\"query\": \"mutation {opname}{(this.hasVars ? $"({mV})" : "")} {{ {string.Join(" ", Mutations)} }}\" }}";

            return query;
        }
    }
    
    public class GraphQLParser
    {
        
       
        internal static Dictionary<Type, Func<object, string>> Parser = new Dictionary<Type, Func<object, string>>()
        {
            {typeof(ulong), (object val) => $"\"{val}\"" },
            {typeof(DateTime), (object val) => { var dt = (DateTime)val; return $"\"{dt.ToString("o")}\""; } },
            {typeof(string), (object val) => $"\"{val}\"" },
            {typeof(bool), (object val) => { bool vl = (bool)val; return vl ? "true" : "false"; } },
            {typeof(Action), (object val) => $"\"{val.ToString()}\"" },
            {typeof(int), (object val) => $"{val}" }
        };
        public class gqlBase
        {
            public string operationName { get; set; }
            public object variables { get; set; }
            public string query { get; set; }
        }
        public static string CleanUserContent(string msg)
        {
            string c = JsonConvert.SerializeObject(msg);
            return c.Remove(0, 1).Remove(c.Length - 2, 1);
        }
        public static string GenerateGQLMutation<T>(string opname, bool hasVars, T obj, string varName = "", string varType = "", params (string, object)[] Params)
        {
            Logger.Write($"Making Mutation for method {opname}", Logger.Severity.State);
            var classtype = typeof(T);
            var typeVars = classtype.GetProperties().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(GraphQLSVar) || y.AttributeType == typeof(GraphQLSObj)));
            List<string> vars = new List<string>();
            if (hasVars)
            {
                foreach (var tv in typeVars)
                {
                    string name = tv.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLName)) ? ((GraphQLName)tv.GetCustomAttribute(typeof(GraphQLName))).Name : tv.Name;
                    if (tv.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLSObj)))
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
            }
            string parms = "";
            foreach (var p in Params)
                parms += $"{p.Item1}: {(p.Item2 == null ? "null" : (p.Item2.GetType() == typeof(bool) ? ((bool)p.Item2 ? "true" : "false") : $"\\\"{p.Item2}\\\""))} ";
            string query = $"{{\"operationName\": \"{opname}\"," +
                           $"\"variables\": {(hasVars ? $"{{ \"{varName}\": {{ {string.Join(", ", vars)} }} }}" : "{ }")}," +
                           $"\"query\": \"mutation {opname}{(hasVars ? $"(${varName}: {varType})" : "")} {{ {opname}({(hasVars ? $"{varName}: ${varName}, {parms}" : parms)}) {genProps(typeof(T))} }}\" }}";

            return query;
        }
        public static List<string> RecurseMutateVars(Type type, object obj)
        {
            var typeVars = type.GetProperties().Where(x => x.CustomAttributes.Any(z => z.AttributeType == typeof(GraphQLSVar) || z.AttributeType == typeof(GraphQLSObj)));
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
                    if (tv.PropertyType.Name.Contains("List") && tv.CustomAttributes.Any(x => x.AttributeType == typeof(GraphQLSObj)))
                        val = GenerateList(tv.PropertyType.GenericTypeArguments.First(), tv.GetValue(obj) as IList);
                    else if (tv.PropertyType.Name.Contains("List"))
                        val = genPropList(tv.GetValue(obj) as IList);
                    else
                        val = Parser.ContainsKey(tv.PropertyType) ? Parser[tv.PropertyType](tv.GetValue(obj)) : tv.GetValue(obj).ToString();
                    vars.Add($"\"{name}\": {val}");
                }
            }
            return vars;
        }
        public static string genPropList(IList obj)
        {
            if (obj.Count == 0)
                return "[]";
            List<string> final = new List<string>();
            var intyp = obj.GetType().GenericTypeArguments.First();
            foreach (var o in obj)
                final.Add(Parser.ContainsKey(intyp) ? Parser[intyp](o) : o.ToString());
            return $"[ {string.Join(", ", final)} ]";
        }
        public static string GenerateList(Type listof, IList obj)
        {
            if (obj.Count == 0)
                return "[]";
            var typeVars = listof.GetProperties().Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(GraphQLSVar) || y.AttributeType == typeof(GraphQLSObj)));
            List<string> fn = new List<string>();
            foreach (var item in obj)
            {
                List<string> fnl = new List<string>();
                foreach (var t in typeVars)
                {
                    fnl.Add($"\"{t.Name}\": {(Parser.ContainsKey(t.PropertyType) ? Parser[t.PropertyType](t.GetValue(item)) : t.GetValue(item))}");

                }
                fn.Add(string.Join(", ", fnl));
            }

            return $"[ {{ {string.Join(" }, { ", fn)} }} ]";

        }
        public static string GenerateGQLQuery<T>(string method, params (string, object)[] Params)
        {
            Logger.Write($"Making Query for method {method}", Logger.Severity.State);
            string parms = "";
            foreach (var p in Params)
            {
                var t = p.Item2.GetType();
                parms += $"{p.Item1}: {(Parser.ContainsKey(t) ? Parser[t](p.Item2) : $"\"{p.Item2}\"")} "; 
            }
            string query = $"{{ {method}{(Params.Length > 0 ? $"({parms})" : " ")} {{ ";
            var classType = typeof(T);
            if (classType.Name.StartsWith("List"))
                classType = classType.GenericTypeArguments.First();
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
        internal static string genProps(Type inf)
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
