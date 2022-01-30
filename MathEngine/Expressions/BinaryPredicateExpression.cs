using MathEngine.Context;

namespace MathEngine.Expressions
{
    internal abstract class BinaryPredicateExpression : Expression
    {
        private protected readonly Expression LeftExpression;

        private protected readonly Expression RightExpression;

        protected BinaryPredicateExpression(Expression leftExpression, Expression rightExpression)
        {
            this.LeftExpression = leftExpression;
            this.RightExpression = rightExpression;
        }

        public override decimal Evaluate(IDataContext dataContext)
        {
            var lhsVal = this.LeftExpression.Evaluate(dataContext);
            var rhsVal = this.RightExpression.Evaluate(dataContext);

            var result = this.EvaluateOperation(lhsVal, rhsVal);
            return result;
        }

        protected abstract decimal EvaluateOperation(decimal d1, decimal d2);
    }
}