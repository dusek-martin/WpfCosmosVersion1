using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfCosmosVersion1
{
    class Asteroid : ISpaceObject, IGravityObject
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
                image.SetValue(Canvas.LeftProperty, (double)(value.X - Radius));
                image.SetValue(Canvas.TopProperty, (double)(value.Y - Radius));
            }
        }
        public Vector Speed { get; set; }
        public double Density { get; set; }
        private double _radius;
        public double Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                color = new SolidColorBrush(Color.FromRgb(50, (byte)(255 * _radius / 50), 50));
                image.Fill = color;
            }
        }
        public double Volume
        {
            get { return Math.PI * Radius * Radius; }
            set { Radius = Math.Sqrt(value / Math.PI); }
        }

        public Ellipse image;
        private SolidColorBrush color = Brushes.Yellow;
        private int maxSpeed = 350;

        public Asteroid(double density, double radius, Vector position, Vector speed)
        {
            image = new Ellipse
            {
                Width = 2 * radius,
                Height = 2 * radius,
                Fill = color,
            };
            Density = density;
            Radius = radius;
            Position = position;
            Speed = speed;
        }

        public void Update(double tickTime)
        {
            Position += (Speed * tickTime);

            if (Speed.GetSize() > maxSpeed)
            {
                Speed = new Vector(maxSpeed, Speed.GetAngle());
            }
        }

        public void Update(double tickTime, Vector gravityForce)
        {
            Vector targetPosition = Position;
            Vector forceSpeed = gravityForce * (1d / GetWeight());

            targetPosition += (Speed * tickTime);
            targetPosition += (forceSpeed * tickTime);

            Speed = (targetPosition - Position) * (1d / tickTime);
            Position = targetPosition;

            if (Speed.GetSize() > maxSpeed)
            {
                Speed = new Vector(maxSpeed, Speed.GetAngle());
            }
        }

        public void SolveWalls(float width, float height)
        {
            if (Position.X > width)
            {
                Position.X = 0;
            }
            else if (Position.X < 0)
            {
                Position.X = width;
            }
            else if (Position.Y > height)
            {
                Position.Y = 0;
            }
            else if (Position.Y < 0)
            {
                Position.Y = height;
            }
        }

        public double GetWeight() { return Density * Volume; }

        public Shape GetImage() { return image; }
    }
}
