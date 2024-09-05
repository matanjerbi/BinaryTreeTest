using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeTest
{
    internal class Node
    {
        public int MinSeverity { get; set; }
        public int MaxSeverity { get; set; }
        public List<string> Defenses { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }


        public Node()
        {
            Defenses = new List<string>();
            Left = null;
            Right = null;
        }
    }
}
