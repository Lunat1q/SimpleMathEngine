using System;
using System.Collections.Generic;
using System.Linq;
using MathEngine.Expressions;

namespace MathEngine.Functions
{
    [FunctionName(FunctionName)]
    internal class PowerFunctionExpression : NumericFunctionCallBaseExpression
    {
        internal const string FunctionName = MathFunctionNames.Power;
        public PowerFunctionExpression(string name, IReadOnlyCollection<Expression> arguments) : base(name, arguments)
        {
        }

        private protected override bool ValidateArguments()
        {
            return this.Arguments.Count == 2;
        }

        private protected override decimal ExecuteFunction(IList<decimal> arguments)
        {
            var powResult = Math.Pow((double) arguments[0], (double) arguments[1]);
            var ret = (decimal)powResult;
            return ret;
        }

        public override string GetDisplayString()
        {
            return $"{FunctionName}({string.Join(", ", this.Arguments.Select(x=>x.GetDisplayString()))})";
        }
    }
}