using EventProcessing.Objects;

namespace EventProcessing
{
    public partial class Form1 : Form
    {
        MyRectangle myRect;
        public Form1()
        {
            InitializeComponent();
            myRect = new MyRectangle(0, 0, 0);
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            /*g.FillRectangle(new SolidBrush(Color.Yellow), 200, 100, 50, 30);
            g.DrawRectangle(new Pen(Color.Red, 2), 200, 100, 50, 30);*/

            myRect.Render(g);
        }
    }
}
