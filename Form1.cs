using EventProcessing.Objects;

namespace EventProcessing
{
    public partial class Form1 : Form
    {
        MyRectangle myRect;
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        Random rand = new Random();
        int score = 0;
        public Form1()
        {
            InitializeComponent();
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            player.OnGreenCircleOverlap += (gc) =>
            {
                score++;
                objects.Remove(gc);
                CreateGreenCircle();
            };
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            for (int i = 0; i < 2; i++)  //2 круга
            {
                CreateGreenCircle();
            }

            objects.Add(marker);
            objects.Add(player);

            objects.Add(new MyRectangle(50, 50, 0));
            objects.Add(new MyRectangle(100, 100, 0));
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            updatePlayer();

            foreach (var obj in objects.ToList())
            {
      
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj); // то есть игрок пересекся с объектом
                    obj.Overlap(player); // и объект пересекся с игроком
                }
            }
            foreach (var obj in objects)     // рендерим объекты
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
            g.ResetTransform();
            g.FillRectangle(new SolidBrush(Color.LightGray), 745, 5, 85, 35);
            g.DrawString($"Очки: {score}", new Font("Calibri", 12), Brushes.Black, 750, 10);
        }

        private void CreateGreenCircle()
        {
            var gc = new GreenCircle(rand.Next(0, pbMain.Width), rand.Next(0, pbMain.Height), 0);

            gc.OnSizeZero += (circle) =>
            {
                objects.Remove(circle);

                CreateGreenCircle();
            };

            objects.Add(gc);
        }

        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 1.5f;
                player.vY += dy * 1.5f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI; // угол поворота игрока 
            }
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // пересчет позиция игрока с помощью вектора скорости
            player.X += player.vX;
            player.Y += player.vY;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}
