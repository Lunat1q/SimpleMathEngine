namespace MathEngine.Expressions
{
    internal sealed class NumberMultiplyExpression : BinaryPredicateExpression
    {
        public NumberMultiplyExpression(Expression leftExpression, Expression rightExpression) : base(leftExpression, rightExpression)
        {
        }

        protected override double EvaluateOperation(double d1, double d2)
        {
            return d1 * d2;
        }
        public override string GetDisplayString()
        {
            return $"{this.LeftExpression.GetDisplayString()} * {this.RightExpression.GetDisplayString()}";
        }
    }
}