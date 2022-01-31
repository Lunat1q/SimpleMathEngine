namespace MathEngine.Context
{
    public interface IDataContext
    {
        IDataContext InnerDataContext { get; }

        double ResolveVariable(string name);
    }
}