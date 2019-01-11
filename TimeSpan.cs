namespace Code.Timing
{
    internal static class TimeSpan
    {
        public static float Hours(float time) { return time * 3600f; }
        public static float Minutes(float time) { return time * 60f; }
        public static float Seconds(float time) { return time; }
        public static float Milliseconds(float time) { return time * 1000f; }
    }
}
