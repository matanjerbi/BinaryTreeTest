using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace BinaryTreeTest
{
    internal class DefenceTree
    {
        public static Node Root { get; set; }

        //public static Node InsertFromJson(Node node)
        //{
        //    if (Root == null)
        //    {
        //        Root = node;
        //        return null;
        //    }
        //    Root.MinSeverity = node.MinSeverity;
        //    Root.MaxSeverity = node.MaxSeverity;

        //    foreach (var defence in node.Defenses)
        //    {
        //        Root.Defenses.Add(defence);
        //    }
        //    Root.Left = InsertFromJson(node.Left);
        //    Root.Right = InsertFromJson(node.Right);

        //}

        public static void InsertFromJson()
        {
            string filePath = @"C:\Users\Lenovo\Desktop\AA\BinaryTreeTest\BinaryTreeTest\NodesFile.json";
            Node nodeFromJson = new Node();

            string jsonString = File.ReadAllText(filePath);
            var data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, Node>>(jsonString);
            var trees = new Dictionary<string, Node>();

        }

    }
}
