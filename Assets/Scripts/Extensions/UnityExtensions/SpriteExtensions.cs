using UnityEngine;

namespace Shared.Extensions
{
    public static class SpriteExtensions
    {
        public static void SetAlpha(this SpriteRenderer renderer, float alpha)
        {
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }
}