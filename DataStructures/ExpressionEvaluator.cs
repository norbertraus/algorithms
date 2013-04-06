using System;
using System.Diagnostics;
using NUnit.Framework;

namespace DataStructures
{
    /// <summary>
    /// Represents expression evaluator that uses E.W.Dijkstra algorithm with two stacks
    /// </summary>
    public class ExpressionEvaluator
    {
        /// <summary>
        /// For simplicity we assume that all operands are separated by whitespace
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public double Evaluate(string expression)
        {
            var operators = new Stack_LinkedList<string>();
            var values = new Stack_LinkedList<double>();

            foreach(var term in expression.Split(' '))
            {
                if(term == "(") ; //ignore
                else if(term == "+" || term == "-" || term == "*" || term == "/" || term == "sqrt") operators.Push(term);
                else if(term == ")")
                {
                    var op = operators.Pop();
                    var value = values.Pop(); //take one value from the top of the stack since there might be only single argument operator

                    if(op == "+") value = values.Pop() + value;
                    else if(op == "-") value = values.Pop() - value;
                    else if(op == "*") value = values.Pop() * value;
                    else if(op == "/") value = values.Pop() / value;
                    else if(op == "sqrt") value = Math.Sqrt(value);

                    //push calculated value on the stack
                    values.Push(value);

                }
                else values.Push(double.Parse(term));
            }
            return values.Pop();
        }

        //TODO : Use operator precendence and parser for the input string

        [TestFixture]
        public class Tests
        {
            private ExpressionEvaluator _evaluator = new ExpressionEvaluator();

            [Test]
            public void should_calculate_simple_expression()
            {
                var value = _evaluator.Evaluate("( 1 + ( ( 2 + 3 ) * ( 4 * 5 ) ) )");

                Assert.That(value, Is.EqualTo(101.0));
            }

            [Test]
            public void should_calculate_with_sqrt()
            {
                var value = _evaluator.Evaluate("( ( 1 + sqrt ( 5.0 ) ) / 2.0 )");

                Assert.That(Math.Round(value, 4), Is.EqualTo(1.6180));
            }
        }
    }

    
}