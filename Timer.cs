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