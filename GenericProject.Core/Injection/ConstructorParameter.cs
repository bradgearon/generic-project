namespace GenericProject.Core.Injection
{
    public class ConstructorParameter
    {
        public string Name { get; private set; }

        public object Value { get; private set; }

        public ConstructorParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}