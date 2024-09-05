
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
    //פונקציה שקוראת קובץ json של NODE
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

        return nodes;

        // O(n)
    }

    //פונקצייה שרצה על כל ה NODE ומכניסה לעץ
    public static void CreateTree()
    {
        Root = new Node();
        List<Node> nodes = InsertFromJson();
        foreach (var node in nodes)
        {
            Insert(node);
        }
        //O(n)
    }

    //פונקציה שמייצרת NODE חדש
    public static void Insert(Node newNode)
    {
        Root = InsertNode(newNode, Root);
        //O(n)
    }

    //פונקציית עזר שמוצאת היכן להכניס את ה NODE החדש
    private static Node InsertNode(Node newNode, Node root)
    {
        if (root == null)
            return newNode;

        if (newNode.MinSeverity >= root.MinSeverity)
            root.Right = InsertNode(newNode, root.Right);
        else
            root.Left = InsertNode(newNode, root.Left);

        return root;
        //O(!n)
    }

    
    //פונקציית קריאה מ JSON של איומים
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

        return threats;
        // O(n)
    }



    //פונקציית עזר שמוצאת את ה NODE המתאים לאיום
    private static Node Find(Threats threat, Node Root)
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
        //O(!n)
    }


    //מציאת האיום המתאים
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
            else Console.WriteLine("No suitable defence was found. Brace for impact");
        }
        return -1;
        //O(n)

    }
    //פונקציית ההדפסה
    public static void PrintTree()
    {
        PrintTree(Root.Right, "", true);
    }

    //פונקציית עזר להדפסה
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
        //O(!n)
    }

}

