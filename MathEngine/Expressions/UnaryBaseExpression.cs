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

        public override decimal Evaluate(IDataContext dataContext)
        {
            var lhsVal = this.Lhs.Evaluate(dataContext);

            var result = this.EvaluateOperation(lhsVal);
            return result;
        }

        protected abstract decimal EvaluateOperation(decimal d1);
    }
}