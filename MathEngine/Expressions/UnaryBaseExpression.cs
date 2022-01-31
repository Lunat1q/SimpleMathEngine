using MathEngine.Context;

namespace MathEngine.Expressions
{
    internal abstract class UnaryBaseExpression : Expression
    {
        private protected readonly Expression Lhs;

        protected UnaryBaseExpression(Expression lhs)
        {
            this.Lhs = lhs;
        }

        public override double Evaluate(IDataContext dataContext)
        {
            var lhsVal = this.Lhs.Evaluate(dataContext);

            var result = this.EvaluateOperation(lhsVal);
            return result;
        }

        protected abstract double EvaluateOperation(double d1);
    }
}