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

namespace VARP.Timing
{
    /// <summary>
    /// Scaled Time
    /// 
    /// Allow to read @time and @deltaTime but those values depends on
    /// @timeScale property and @enabled flag.
    /// </summary>
    public class FScaledTime : FTickerObjectBase
    {
        private bool enabled;
        private float deltaTime;
        private float timeScale;
        private float time;

        // Constructors.
        public FScaledTime(FTicker inTicker = null) : base(0, inTicker)
        {
            enabled = true;
            TimeScale = 1f;
        }
        public FScaledTime(bool inEnabled, FTicker inTicker = null) : base(0, inTicker)
        {
            enabled = inEnabled;
            TimeScale = 1f;
        }
        public FScaledTime(float inTimeScale, FTicker inTicker = null) : base(0, inTicker)
        {
            enabled = true;
            TimeScale = inTimeScale;
        }
        public FScaledTime(bool inEnabled, float inTimeScale, FTicker inTicker = null) : base(0, inTicker)
        {
            enabled = inEnabled;
            TimeScale = inTimeScale;
        }
        // ==============================================================================================
        // Methods.
        // ==============================================================================================
        
        public float DetaTime { get { return deltaTime; } }
        public float Time { get { return time; } }
        public float TimeScale { get { return timeScale; } set { timeScale = value; } }
        public bool Enabled { get { return enabled; } set { enabled = value; } }

        // ==============================================================================================
        // Private stuff
        // ==============================================================================================
        
        /// <inheritdoc/>
        protected override bool Tick(float deltaTime)
        {
            this.deltaTime = Enabled ? UnityEngine.Time.deltaTime * timeScale : 0;
            time += this.deltaTime;
            return true;
        }
    }
}
