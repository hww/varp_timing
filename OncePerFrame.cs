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
