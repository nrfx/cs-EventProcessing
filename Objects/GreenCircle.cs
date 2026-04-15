using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;

namespace EventProcessing.Objects
{
    class GreenCircle : BaseObject
    {
        public float Size = 35f;
        public Action<GreenCircle> OnSizeZero;
        public GreenCircle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        public void UpdatingCircle()
        {
            Size -= 0.15f;

            if (Size <= 0)
            {
                Size = 0;
                OnSizeZero?.Invoke(this);
            }
        }
        public override void Render(Graphics g)
        {
            UpdatingCircle();
            if (Size > 0)
            { 
                float radius = Size / 2;
                g.FillEllipse(new SolidBrush(Color.LightGreen), -radius, -radius, Size, Size);
                g.DrawEllipse(new Pen(Color.Green, 2), -radius, -radius, Size, Size);
            }
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            float radius = Size / 2;
            path.AddEllipse(-radius, -radius, Size, Size);
            return path;
        }
    }
}
