using UnityEngine;

namespace Shared.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 NewVectorWithX(this Vector2 v, float x)
        {
            return new Vector2(x, v.y);
        }

        public static Vector2 NewVectorWithY(this Vector2 v, float y)
        {
            return new Vector2(v.x, y);
        }

        public static Vector3 NewVectorWithX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }

        public static Vector3 NewVectorWithY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }

        public static Vector3 NewVectorWithZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        public static Vector2 NewVectorWithXMovedBy(this Vector2 v, float deltaX)
        {
            return new Vector2(v.x + deltaX, v.y);
        }

        public static Vector2 NewVectorWithYMovedBy(this Vector2 v, float deltaY)
        {
            return new Vector2(v.x, v.y + deltaY);
        }

        public static Vector3 NewVectorWithXMovedBy(this Vector3 v, float deltaX)
        {
            return new Vector3(v.x + deltaX, v.y, v.z);
        }

        public static Vector3 NewVectorWithYMovedBy(this Vector3 v, float deltaY)
        {
            return new Vector3(v.x, v.y + deltaY, v.z);
        }

        public static Vector3 NewVectorWithZMovedBy(this Vector3 v, float deltaZ)
        {
            return new Vector3(v.x, v.y, v.z + deltaZ);
        }
    }
}