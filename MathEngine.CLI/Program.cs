using System;
using MathEngine.Context;
using MathEngine.Engine;
using MathEngine.Expressions;

namespace MathEngine.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter your expression:");

                var exp = Console.ReadLine();
                try
                {
                    var expressionTree = ExpressionParser.Parse(exp);
                    //Console.WriteLine($"Parsed expression as string: {expressionTree.GetDisplayString()}");
                    var total = Expression.Evaluate(expressionTree);

                    Console.WriteLine($"Result: {total}");
                }
                catch (ExpressionSyntaxException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArithmeticException a)
                {
                    Console.WriteLine(a.Message);
                }
                Console.WriteLine(new string('-', 20));
            }
        }
    }
}
