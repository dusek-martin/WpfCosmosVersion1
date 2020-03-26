using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace WpfCosmosVersion1
{
    class Shot
    {
        private Vector _position;
        public Vector Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                LineShot.X1 = _position.X;
                LineShot.Y1 = _position.Y;
                LineShot.X2 = (_position + EndLineShot).X;
                LineShot.Y2 = (_position + EndLineShot).Y;
            }
        }
        public Vector Speed { get; set; }
        public Line LineShot { get; private set; }

        private Vector EndLineShot
        {
            get { return new Vector(30, Speed.GetAngle()); }
            set { }
        }

        public Shot(Vector position, double angle)
        {
            LineShot = new Line();
            LineShot.StrokeThickness = 2;
            LineShot.Stroke = System.Windows.Media.Brushes.Red;

            Speed = new Vector(1000, angle);
            Position = position;
        }

        public void Update(double tickTime)
        {
            Position += Speed * tickTime;
        }
    }
}
