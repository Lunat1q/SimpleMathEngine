using System.Diagnostics;
using MathEngine.Context;

namespace MathEngine.Expressions
{
    [DebuggerDisplay("{GetDisplayString()}")]
    public abstract class Expression
    {
        public abstract decimal Evaluate(IDataContext dataContext);

        public abstract string GetDisplayString();

        public static decimal Evaluate(Expression exp, IDataContext context = null)
        {
            return exp.Evaluate(context ?? DefaultContext.Instance);
        }
    }
}