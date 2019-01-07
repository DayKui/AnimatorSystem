using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSystemEditorWindow : OdinMenuEditorWindow
{
    public AnimationEventSystem animSystem;

    public static void OpenWindow(AnimationEventSystem animationEventSystem)
    {
        var window = GetWindow<AnimationEventSystemEditorWindow>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        window.titleContent = new GUIContent("AnimationEventSystem");
        window.animSystem = animationEventSystem;
        window.animSystem.animator = animationEventSystem.GetComponent<Animator>();
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree(true);

        var customMenuStyle = new OdinMenuStyle
        {
            BorderPadding = 0f,
            AlignTriangleLeft = true,
            TriangleSize = 16f,
            TrianglePadding = 0f,
            Offset = 50f,
            Height = 23,
            IconPadding = 0f,
            BorderAlpha = 0.323f
        };
        tree.DefaultMenuStyle = customMenuStyle;
        tree.AddObjectAtPath("Menu Style", tree.DefaultMenuStyle);

        if (animSystem == null)
        {
            return tree;
        }

        if (animSystem.animator.runtimeAnimatorController.animationClips.Length > animSystem.tracks.Count)
        {
            //需要添加
            animSystem.AddTracks();
        }
        else if(animSystem.animator.runtimeAnimatorController.animationClips.Length < animSystem.tracks.Count)
        {
            //需要删除
            animSystem.RemoveTracks();
        }

        for (int i = 0; i < animSystem.tracks.Count; i++)
        {
            var track = animSystem.tracks[i];
            tree.AddObjectAtPath(track.animationClip.name, track);
        }

        return tree;
    }
}
