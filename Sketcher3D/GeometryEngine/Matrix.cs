using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sketcher3D.GeometryEngine
{
    internal class Matrix
    {
        private int mRows;
        private int mCols;
        public List<List<double>> data = new List<List<double>>();


        public Matrix (int row = 4, int col = 4) 
        {
            this.mRows = row;
            this.mCols = col;

            //initialization of all values with zero (0.0)
            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mCols; j++)
                {
                    data[i][j] = 0.0;
                }
            }
        }

        public static Matrix GetIdentity(int s = 4)
        {
            Matrix identity = new Matrix(s, s);

            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                {
                    if (i == j)
                        identity.data[i][j] = 1.0;
                    else
                        identity.data[i][j] = 0.0;
                }
            }
            return identity;
        }

        public static Matrix MatrixAdd(Matrix m1, Matrix m2)
        {
            Matrix result = new Matrix(m1.mRows, m1.mCols);

            if (m1.mRows == m2.mRows && m1.mCols == m2.mCols)
            {
                for (int i = 0; i < m1.mRows; i++)
                {
                    for (int j = 0; j < m1.mCols; j++)
                    {
                        result.data[i][j] = m1.data[i][j] + m2.data[i][j];
                    }
                }
                return result;
            }
            else
                return null;
        }

        public static Matrix MatrixMultiply(Matrix m1, Matrix m2)
        {
            Matrix result = new Matrix(m1.mRows, m2.mCols);

            for (int i = 0; i < m1.mRows; i++)
            {
                for (int j = 0; j < m2.mCols; j++)
                {
                    result.data[i][j] = 0;
                    for (int k = 0; k < m1.mCols; k++) // this->cols == other.rows
                    {
                        result.data[i][j] = result.data[i][j] + (m1.data[i][k] * m2.data[k][j]);
                    }
                }
            }
            return result;
        }

        public static Matrix GetTranslationMatrix(double tx, double ty, double tz)
        {
            Matrix transMat = GetIdentity();
            transMat.data[0][3] = tx;
            transMat.data[1][3] = ty;
            transMat.data[2][3] = tz;
            return transMat;
        }


        public static Matrix GetScalingMatrix(double sx, double sy, double sz)
        {
            Matrix scaleMat = GetIdentity();
            scaleMat.data[0][0] = sx;
            scaleMat.data[1][1] = sy;
            scaleMat.data[2][2] = sz;
            return scaleMat;
        }

        public static Matrix GetRotationXMatrix(double degreeX)
        {
            Matrix rotXMat = GetIdentity();
            double radAngX = degreeX * Math.PI / 180.0;
            double cosX = Math.Cos(radAngX);
            double sinX = Math.Sin(radAngX);

            rotXMat.data[1][1] = cosX; rotXMat.data[1][2] = -sinX;
            rotXMat.data[2][1] = sinX; rotXMat.data[2][2] = cosX;
            return rotXMat;
        }


        public static Matrix GetRotationYMatrix(double degreeY)
        {
            Matrix rotYMat = GetIdentity();
            double radAngY = degreeY * Math.PI / 180.0;
            double cosY = Math.Cos(radAngY);
            double sinY = Math.Sin(radAngY);

            rotYMat.data[0][0] = cosY; rotYMat.data[0][2] = sinY;
            rotYMat.data[2][0] = -sinY; rotYMat.data[2][2] = cosY;
            return rotYMat;
        }

        public static Matrix GetRotationZMatrix(double degreeZ)
        {
            Matrix rotZMat = GetIdentity();
            double radAngZ = degreeZ * Math.PI / 180.0;
            double cosZ = Math.Cos(radAngZ);
            double sinZ = Math.Sin(radAngZ);

            rotZMat.data[0][0] = cosZ; rotZMat.data[0][1] = -sinZ;
            rotZMat.data[1][0] = sinZ; rotZMat.data[1][1] = cosZ;
            return rotZMat;
        }
    }
}




