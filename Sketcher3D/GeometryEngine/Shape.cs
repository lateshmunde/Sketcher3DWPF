using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sketcher3D.GeometryEngine
{
    internal abstract class Shape
    {
        private string mName;
        private string mType;
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
