using UnityEngine;

namespace Shared.Extensions
{
    public static class TimeExtensions
    {
        public static int ToMilliseconds(this float seconds)
        {
            return Mathf.RoundToInt(seconds * 1000f);
        }
    }
}