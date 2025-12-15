namespace Sketcher3D.GeometryEngine
{
    internal class Triangle
    {
        public int m1;
        public int m2;
        public int m3;
        public Point mNormal;

        public Triangle(int m1, int m2, int m3, Point normal = null)
        {
            if(normal == null)
                normal = new Point();
            else 
                this.mNormal = normal;

            this.m1 = m1;
            this.m2 = m2;
            this.m3 = m3;
        }

    }
}
