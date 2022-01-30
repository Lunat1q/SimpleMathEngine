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

        public decimal ResolveVariable(string name)
        {
            switch (name.ToLower())
            {
                case "pi":
                    return (decimal) Math.PI;
                case "e":
                    return (decimal) Math.E;
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