using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shared.Extensions
{
    public static class SceneExtensions
    {
        public static GameObject GetGameObjectAtPath(this Scene scene, string path)
        {
            return scene.GetGameObjectAtPath(path.Split('/'), 0,
                scene.GetRootGameObjects().Select(go => go.transform).ToArray());
        }

        static GameObject GetGameObjectAtPath(this Scene scene, string[] pathSplits, int index,
            Transform[] currentLevelTransforms)
        {
            var transform = currentLevelTransforms.FirstOrDefault(t => pathSplits[index] == t.name);
            if (transform != null)
            {
                if (pathSplits.Length == index + 1)
                {
                    return transform.gameObject;
                }
                else
                {
                    return scene.GetGameObjectAtPath(pathSplits, index + 1, transform.GetChildren());
                }
            }
            return null;
        }
    }
}