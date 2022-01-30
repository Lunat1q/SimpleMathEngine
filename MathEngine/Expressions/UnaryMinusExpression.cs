namespace MathEngine.Expressions
{
    internal sealed class UnaryMinusExpression : UnaryBaseExpression
    {
        public UnaryMinusExpression(Expression lhs) : base(lhs)
        {
        }

        protected override decimal EvaluateOperation(decimal d1)
        {
            return -1 * d1;
        }

        public override string GetDisplayString()
        {
            return $"-1 * ({this.Lhs.GetDisplayString()})";
        }
    }
}