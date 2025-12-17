using System;
using System.IO;

namespace Sketcher3D.GeometryEngine
{
    public class Point
    {
        private double mX;
        private double mY;
        private double mZ;

        public Point() 
        {
            this.mX = 0;
            this.mY = 0;
            this.mZ = 0;
        }
        public Point(double x, double y, double z)
        {
            this.mX = x;
            this.mY = y;
            this.mZ = z;
        }

        public double GetX() { return mX; }
        public double GetY() { return mY; }
        public double GetZ() { return mZ; }

        public void SetX(double x) { mX = x; }
        public void SetY(double y) { mY = y; }
        public void SetZ(double z) { mZ = z; }

        public double Distance (Point other)
        {
            return Math.Sqrt((mX - other.mX) * (mX - other.mX) +
                      (mY - other.mY) * (mY - other.mY) +
                      (mZ - other.mZ) * (mZ - other.mZ));
        }

        public void WriteXYZ(StreamWriter writer)
        {
            writer.Write(mX); writer.Write(" ");
            writer.Write(mY); writer.Write(" ");
            writer.Write(mZ);
        }

        public const double tolerance = 10e-6; // check is this correct way
        public bool IsEqual(Point other)
        {
            return ((mX - other.mX < tolerance) && 
                    (mY - other.mY < tolerance) &&
                    ((mZ - other.mZ) < tolerance));
        }
    }
}
