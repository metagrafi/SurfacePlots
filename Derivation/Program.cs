using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivation
{
    class Program
    {
      
        static void Main(string[] args)
        {
           
            var T = new BinaryStorage();
            Node root = null;

            List<string> testExprs = new List<string>
            {
                "((x-2.)*sin(1+x))/(12+y)",
                "1/(x-y)",
                "x^2+y^2",
                "cos(x / 15 * 3.14) *cos(y / 15 * 3.14) * 60 + cos(x / 8 * 3.14) *cos(y / 10 * 3.14) * 40"
            };
            var expressionTreeBuilder = new ExpressionTreeBuilder(testExprs[3], new List<string> { "X", "y" });
            expressionTreeBuilder.Build();
            root = expressionTreeBuilder.ExpressionTreeRoot;
            
            T.InOrder(root);
            var display = T.Display();
            Console.WriteLine(display);
            Console.WriteLine(expressionTreeBuilder.Error);
            Console.WriteLine(expressionTreeBuilder.ErrorPosition);
            //var expression = new Expression(root, new List<string> { "X", "y" });
            
            //for(int i = -20; i<21; i++)
            //{
                
            //    for(int j=-20; j < 21; j++)
            //    {
                   
            //        Console.Write("{0} ", expression.Evaluate(new List<double> { i, j }));
            //    }
            //    Console.WriteLine();
            //}
            
        }
    }
}
