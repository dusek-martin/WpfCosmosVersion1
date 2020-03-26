using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCosmosVersion1
{
    class FramePerSecondCounter
    {
        private int frameCount;
        private DateTime lastTime;
        public int FPS { get; set; } 

        public FramePerSecondCounter()
        {
            frameCount = 0;
            FPS = 0;
            lastTime = DateTime.Now;
        }

        public void IncrementFrameCount()
        {
            frameCount++;
            DateTime now = DateTime.Now;
            if ((now - lastTime).TotalSeconds > 1)
            {
                Debug.WriteLine(frameCount);
                lastTime = now;
                FPS = frameCount;
                frameCount = 0;
            }
        }
    }
}
