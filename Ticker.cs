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

using System.Collections.Generic;
using System.Diagnostics;

namespace VARP.Timing
{
    public class FTicker
    {
        #region Singletone

        private static FTicker coreTicker;
        // Get global core ticker
        public static FTicker GetCoreTicker()
        {
            if (coreTicker == null) coreTicker = new FTicker();
            return coreTicker;
        }

        #endregion

        // @return true if have to be fired again or false to terminate
        // @deltaTime 
        public delegate bool FTickerDelegate(float deltaTime);

        /// <summary>
        /// Single delegate item
        /// </summary>
        public class FElement
        {
            public LinkedListNode<FElement> link;
            /// <summary>
            /// Time that this delegate must not fire before
            /// </summary>
            public double fireTime;
            /// <summary>
            /// Delay that this delegate was scheduled with. Kept here so that if the delegate returns true, we will reschedule it.
            /// </summary>
            public float delayTime;
            /// <summary>
            /// Delegate to call
            /// </summary>
            private FTickerDelegate theDelegate;

            /// <summary>
            /// This is the ctor that the code will generally use. 
            /// </summary>
            /// <param name="inFireTime"></param>
            /// <param name="inDelayTime"></param>
            /// <param name="inDelegate"></param>
            /// <param name="inDelegateHandle"></param>
            public FElement(double inFireTime, float inDelayTime, FTickerDelegate inDelegate, object inDelegateHandle = null)
            {
                delayTime = inDelayTime;
                fireTime = inDelayTime;
                theDelegate = inDelegate;
            }

            /// <summary>
            /// Invoke the delegate if possible 
            /// </summary>
            /// <param name="deltaTime"></param>
            /// <returns></returns>
            public bool Tick(float deltaTime)
            {
                if (theDelegate == null) return false;
                if (theDelegate(deltaTime)) return true;
                theDelegate = null; // terminate
                return false;
            }

            public bool Equals(FTickerDelegate inDelegate)
            {
                return theDelegate == inDelegate;
            }

            public bool EqualsHandle(int inHandle)
            {
                return GetHashCode() == inHandle;
            }

            public void Terminate()
            {
                theDelegate = null;
            }

            public bool IsTerminated => theDelegate == null;
        };

        /// <summary>
        /// Create new ticker with given delegate method
        /// </summary>
        /// <param name="inDelegate">the function will be fired</param>
        /// <param name="inDelay">delay before fire</param>
        /// <returns>the ID can be used later to find this delegate</returns>
        public int AddTicker(FTickerDelegate inDelegate, float inDelay)
        {
            var e = new FElement(currentTime + inDelay, inDelay, inDelegate);
            elements.AddFirst(e);
            return e.GetHashCode();
        }

        /// <summary>
        /// Remove ticker with give ID
        /// </summary>
        /// <param name="inHandle">The id of ticker</param>
        public void RemoveTicker(int inHandle)
        {
            foreach (var el in elements)
            {
                if (el.EqualsHandle(inHandle))
                    el.Terminate();
            }
        }
        
        /// <summary>
        /// Remove ticker for given delegate
        /// </summary>
        /// <param name="inDelegate"></param>
        public void RemoveTicker(FTickerDelegate inDelegate)
        {
            foreach (var el in elements)
                if (el.Equals(inDelegate))
                    el.Terminate();

        }

        /// <summary>
        /// Call it once per frame to update all tickers
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Tick(float deltaTime)
        {
            lock (lockObject)
            {
                // Do not call it more that once per frame
                if (!oncePerFrame.IsOnce) return;
                // Benchmarking
                var timer = Stopwatch.StartNew();
                isInTick = true;
                currentTime += deltaTime;

                var element = elements.First;
                while (element != null)
                {
                    // just in case deleting the element, check who is next
                    var next = element.Next;
                    // optionally: set current element for some of side effect tests
                    currentElement = element.Value;
                    // Tick
                    if (currentElement.Tick(deltaTime))
                        currentElement.fireTime = currentTime + currentElement.delayTime;
                    else
                        elements.Remove(currentElement.link);
                    element = next;
                }
                // Benchmarking end
                timer.Stop();
                totalTimeMicroseconds = timer.ElapsedMilliseconds;
            }
        }

        // --------------------------------------------------------------------

        private object lockObject;              //< Lock object
        private OncePerFrame oncePerFrame;      //< Last frame count (prevent call twice in frame)
        private double currentTime;             //< Current time of the ticker
        private bool isInTick;                  //< State to track whether CurrentElement is valid. 
        private FElement currentElement;        //< Current element being ticked (only valid during tick).
        private long totalTimeMicroseconds;     //< Time of single invoke. Used for benchmarking
        // List of delegates
        private readonly LinkedList<FElement> elements = new LinkedList<FElement>();
    }

    /// <summary>
    /// The base class for objects which have to be called time to time.
    /// </summary>
    public class FTickerObjectBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="InDelay">Delay until next fire; 0 means "next frame"</param>
        /// <param name="inTicker">Ticker the ticker to register with. Defaults to FTicker::GetCoreTicker()</param>
        public FTickerObjectBase(float InDelay = 0.0f, FTicker inTicker = null)
        {
            ticker = inTicker ?? FTicker.GetCoreTicker();
            tickHandle = ticker.AddTicker(Tick, InDelay);
        }

        // Virtual destructor. 
        ~FTickerObjectBase()
        {
            if (ticker != null) ticker.RemoveTicker(tickHandle);
            ticker = null;
        }
        
        /// <summary>
        /// Pure virtual that must be overloaded by the inheriting class.
        /// </summary>
        /// <param name="DeltaTime">time passed since the last call.</param>
        /// <returns>true if should continue ticking</returns>
        protected virtual bool Tick(float DeltaTime) { return false; }

        /// <summary>Ticker to register with </summary>
        private FTicker ticker;
        
        /// <summary>Delegate for callbacks to Tick</summary>
        private readonly int tickHandle;
    };
}
