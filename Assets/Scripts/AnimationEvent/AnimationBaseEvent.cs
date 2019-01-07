using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AnimationBaseEvent:ScriptableObject
{
    [MinValue(0)]
    public int frame;//第几帧播放事件

    public AnimationClip animationClip;

    public float FrameToTime()
    {
        float result = frame / 30.0f;
        result= Mathf.Clamp(result, 0, animationClip.length);
        return result;
    }
}

