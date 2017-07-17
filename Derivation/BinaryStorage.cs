using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Derivation
{
    public class Node 
    {
        public string value;
        public Node RLink;
        public Node LLink;

        public Node()
        {
            value = null;
            RLink = null;
            LLink = null;
        }
        public Node(string value)
        {
            this.value = value;
            RLink = null;
            LLink = null;
        }

        public bool IsLeaf(ref Node node)
        {
            return (node.RLink == null && node.LLink == null);
        }
               
    }
    public static class StringManager
    {
        //whitespace Regex
        private static readonly Regex wsRegex = new Regex(@"\s+");
        //floating point Regex
        private static readonly Regex fpRegex = new Regex(@"^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$");
        public static string ReplaceWhitespace(this string input, string replacement)
        {
            return wsRegex.Replace(input, replacement);
        }
        public static MatchCollection FloatingPointNumbers(this string input)
        {
            return fpRegex.Matches(input);
        }

    }
    public class BinaryStorage
    {
        private Node root;
     
        private List<string> postFixArrangement = new List<string>();
        public bool IsEmpty
        {
            get { return root == null; }
        }

        public int Count { get; private set; }

        public BinaryStorage()
        {
            root = null;
            Count = 0;
        }

        public BinaryStorage Insert(ref Node node, string data)
        {
            if (node == null)
            {
                node  = new Node(data);Count++;
                return this;
            }

            if (node.value.CompareTo(data) < 0)
            {
                return Insert(ref node.RLink, data);
            }
            else
            {
               return Insert(ref node.LLink, data);
            }
        }

        public void PostOrder(Node root)
        {
            if (root == null)
            {
                return;
            }
            PostOrder(root.LLink);
            PostOrder(root.RLink);
            postFixArrangement.Add( root.value);
        }

        public  List<string> PostFix {
            get
            {
                return postFixArrangement;
            }
            set
            {
                postFixArrangement = value;
            }
        }
    }


}
