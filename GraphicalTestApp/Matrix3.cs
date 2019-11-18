using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Matrix3
    {
        //Static reference to the identity matrix
        public static Matrix3 Identity = new Matrix3();
        //Each value stored in the Matrix3
        public float m11, m12, m13, m21, m22, m23, m31, m32, m33;

        //Creates a Matrix3 equal to the identity matrix
        public Matrix3()
        {
            m11 = 1; m12 = 0; m13 = 0;
            m21 = 0; m22 = 1; m23 = 0;
            m31 = 0; m32 = 0; m33 = 1;
        }

        //Creates a Matrix3 with the specified values
        public Matrix3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
        {
            this.m11 = m11; this.m12 = m12; this.m13 = m13;
            this.m21 = m21; this.m22 = m22; this.m23 = m23;
            this.m31 = m31; this.m32 = m32; this.m33 = m33;
        }

        //Creates a copy of the specified Matrix3
        public Matrix3(Matrix3 matrix3)
        {
            m11 = matrix3.m11; m12 = matrix3.m12; m13 = matrix3.m13;
            m21 = matrix3.m21; m22 = matrix3.m22; m23 = matrix3.m23;
            m31 = matrix3.m31; m32 = matrix3.m32; m33 = matrix3.m33;
        }

        public override string ToString()
        {
            return
                "{[" + m11 + ", " + m12 + ", " + m13 + "]," +
                "[" + m21 + ", " + m22 + ", " + m23 + "]," +
                "[" + m31 + ", " + m32 + ", " + m33 + "]}";
        }

        //Sets this Matrix3 to the specified values
        public void Set(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
        {
            this.m11 = m11; this.m12 = m12; this.m13 = m13;
            this.m21 = m21; this.m22 = m22; this.m23 = m23;
            this.m31 = m31; this.m32 = m32; this.m33 = m33;
        }

        //Sets this Matrix3 to the values of the specified Matrix3
        public void Set(Matrix3 matrix3)
        {
            m11 = matrix3.m11; m12 = matrix3.m12; m13 = matrix3.m13;
            m21 = matrix3.m21; m22 = matrix3.m22; m23 = matrix3.m23;
            m31 = matrix3.m31; m32 = matrix3.m32; m33 = matrix3.m33;
        }

        //Returns the transpose of the Matrix3
        public Matrix3 GetTransposed()
        {
            return new Matrix3(m11, m21, m31, m12, m22, m32, m13, m23, m33);
        }

        //Set this Matrix3 to a scale matrix with the specified values
        public void SetScaled(float x, float y, float z)
        {
            m11 = x; m12 = 0; m13 = 0;
            m21 = 0; m22 = y; m23 = 0;
            m31 = 0; m32 = 0; m33 = z;
        }

        //Set this Matrix3 to a scale matrix with the specified Vector3's values
        public void SetScaled(Vector3 v)
        {
            m11 = v.x; m12 = 0; m13 = 0;
            m21 = 0; m22 = v.y; m23 = 0;
            m31 = 0; m32 = 0; m33 = v.z;
        }

        //Scale this Matrix3 by the specified values
        public void Scale(float x, float y, float z)
        {
            Matrix3 m = new Matrix3();
            m.SetScaled(x, y, z);
            Set(this * m);
        }

        //Scale this Matrix3 by the specified Vector3's values
        public void Scale(Vector3 v)
        {
            Matrix3 m = new Matrix3();
            m.SetScaled(v);
            Set(this * m);
        }

        //Set this Matrix3 to a rotate matrix with the specified values
        public void SetRotateX(double radians)
        {
            Set(1, 0, 0,
                0, (float)Math.Cos(radians), (float)-Math.Sin(radians),
                0, (float)Math.Sin(radians), (float)Math.Cos(radians));
        }

        //Rotate this Matrix3 with the specified values
        public void RotateX(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateX(radians);
            Set(this * m);
        }

        //Set this Matrix3 to a rotate matrix with the specified values
        public void SetRotateY(double radians)
        {
            Set((float)Math.Cos(radians), 0, (float)Math.Sin(radians),
                0, 1, 0,
                (float)-Math.Sin(radians), 0, (float)Math.Cos(radians));
        }

        //Rotate this Matrix3 with the specified values
        public void RotateY(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateY(radians);
            Set(this * m);
        }

        //Set this Matrix3 to a rotate matrix with the specified values
        public void SetRotateZ(double radians)
        {
            Set((float)Math.Cos(radians), (float)-Math.Sin(radians), 0,
                (float)Math.Sin(radians), (float)Math.Cos(radians), 0,
                0, 0, 1);
        }

        //Rotate this Matrix3 with the specified values
        public void RotateZ(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateZ(radians);
            Set(this * m);
        }

        public void SetEuler(float pitch, float yaw, float roll)
        {
            Matrix3 x = new Matrix3();
            Matrix3 y = new Matrix3();
            Matrix3 z = new Matrix3();
            x.SetRotateX(pitch);
            y.SetRotateY(yaw);
            z.SetRotateZ(roll);

            Set(z * y * x);
        }

        public void SetTranslation(float x, float y, float z)
        {
            m13 = x; m23 = y; m33 = z;
        }

        public void Translate(float x, float y, float z)
        {
            m13 += x; m23 += y; m33 += z;
        }

        //Matrix3 * Matrix3
        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3(
                lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31,
                lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32,
                lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33,

                lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31,
                lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32,
                lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33,

                lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31,
                lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32,
                lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33);
        }

        //Matrix3 * Vector3
        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {
            return new Vector3(
                lhs.m11 * rhs.x + lhs.m12 * rhs.y + lhs.m13 * rhs.z,
                lhs.m21 * rhs.x + lhs.m22 * rhs.y + lhs.m23 * rhs.z,
                lhs.m31 * rhs.x + lhs.m32 * rhs.y + lhs.m33 * rhs.z);
        }
    }
}