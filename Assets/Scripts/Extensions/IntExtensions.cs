namespace Shared.Extensions
{
    public static class IntExtensions
    {
        public static bool IsBetween (this int x, int inclusiveLower, int exclusiveUpper)
        {
            return x >= inclusiveLower && x < exclusiveUpper;
        }
    }
}