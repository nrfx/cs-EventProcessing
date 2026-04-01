    using System;
using System.Collections.Generic;
using System.Text;

namespace EventProcessing.Objects
{
    class MyRectangle : BaseObject
    {
        public MyRectangle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Yellow), 200, 100, 50, 30);
            g.DrawRectangle(new Pen(Color.Red, 2), 200, 100, 50, 30);
        }
    }
}
