using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ExempelDT : MonoBehaviour
{
    static void Main()
    {
        Console.WriteLine("Hello Decision World!\nLet us decide on what kind of pet you want!");
        DecisionTree petTree = new DecisionTree();
        Generate(petTree);
        petTree.ParseTree();
    }
    //Moved to own method, just to clean up Main()
    static void Generate(DecisionTree newTree)
    {
        newTree.SetRoot(1, "Do you want to have a pet with fur?");
        newTree.AddTrueNode(1, 2, "Do you want a pet that is smart?");
        newTree.AddFalseNode(1, 3, "Do you want a pet that is smart?");
        newTree.AddTrueNode(2, 4, "Pet should be a Dog");
        newTree.AddFalseNode(2, 5, "Pet should be a Cat");
        newTree.AddTrueNode(3, 6, "Pet should be a Parrot");
        newTree.AddFalseNode(3, 7, "Pet should be a Guppy");
    }
}
class DecisionTree
{
    class BinTree
    {
        public int ID;
        public String Eval;
        public BinTree trueBranch;
        public BinTree falseBranch;
        public BinTree(int newID, String newEval)
        {
            this.ID = newID;
            Eval = newEval;
        }
    }
    BinTree root;
    public void SetRoot(int newID, String newEval)
    {
        root = new BinTree(newID, newEval);
    }
    public void AddTrueNode(int existingNodeID, int newNodeID, String
    newQuestAns)
    {
        if (root == null)
        {
            return;
        }
        if (ParseTreeAndAddTrueNode(root, existingNodeID, newNodeID,
        newQuestAns))
        {
            Console.WriteLine("Added node " + newNodeID + " onto \"True\" branch of node " + existingNodeID);
        }
        else
        {
            Console.WriteLine("Node " + existingNodeID + " not found!");
        }
    }
    private bool ParseTreeAndAddTrueNode(BinTree currentNode, int
    existingNodeID, int newNodeID, String newQuestAns)
    {
        if (currentNode.ID == existingNodeID)
        {
            if (currentNode.trueBranch == null)
            {
                currentNode.trueBranch = new BinTree(newNodeID, newQuestAns);
            }
            else
            {
                Console.WriteLine("WARNING: Replacing " + "(id = " +
                currentNode.trueBranch.ID + ") linked to True-branch of " + existingNodeID);
                currentNode.trueBranch = new BinTree(newNodeID, newQuestAns);
            }
            return (true);
        }
        else
        {
            if (currentNode.trueBranch != null)
            {
                if (ParseTreeAndAddTrueNode(currentNode.trueBranch,
                existingNodeID, newNodeID, newQuestAns))
                {
                    return (true);
                }
                else
                {
                    if (currentNode.falseBranch != null)
                    {
                        return
                        (ParseTreeAndAddTrueNode(currentNode.falseBranch, existingNodeID, newNodeID,
                        newQuestAns));
                    }
                    else
                    {
                        return (false);
                    }
                }
            }
            return (false);
        }
    }
    public void AddFalseNode(int existingNodeID, int newNodeID, String
    newQuestAns)
    {
        if (root == null)
        {
            Console.WriteLine("ERROR: No DT!");
            return;
        }
        // Search tree
        if (ParseTreeAndAddFalseNode(root, existingNodeID, newNodeID,
        newQuestAns))
        {
            Console.WriteLine("Added node " + newNodeID +
            " onto \"False\" branch of node " + existingNodeID);
        }
        else Console.WriteLine("Node " + existingNodeID + " not found");
    }
    private bool ParseTreeAndAddFalseNode(BinTree currentNode,
    int existingNodeID, int newNodeID, String newQuestAns)
    {
        if (currentNode.ID == existingNodeID)
        {
            if (currentNode.falseBranch == null) currentNode.falseBranch = new
            BinTree(newNodeID, newQuestAns);
            else
            {
                Console.WriteLine("WARNING: Replacing " + "(id = " +
                currentNode.falseBranch.ID + ") linked to True-branch of node " + existingNodeID);
                currentNode.falseBranch = new BinTree(newNodeID, newQuestAns);
            }
            return (true);
        }
        else
        {
            if (currentNode.trueBranch != null)
            {
                if (ParseTreeAndAddFalseNode(currentNode.trueBranch,
                existingNodeID, newNodeID, newQuestAns))
                {
                    return (true);
                }
                else
                {
                    if (currentNode.falseBranch != null)
                    {
                        return
                        (ParseTreeAndAddFalseNode(currentNode.falseBranch, existingNodeID, newNodeID,
                        newQuestAns));
                    }
                    else return (false);
                }
            }
            else
            {
                return (false);
            }
        }
    }
    private void Eval(BinTree currentNode)
    {
        Console.WriteLine(currentNode.Eval + " (enter \"Y\" or \"N\")");
        String input = Console.ReadLine();
        if (input.Equals("Y")) ParseTree(currentNode.trueBranch);
        else
        {
            if (input.Equals("N")) ParseTree(currentNode.falseBranch);
            else
            {
                Console.WriteLine("Please answer \"Y\" or \"N\"");
                Eval(currentNode);
            }
        }
    }
    void ParseTree(BinTree currentNode)
    {
        if (currentNode.trueBranch == null)
        {
            if (currentNode.falseBranch == null)
            {
                Console.WriteLine(currentNode.Eval);
            }
            return;
        }
        if (currentNode.falseBranch == null)
        {
            return;
        }
        Eval(currentNode);
    }
    public void ParseTree()
    {
        ParseTree(root);
    }
}
