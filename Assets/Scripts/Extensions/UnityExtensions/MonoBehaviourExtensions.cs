using UnityEngine;
using System.Collections;

namespace Shared.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static void InvokeAtEndOfFrame(this MonoBehaviour mb, System.Action action)
        {
            mb.StartCoroutine(InvokeRoutine(action, new WaitForEndOfFrame()));
        }

        public static void InvokeAfter(this MonoBehaviour mb, System.Action action, YieldInstruction delay)
        {
            mb.StartCoroutine(InvokeRoutine(action, delay));
        }

        public static void InovkeAfterFrames(this MonoBehaviour mb, System.Action action, int numFrames)
        {
            mb.StartCoroutine(InvokeAfterFramesRoutine(action, numFrames));
        }

        static IEnumerator InvokeAfterFramesRoutine(System.Action action, int numFrames)
        {
            for (int i = 0; i < numFrames; i++)
            {
                yield return null;
            }
            action();
        }

        static IEnumerator InvokeRoutine(System.Action action, YieldInstruction delay)
        {
            yield return delay;
            action();
        }
    }
}