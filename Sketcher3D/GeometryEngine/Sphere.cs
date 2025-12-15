using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketcher3D.GeometryEngine
{
    internal class Sphere : Shape
    {
        private double mRadius;

        public Sphere(string name, double radius, double height) : base("Sphere", name)
        {
            this.mRadius = radius;
            Build();
        }
        public double GetRadius() { return mRadius; }

        protected override void Build()
        {
            double x = 0;
            double y = 0;
            double z = 0;
            Point center = new Point(x, y, z);

            int stacks = 36;
            int number = 72;

            for (int i = 0; i < stacks; i++)
            {
                double iLatitude1 = Math.PI * (-0.5 + (double)i / stacks);
                double iLatitude2 = Math.PI * (-0.5 + (double)(i + 1) / stacks);

                double z1 = mRadius * Math.Sin(iLatitude1);
                double r1 = mRadius * Math.Cos(iLatitude1);

                double z2 = mRadius * Math.Sin(iLatitude2);
                double r2 = mRadius * Math.Cos(iLatitude2);

                for (int j = 0; j < number; j++)
                {
                    double jLatitude1 = 2 * Math.PI * (double)j / number;
                    double jLatitude2 = 2 * Math.PI * (double)(j + 1) / number;

                    // First ring
                    int idx1 = mTriangulation.GetPointIndex(new Point(r1 * Math.Cos(jLatitude1), r1 * Math.Sin(jLatitude1), z1));
                    int idx2 = mTriangulation.GetPointIndex(new Point(r1 * Math.Cos(jLatitude2), r1 * Math.Sin(jLatitude2), z1));

                    // Second ring
                    int idx3 = mTriangulation.GetPointIndex(new Point(r2 * Math.Cos(jLatitude1), r2 * Math.Sin(jLatitude1), z2));
                    int idx4 = mTriangulation.GetPointIndex(new Point(r2 * Math.Cos(jLatitude2), r2 * Math.Sin(jLatitude2), z2));

                    // Triangle 1
                    mTriangulation.AddTriangle(idx1, idx2, idx3);
                    // Triangle 2
                    mTriangulation.AddTriangle(idx2, idx4, idx3);
                }
            }
        }
        public override void Save(StreamWriter writer)
        {
            writer.WriteLine($"{GetShapeType()} {GetShapeName()} " +
                $"R {GetRadius()}");
        }

        public override void SaveForGNU(StreamWriter writer)
        {
            List<List<Point>> vec = new List<List<Point>>();
            List<Point> pts = new List<Point>();

            double x = 0;
            double y = 0;
            double z = 0;
            int number = 72;
            double dTheta = Math.PI / number; // 0 to 180
            double dPhi = 2 * Math.PI / number; // 0 to 360
            double phi = 0;
            double theta = 0;

            for (int i = 0; i <= number; i++)
            {
                theta = i * dTheta;
                for (int j = 0; j <= number; j++)
                {
                    phi = j * dPhi;

                    double x1 = mRadius * Math.Sin(theta) * Math.Cos(phi);
                    double y1 = mRadius * Math.Sin(theta) * Math.Sin(phi);
                    double z1 = mRadius * Math.Cos(theta);

                    pts.Add(new  Point(x + x1, y + y1, z + z1));
                }
                double x2 = mRadius * Math.Sin(theta) * Math.Cos(0);
                double y2 = mRadius * Math.Sin(theta) * Math.Sin(0);
                double z2 = mRadius * Math.Cos(theta);
                pts.Add(new  Point(x + x2, y + y2, z + z2));
                vec.Add(pts);
                pts.Clear();
            }

            for (int i = 0; i <= number; i++)
            {
                theta = i * dTheta;
                for (int j = 0; j <= number; j++)
                {
                    phi = j * dPhi;

                    double x1 = mRadius * Math.Cos(theta);
                    double y1 = mRadius * Math.Sin(theta) * Math.Cos(phi);
                    double z1 = mRadius * Math.Sin(theta) * Math.Sin(phi);
                    pts.Add(new  Point(x + x1, y + y1, z + z1));
                }
                double x2 = mRadius * Math.Cos(theta);
                double y2 = mRadius * Math.Sin(theta) * Math.Cos(0);
                double z2 = mRadius * Math.Sin(theta) * Math.Sin(0);
                pts.Add(new  Point(x + x2, y + y2, z + z2));
                vec.Add(pts);
                pts.Clear();
            }

            for (int i = 0; i <= number; i++)
            {
                theta = i * dTheta;
                for (int j = 0; j <= number; j++)
                {
                    phi = j * dPhi;

                    double x1 = mRadius * Math.Sin(theta) * Math.Sin(phi);
                    double y1 = mRadius * Math.Cos(theta);
                    double z1 = mRadius * Math.Sin(theta) * Math.Cos(phi);
                    pts.Add(new  Point(x + x1, y + y1, z + z1));
                }
                double x2 = mRadius * Math.Sin(theta) * Math.Sin(0);
                double y2 = mRadius * Math.Cos(theta);
                double z2 = mRadius * Math.Sin(theta) * Math.Cos(0);
                pts.Add(new  Point(x + x2, y + y2, z + z2));
                vec.Add(pts);
                pts.Clear();
            }

            foreach (var ptsVec in vec)
            {
                foreach (var pt in ptsVec)
                {
                    pt.WriteXYZ(writer);
                }
                writer.Write("\n\n");
            }
            writer.Write("\n\n");
        }
    }
}
