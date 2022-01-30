using System;

namespace MathEngine.Functions
{
    internal class FunctionNameAttribute : Attribute
    {
        public string Name { get; }

        public FunctionNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}