using VARP.Utilities;
using UnityEngine;

namespace VARP.Timing
{
    /// <summary>
    /// Generic FPS counter agnostic to the visualization 
    /// </summary>
    public static class FpsCounter
    {
        public static float Fps;
        
        private static float TotalTime;
        private static int FrameCount;
        private static float Timer;
        private static int LastFps = -1;
        private static string LastFpsString;
        private const float TimerInterval = 0.5f;
        
        public static void AddFrameTime(float deltaTime)
        {
            TotalTime += deltaTime;
            FrameCount++;
            Timer += deltaTime;
            if (Timer > TimerInterval)
            {
                Fps = TotalTime / FrameCount;
                TotalTime = 0;
                FrameCount = 0;  
            }
        }

        public static string GetFpsString()
        {
            var fps = Mathf.RoundToInt(Fps);
            if (LastFps != fps)
            {
                LastFps = fps;
                LastFpsString = FastNumberToString.IntegerToString(fps);
            }
            return LastFpsString;
        }
        
    }
}