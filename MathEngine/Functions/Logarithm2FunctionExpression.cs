using System;
using System.Collections.Generic;
using System.Linq;
using MathEngine.Expressions;

namespace MathEngine.Functions
{
    [FunctionName(FunctionName)]
    internal class Logarithm2FunctionExpression : NumericFunctionCallBaseExpression
    {
        internal const string FunctionName = MathFunctionNames.LogarithmBase2;

        public Logarithm2FunctionExpression(string name, IReadOnlyCollection<Expression> arguments) : base(name, arguments)
        {
        }

        private protected override bool ValidateArguments()
        {
            return this.Arguments.Count == 1;
        }

        private protected override double ExecuteFunction(IList<double> arguments)
        {
            var ret = Math.Log2(arguments[0]);
            return ret;
        }

        public override string GetDisplayString()
        {
            return $"{FunctionName}({string.Join(", ", this.Arguments.Select(x => x.GetDisplayString()))})";
        }
    }
}