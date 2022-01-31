using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MathEngine.Expressions;

namespace MathEngine.Functions
{
    [FunctionName("cos")]
    internal class CosineFunctionExpression : NumericFunctionCallBaseExpression
    {
        private const string FunctionName = MathFunctionNames.Cosine;

        public CosineFunctionExpression(string name, IReadOnlyCollection<Expression> arguments) : base(name, arguments)
        {
        }

        private protected override bool ValidateArguments()
        {
            return this.Arguments.Count == 1;
        }

        private protected override double ExecuteFunction(IList<double> arguments)
        {
            var rad = MathHelper.Reg2Rad(arguments.First());
            var res = Math.Cos(rad);
            return res;
        }

        public override string GetDisplayString()
        {
            return $"{FunctionName}({string.Join(", ", this.Arguments.Select(x => x.GetDisplayString()))})";
        }
    }
}