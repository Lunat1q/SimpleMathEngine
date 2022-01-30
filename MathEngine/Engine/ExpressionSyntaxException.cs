using System;

namespace MathEngine.Engine
{
    public sealed class ExpressionSyntaxException : Exception
    {
        public ExpressionSyntaxException(string message) : base(message)
        {
        }
    }
}