using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrack : ScriptableObject
{
    public AnimationClip animationClip;

    public List<AnimationBaseEvent> animationEventList = new List<AnimationBaseEvent>();
}
