﻿
using BinaryTreeTest;
using System.Reflection.Metadata;
using System.Text.Json;

internal class DefenceTree
{
    public static Node Root { get; set; }

    public enum ResourceType
    {
        WebServer = 10,
        Database = 15,
        UserCredentials = 20,
        Other = 5
    }

    public static List<Node> InsertFromJson()
    {
        string filePath = @"C:\Users\Lenovo\Desktop\AA\BinaryTreeTest\BinaryTreeTest\NodesFile.json";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("The file does not exist.");
            return new List<Node>();
        }

        string jsonString = File.ReadAllText(filePath);
        List<Node> nodes = JsonSerializer.Deserialize<List<Node>>(jsonString);

        //foreach (var node in nodes)
        //{
        //    Console.WriteLine($"MinSeverity: {node.MinSeverity}, MaxSeverity: {node.MaxSeverity}");
        //    Console.WriteLine("Defenses:");
        //    foreach (var defense in node.Defenses)
        //    {
        //        Console.WriteLine($"- {defense}");
        //    }
        //    Console.WriteLine();
        //}

        return nodes;
    }

    public static void CreateTree()
    {
        Root = new Node();
        List<Node> nodes = InsertFromJson();
        foreach (var node in nodes)
        {
            Insert(node);
        }
    }


    public static void Insert(Node newNode)
    {
        Root = InsertNode(newNode, Root);
    }

    private static Node InsertNode(Node newNode, Node root)
    {
        if (root == null)
            return newNode;

        if (newNode.MinSeverity >= root.MinSeverity)
            root.Right = InsertNode(newNode, root.Right);
        else
            root.Left = InsertNode(newNode, root.Left);

        return root;
    }

    public static void PrintInOrder(Node root)
    {
        if (root == null) return;
        PrintInOrder(root.Left);
        Console.Write(root.MinSeverity + " ");
        PrintInOrder(root.Right);
    }

    public static void PrintTree1(Node root, string indent = "", bool isLeft = true)
    {
        if (root != null)
        {
            // Print right subtree
            PrintTree(root.Right, indent + (isLeft ? "│   " : "    "), false);

            // Print current node
            Console.WriteLine(indent + (isLeft ? "└── " : "┌── ") + root.MinSeverity);

            // Print left subtree
            PrintTree(root.Left, indent + (isLeft ? "    " : "│   "), true);
        }
    }

    public static List<Threats> InsertFromJsonThreats()
    {
        string filePath = @"C:\Users\Lenovo\Desktop\AA\BinaryTreeTest\BinaryTreeTest\Threats.json";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("The file does not exist.");
            return new List<Threats>();
        }

        string jsonString = File.ReadAllText(filePath);
        List<Threats> threats = JsonSerializer.Deserialize<List<Threats>>(jsonString);

        //foreach (var threat in threats)
        //{
        //    Console.WriteLine($"Target: {threat.Target}, Volume: {threat.Volume}");
            
            
        //    Console.WriteLine();
        //}

        return threats;
    }

    public static Node Find(Threats threat, Node Root)
    {
        Node root = new Node();
        root = Root;
        if (Root == null) return null;
        else
        {
            {
                int threatTarget = 0;
                switch (threat.ThreatType)
                {
                    case "Web Server":
                        threatTarget = 10;
                        break;
                    case "Database":
                        threatTarget = 15;
                        break;
                    case "User Credentials":
                        threatTarget = 20;
                        break;
                    default:
                        threatTarget = 5;
                        break;
                }

                if (threat.Volume * threat.Sophistication + threatTarget >= Root.MinSeverity
                    && threat.Volume * threat.Sophistication + threatTarget <= Root.MaxSeverity)
                {
                    return root;
                }
                else
                {
                    if (threat.Volume * threat.Sophistication + threatTarget > Root.MaxSeverity)
                    {
                        return Find(threat, root.Right);
                    }
                    else
                    {
                        return Find(threat, root.Left);
                    }
                }
            }
        }
    }



    public static int FindTheProperNode()
    {
        List<Threats> threats = InsertFromJsonThreats();

        foreach (var threat in threats)
        {
            Thread.Sleep(2000);
            Node node = Find(threat, Root);
            if (node != null)
            {
                Console.Write($"The severity of the attack is: {node.Defenses[0]}");
                Thread.Sleep(2000);
                Console.WriteLine($", {node.Defenses[1]}");
            }
        }
        Console.WriteLine("Attack severity is below the threshold Attack is ignored");
        return -1;

    }

    public static void PrintTree()
    {
        PrintTree(Root.Right, "", true);
    }

    private static void PrintTree(Node node, string indent, bool last)
    {
        if (node != null)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("Right----");
                indent += "   ";
            }
            else
            {
                Console.Write("Left----");
                indent += "|  ";
            }
            string defenses = string.Join(", ", node.Defenses);
            Console.WriteLine($"[{node.MinSeverity}-{node.MaxSeverity}] Defences: {defenses}");
            PrintTree(node.Left, indent, false);
            PrintTree(node.Right, indent, true);
        }
    }

}

