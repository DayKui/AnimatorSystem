using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;

[CustomEditor(typeof(AnimationTrack))]
public class AnimationTrackInspector:OdinEditor
{
    private AnimationTrack _animationTrack { get { return target as AnimationTrack; } }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.ObjectField("AnimationClip", _animationTrack.animationClip, typeof(AnimationClip), true);
        EditorGUILayout.LabelField("Event Count" + _animationTrack.animationClip.events.Length);

        for (int i = 0; i < _animationTrack.animationEventList.Count; i++)
        {
            var animEvent = _animationTrack.animationEventList[i];
            if(animEvent is AnimationEffectEvent)
            {
                DrawEffectEvent(animEvent as AnimationEffectEvent);
            }
        }

        DrawAddEventButton();

        DrawUpdateAllEventsButton();
    }

    private void DrawEffectEvent(AnimationEffectEvent effectEvent)
    {
        SirenixEditorGUI.BeginBox("Effect Event");

        effectEvent.frame = EditorGUILayout.IntField("Frame", effectEvent.frame);
        effectEvent.effectPrefab = (GameObject)EditorGUILayout.ObjectField("Effect Prefab", effectEvent.effectPrefab, typeof(GameObject), true);
        effectEvent.effectParent = (Transform)EditorGUILayout.ObjectField("Effect Parent", effectEvent.effectParent, typeof(Transform), true);
        effectEvent.effectPos = EditorGUILayout.Vector3Field("LocalPosition", effectEvent.effectPos);
        effectEvent.effectLocalScale = EditorGUILayout.Vector3Field("LocalScale", effectEvent.effectLocalScale);

        if (GUILayout.Button("Remove"))
        {
            _animationTrack.animationEventList.Remove(effectEvent);
        }
        SirenixEditorGUI.EndBox();
    }

    private void DrawAddEventButton()
    {
        if(GUILayout.Button("Add A Event In List", GUILayout.MaxHeight(50)))
        {
            int select = 0;

            string[] showString = new string[]
            {
                "effect event","other event"
            };
            GUIContent[] contents = new GUIContent[showString.Length];
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = new GUIContent(showString[i]);
            }
            Rect rect = new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 0, 0);
            EditorUtility.DisplayCustomMenu(rect, contents, select, AddEventButtonCallback, null);
        }
    }

    private void AddEventButtonCallback(object userData,string[] stringArray,int select)
    {
        if (select == 0)
        {
            var effectEvent = ScriptableObject.CreateInstance<AnimationEffectEvent>();
            effectEvent.animationClip = _animationTrack.animationClip;
            _animationTrack.animationEventList.Add(effectEvent);
        }
    }

    private void DrawUpdateAllEventsButton()
    {
        if(GUILayout.Button("Update All Events In Clip", GUILayout.MaxHeight(50)))
        {
            AnimationEvent[] animationEvents = new AnimationEvent[_animationTrack.animationEventList.Count];

            for (int i = 0; i < animationEvents.Length; i++)
            {
                animationEvents[i] = new AnimationEvent();
                if(_animationTrack.animationEventList[i] is AnimationEffectEvent)
                {
                    var effectEvent = _animationTrack.animationEventList[i];
                    animationEvents[i].time = effectEvent.FrameToTime();
                    animationEvents[i].functionName = "PlayEffectEvent";
                    animationEvents[i].objectReferenceParameter = effectEvent;
                }
            }

            AnimationUtility.SetAnimationEvents(_animationTrack.animationClip, animationEvents);
        }
    }
}

