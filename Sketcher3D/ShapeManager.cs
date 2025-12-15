using Sketcher3D.GeometryEngine;
using System.Collections.Generic;

namespace Sketcher3D
{
    internal class ShapeManager
    {
        private List<Shape> mShapes;
        public ShapeManager() { }

        public void AddShape(Shape shape)
        {
            mShapes.Add(shape);
        }

        public List<Shape> GetShapesVec()
        {
            return mShapes;
        }

        public void DeleteShape(int index)
        {
            if (mShapes == null) return;
            if ((index >= 0) && (index < mShapes.Count))
            {
                mShapes.RemoveAt(index);
            }
        }

        public void DeleteShape()
        {
            int index = mShapes.Count - 1;
            mShapes.RemoveAt(index);
        }

        public void ClearShape()
        {
            mShapes.Clear();
        }

        public Shape GetLastShape()
        {
            if (mShapes == null) return null;
            return mShapes[mShapes.Count - 1];
        }

    }
}
