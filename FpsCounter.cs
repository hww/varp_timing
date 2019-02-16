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

using UnityEngine;
using VARP.DebugMenus;

namespace VARP.Timing
{
    /// <summary>
    /// Generic FPS counter agnostic to the visualization 
    /// </summary>
    public static class FpsCounter
    {
        private const float UPDATE_FPS_HUD_EVERY = 0.5f;
        
        public static bool ShowFpsHud;
        public static bool ShowFpsMinMax;
        public static float Fps;
        public static int MinFps;
        public static int MaxFps;
        public static int NumFixedUpdates;

        private static int numFixedUpdates;
        
        private static float TotalDeltaTime;
        private static float MinDeltaTime;
        private static float MaxDeltaTime;
        private static int TotalFrames;
        private static float Timer;
        private static int LastFps = -1;


        private static FpsRepresentation representation;
        private static FpsCounterData fpsCounterData;

        public static void Initialize(FpsCounterData data)
        {
            fpsCounterData = data;
            representation = GameObject.FindObjectOfType<FpsRepresentation>();
            ShowFpsHud = fpsCounterData.showFpsHud;
            ShowFpsMinMax = fpsCounterData.showFpsMinMax;
            Reset();
        }
        
        public static void OnUpdate(float unscaledDeltaTime)
        {
            TotalFrames++;
            TotalDeltaTime += unscaledDeltaTime;
            if (unscaledDeltaTime > MaxDeltaTime) 
                MaxDeltaTime = unscaledDeltaTime;
            if (unscaledDeltaTime < MinDeltaTime) 
                MinDeltaTime = unscaledDeltaTime;

            Timer += unscaledDeltaTime;
            NumFixedUpdates = numFixedUpdates;
            
            if (Timer >= UPDATE_FPS_HUD_EVERY)
            {
                Timer -= UPDATE_FPS_HUD_EVERY;
                
                var aveFps_ = Mathf.RoundToInt(1f / (TotalDeltaTime / TotalFrames));
                var minFps_ = Mathf.RoundToInt(1f / MaxDeltaTime);
                var maxFps_ = Mathf.RoundToInt(1f / MinDeltaTime) ; 
                if (Fps != aveFps_ || MaxFps != maxFps_ || MinFps != minFps_)
                {
                    Fps = aveFps_;
                    MinFps = minFps_;
                    MaxFps = maxFps_;
                    representation.OnUpdate(unscaledDeltaTime);
                }
     
                Reset();
            }

            numFixedUpdates = 0;
        }
        
        public static void OnFixedUpdate()
        {
            numFixedUpdates++;
        }

        private static void Reset()
        {
            TotalFrames = 0;
            TotalDeltaTime = 0;
            MaxDeltaTime = 0;
            MinDeltaTime = float.MaxValue;
        }

        public static void CreateMenu(int order)
        {
            var menu = new DebugMenu("Profiling", order);
            new DebugMenuToggle(menu, "Show FPS", () => ShowFpsHud, val => ShowFpsHud = val);
            new DebugMenuToggle(menu, "Show Min/Max FPS", () => ShowFpsMinMax, val => ShowFpsMinMax = val);
        }
        
    }

    [System.Serializable]
    public class FpsCounterData
    {
        public bool showFpsHud;
        public bool showFpsMinMax;
    }
}