using System.Windows.Media.Media3D;
using Sketcher3D.GeometryEngine;

namespace Sketcher3D
{
    public static class MeshBuilder
    {
        public static MeshGeometry3D FromTriangulation(Triangulation t)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            var v = t.GetDoubleDataforWPF();
            var n = t.GetNormalDoubleDataforWPF();

            for (int i = 0; i < v.Count; i += 3)
            {
                mesh.Positions.Add(new Point3D(v[i], v[i + 1], v[i + 2]));
                mesh.TriangleIndices.Add(i / 3);
            }

            for (int i = 0; i < n.Count; i += 3)
            {
                mesh.Normals.Add(
                    new Vector3D(n[i], n[i + 1], n[i + 2]));
            }

            return mesh;
        }
    }
}
