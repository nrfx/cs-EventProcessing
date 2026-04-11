using EventProcessing.Objects;

namespace EventProcessing
{
    public partial class Form1 : Form
    {
        MyRectangle myRect;
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        GreenCircle greenCircle;
        Random rand = new Random();
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
                greenCircle.X = rand.Next(0, pbMain.Width);
                greenCircle.Y = rand.Next(0, pbMain.Height);
            };
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            greenCircle = new GreenCircle(pbMain.Width / 2 - 100, pbMain.Height / 2 - 100, 0);

            objects.Add(marker);
            objects.Add(player);
            objects.Add(greenCircle);

            objects.Add(new MyRectangle(50, 50, 0));
            objects.Add(new MyRectangle(100, 100, 0));
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            updatePlayer();

            foreach (var obj in objects.ToList())    // пересчитываем пересечения
            {
                // проверка на пересечение с игроком
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

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

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
                objects.Add(marker); // и главное не забыть пололжить в objects
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}
