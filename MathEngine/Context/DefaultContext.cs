namespace MathEngine.Context
{
    public static class DefaultContext
    {
        public static IDataContext Instance => new MathBasicDataContext();
    }
}