using System.Text;

namespace Coding.Exercise
{
    public class Property
    {
        public string Name, Type;
        public List<Property> Properties = new List<Property>();
        private const int indentSize = 2;

        public Property()
        {

        }

        public Property(string name, string type)
        {
            Name = name;
            Type = type;
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize + indent);


            if (string.IsNullOrWhiteSpace(Type))
            {
                sb.AppendLine($"public class {Name}");
                sb.AppendLine("{");
            } 
            
            if(!string.IsNullOrWhiteSpace(Type) && !string.IsNullOrWhiteSpace(Name))
            {
                sb.AppendLine($"{i}public {Type} {Name};");
            }
                
            foreach (var e in Properties)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }

            if(Properties.Count > 0)
            {
                sb.AppendLine("}");
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class CodeBuilder
    {
        private readonly string className;
        Property rootForClassName = new Property();

        public CodeBuilder(string className)
        {
            this.className = className;
            rootForClassName.Name = className;
        }

        public CodeBuilder AddProperty(string name, string type)
        {
            var property = new Property(name, type);
            rootForClassName.Properties.Add(property);
            return this;
        }

        public void Clear()
        {
            rootForClassName = new Property { Name = className };
        }

        public override string ToString()
        {
            return rootForClassName.ToString();
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var @class = new CodeBuilder("Person");
            @class.AddProperty("Name", "string").AddProperty("Age", "int");
            Console.WriteLine(@class.ToString());
        }
    }
}