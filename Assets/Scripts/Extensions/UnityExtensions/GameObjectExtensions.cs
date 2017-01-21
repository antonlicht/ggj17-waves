using UnityEngine;

namespace Shared.Extensions
{
    public static class GameObjectExtensions
    {
        public static void SetLayerRecursively(this GameObject go, int layer)
        {
            go.layer = layer;
            foreach (Transform t in go.transform)
            {
                t.gameObject.layer = layer;
                SetLayerRecursively(t.gameObject, layer);
            }
        }

        public static T EnsureComponent<T>(this GameObject go) where T : MonoBehaviour
        {
            return go.GetComponent<T>() ?? go.AddComponent<T>();
        }
    }
}