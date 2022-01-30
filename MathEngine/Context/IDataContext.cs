namespace MathEngine.Context
{
    public interface IDataContext
    {
        IDataContext InnerDataContext { get; }

        decimal ResolveVariable(string name);
    }
}