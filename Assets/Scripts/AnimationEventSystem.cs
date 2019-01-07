using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSystem : MonoBehaviour {

    public Animator animator;

    public List<AnimationTrack> tracks = new List<AnimationTrack>();

    public void AddTracks()
    {
        for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            var animationClip = animator.runtimeAnimatorController.animationClips[i];
            if (IsContainAnimationClip(animationClip) == false)
            {
                var animationTrack = ScriptableObject.CreateInstance<AnimationTrack>();
                animationTrack.animationClip = animationClip;
                tracks.Add(animationTrack);
            }
        }
    }

    public bool IsContainAnimationClip(AnimationClip animationClip)
    {
        for (int i = 0; i < tracks.Count; i++)
        {
            if (tracks[i].animationClip == animationClip)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveTracks()
    {
        for (int i = 0; i < tracks.Count; i++)
        {
            var animationClip = tracks[i].animationClip;
            bool isContainInAnimator = false;

            for (int j = 0; j < animator.runtimeAnimatorController.animationClips.Length; j++)
            {
                if (animator.runtimeAnimatorController.animationClips[j] == animationClip)
                {
                    isContainInAnimator = true;
                    break;
                }
            }

            if (isContainInAnimator == false)
            {
                tracks.RemoveAt(i);
            }
        }
    }

    public void PlayEffectEvent(Object effectEvent)
    {
        AnimationEffectEvent animEffectEvent = effectEvent as AnimationEffectEvent;
        if (animEffectEvent == null)
        {
            return;
        }

        var effectObj = GameObject.Instantiate(animEffectEvent.effectPrefab, animEffectEvent.effectParent);
        effectObj.transform.localPosition = animEffectEvent.effectPos;
        effectObj.transform.localScale = animEffectEvent.effectLocalScale;
    }
}
