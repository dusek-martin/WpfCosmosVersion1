using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfCosmosVersion1
{
    class Rocket
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
                UpdatePolygon();
            }
        }

        public Vector Speed { get; set; }

        private double _angle;
        public double Angle
        {
            get { return _angle; }
            private set
            {
                _angle = value;
                UpdatePolygon();
            }
        }

        public Polygon RocketPolygon { get; private set; }
        public List<Shot> Shots { get; set; }

        private SolidColorBrush color = Brushes.Black;
        private int size;
        private int maxSpeed;
        private Point rocketTip, rocketBaseLeft, rocketBaseRight;
        private Canvas canvas;
        private GameInput input;



        public Rocket(Vector position, Canvas canvas, GameInput input)
        {
            Shots = new List<Shot>();
            size = 100;
            maxSpeed = 800;
            this.canvas = canvas;
            this.input = input;

            RocketPolygon = new Polygon()
            {
                StrokeThickness = 2,
                Stroke = color,
                Fill = Brushes.LightGray,
                Points = new PointCollection { rocketTip, rocketBaseLeft, rocketBaseRight, rocketTip },
            };
            this.canvas.Children.Add(RocketPolygon);

            Speed = new Vector(0, 0);
            Position = position;
            Angle = 0;
        }

        public List<ISpaceObject> Update(double tickTime, List<ISpaceObject> spaceObjects)
        {
            SolveWalls((float)canvas.ActualWidth, (float)canvas.ActualHeight);
            Position += (Speed * tickTime);
            ControlRocket(tickTime);
            return ShotsUpdate(tickTime, spaceObjects);
        }

        public void UpdatePolygon()
        {
            rocketTip = (Position + new Vector((double)(size * 2 / 3), Angle)).ToPoint();
            rocketBaseLeft = (Position - new Vector((double)(size * 1 / 3), Angle) + new Vector((double)(size * 1 / 5), Angle + Math.PI / 2)).ToPoint();
            rocketBaseRight = (Position - new Vector((double)(size * 1 / 3), Angle) + new Vector((double)(size * 1 / 5), Angle - Math.PI / 2)).ToPoint();

            RocketPolygon.Points[0] = rocketTip;
            RocketPolygon.Points[1] = rocketBaseLeft;
            RocketPolygon.Points[2] = rocketBaseRight;
            RocketPolygon.Points[3] = rocketTip;
        }

        public double GetWeight() { return size; }

        private List<ISpaceObject> ShotsUpdate(double tickTime, List<ISpaceObject> spaceObjects)
        {
            List<Shot> shotsToDel = new List<Shot>();
            List<ISpaceObject> soToDel = new List<ISpaceObject>();

            foreach (Shot shot in Shots)
            {
                if ((shot.Position.X > canvas.ActualWidth || shot.Position.X < 0) || (shot.Position.Y > canvas.ActualHeight || shot.Position.Y < 0))
                {
                    shotsToDel.Add(shot);
                    continue;
                }
                foreach (ISpaceObject so in spaceObjects)
                {
                    if ((shot.Position - so.Position).GetSize() < so.Radius * 0.9)
                    {
                        shotsToDel.Add(shot);
                        if (!soToDel.Contains(so))
                        {
                            soToDel.Add(so);
                        }
                        break;
                    }
                }
            }

            foreach(Shot shot in shotsToDel)
            {
                canvas.Children.Remove(shot.LineShot);
                Shots.Remove(shot);
            }

            foreach (Shot shot in Shots)
            {
                shot.Update(tickTime);
            }

            return soToDel;
        }

        private void SolveWalls(float width, float height)
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

        private void ControlRocket(double tickTime)
        {
            if (input.left)
            {
                Angle += tickTime * 2 * Math.PI / 1.5;
            }
            if (input.right)
            {
                Angle -= tickTime * 2 * Math.PI / 1.5;
            }

            if (input.up)
            {
                if (Speed.GetSize() < maxSpeed)
                {
                    Speed += new Vector((tickTime * 750), Angle);
                }
            }
            else if (Speed.GetSize() > 1)
            {
                Speed *= Math.Pow(0.35f, tickTime);
            }
            else
            {
                Speed.X = 0;
                Speed.Y = 0;
            }

            if (input.fire)
            {
                Shot shot = new Shot(Vector.PointToVector(rocketTip), Angle);
                canvas.Children.Add(shot.LineShot);
                Shots.Add(shot);
            }
        }
    }
}
