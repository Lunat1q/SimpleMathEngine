namespace MathEngine.Expressions
{
    internal sealed class NumberSubtractExpression : BinaryPredicateExpression
    {
        public NumberSubtractExpression(Expression leftExpression, Expression rightExpression) : base(leftExpression, rightExpression)
        {
        }

        protected override decimal EvaluateOperation(decimal d1, decimal d2)
        {
            return d1 - d2;
        }
        public override string GetDisplayString()
        {
            return $"{this.LeftExpression.GetDisplayString()} - {this.RightExpression.GetDisplayString()}";
        }
    }
}