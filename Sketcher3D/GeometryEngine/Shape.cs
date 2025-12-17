using System.IO;

namespace Sketcher3D.GeometryEngine
{
    abstract public class Shape
    {
        private string mType;
        private string mName;
        
        public Shape(string type, string name) 
        {
            this.mType = type;
            this.mName = name;
        }

        protected Triangulation mTriangulation = new Triangulation();
        public Triangulation GetTriangulation() { return mTriangulation; }
        protected abstract void Build();

        public string GetShapeName() {  return mName;  }
        public string GetShapeType() { return mType; }

        public abstract void Save(StreamWriter writer);
        public abstract void SaveForGNU(StreamWriter writer);

    }
}
