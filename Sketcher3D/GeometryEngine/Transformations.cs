using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sketcher3D.GeometryEngine
{
    internal class Transformations
    {
        public Transformations() { }

        public static Point CalculatePivot(List<Point> vertices) //centroid calculation
        {
            double cx = 0;
            double cy = 0;
            double cz = 0;

            return new Point(cx, cy, cz);
        }

        public static List<Point> ApplyTransform(List<Point> vertices, Matrix matrix)
        {
            List<Point> transformedPts = new List<Point>();

            foreach (Point p in vertices)
            {
                Matrix pt = new Matrix(4, 1);
                pt.data[0][0] = p.GetX();
                pt.data[1][0] = p.GetY();
                pt.data[2][0] = p.GetZ();
                pt.data[3][0] = 1.0;

                Matrix result = Matrix.MatrixMultiply(matrix, pt);

                double X = result.data[0][0];
                double Y = result.data[1][0];
                double Z = result.data[2][0];
                transformedPts.Add(new Point(X, Y, Z));
            }
            return transformedPts;
        }

        public static List<float> Translate(List<float> vec, double transX, double transY, double transZ)
        {
            List<Point> vertices = new List<Point>();

            for (int i = 0; i < vec.Count; i += 3)
            {
                vertices.Add(new Point(vec[i], vec[i + 1], vec[i + 2]));
            }

            Matrix transMat = Matrix.GetTranslationMatrix(transX, transY, transZ);
            List<Point> transformedPts = ApplyTransform(vertices, transMat);

            for (int i = 0; i < transformedPts.Count; i++)
            {
                vec.Add((float)(transformedPts[i].GetX()));
                vec.Add((float)(transformedPts[i].GetY()));
                vec.Add((float)(transformedPts[i].GetZ()));
            }
            return vec;
        }

        public static List<float> Scale(List<float> vec, double scaleX, double scaleY, double scaleZ)
        {
            List<Point> vertices = new List<Point>();

            for (int i = 0; i < vec.Count; i += 3)
            {
                vertices.Add(new Point(vec[i], vec[i + 1], vec[i + 2]));
            }


            Point pivot = CalculatePivot(vertices); //Centroid

            Matrix translate1 = Matrix.GetTranslationMatrix(-pivot.GetX(), -pivot.GetY(), -pivot.GetY());
            Matrix scaleMat = Matrix.GetScalingMatrix(scaleX, scaleY, scaleZ);
            Matrix translate2 = Matrix.GetTranslationMatrix(pivot.GetX(), pivot.GetY(), pivot.GetY());

            Matrix transformed = Matrix.MatrixMultiply(translate2 , scaleMat);
            transformed = Matrix.MatrixMultiply(transformed , translate1);

            List<Point> transformedPts = ApplyTransform(vertices, transformed);
            for (int i = 0; i < transformedPts.Count; i++)
            {
                vec.Add((float)(transformedPts[i].GetX()));
                vec.Add((float)(transformedPts[i].GetY()));
                vec.Add((float)(transformedPts[i].GetZ()));
            }

            return vec;
        }

        public static List<float> RotationX(List<float> vec, double degreeX)
        {
            List<Point> vertices = new List<Point>();

            for (int i = 0; i < vec.Count; i += 3)
            {
                vertices.Add(new Point(vec[i], vec[i + 1], vec[i + 2]));
            }


            Point pivot = CalculatePivot(vertices); //Centroid

            Matrix translate1 = Matrix.GetTranslationMatrix(-pivot.GetX(), -pivot.GetY(), -pivot.GetY());
            Matrix rotateXMat = Matrix.GetRotationXMatrix(degreeX);
            Matrix translate2 = Matrix.GetTranslationMatrix(pivot.GetX(), pivot.GetY(), pivot.GetY());

            Matrix transformed = Matrix.MatrixMultiply(translate2, rotateXMat);
            transformed = Matrix.MatrixMultiply(transformed, translate1);

            List<Point> transformedPts = ApplyTransform(vertices, transformed);
            for (int i = 0; i < transformedPts.Count; i++)
            {
                vec.Add((float)(transformedPts[i].GetX()));
                vec.Add((float)(transformedPts[i].GetY()));
                vec.Add((float)(transformedPts[i].GetZ()));
            }
            return vec;
        }

        public static List<float> RotationY(List<float> vec, double degreeY)
        {
            List<Point> vertices = new List<Point>();

            for (int i = 0; i < vec.Count; i += 3)
            {
                vertices.Add(new Point(vec[i], vec[i + 1], vec[i + 2]));
            }


            Point pivot = CalculatePivot(vertices); //Centroid

            Matrix translate1 = Matrix.GetTranslationMatrix(-pivot.GetX(), -pivot.GetY(), -pivot.GetY());
            Matrix rotateYMat = Matrix.GetRotationYMatrix(degreeY);
            Matrix translate2 = Matrix.GetTranslationMatrix(pivot.GetX(), pivot.GetY(), pivot.GetY());

            Matrix transformed = Matrix.MatrixMultiply(translate2, rotateYMat);
            transformed = Matrix.MatrixMultiply(transformed, translate1);

            List<Point> transformedPts = ApplyTransform(vertices, transformed);
            for (int i = 0; i < transformedPts.Count; i++)
            {
                vec.Add((float)(transformedPts[i].GetX()));
                vec.Add((float)(transformedPts[i].GetY()));
                vec.Add((float)(transformedPts[i].GetZ()));
            }
            return vec;
        }

        public static List<float> RotationZ(List<float> vec, double degreeZ)
        {
            List<Point> vertices = new List<Point>();

            for (int i = 0; i < vec.Count; i += 3)
            {
                vertices.Add(new Point(vec[i], vec[i + 1], vec[i + 2]));
            }


            Point pivot = CalculatePivot(vertices); //Centroid

            Matrix translate1 = Matrix.GetTranslationMatrix(-pivot.GetX(), -pivot.GetY(), -pivot.GetY());
            Matrix rotateZMat = Matrix.GetRotationZMatrix(degreeZ);
            Matrix translate2 = Matrix.GetTranslationMatrix(pivot.GetX(), pivot.GetY(), pivot.GetY());

            Matrix transformed = Matrix.MatrixMultiply(translate2, rotateZMat);
            transformed = Matrix.MatrixMultiply(transformed, translate1);

            List<Point> transformedPts = ApplyTransform(vertices, transformed);
            for (int i = 0; i < transformedPts.Count; i++)
            {
                vec.Add((float)(transformedPts[i].GetX()));
                vec.Add((float)(transformedPts[i].GetY()));
                vec.Add((float)(transformedPts[i].GetZ()));
            }
            return vec;
        }

    }
}
