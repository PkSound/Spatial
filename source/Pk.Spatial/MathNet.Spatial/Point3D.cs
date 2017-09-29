using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using UnitsNet;

namespace MathNet.Spatial.Euclidean
{
    [Serializable]
    public struct Point3D : IEquatable<Point3D>, IFormattable
    {
        /// <summary>
        /// Using public fields cos: http://blogs.msdn.com/b/ricom/archive/2006/08/31/performance-quiz-11-ten-questions-on-value-based-programming.aspx
        /// </summary>
        public readonly double X;

        /// <summary>
        /// Using public fields cos: http://blogs.msdn.com/b/ricom/archive/2006/08/31/performance-quiz-11-ten-questions-on-value-based-programming.aspx
        /// </summary>
        public readonly double Y;

        /// <summary>
        /// Using public fields cos: http://blogs.msdn.com/b/ricom/archive/2006/08/31/performance-quiz-11-ten-questions-on-value-based-programming.aspx
        /// </summary>
        public readonly double Z;

        public Point3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Point3D(IEnumerable<double> data)
            : this(data.ToArray())
        {
        }

        public Point3D(double[] data)
            : this(data[0], data[1], data[2])
        {
            if (data.Length != 3)
            {
                throw new ArgumentException("Size must be 3");
            }
        }

        /// <summary>
        /// Creates a Point3D from its string representation
        /// </summary>
        /// <param name="s">The string representation of the Point3D</param>
        /// <returns></returns>
        public static Point3D Parse(string s)
        {
            var doubles = Parser.ParseItem3D(s);
            return new Point3D(doubles);
        }

        public static bool operator ==(Point3D left, Point3D right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point3D left, Point3D right)
        {
            return !left.Equals(right);
        }

        [Obsolete("Not sure this is nice")]
        public static Vector<double> operator *(Matrix<double> left, Point3D right)
        {
            return left*right.ToVector();
        }

        [Obsolete("Not sure this is nice")]
        public static Vector<double> operator *(Point3D left, Matrix<double> right)
        {
            return left.ToVector()*right;
        }

        public override string ToString()
        {
            return this.ToString(null, CultureInfo.InvariantCulture);
        }

        public string ToString(IFormatProvider provider)
        {
            return this.ToString(null, provider);
        }

        public string ToString(string format, IFormatProvider provider = null)
        {
            var numberFormatInfo = provider != null ? NumberFormatInfo.GetInstance(provider) : CultureInfo.InvariantCulture.NumberFormat;
            string separator = numberFormatInfo.NumberDecimalSeparator == "," ? ";" : ",";
            return string.Format("({0}{1} {2}{1} {3})", this.X.ToString(format, numberFormatInfo), separator, this.Y.ToString(format, numberFormatInfo), this.Z.ToString(format, numberFormatInfo));
        }

        public bool Equals(Point3D other)
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
            // ReSharper restore CompareOfFloatsByEqualityOperator
        }

        public bool Equals(Point3D other, double tolerance)
        {
            if (tolerance < 0)
            {
                throw new ArgumentException("epsilon < 0");
            }

            return Math.Abs(other.X - this.X) < tolerance &&
                   Math.Abs(other.Y - this.Y) < tolerance &&
                   Math.Abs(other.Z - this.Z) < tolerance;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Point3D && this.Equals((Point3D)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.X.GetHashCode();
                hashCode = (hashCode*397) ^ this.Y.GetHashCode();
                hashCode = (hashCode*397) ^ this.Z.GetHashCode();
                return hashCode;
            }
        }
    
        public static Point3D Origin
        {
            get { return new Point3D(0, 0, 0); }
        }

        public static Point3D NaN
        {
            get { return new Point3D(double.NaN, double.NaN, double.NaN); }
        }

        public static Point3D Centroid(IEnumerable<Point3D> points)
        {
            return Centroid(points.ToArray());
        }

        public static Point3D Centroid(params Point3D[] points)
        {
            return new Point3D(
                points.Average(point => point.X),
                points.Average(point => point.Y),
                points.Average(point => point.Z));
        }

        public static Point3D MidPoint(Point3D p1, Point3D p2)
        {
            return Centroid(p1, p2);
        }

        public static Point3D IntersectionOf(Plane plane1, Plane plane2, Plane plane3)
        {
            var ray = plane1.IntersectionWith(plane2);
            return plane3.IntersectionWith(ray);
        }

        public static Point3D IntersectionOf(Plane plane, Ray3D ray)
        {
            return plane.IntersectionWith(ray);
        }

        public static Point3D operator +(Point3D p, Vector3D v)
        {
            return new Point3D(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }

        public static Point3D operator +(Point3D p, UnitVector3D v)
        {
            return new Point3D(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }

        public static Point3D operator -(Point3D p, Vector3D v)
        {
            return new Point3D(p.X - v.X, p.Y - v.Y, p.Z - v.Z);
        }

        public static Point3D operator -(Point3D p, UnitVector3D v)
        {
            return new Point3D(p.X - v.X, p.Y - v.Y, p.Z - v.Z);
        }

        public static Vector3D operator -(Point3D lhs, Point3D rhs)
        {
            return new Vector3D(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        // Not sure a ref to System.Windows.Media.Media3D is nice
        ////public static explicit operator Point3D(System.Windows.Media.Media3D.Point3D p)
        ////{
        ////    return new Point3D(p.X, p.Y, p.Z);
        ////}

        ////public static explicit operator System.Windows.Media.Media3D.Point3D(Point3D p)
        ////{
        ////    return new System.Windows.Media.Media3D.Point3D(p.X, p.Y, p.Z);
        ////}

        public Point3D MirrorAbout(Plane plane)
        {
            return plane.MirrorAbout(this);
        }

        public Point3D ProjectOn(Plane plane)
        {
            return plane.Project(this);
        }

        public Point3D Rotate(Vector3D aboutVector, Angle angle)
        {
            return Rotate(aboutVector.Normalize(), angle);
        }

        public Point3D Rotate(UnitVector3D aboutVector, Angle angle)
        {
            var cs = CoordinateSystem.Rotation(angle, aboutVector);
            return cs.Transform(this);
        }

        [Pure]
        public Vector3D VectorTo(Point3D p)
        {
            return p - this;
        }

        public double DistanceTo(Point3D p)
        {
            var vector = this.VectorTo(p);
            return vector.Length;
        }

        public Vector3D ToVector3D()
        {
            return new Vector3D(this.X, this.Y, this.Z);
        }

        public Point3D TransformBy(CoordinateSystem cs)
        {
            return cs.Transform(this);
        }

        public Point3D TransformBy(Matrix<double> m)
        {
            return new Point3D(m.Multiply(this.ToVector()));
        }

        /// <summary>
        /// Create a new Point3D from a Math.NET Numerics vector of length 3.
        /// </summary>
        public static Point3D OfVector(Vector<double> vector)
        {
            if (vector.Count != 3)
            {
                throw new ArgumentException("The vector length must be 3 in order to convert it to a Point3D");
            }

            return new Point3D(vector.At(0), vector.At(1), vector.At(2));
        }

        /// <summary>
        /// Convert to a Math.NET Numerics dense vector of length 3.
        /// </summary>
        public Vector<double> ToVector()
        {
            return Vector<double>.Build.Dense(new[] { X, Y, Z });
        }
    }
}
