using System.Windows;
using System.Windows.Media.Media3D;
using Sketcher3D.GeometryEngine;

namespace Sketcher3D
{
    public static class MeshBuilder
    {
        //convert Point(Geometry Engine) to Point3D(system.windos)
        public static MeshGeometry3D FromTriangulation(Triangulation t)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            
            var points = t.GetPointsDoubleData();
            var normals = t.GetNormalDoubleData();
         
            for (int i = 0; i < points.Count; i += 3) 
            {
                mesh.Positions.Add(new Point3D(points[i], points[i + 1], points[i + 2]));
                mesh.TriangleIndices.Add(i / 3);
            }

            for (int i = 0; i < normals.Count; i += 3)
            {
                mesh.Normals.Add(
                    new Vector3D(normals[i], normals[i + 1], normals[i + 2]));
            }
            return mesh;
        }
    }
}
