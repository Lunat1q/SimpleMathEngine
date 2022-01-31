using System;
using MathEngine.Context;

namespace MathEngine.Expressions
{
    internal sealed class StringVariableExpression : Expression
    {
        private readonly string _name;

        public StringVariableExpression(string name)
        {
            this._name = name;
        }

        public override double Evaluate(IDataContext dataContext)
        {
            return dataContext?.ResolveVariable(this._name) ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public override string GetDisplayString()
        {
            return $"{this._name}";
        }
    }
}