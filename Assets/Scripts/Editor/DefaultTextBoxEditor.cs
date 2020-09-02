using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DefaultTextBox))]
public class DefaultTextBoxEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DefaultTextBox textBox = (DefaultTextBox)target;

        if (GUILayout.Button("Set Up TextBox"))
        {
            textBox.SetUpTextBox();
        }
    }
}