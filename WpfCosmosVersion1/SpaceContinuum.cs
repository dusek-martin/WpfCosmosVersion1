using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfCosmosVersion1
{
    class SpaceContinuum
    {
        public List<ISpaceObject> SpaceObjects { get; set; }
        public List<Planet> Planets { get; set; }
        public List<Asteroid> Asteroids { get; set; }

        private Canvas canvas;
        private double G = 6.673 * Math.Pow(10, -1);

        public SpaceContinuum(Canvas canvas)
        {
            Planets = new List<Planet>();
            Asteroids = new List<Asteroid>();
            SpaceObjects = new List<ISpaceObject>();
            this.canvas = canvas;
        }

        public void Update(double tickTime, bool isGravityOn)
        {
            BouncyCollisions(tickTime, isGravityOn);
            if (isGravityOn)
            {
                UpdateWithGravity(tickTime);
            }
            else
            {
                UpdateWithoutGravity(tickTime);
            }
        }

        public void AddPlanet(double density, double radius, Vector position, Vector speed)
        {
            Planet planet = new Planet(density, radius, position, speed);
            Planets.Add(planet);
            SpaceObjects.Add(planet);
            canvas.Children.Add(planet.image);
        }
        public void AddAsteroid(double density, double radius, Vector position, Vector speed)
        {
            Asteroid asteroid = new Asteroid(density, radius, position, speed);
            Asteroids.Add(asteroid);
            SpaceObjects.Add(asteroid);
            canvas.Children.Add(asteroid.image);
        }

        private void UpdateWithoutGravity(double tickTime)
        {
            foreach (ISpaceObject so in SpaceObjects)
            {
                so.SolveWalls((float)canvas.ActualWidth, (float)canvas.ActualHeight);
                so.Update(tickTime);
            }
        }

        private void UpdateWithGravity(double tickTime)
        {
            List<Vector> gravityForces = new List<Vector>();

            foreach (ISpaceObject so in SpaceObjects)
            {
                if (so is IGravityObject gravityObject)
                {
                    gravityForces.Add(CalcGravityForce(gravityObject));
                }
            }
            int i = 0;
            foreach (ISpaceObject so in SpaceObjects)
            {
                so.SolveWalls((float)canvas.ActualWidth, (float)canvas.ActualHeight);
                if (so is IGravityObject gravityObject)
                {
                    gravityObject.Update(tickTime, gravityForces[i++]);
                }
                else
                {
                    so.Update(tickTime);
                }
            }
        }

        private Vector CalcGravityForce(IGravityObject gravityObject)
        {
            Vector targetGravityForce = new Vector(0, 0);

            foreach (ISpaceObject so in SpaceObjects)
            {
                if (so is IGravityObject go)
                {
                    if (gravityObject == go)
                    {
                        continue;
                    }

                    Vector gravityForce = go.Position - gravityObject.Position;
                    if (gravityForce.GetSize() != 0)
                    {
                        gravityForce = new Vector((double)(G * (gravityObject.GetWeight() * go.GetWeight()) / Math.Pow(gravityForce.GetSize(), 2)), gravityForce.GetAngle());
                        targetGravityForce += gravityForce;
                    }
                }
            }

            return targetGravityForce;
        }

        public void BouncyCollisions(double tickTime, bool isGravityOn)
        {
            double x = isGravityOn ? 2.3 : 1;
            for (int i = 0; i < SpaceObjects.Count() - 1; i++)
            {
                ISpaceObject spaceObject = SpaceObjects[i];
                for (int j = i + 1; j < SpaceObjects.Count(); j++)
                {
                    ISpaceObject so = SpaceObjects[j];

                    if ((spaceObject.Position - so.Position).GetSize() < (spaceObject.Radius * 0.95 + so.Radius * 0.95 / 2))
                    {
                        spaceObject.Position -= (spaceObject.Speed * tickTime);
                        so.Position -= (so.Speed * tickTime);
                        
                        Vector distance = so.Position - spaceObject.Position;
                        double impactAngle = spaceObject.Speed.GetAngle();
                        double outcomeAngle = distance.GetAngle() - impactAngle + distance.GetAngle() + Math.PI;
                        spaceObject.Speed = new Vector(spaceObject.Speed.GetSize() * x, outcomeAngle);

                        distance.AddAngle(Math.PI);
                        impactAngle = so.Speed.GetAngle();
                        outcomeAngle = distance.GetAngle() - impactAngle + distance.GetAngle() + Math.PI;
                        so.Speed = new Vector(so.Speed.GetSize() * x, outcomeAngle);
                    }
                }
            }
        }

        public void HandleRocket(List<ISpaceObject> soToDel)
        {
            foreach (ISpaceObject so in soToDel)
            {
                if (so is Planet p)
                {
                    if (p.Radius > 50)
                    {
                        /*
                        double r = Math.Sqrt(((p.GetWeight() / 2) / p.Density) / Math.PI);
                        AddPlanet(p.Density, r, p.Position + new Vector(p.Radius * 0.5, p.Speed.GetAngle() + Math.PI), p.Speed.AddAngle(Math.PI / 2));
                        AddPlanet(p.Density, r, p.Position + new Vector(p.Radius * 0.5, p.Speed.GetAngle() - Math.PI), p.Speed.AddAngle(-Math.PI / 2));
                        Remove(p);
                        */
                        p.Volume = p.Volume / 3;
                    }
                    else
                    {
                        Remove(p);
                    }
                }
                else if (so is Asteroid a)
                {
                    Remove(a);
                }
            }
        }

        private void Remove(ISpaceObject so)
        {
            canvas.Children.Remove(so.GetImage());
            if (so is Planet p)
            {
                Planets.Remove(p);
            }
            if (so is Asteroid a)
            {
                Asteroids.Remove(a);
            }

            SpaceObjects.Remove(so);
        }

        public void reset()
        {
            foreach (ISpaceObject so in SpaceObjects)
            {
                canvas.Children.Remove(so.GetImage());
            }
            SpaceObjects.Clear();
            Planets.Clear();
            Asteroids.Clear();
        }
    }
}
