using BinaryTreeTest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


public class Program
{

    static void Main(string[] args)
    {
        DefenceTree.InsertFromJson();
        DefenceTree.CreateTree();
        //DefenceTree.PrintTree();
        DefenceTree.FindTheProperNode();

    }
}