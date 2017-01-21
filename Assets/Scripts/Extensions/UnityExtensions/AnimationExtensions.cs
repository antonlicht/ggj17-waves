using UnityEngine;
using System.Collections;
using System;

namespace Shared.Extensions
{
    public static class AnimationExtensions
    {
        public static IEnumerator PlayAnimationRoutine(this Animation animation)
        {
            return PlayAnimationRoutine(animation, animation.clip, true);
        }

        public static IEnumerator PlayAnimationRoutine(this Animation animation, bool forward)
        {
            return PlayAnimationRoutine(animation, animation.clip, forward);
        }

        public static IEnumerator PlayAnimationRoutine(this Animation animation, AnimationClip clip)
        {
            return PlayAnimationRoutine(animation, clip, true);
        }

        public static IEnumerator PlayAnimationRoutine(this Animation animation, AnimationClip clip, bool forward,
            Func<bool> shouldSkip = null)
        {
            animation.AddClip(clip, clip.name);

            animation[clip.name].speed = forward ? 1f : -1f;
            animation[clip.name].normalizedTime = forward ? 0f : 1f;

            animation.clip = clip;
            animation.Play(clip.name);

            while (animation.isPlaying && animation.clip == clip)
            {
                if (shouldSkip != null && shouldSkip())
                {
                    animation[clip.name].normalizedTime = forward ? 1f : 0f;
                }
                yield return null;
            }
        }

        public static void PlayBackwards(this Animation anim)
        {
            var name = anim.clip.name + "Reversed";
            anim.SetBackwardClip(anim.clip, name);
            anim.Play(name);
        }

        public static void PlayBackwards(this Animation anim, string clipName)
        {
            var name = clipName + "Reversed";
            anim.SetBackwardClip(anim.GetClip(clipName), name);
            anim.Play(name);
        }

        public static void BlendBackwards(this Animation anim, string clipName)
        {
            var name = clipName + "Reversed";
            anim.SetBackwardClip(anim.GetClip(clipName), name);
            anim.Blend(name, 1f, 0f);
        }

        private static void SetBackwardClip(this Animation anim, AnimationClip clip, string clipName)
        {
            if (anim[clipName] == null)
            {
                anim.AddClip(clip, clipName);
                anim[clipName].speed = -1;
            }
            anim[clipName].time = 1;
        }
    }
}