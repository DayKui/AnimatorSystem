using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimationEventSystem))]
public class AnimationEventSystemInspector:Editor
{
    private AnimationEventSystem _animSystem { get { return target as AnimationEventSystem; } }

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Eidtor Window", GUILayout.MaxHeight(60)))
        {
            AnimationEventSystemEditorWindow.OpenWindow(_animSystem);
        }
    }
}

