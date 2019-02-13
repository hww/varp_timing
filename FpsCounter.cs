// =============================================================================
// MIT License
// 
// Copyright (c) 2018 Valeriya Pudova (hww.github.io)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// =============================================================================

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