using System;
using System.Collections.Generic;
using System.Linq;

namespace Derivation
{

    public class ExpressionTreeBuilder
    {
        private Node Q, L = null;

        private char EOL = '\r';
        private char Cursor;

        private int p = 0, errorPos = 0;

        private string[] MathFunctions = { "sin", "cos", "tan", "atan", "exp", "ln", "log", "sqrt", "abs" };

        public string Input { get; set; }
        public List<string> Variables { get; set; }

        public Node ExpressionTreeRoot { get; private set; }

        public bool Error
        {
            get { return errorPos != 0; }
        }
        public int ErrorPosition
        {
            get { return errorPos; }
        }
        public ExpressionTreeBuilder(string expressionStr, List<string> vars)
        {
            Input = expressionStr.ReplaceWhitespace("").ToLower();
            Cursor = Input[0];
            Variables = vars.Select(v => v.ToLower()).ToList();
        }

        public void Build()
        {
            if (string.IsNullOrEmpty(Input))
            {
                throw new ArgumentNullException("expression cannot be null");
            }
            ExpressionTreeRoot = Expression(ref Cursor);
            if (errorPos == 0 && p < Input.Length - 1) errorPos = p;
        }
        private void MoveCursor(ref char ch)
        {
            if (p < Input.Length - 1)
            {
                p++;
                ch = Input[p];
            }
            else
                ch = EOL;
        }
        private Node Expression(ref char ch)
        {
            Node E = null;
            char Opr;
            L = Term(ref ch);
            var i = 0;
            if (ch == '+' || ch == '-')
            {
                do
                {
                    Opr = ch;
                    Q = (i < 1) ? L : E;
                    i++;
                    if (p < Input.Length)
                    {
                        MoveCursor(ref ch);
                        E = new Node(Opr.ToString())
                        {
                            LLink = Q,
                            RLink = Term(ref ch)
                            
                        };
                    }
                    else errorPos = p;

                } while ((ch == '+' || ch == '-') && errorPos == 0);
            }
            else
                E = L;
            return E;
        }
        private Node Term(ref char ch)
        {
            Node T = null;
            char Opr;
            L = SignedFactor(ref ch);
            var i = 0;
            if ((ch == '*') || (ch == '/') || (ch == '^'))
            {
                do
                {
                    Opr = ch;
                    Q = (i < 1) ? L : T;
                    i++;
                    if (p < Input.Length)
                    {
                        MoveCursor(ref ch);
                        T = new Node(Opr.ToString())
                        {
                            LLink = Q,
                            RLink = SignedFactor(ref ch)
                        };
                    }
                    else errorPos = p;

                } while (((ch == '*') || (ch == '/') || (ch == '^')) && errorPos == 0);
            }
            else
                T = L;
            return T;
        }
        private Node SignedFactor(ref char ch)
        {
            Node S = null;
            if (ch == '-')
            {
                S = new Node("-");
                MoveCursor(ref ch);
                S.LLink = new Node("0");
                S.RLink = Factor(ref ch); 
            }
            else
                S = Factor(ref ch);
            return S;
        }
        private Node Factor(ref char ch)
        {
            Node F = null;
            bool numberFound = false;
            bool InOperators = ((ch == '+') || (ch == '-') || (ch == '*') || (ch == '/'));
            if (InOperators) errorPos = p;
            F = Number(ref ch, ref numberFound);
            if (!numberFound && errorPos == 0)
            {
                if (ch == '(')
                {
                    MoveCursor(ref ch);
                    F = Expression(ref ch);
                    if (ch == ')') MoveCursor(ref ch);
                    else
                        errorPos = p;
                }
                else
                    MathFunction(ref ch, ref F);
            }
            return F;
        }

        private Node Number(ref char ch, ref bool numberFound)
        {
            Node N = null;
            if (Char.IsNumber(ch))
            {
                numberFound = true;
                var ip = p;
                ch = NextNonDigitChar();
                if (ch == '.') ch = NextNonDigitChar();
                try
                {
                    var number = Input.Substring(ip, p - ip);
                    var value = Convert.ToDouble(number);
                    N = new Node(number);
                }
                catch
                {
                    errorPos = ip;
                }
            }
            else
                numberFound = false;
            return N;
        }
        private char NextNonDigitChar()
        {
            char ch;
            do
            {
                p++;
                ch = p < Input.Length ? Input[p] : EOL;
            } while (Char.IsNumber(ch) && p < Input.Length);
            return ch;
        }
        private void MathFunction(ref char ch, ref Node F)
        {
            var found = false;
            var id = MathFunctionId(ref found);
            if (found)
            {
                F = new Node(MathFunctions[id]);
                MoveCursor(ref ch);
                if (ch == '(')
                {
                    MoveCursor(ref ch);
                    F.LLink = Expression(ref ch);
                    if (ch == ')') MoveCursor(ref ch);
                    else
                        errorPos = p;
                }
                else
                    errorPos = p;
            }
            else
            {
                id = VariableId(ref found);
                if (found)
                {
                    F = new Node(Variables[id]);
                    MoveCursor(ref ch);
                }
            }

        }

        private int VariableId(ref bool found)
        {
            return SearchIn(Variables, ref found);
        }
        private int MathFunctionId(ref bool found)
        {
            return SearchIn(MathFunctions.ToList(), ref found);
        }

        private int SearchIn(List<string> list, ref bool found)
        {
            var id = 0;
            while (id < list.Count && !found)
            {
                var l = list[id].Length;
                var name = Input.Substring(p, (p + l < Input.Length) ? l : 0);
                if (name == list[id])
                {
                    found = true;
                    p = p + l - 1;
                }
                else
                    id++;
            }
            return id;
        }
    }

}
