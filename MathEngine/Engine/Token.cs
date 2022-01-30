namespace MathEngine.Engine
{
    internal enum Token
    {
        EOF,
        Add,
        Subtract,
        Number,
        Multiply,
        Divide,
        OpenParenthesis,
        CloseParenthesis,
        Identifier,
        Comma
    }
}