using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEffectEvent:AnimationBaseEvent
{
    public GameObject effectPrefab;

    public Transform effectParent;

    public Vector3 effectPos = Vector3.zero;

    public Vector3 effectLocalScale = Vector3.one;
}

