using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCosmosVersion1
{
    class Vector
    {
		public float X;
		public float Y;

		public Vector(float x, float y) { X = x; Y = y; }
		
		public Vector(Vector orig) { X = orig.X; Y = orig.Y; }
		
		public Vector(double size, double angle)
		{
			X = (float)(size * Math.Sin(angle));
			Y = (float)(size * Math.Cos(angle));
		}

		//vrati smerovy vektor pod danym uhlem. uhel je v radianech, 0 je nahoru a stoupa po smeru hodinovych rucicek
		public static Vector ByAngle(double a) { return new Vector((float)Math.Sin(a), (float)Math.Cos(a)); }

		public static Vector PointToVector(Point point) { return new Vector((float)point.X, (float)point.Y); }
		
		public static Vector Min(Vector a, Vector b) { return new Vector(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y)); }
		public static Vector Max(Vector a, Vector b) { return new Vector(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y)); }


		//binarni operatory
		public static Vector operator +(Vector a, Vector b) { return new Vector((float)(a.X + b.X), (float)(a.Y + b.Y)); }
		public static Vector operator -(Vector a, Vector b) { return new Vector((float)(a.X - b.X), (float)(a.Y - b.Y)); }
		public static Vector operator *(Vector a, Vector b) { return new Vector((float)(a.X * b.X), (float)(a.Y * b.Y)); }

		//operatory pro scale vectoru
		public static Vector operator *(Vector v, double s) { return new Vector((float)(v.X * s), (float)(v.Y * s)); }
		public static Vector operator *(double s, Vector v) { return new Vector((float)(v.X * s), (float)(v.Y * s)); }

		//unarni minus
		public static Vector operator -(Vector v) { return new Vector((float)(-v.X), (float)(-v.Y)); }

		//vrátí velikost daného vektoru		
		public double GetSize() { return (Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2))); }

		//vrátí úhel daného vektoru v radianech, 0 je nahoru a stoupa po smeru hodinovych rucicek
		public double GetAngle()
		{
			double angle = Math.Atan2(this.X, this.Y);
			if (angle < 0)
			{
				return (float)(angle + 2 * Math.PI);
			}
			return angle;
		}

		//vrátí daný vektor otočený o daný úhel
		public Vector AddAngle(double s)
		{
			return new Vector((double)(this.GetSize()), (double)(this.GetAngle() + s));
		}

		public Point ToPoint() { return new Point(X, Y); }

	}
}
