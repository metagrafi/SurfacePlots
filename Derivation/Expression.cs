using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivation
{
    public class Expression
    {
        private string[] MathFunctions = { "sin", "cos", "tan", "atan", "exp", "ln", "log", "sqrt", "abs" };

        public Node ExpressionTreeRoot { get; set; }
        public List<string> Variables { get; set; }
        public List<double> Values { get; set; }

        public Expression(Node root, List<string> variables)
        {
            ExpressionTreeRoot = root ?? throw new ArgumentNullException("Expression Tree Empty");
            Variables = variables.Select(v => v.ToLower()).ToList();
        }

        public double Evaluate(List<double> values)
        {

            Values = values;
            return Resolve(ExpressionTreeRoot);
        }

        private double Resolve(Node F)
        {
            if (F != null)
            {
                var Opr = F.value[0];
                switch (Opr)
                {
                    case '+':
                        return Factor(F.LLink) + Factor(F.RLink);
                    case '-':
                        return Factor(F.LLink) - Factor(F.RLink);
                    case '*':
                        return Factor(F.LLink) * Factor(F.RLink);
                    case '/':
                        return Factor(F.LLink) / Factor(F.RLink);
                    case '^':
                        var E = Factor(F.LLink);
                        var p = Factor(F.RLink);
                        if (p % 1 == 0) return Math.Pow(E, p);
                        else
                            return Math.Exp(Math.Log(E) * p);
                    default:
                        return Factor(F);
                }
            }
            return double.NaN;
        }
        private double Factor(Node F)
        {
            var numberFound = false;
            var mathFunctionFound = false;
            var val = Number(F, ref numberFound);
            if (!numberFound)
            {
                val = MathFunction(F, val, ref mathFunctionFound);
                if (!mathFunctionFound) val = Resolve(F);
            }
            return val;
        }
        private double Number(Node F, ref bool found)
        {
            double value = 0; ;
            if (F != null)
            {
                try
                {
                    value = Convert.ToDouble(F.value);
                    found = true;
                }
                catch
                {
                    found = false;
                    if (Variables.Contains(F.value))
                    {
                        found = true;
                        var index = Variables.IndexOf(F.value);
                        value = Values[index];
                    }
                }
            }

            return value;
        }
        private double MathFunction(Node F, double value, ref bool found)
        {
            if (F != null)
            {
                double val;
                found = MathFunctions.Contains(F.value);
                val = found ? Resolve(F.LLink) : 0;
                switch (F.value)
                {
                    case "sin":
                        return Math.Sin(val);
                    case "cos":
                        return Math.Cos(val);
                    case "tan":
                        return Math.Tan(val);
                    case "atan":
                        return Math.Atan(val);
                    case "exp":
                        return Math.Exp(val);
                    case "ln":
                        return Math.Log(val);
                    case "log":
                        return Math.Log10(val);
                    case "sqrt":
                        return Math.Sqrt(val);
                    case "abs":
                        return Math.Abs(val);
                }
            }

            return 0;
        }

    }
}
