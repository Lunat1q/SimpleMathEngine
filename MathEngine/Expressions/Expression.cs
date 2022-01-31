using System.Diagnostics;
using MathEngine.Context;

namespace MathEngine.Expressions
{
    [DebuggerDisplay("{GetDisplayString()}")]
    public abstract class Expression
    {
        public abstract double Evaluate(IDataContext dataContext);

        public abstract string GetDisplayString();

        public static double Evaluate(Expression exp, IDataContext context = null)
        {
            return exp.Evaluate(context ?? DefaultContext.Instance);
        }
    }
}