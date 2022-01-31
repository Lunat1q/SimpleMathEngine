using System;
using MathEngine.Engine;

namespace MathEngine.Context
{
    internal sealed class MathBasicDataContext : IDataContext
    {
        public MathBasicDataContext(IDataContext innerDataContext)
        {
            this.InnerDataContext = innerDataContext;
        }

        public MathBasicDataContext() : this(null)
        {
        }

        public double ResolveVariable(string name)
        {
            switch (name.ToLower())
            {
                case "pi":
                    return Math.PI;
                case "e":
                    return Math.E;
                default:
                    if (this.InnerDataContext != null)
                    {
                        return this.InnerDataContext.ResolveVariable(name);
                    }

                    throw new ExpressionSyntaxException($"Unknown variable '{name}'");
            }
        }

        public IDataContext InnerDataContext { get; }
    }
}