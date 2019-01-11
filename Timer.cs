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

namespace Code.Timing
{
    /// <summary>
    /// Simple timer, can be started and used for measuring intervals
    /// </summary>
    /// <example>
    /// var timer = new Timer();
    /// timer.timeScale = 2f;
    /// timer.GetElapseTime(true);
    /// </example>
    public class Timer
    {
        private float timeStart;
        private float timeCur;
        private float timeScale = 1f;

        /// <summary>Start timer</summary>
        public void InitTime()
        {
            timeStart = Time.time;
        }
        
        /// <summary>Get elapse time without scale</summary>
        public float GetTime()
        {
            return timeCur  = Time.time;
        }
        
        /// <summary>Get elapse time with scale or without</summary>
        public float GetTime(bool scale)
        {
            timeCur  = Time.time;
            return scale ? timeCur * timeScale : timeCur;
        }
        
        /// <summary>Get elapsed time with scale or without</summary>
        public float GetElapsedTime(bool scale)
        {
            timeCur  = Time.time;
            var elapsed = timeCur - timeStart;
            return scale ? elapsed * timeScale : elapsed;
        }
        
        /// <summary>Get elapsed time without scale</summary>
        public float GetElapsedTime()
        {
            return timeCur - timeStart;
        }
    }
}