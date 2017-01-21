using UnityEngine;
using System.IO;

namespace Shared.Extensions
{
    public static class TransformExtensions
    {
        public static void SetX(this Transform t, float x, bool local = false)
        {
            var pos = local ? t.localPosition : t.position;
            pos.x = x;

            if (local) t.localPosition = pos;
            else t.position = pos;
        }

        public static void SetY(this Transform t, float y, bool local = false)
        {
            var pos = local ? t.localPosition : t.position;
            pos.y = y;

            if (local) t.localPosition = pos;
            else t.position = pos;
        }

        public static void SetZ(this Transform t, float z, bool local = false)
        {
            var pos = local ? t.localPosition : t.position;
            pos.z = z;

            if (local) t.localPosition = pos;
            else t.position = pos;
        }

        public static void Move(this Transform t, Vector3 v, bool local = false)
        {
            t.MoveX(v.x, local);
            t.MoveY(v.y, local);
            t.MoveZ(v.z, local);
        }

        public static void MoveX(this Transform t, float moveXBy, bool local = false)
        {
            var pos = local ? t.localPosition : t.position;
            pos.x += moveXBy;

            if (local) t.localPosition = pos;
            else t.position = pos;
        }

        public static void MoveY(this Transform t, float moveYBy, bool local = false)
        {
            var pos = local ? t.localPosition : t.position;
            pos.y += moveYBy;

            if (local) t.localPosition = pos;
            else t.position = pos;
        }

        public static void MoveZ(this Transform t, float moveZBy, bool local = false)
        {
            var pos = local ? t.localPosition : t.position;
            pos.z += moveZBy;

            if (local) t.localPosition = pos;
            else t.position = pos;
        }

        public static string GetScenePath(this Transform transform)
        {
            if (transform == null) return null;

            var path = transform.parent != null ? GetScenePath(transform.parent) : "";
            return Path.Combine(path, transform.name);
        }

        public static Transform[] GetChildren(this Transform t)
        {
            var childs = new Transform[t.childCount];
            for (int i = 0; i < t.childCount; i++)
            {
                childs[i] = t.GetChild(i);
            }
            return childs;
        }

        public static Transform GetChildAtPath(this Transform transform, string relativePath)
        {
            return transform.GetChildAtPath(relativePath.Split('/'), 0, transform.GetChildren());
        }

        static Transform GetChildAtPath(this Transform transform, string[] pathSplits, int index,
            Transform[] currentLevelTransforms)
        {
            for (int i = 0; i < currentLevelTransforms.Length; i++)
            {
                if (currentLevelTransforms[i].name == pathSplits[index])
                {
                    if (pathSplits.Length == index + 1)
                    {
                        return currentLevelTransforms[i];
                    }
                    else
                    {
                        return transform.GetChildAtPath(pathSplits, index + 1, currentLevelTransforms[i].GetChildren());
                    }
                }
            }
            return null;
        }

        public static Transform CreateChild(this Transform transform, string name)
        {
            var child = new GameObject(name);
            child.transform.parent = transform;
            return child.transform;
        }
    }
}