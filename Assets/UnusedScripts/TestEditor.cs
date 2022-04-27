using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Test test = (Test)target;

        base.OnInspectorGUI();
        if(GUILayout.Button("Run"))
        {
            test.Run();
        }
    }
}
