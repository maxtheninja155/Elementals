using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SequenceBlock
{
    public GenericControls[] buttons;
    public float frameWindow;

    public SequenceBlock()
    {
        buttons = new GenericControls[0];
        frameWindow = 0f;
    }

    public SequenceBlock(GenericControls[] buttonsP, float frameWindowP)
    {
        buttons = buttonsP;
        frameWindow = frameWindowP;
    }
   
    //Compares if the buttons of this SequenceBlock are the same as block, regardless of order.
    //returns true or false
    public bool CompareButtons(SequenceBlock block)
    {
        if (buttons.Length != block.buttons.Length)
            return false;
        else
        {
            // Create a Dictionary to count the number of occurrences of each element in the other object's keys
            Dictionary<GenericControls, int> ButtonsCount = new Dictionary<GenericControls, int>();

            foreach (var keyCount in block.buttons)
            {
                if (!ButtonsCount.ContainsKey(keyCount))
                    ButtonsCount[keyCount] = 1;
                else
                    ButtonsCount[keyCount]++;
            }

            // Loop through this object's keys
            for (int i = 0; i < buttons.Length; i++)
            {
                // If the Dictionary does not contain the element, or if its count is 0, return false
                if (!ButtonsCount.ContainsKey(buttons[i]) || ButtonsCount[buttons[i]] == 0)
                    return false;
                else
                    ButtonsCount[buttons[i]]--;      // If the Dictionary contains the element and its count is greater than 0, decrement its count
            }

            // If all elements in this object's keys have been found in the other block's keys, return true
            return true;
        }
    }
}
