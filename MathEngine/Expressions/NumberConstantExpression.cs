using MathEngine.Context;

namespace MathEngine.Expressions
{
    internal sealed class NumberConstantExpression : Expression
    {
        private readonly decimal _number;

        public NumberConstantExpression(decimal number)
        {
            this._number = number;
        }

        public override decimal Evaluate(IDataContext dataContext = null)
        {
            return this._number;
        }
        public override string GetDisplayString()
        {
            return $"{this._number}";
        }
    }
}