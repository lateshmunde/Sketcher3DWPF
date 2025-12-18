using System;
using System.Collections.Generic;

namespace Sketcher3D.GeometryEngine
{
    public class Triangulation
    {
        private List<Point> mPoints = new List<Point>();
        private List<Point> mNormals = new List<Point>();
        private List<Triangle> mTriangles = new List<Triangle>();
        private Dictionary<Point, int> pointIndex = new Dictionary<Point, int>(); //// map: point - index in mPoints

        public Triangulation() { }

        public List<Point> GetPoints() { return mPoints; }
        public List<Triangle> GetTriangles() { return mTriangles; }
        public List<Point> GetNormals() { return mNormals; }

        public int AddPoint(Point p)
        {
            if (pointIndex.TryGetValue(p, out int existingIndex)) 
                return existingIndex;
            int index = mPoints.Count;
            mPoints.Add(p);
            pointIndex[p] = index;
            return index;
        }

        public void AddTriangle(int a, int b, int c, Point normal = null)
        {
            
            Triangle tri = new Triangle(a, b, c, normal);
            mTriangles.Add(tri);
            if (normal == null)
                normal = new Point();
            else
                normal = CalculateNormal(tri);
            mNormals.Add(normal);

        }
        private Point CalculateNormal(Triangle tri)
        {
            double x = mPoints[tri.m2].GetX() - mPoints[tri.m1].GetX();
            double y = mPoints[tri.m2].GetY() - mPoints[tri.m1].GetY();
            double z = mPoints[tri.m2].GetZ() - mPoints[tri.m1].GetZ();

            Point u = new Point(x, y, z);

            x = mPoints[tri.m3].GetX() - mPoints[tri.m1].GetX();
            y = mPoints[tri.m3].GetY() - mPoints[tri.m1].GetY();
            z = mPoints[tri.m3].GetZ() - mPoints[tri.m1].GetZ();

            Point v = new Point(x, y, z);

            double nx = u.GetY() * v.GetZ() - u.GetZ() * v.GetY();
            double ny = u.GetZ() * v.GetX() - u.GetX() * v.GetZ();
            double nz = u.GetX() * v.GetY() - u.GetY() * v.GetX();

            double len = Math.Sqrt(nx * nx + ny * ny + nz * nz);
            u = new Point(nx / len, ny / len, nz / len);
            return u;
        }

        public List<double> GetDoubleDataforWPF()
        {
            List<double> points = new List<double>();

            foreach(Triangle tri in mTriangles)
            {
                points.Add(mPoints[tri.m1].GetX());
                points.Add(mPoints[tri.m1].GetY());
                points.Add(mPoints[tri.m1].GetZ());

                points.Add(mPoints[tri.m2].GetX());
                points.Add(mPoints[tri.m2].GetY());
                points.Add(mPoints[tri.m2].GetZ());

                points.Add(mPoints[tri.m3].GetX());
                points.Add(mPoints[tri.m3].GetY());
                points.Add(mPoints[tri.m3].GetZ());
            }
            return points;
        }

        public List<double> GetNormalDoubleDataforWPF()
        {
            List<double> points = new List<double>();

            foreach (Triangle tri in mTriangles)
            {
                Point pt = CalculateNormal(tri);

                points.Add(pt.GetX());
                points.Add(pt.GetY());
                points.Add(pt.GetZ());
            }
            return points;
        }
    }
}
