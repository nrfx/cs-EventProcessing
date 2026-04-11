using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;

namespace EventProcessing.Objects
{
    class GreenCircle : BaseObject
    {
        public GreenCircle(float x, float y, float angle) : base(x, y, angle)
        {
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.LightGreen), -15, -15, 30, 30);
            g.DrawEllipse(new Pen(Color.Green, 2), -15, -15, 30, 30);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }
    }
}
