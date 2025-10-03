using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTree 
{
    public Move[] moveList; 
    public Node root;


    // used to initialize new ComboTree in the character controller
    public ComboTree(Move[] moveListP)
    {
        //step 1: Get move list data from character conntoller to convert into tree
        moveList = moveListP;

        //step 2: create an enmpty root node as the start the tree
        root = new Node(new SequenceBlock());
        root.isRoot = true;

        //step 3: generate the rest of the tree starting from the root node. Use movelist data to fill up the node data
        GenerateTree();

    }

    //Used to generate the rest of the tree/Nodes after the Root Node
    public void GenerateTree()
    {
        Node current;
        Node previous;

        for (int i = 0; i < moveList.Length; i++)
        {
            previous = root;
            Move currentMove = moveList[i];
            List<SequenceBlock> currentSequence = currentMove.inputSequence;

            for (int j = 0; j < currentSequence.Count; j++)
            {
                current = new Node(currentSequence[j]);
                //Mainly for debugigng purposes
                current.associatedMove = currentMove.moveName;

                bool alreadyInserted = false;
                //checks to see if there are any children under the previous node, and if there are, then continue 
                if (previous.children.Count != 0)
                {
                    for (int k = 0; k < previous.children.Count; k++)
                    {
                        //checking if the data from the current node is = to the data from one of its children 
                        //in other words checking to see if this input was already inserted by another move
                        if(current.sequenceBlockData.CompareButtons(previous.children[k].sequenceBlockData))
                        {
                            alreadyInserted = true;

                            //This makes it so that we are not building our tree in the space of nothing, and that it builds off of the node that alr exists.
                            //and that the current node is gotten rid of properly. 
                            previous = previous.children[k];
                        }
                    }
                }

                //add node to tree if identical node has not already been inserted
                if (alreadyInserted == false)
                {
                    //This is what makes the Node linked to the tree, and give it a family!
                    current.parent = previous;
                    previous.children.Add(current);

                    //this is what makes it multi-layered! and not all children of the root.
                    previous = current;

                    if (currentSequence.Count - 1 == j)
                    {
                        current.containsMove = true;
                        current.moveAnim = currentMove.moveAnimation;
                        current.moveName = currentMove.moveName;

                    }
                }

                
            }
        }
    }

    public int GetNodeCount(Node current)
    {
        int nodeCount = 0;
        if (current == null)
            return nodeCount;
        else
        {
            //Add one to the count for root node
            nodeCount++;

            //recurse through all child nodes
            if (current.children.Count != 0)
            {
                for (int i = 0; i < current.children.Count; i++)
                {
                    nodeCount += GetNodeCount(current.children[i]);
                }
            }
        }

        return nodeCount;
    }



}


public class Node
{
    public bool isRoot = false;
    public bool containsMove = false;

    public Node parent = null;
    public List<Node> children = new List<Node>();

    public string moveName = null;
    public AnimationClip moveAnim = null;

    public SequenceBlock sequenceBlockData;

    public string associatedMove = null;

    public Node(SequenceBlock sequence)
    {
        sequenceBlockData = sequence;
    }
}
