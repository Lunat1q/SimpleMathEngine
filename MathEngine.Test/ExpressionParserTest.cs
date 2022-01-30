using System;
using FluentAssertions;
using MathEngine.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathEngine.Test
{
    [TestClass]
    public class ExpressionParserTest
    {
        [TestMethod]
        public void TestParseEvaluate_Add10and10_20()
        {
            var expression = ExpressionParser.Parse("10 + 10");
            expression.Evaluate(null).Should().Be(20);
        }

        [TestMethod]
        public void TestParseEvaluate_Add10and10NoSpace_20()
        {
            var expression = ExpressionParser.Parse("10+10");
            expression.Evaluate(null).Should().Be(20);
        }

        [TestMethod]
        public void TestParseEvaluate_withDecimalPoint_20()
        {
            var expression = ExpressionParser.Parse("10.5555+10");
            expression.Evaluate(null).Should().Be(20.5555m);
        }

        [TestMethod]
        public void TestParseEvaluate_Add10and20_minus10()
        {
            var expression = ExpressionParser.Parse("10 - 20");
            expression.Evaluate(null).Should().Be(-10);
        }

        [TestMethod]
        public void TestParseEvaluate_minus10_minus10()
        {
            var expression = ExpressionParser.Parse("-10");
            expression.Evaluate(null).Should().Be(-10);
        }

        [TestMethod]
        public void TestParseEvaluate_20PlusMinus10_10()
        {
            var expression = ExpressionParser.Parse("20+-10");
            expression.Evaluate(null).Should().Be(10);
        }

        [TestMethod]
        public void TestParseEvaluate_20PlusMinus10Spaced_10()
        {
            var expression = ExpressionParser.Parse("20 + -10");
            expression.Evaluate(null).Should().Be(10);
        }

        [TestMethod]
        public void TestParseEvaluate_negativeOfNegative10_10()
        {
            var expression = ExpressionParser.Parse("--10");
            expression.Evaluate(null).Should().Be(10);
        }

        [TestMethod]
        public void TestParseEvaluate_differentPriorityOperation10Plus20Mult30_610()
        {
            var expression = ExpressionParser.Parse("10 + 20 * 30");
            expression.Evaluate(null).Should().Be(610);
        }

        [TestMethod]
        public void TestParseEvaluate_differentPriorityOperation20Mult30Plus10_610()
        {
            var expression = ExpressionParser.Parse("20 * 30 + 10");
            expression.Evaluate(null).Should().Be(610);
        }

        [TestMethod]
        public void TestParseEvaluate_parenthesisExpression_800()
        {
            var expression = ExpressionParser.Parse("20 * (30 + 10)");
            expression.Evaluate(null).Should().Be(800);
        }

        [TestMethod]
        public void TestParseEvaluate_parenthesisExpression_1200()
        {
            var expression = ExpressionParser.Parse("20 * (30 + 10 + 20)");
            expression.Evaluate(null).Should().Be(1200);
        }

        [TestMethod]
        public void TestParseEvaluate_parenthesisExpressionWithUnary_minus2480()
        {
            var expression = ExpressionParser.Parse("-((1 + 30) * 4) * 20");
            expression.Evaluate(null).Should().Be(-2480M);
        }

        [TestMethod]
        public void TestParseEvaluate_withMathConstant_2pi()
        {
            var expression = ExpressionParser.Parse("2 * pi");
            expression.Evaluate(DefaultContext.Instance).Should().Be(2m * (decimal)Math.PI);
        }

        [TestMethod]
        public void TestParseEvaluate_sineOf90_1()
        {
            var expression = ExpressionParser.Parse("sin(90)");
            expression.Evaluate(DefaultContext.Instance).Should().Be(1m);
        }

        [TestMethod]
        public void TestParseEvaluate_cosOf0_1()
        {
            var expression = ExpressionParser.Parse("cos(0)");
            expression.Evaluate(DefaultContext.Instance).Should().Be(1m);
        }
    }
}
