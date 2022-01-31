using MathEngine.Context;

namespace MathEngine.Expressions
{
    internal sealed class NumberConstantExpression : Expression
    {
        private readonly double _number;

        public NumberConstantExpression(double number)
        {
            this._number = number;
        }

        public override double Evaluate(IDataContext dataContext = null)
        {
            return this._number;
        }
        public override string GetDisplayString()
        {
            return $"{this._number}";
        }
    }
}