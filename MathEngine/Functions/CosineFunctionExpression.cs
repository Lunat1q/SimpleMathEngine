using System;
using System.Collections.Generic;
using System.Linq;
using MathEngine.Expressions;

namespace MathEngine.Functions
{
    [FunctionName("cos")]
    internal class CosineFunctionExpression : NumericFunctionCallBaseExpression
    {
        public CosineFunctionExpression(string name, IReadOnlyCollection<Expression> arguments) : base(name, arguments)
        {
        }

        private protected override bool ValidateArguments()
        {
            return this.Arguments.Count == 1;
        }

        private protected override decimal ExecuteFunction(IEnumerable<decimal> arguments)
        {
            var rad = MathHelper.Reg2Rad(arguments.First());
            var res = (decimal)Math.Cos(rad);
            return res;
        }

        public override string GetDisplayString()
        {
            return $"cos({string.Join(", ", this.Arguments.Select(x=>x.GetDisplayString()))})";
        }
    }
}