/* Copyright (c) 2016 Valery Alex P. All rights reserved. */
 
using UnityEngine;

namespace VARP.Timing
{
    /// <summary>
    /// The guard for calling function only once per frame
    /// </summary>
    /// <example>
    ///     OncePerFrame oncePerFrame = new OncePerFrame();
    ///     
    ///     void Update()
    ///     {
    ///         if (oncePerFrame.IsOnce)
    ///         {
    ///             ... this code will be executed once per frame only ...
    ///         }
    ///     }
    /// </example>
    public struct OncePerFrame
    {
        private int lastFrame;
        
        public OncePerFrame(int initFrame = -1)
        {
            lastFrame = initFrame;
        }
        
        /// <summary>Will return true only once per frame</summary>
        public bool IsOnce
        {
            get
            {
                if (lastFrame == Time.frameCount) 
                    return false;
                lastFrame = Time.frameCount;
                return true;
            }
        }
    }
}
