using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.UIElements;

[Serializable]
public class InputManager
{
    
    public ControlSet controls;
    public AnimationManager animM;

    //root node and traversal node
    public Node root;
    public Node currentNode;

    //storing the input of this frame
    public SequenceBlock currentInput;
    public float inputTimer = 0f;

    public Dictionary<GenericControls, KeyCode> keybindRefereces;

    public InputManager(ControlSet controlP, ComboTree treeP, AnimationManager animMP)
    {
        controls = controlP;
        animM = animMP;
        root = treeP.root;
        currentNode = root;

        keybindRefereces = GetKeybindReference();
    }
    public void UpdateInput()
    {
        IncrementTimer();

        if (TryGetPlayerInput(out currentInput))
        {
            NextNode();
        }

    }

    public bool TryGetPlayerInput(out SequenceBlock inputBlock)
    {


        List<GenericControls> pressedButtons = new List<GenericControls>();
        foreach (var reference in keybindRefereces)
        {
            if (Input.GetKeyDown(reference.Value)
                || Input.GetKey(reference.Value) && pressedButtons.Count > 0 && (reference.Key == GenericControls.Left || reference.Key == GenericControls.Right || reference.Key == GenericControls.Down || reference.Key == GenericControls.Up))
            {
                pressedButtons.Add(reference.Key);

            }
            else if (Input.GetKeyUp(reference.Value) && (reference.Key == GenericControls.Left || reference.Key == GenericControls.Right))
                animM.PlayAnimation("Idle");
        }


        if (pressedButtons.Count == 0)
        {
            inputBlock = null;
            return false;
        }
        else
        {
            inputBlock = new SequenceBlock(pressedButtons.ToArray(), 0);
            return true;
        }

    }

    public Dictionary<GenericControls, KeyCode> GetKeybindReference()
    {
        Dictionary<GenericControls, KeyCode> references = new Dictionary<GenericControls, KeyCode>();

        foreach (var field in typeof(ControlSet).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            string control = field.Name;
            KeyCode keybind = (KeyCode)field.GetValue(controls);

            GenericControls output;
            if(Enum.TryParse(control, out output))
                references.Add(output, keybind);

        }
        return references;
    }

    public void NextNode()
    {
        for (int i = 0; i < currentNode.children.Count; i++)
        {
            if (currentInput.CompareButtons(currentNode.children[i].sequenceBlockData) 
                && inputTimer <= currentNode.sequenceBlockData.frameWindow)
            {
               
                //this is what changes the node
                currentNode = currentNode.children[i];
                inputTimer = 0f;

                //activate move
                if (currentNode.containsMove)
                {
                    //anim.Play(currentNode.moveAnim.name);
                    animM.PlayAnimation(currentNode.moveAnim.name);
                }

            }

        }
    }

    public void IncrementTimer()
    {
        if (!currentNode.isRoot)
        {
            inputTimer += Time.deltaTime;

            if(inputTimer > currentNode.sequenceBlockData.frameWindow)
            {
                inputTimer = 0f;
                currentNode = root;
            }
        }
    }


}
