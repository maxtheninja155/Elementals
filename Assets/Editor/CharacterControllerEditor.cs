using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(CharacterController))]
public class CharacterControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CharacterController controller = (CharacterController)target;

        if (GUILayout.Button("Show Animation Graph"))
        {

            controller.ShowAnimationGraph();
        }
    }
}
