using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line98.Model
{
    public class Ball
    {
        public int colorIndex { get; set; }
        public bool isTriggered { get; set; }
        public bool isBig { get; set; }
        public Ball(int ColorIndex, bool IsBig = true, bool isTriggered = false)
        {
            this.colorIndex = ColorIndex;
            this.isBig = IsBig;
            this.isTriggered = isTriggered;
        }

    }
}
