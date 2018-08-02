using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPTools_Math
{
    /// <summary>
    /// Vector class with basic vector operations and ordering
    /// 
    /// Mehrdad Negahban
    /// 07-12-2009
    /// Modified: 12-30-2010, 02-23-2012
    /// 
    /// </summary>
    [Serializable]
    public class Vector
    {
        public int Rows;
        public double[] Values;
        public Vector()
        {
        }
        public Vector(int NumberOfRows)
        {
            Rows = NumberOfRows;
            Values = new double[Rows];
        }
        public Vector(double[] VectorValues)
        {
            Rows = VectorValues.Length;
            Values = new double[Rows];
            for (int i = 0; i < Rows; i++)
            {
                Values[i] = VectorValues[i];
            }
        }
        public Vector(Vector VectorValues)
        {
            Rows = VectorValues.Values.Length;
            Values = new double[Rows];
            for (int i = 0; i < Rows; i++)
            {
                Values[i] = VectorValues.Values[i];
            }
        }

        #region Methods to calculate: max, min, average

        /// <summary>
        /// Return the average of the components in the vector (or this vector)
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public double Average(double[] u)
        {
            return ArrayTools.Average(u);
        }
        public double Average(Vector u)
        {
            return ArrayTools.Average(u.Values);
        }
        public static Vector Average(Vector[] Vectors)
        {
            int N = Vectors.Length;
            Vector Ave = Vectors[0];
            for (int i = 1; i < N; i++)
            {
                Ave += Vectors[i];
            }
            return Ave / Convert.ToDouble(N);
        }
        public double Average()
        {
            return ArrayTools.Average(Values);
        }
        /// <summary>
        /// Return the maximum element in the vector (or this vector)
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public double Max(double[] u)
        {
            return ArrayTools.Max(u);
        }
        public double Max(Vector u)
        {
            return ArrayTools.Max(u.Values);
        }
        public double Max()
        {
            return ArrayTools.Max(Values);
        }
        /// <summary>
        /// Return the minimum element in the vector (or this vector)
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public double Min(double[] u)
        {
            return ArrayTools.Min(u);
        }
        public double Min(Vector u)
        {
            return ArrayTools.Min(u.Values);
        }
        public double Min()
        {
            return ArrayTools.Min(Values);
        }
        #endregion

        #region Methods to calculate: magnitude, unit vector
        /// <summary>
        /// Return the magnitude of the vector: Sqrt[Sum(u_i^2)]
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public double Magnitude(double[] u)
        {
            return ArrayTools.Vector_Magnitude(u);
        }
        public double Magnitude()
        {
            return ArrayTools.Vector_Magnitude(Values);
        }
        /// <summary>
        /// Return a unit vector made from the vector
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public Vector UnitVector(double[] u)
        {
            Vector Vector1 = new Vector();
            Vector1.Values = ArrayTools.Vector_MakeUnitVector(u);
            Vector1.Rows = Vector1.Values.Length;
            return Vector1;
        }
        public Vector UnitVector()
        {
            return UnitVector(Values);
        }
        public Vector UnitVector(Vector Vector1)
        {
            return UnitVector(Vector1.Values);
        }
        /// <summary>
        /// Make this vector a unit vector
        /// </summary>
        public void MakeThisAUnitVector()
        {
             Values = UnitVector().Values;
        }
        #endregion

        #region Methods to calculate: +, -, *, /
        /// <summary>
        /// Return a vector that is an addition of the two vectors: w = u+v
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector Add(double[] u, double[] v)
        {
            Vector Vector1 = new Vector();
            Vector1.Values =ArrayTools.Add(u, v);
            return Vector1;
        }
        public static Vector Add(Vector u, double[] v)
        {
            Vector Vector1 = new Vector();
            Vector1.Values = ArrayTools.Add(u.Values, v);
            return Vector1;
        }
        public static Vector Add(double[] u, Vector v)
        {
            Vector Vector1 = new Vector();
            Vector1.Values = ArrayTools.Add(u, v.Values);
            return Vector1;
        }
        public static Vector Add(Vector u, Vector v)
        {
            Vector Vector1 = new Vector();
            Vector1.Values = ArrayTools.Add(u.Values, v.Values);
            return Vector1;
        }
        /// <summary>
        /// Return a vector that is a subtraction of the two vectors: w = u-v
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector Subtract(double[] u, double[] v)
        {
            Vector Vector1 = new Vector();
            Vector1.Values =ArrayTools.Subtract(u, v);
            return Vector1;
        }
        public static Vector Subtract(Vector u, double[] v)
        {
            Vector Vector1 = new Vector();
            Vector1.Values = ArrayTools.Subtract(u.Values, v);
            return Vector1;
        }
        public static Vector Subtract(double[] u, Vector v)
        {
            Vector Vector1 = new Vector();
            Vector1.Values = ArrayTools.Subtract(u, v.Values);
            return Vector1;
        }
        public static Vector Subtract(Vector u, Vector v)
        {
            Vector Vector1 = new Vector();
            Vector1.Values = ArrayTools.Subtract(u.Values, v.Values);
            return Vector1;
        }
        /// <summary>
        /// Return a vector that is negative the given vector
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static Vector Negative(double[] u)
        {
            Vector Vector1 = new Vector();
            Vector1.Values =ArrayTools.Negative(u);
            return Vector1;
        }
        /// <summary>
        /// Return a vector that scales the current vector by the given scale factor
        /// </summary>
        /// <param name="ScaleFactor"></param>
        /// <param name="u"></param>
        /// <returns></returns>
        public static Vector Scale(double ScaleFactor, double[] u)
        {
            Vector Vector1 = new Vector();
            Vector1.Values =ArrayTools.Multiply(ScaleFactor,u);
            return Vector1;
        }

        public static Vector operator +(Vector Vector1, Vector Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values =ArrayTools.Add(Vector1.Values, Vector2.Values);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        /// <summary>
        /// Return a vecto which is this vector plus the given vector: z_i = x_i+y_i
        /// </summary>
        /// <param name="Vector2"></param>
        /// <returns></returns>
        public  Vector Plus(Vector Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Add(this.Values, Vector2.Values);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        public static Vector operator +(double[] Vector1, Vector Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values =ArrayTools.Add(Vector1, Vector2.Values);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        public static Vector operator +(Vector Vector1, double[] Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values =ArrayTools.Add(Vector1.Values, Vector2);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        /// <summary>
        /// Return a vecto which is this vector plus the given vector: z_i = x_i+y_i
        /// </summary>
        /// <param name="Vector2"></param>
        /// <returns></returns>
        public Vector Plus(double[] Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Add(this.Values, Vector2);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }

        public static Vector operator -(Vector Vector1, Vector Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values =ArrayTools.Subtract(Vector1.Values, Vector2.Values);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        /// <summary>
        /// Return a vector that is this vector minus the given vector: z_i = x_i-y_i
        /// </summary>
        /// <param name="Vector2"></param>
        /// <returns></returns>
        public Vector Minus(Vector Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Subtract(this.Values, Vector2.Values);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        public static Vector operator -(double[] Vector1, Vector Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values =ArrayTools.Subtract(Vector1, Vector2.Values);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        public static Vector operator -(Vector Vector1, double[] Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values =ArrayTools.Subtract(Vector1.Values, Vector2);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        /// <summary>
        /// Return a vector that is this vector minus the given vector: z_i = x_i-y_i
        /// </summary>
        /// <param name="Vector2"></param>
        /// <returns></returns>
        public Vector Minus(double[] Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Subtract(this.Values, Vector2);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        public static Vector operator -(Vector Vector1)
        {
            Vector Vector3 = new Vector();
            Vector3.Values =ArrayTools.Negative(Vector1.Values);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        /// <summary>
        /// Return a vector that is the negative of the original vector
        /// </summary>
        /// <returns></returns>
        public Vector Negative()
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Negative(this.Values);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }

        public static Vector operator *(double Scalar, Vector Vector1)
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Multiply(Vector1.Values, Scalar);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        /// <summary>
        /// Return a vector that is has each component multiplied by the given scalar
        /// </summary>
        /// <param name="Scalar"></param>
        /// <returns></returns>
        public Vector Times(double Scalar)
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Multiply(this.Values, Scalar);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        public static Vector operator *(Vector Vector1, double Scalar)
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Multiply(Vector1.Values, Scalar); 
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        public static Vector operator *(Vector Vector1, Vector Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values =ArrayTools.Multiply(Vector1.Values, Vector2.Values);
            return Vector3;
        }
        /// <summary>
        /// Create a vector that has each component given as the product of the components of the two vectors: z_i = x_i*y_i
        /// </summary>
        /// <param name="Vector2"></param>
        /// <returns></returns>
        public Vector ComponentTimes(Vector Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Multiply(this.Values, Vector2.Values);
            return Vector3;
        }
        /// <summary>
        /// Calculate the multidimensional dot product: Sum (x_i*y_i)
        /// </summary>
        /// <param name="Vector2"></param>
        /// <returns></returns>
        public double Times(Vector Vector2)
        {
            return DotProduct(this,Vector2);
        }
        public double Times(double[] Vector2)
        {
            return DotProduct(this, Vector2);
        }
   
        public static Vector operator /(Vector Vector1, double Scalar)
        {
            double Inverse = 1.0d / Scalar;
            return Vector.Scale(Inverse, Vector1.Values);
        }
        /// <summary>
        /// Divide each component by the given scalar: x_i/a
        /// </summary>
        /// <param name="Scalar"></param>
        /// <returns></returns>
        public Vector DivideBy(double Scalar)
        {
            double Inverse = 1.0d / Scalar;
            return Vector.Scale(Inverse, this.Values);
        }
        public static Vector operator /(Vector Vector1, Vector Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values =ArrayTools.Divide(Vector1.Values, Vector2.Values);
            return Vector3;
        }
        /// <summary>
        /// Divide each component of the current vector by the given vector: x_i/y_i
        /// </summary>
        /// <param name="Vector2"></param>
        /// <returns></returns>
        public Vector ComponentDivideBy(Vector Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Divide(this.Values, Vector2.Values);
            return Vector3;
        }

        #endregion

        #region Methods to calculate: Dot and cross products

        /// <summary>
        /// Calculate dot product of two vectors
        /// </summary>
        /// <param name="Vector1"></param>
        /// <param name="Vector2"></param>
        /// <returns></returns>
        public static double DotProduct(Vector Vector1, Vector Vector2)
        {
            Vector Vector3 = new Vector();
            return ArrayTools.Vector_DotProduct(Vector1.Values, Vector2.Values); 
        }
        public static double DotProduct(double[] Vector1, Vector Vector2)
        {
            Vector Vector3 = new Vector();
            return ArrayTools.Vector_DotProduct(Vector1, Vector2.Values);
        }
        public static double DotProduct(Vector Vector1, double[] Vector2)
        {
            Vector Vector3 = new Vector();
            return ArrayTools.Vector_DotProduct(Vector1.Values, Vector2);
        }
        public static double DotProduct(double[] Vector1, double[] Vector2)
        {
            Vector Vector3 = new Vector();
            return ArrayTools.Vector_DotProduct(Vector1, Vector2);
        }

        /// <summary>
        /// Calculate cross product to two vectors and return a Vector (only for 3D vectors)
        /// </summary>
        /// <param name="Vector1"></param>
        /// <param name="Vector2"></param>
        /// <returns></returns>
        public static Vector CrossProduct(Vector Vector1, Vector Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Vector_CrossProduct(Vector1.Values, Vector2.Values);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        public static Vector CrossProduct(double[] Vector1, Vector Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values =ArrayTools.Vector_CrossProduct(Vector1, Vector2.Values);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        public static Vector CrossProduct(Vector Vector1, double[] Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Vector_CrossProduct(Vector1.Values, Vector2);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }
        public static Vector CrossProduct(double[] Vector1, double[] Vector2)
        {
            Vector Vector3 = new Vector();
            Vector3.Values = ArrayTools.Vector_CrossProduct(Vector1, Vector2);
            Vector3.Rows = Vector3.Values.Length;
            return Vector3;
        }

        /// <summary>
        /// Calculate triple scalar product of vectors (only for 3D vectors): u . (v x w) 
        /// </summary>
        /// <param name="Vector1"></param>
        /// <param name="Vector2"></param>
        /// <param name="Vector3"></param>
        /// <returns></returns>
        public static double TripleScalarProduct(Vector Vector1, Vector Vector2, Vector Vector3)
        {
            // u . (v x w)
            Vector Vector4 = new Vector();
            return ArrayTools.Vector_DotProduct(Vector1.Values, ArrayTools.Vector_CrossProduct(Vector2.Values, Vector3.Values));
        }
        public static double TripleScalarProduct(double[] Vector1, Vector Vector2, Vector Vector3)
        {
            // u . (v x w)
            Vector Vector4 = new Vector();
            return ArrayTools.Vector_DotProduct(Vector1, ArrayTools.Vector_CrossProduct(Vector2.Values, Vector3.Values));
        }
        public static double TripleScalarProduct(Vector Vector1, double[] Vector2, Vector Vector3)
        {
            // u . (v x w)
            Vector Vector4 = new Vector();
            return ArrayTools.Vector_DotProduct(Vector1.Values, ArrayTools.Vector_CrossProduct(Vector2, Vector3.Values));
        }
        public static double TripleScalarProduct(Vector Vector1, Vector Vector2, double[] Vector3)
        {
            // u . (v x w)
            Vector Vector4 = new Vector();
            return ArrayTools.Vector_DotProduct(Vector1.Values, ArrayTools.Vector_CrossProduct(Vector2.Values, Vector3));
        }
        public static double TripleScalarProduct(double[] Vector1, double[] Vector2, Vector Vector3)
        {
            // u . (v x w)
            Vector Vector4 = new Vector();
            return ArrayTools.Vector_DotProduct(Vector1, ArrayTools.Vector_CrossProduct(Vector2, Vector3.Values));
        }
        public static double TripleScalarProduct(Vector Vector1, double[] Vector2, double[] Vector3)
        {
            // u . (v x w)
            Vector Vector4 = new Vector();
            return ArrayTools.Vector_DotProduct(Vector1.Values, ArrayTools.Vector_CrossProduct(Vector2, Vector3));
        }
        public static double TripleScalarProduct(double[] Vector1, Vector Vector2, double[] Vector3)
        {
            // u . (v x w)
            Vector Vector4 = new Vector();
            return ArrayTools.Vector_DotProduct(Vector1, ArrayTools.Vector_CrossProduct(Vector2.Values, Vector3));
        }
        public static double TripleScalarProduct(double[] Vector1, double[] Vector2, double[] Vector3)
        {
            // u . (v x w)
            Vector Vector4 = new Vector();
            return ArrayTools.Vector_DotProduct(Vector1, ArrayTools.Vector_CrossProduct(Vector2, Vector3));
        }
        /// <summary>
        /// Calculate triple vector product of three vectors and return a Vector (only for 3D vectors): u x (v x w)
        /// </summary>
        /// <param name="Vector1"></param>
        /// <param name="Vector2"></param>
        /// <param name="Vector3"></param>
        /// <returns></returns>
        public static Vector TripleVectorProduct(Vector Vector1, Vector Vector2, Vector Vector3)
        {
            // u x (v x w)
            Vector Vector4 = new Vector();
            Vector4.Values = ArrayTools.Vector_CrossProduct(Vector1.Values, ArrayTools.Vector_CrossProduct(Vector2.Values, Vector3.Values));
            Vector4.Rows = Vector4.Values.Length;
            return Vector4;
        }
        public static Vector TripleVectorProduct(double[] Vector1, Vector Vector2, Vector Vector3)
        {
            // u x (v x w)
            Vector Vector4 = new Vector();
            Vector4.Values = ArrayTools.Vector_CrossProduct(Vector1, ArrayTools.Vector_CrossProduct(Vector2.Values, Vector3.Values));
            Vector4.Rows = Vector4.Values.Length;
            return Vector4;
        }
        public static Vector TripleVectorProduct(Vector Vector1, double[] Vector2, Vector Vector3)
        {
            // u x (v x w)
            Vector Vector4 = new Vector();
            Vector4.Values = ArrayTools.Vector_CrossProduct(Vector1.Values, ArrayTools.Vector_CrossProduct(Vector2, Vector3.Values));
            Vector4.Rows = Vector4.Values.Length;
            return Vector4;
        }
        public static Vector TripleVectorProduct(Vector Vector1, Vector Vector2, double[] Vector3)
        {
            // u x (v x w)
            Vector Vector4 = new Vector();
            Vector4.Values = ArrayTools.Vector_CrossProduct(Vector1.Values, ArrayTools.Vector_CrossProduct(Vector2.Values, Vector3));
            Vector4.Rows = Vector4.Values.Length;
            return Vector4;
        }
        public static Vector TripleVectorProduct(double[] Vector1, double[] Vector2, Vector Vector3)
        {
            // u x (v x w)
            Vector Vector4 = new Vector();
            Vector4.Values = ArrayTools.Vector_CrossProduct(Vector1, ArrayTools.Vector_CrossProduct(Vector2, Vector3.Values));
            Vector4.Rows = Vector4.Values.Length;
            return Vector4;
        }
        public static Vector TripleVectorProduct(Vector Vector1, double[] Vector2, double[] Vector3)
        {
            // u x (v x w)
            Vector Vector4 = new Vector();
            Vector4.Values = ArrayTools.Vector_CrossProduct(Vector1.Values, ArrayTools.Vector_CrossProduct(Vector2, Vector3));
            Vector4.Rows = Vector4.Values.Length;
            return Vector4;
        }
        public static Vector TripleVectorProduct(double[] Vector1, Vector Vector2, double[] Vector3)
        {
            // u x (v x w)
            Vector Vector4 = new Vector();
            Vector4.Values = ArrayTools.Vector_CrossProduct(Vector1, ArrayTools.Vector_CrossProduct(Vector2.Values, Vector3));
            Vector4.Rows = Vector4.Values.Length;
            return Vector4;
        }
        public static Vector TripleVectorProduct(double[] Vector1, double[] Vector2, double[] Vector3)
        {
            // u x (v x w)
            Vector Vector4 = new Vector();
            Vector4.Values = ArrayTools.Vector_CrossProduct(Vector1, ArrayTools.Vector_CrossProduct(Vector2, Vector3));
            Vector4.Rows = Vector4.Values.Length;
            return Vector4;
        }

        #endregion

        #region Methods to initialize vector
        /// <summary>
        /// Make all components equal to 1
        /// </summary>
        public void SetComponentsEqualToUnity()
        {
            Values = SetComponentsEqualToUnity(Values);
        }
        public static double[] SetComponentsEqualToUnity(double[] u)
        {
            int lu = u.Length;
            double[] w = new double[lu];
            for (int i = 0; i < lu; i++)
            {
                w[i] = 1.0d;
            }
            return w;
        }

            
        #endregion 
        
        #region Methods to rotate vector
        /// <summary>
        /// Rotate this vector using RotationMatrix (only for 3D vectors): x = Ax
        /// </summary>
        /// <param name="RotationMatrix"></param>
        public void Rotate(Matrix RotationMatrix)
        {
            Values = (RotationMatrix * this).Values;
        }
        public void Rotate(Matrix_Jagged RotationMatrix)
        {
            Values = (RotationMatrix * this).Values;
        }
        /// <summary>
        /// Rotate a vector using RotationMatrix and return the rotated vector (only for 3D vectors): y = Ax
        /// </summary>
        /// <param name="RotationMatrix"></param>
        /// <param name="U"></param>
        /// <returns></returns>
        public Vector RotateVector(Matrix RotationMatrix, Vector U)
        {
            return RotationMatrix * U;
        }
        public Vector RotateVector(Matrix RotationMatrix)
        {
            return RotateVector(RotationMatrix, this);
        }
        public Vector RotateVector(Vector VectorToRotate, double AngleOfRotation_Rad, Vector UnitVectorToRotateAbout)
        {
            Vector NewVector = new Vector();
            NewVector.Values = ArrayTools.RotationVectorAboutAxis(VectorToRotate.Values, AngleOfRotation_Rad, UnitVectorToRotateAbout.Values);
            return NewVector;
        }
        public Vector RotateVector(Matrix_Jagged RotationMatrix, Vector U)
        {
            return RotationMatrix * U;
        }
        public Vector RotateVector(Matrix_Jagged RotationMatrix)
        {
            return RotateVector(RotationMatrix, this);
        }
        
        /// <summary>
        /// Get rotation matrix for a rotation of given ange around given axis (only for 3D vectors)
        /// </summary>
        /// <param name="AxisOfRotation"></param>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static Matrix RotationMatrixForRotationAboutAxis(Vector AxisOfRotation, double AngleOfRotation_Rad)
        {
            return RotationMatrixForRotationAboutAxis(AxisOfRotation.Values, AngleOfRotation_Rad);
        }
        public static Matrix RotationMatrixForRotationAboutAxis(double[] AxisOfRotation, double AngleOfRotation_Rad)
        {
            Matrix Q = new Matrix();
            Q.Values = ArrayTools.RotationMatrixAboutAxis(AngleOfRotation_Rad, AxisOfRotation);
            return Q;
        }
        /// <summary>
        /// Get rotation matrix for a rotation of given ange around given axis (only for 3D vectors)
        /// </summary>
        /// <param name="AxisOfRotation"></param>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static Matrix_Jagged RotationMatrixForRotationAboutAxis_Jagged(Vector AxisOfRotation, double AngleOfRotation_Rad)
        {
            Matrix_Jagged NewMatrix = new Matrix_Jagged();
            NewMatrix.Values = ArrayTools.RotationMatrixAboutAxis_Jagged(AngleOfRotation_Rad, AxisOfRotation.Values);
            return NewMatrix;
        }
        public static Matrix_Jagged RotationMatrixForRotationAboutAxis_Jagged(double[] AxisOfRotation, double AngleOfRotation_Rad)
        {
            Matrix_Jagged Q = new Matrix_Jagged();
            Q.Values = ArrayTools.RotationMatrixAboutAxis_Jagged(AngleOfRotation_Rad, AxisOfRotation);
            return Q;
        }
        /// <summary>
        /// Convert radians to degrees
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static double RadToDeg(double AngleRad)
        {
            return RadToDeg(AngleRad);
        }
        /// <summary>
        /// Convert degrees to radians
        /// </summary>
        /// <param name="AngleDeg"></param>
        /// <returns></returns>
        public static double DegToRad(double AngleDeg)
        {
            return DegToRad(AngleDeg);
        }
        /// <summary>
        /// Rotation matrix for rotation about 0-th axis (only for 3D vectors)
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static Matrix RotationAbout0Axis(double AngleRad)
        {
            return RotationAbout0Axis(AngleRad);
        }
        /// <summary>
        /// Rotation matrix for rotation about 1-th axis (only for 3D vectors)
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static Matrix RotationAbout1Axis(double AngleRad)
        {
            return RotationAbout1Axis(AngleRad);
        }
        /// <summary>
        /// Rotation matrix for rotation about 2-th axis (only for 3D vectors)
        /// </summary>
        /// <param name="AngleRad"></param>
        /// <returns></returns>
        public static Matrix RotationAbout2Axis(double AngleRad)
        {
            return RotationAbout2Axis(AngleRad);
        }
        /// <summary>
        /// Calculate angle in radians between given vectors
        /// </summary>
        /// <param name="Vector1"></param>
        /// <param name="Vector2"></param>
        /// <returns></returns>
        public static double AngleInRadiansBetweenVectors(Vector Vector1, Vector Vector2)
        {
            // Calculates an angle between 0 and Pi between two vectors 
            double Mag1 = Vector1.Magnitude();
            double Mag2 = Vector2.Magnitude();
            double dot = DotProduct(Vector1, Vector2);
            if ((Mag1 != 0.0d) && (Mag2 != 0.0d))
            {
                return Math.Acos(dot / (Mag1 * Mag2));
            }
            else
            {
                return 0.0d;
            }
        }
        public static double AngleInRadiansBetweenVectors(double[] Vector1, Vector Vector2)
        {
            // Calculates an angle between 0 and Pi between two vectors 
            Vector V1 = new Vector(Vector1);
            double Mag1 = V1.Magnitude();
            double Mag2 = Vector2.Magnitude();
            double dot = DotProduct(V1, Vector2);
            if ((Mag1 != 0.0d) && (Mag2 != 0.0d))
            {
                return Math.Acos(dot / (Mag1 * Mag2));
            }
            else
            {
                return 0.0d;
            }
        }
        public static double AngleInRadiansBetweenVectors(Vector Vector1, double[] Vector2)
        {
            // Calculates an angle between 0 and Pi between two vectors 
            Vector V2 = new Vector(Vector2);
            double Mag1 = Vector1.Magnitude();
            double Mag2 = V2.Magnitude();
            double dot = DotProduct(Vector1, V2);
            if ((Mag1 != 0.0d) && (Mag2 != 0.0d))
            {
                return Math.Acos(dot / (Mag1 * Mag2));
            }
            else
            {
                return 0.0d;
            }
        }
        public static double AngleInRadiansBetweenVectors(double[] Vector1, double[] Vector2)
        {
            // Calculates an angle between 0 and Pi between two vectors 
            Vector V1 = new Vector(Vector1);
            Vector V2 = new Vector(Vector2);
            double Mag1 = V1.Magnitude();
            double Mag2 = V2.Magnitude();
            double dot = DotProduct(V1, V2);
            if ((Mag1 != 0.0d) && (Mag2 != 0.0d))
            {
                return Math.Acos(dot / (Mag1 * Mag2));
            }
            else
            {
                return 0.0d;
            }
        }
        /// <summary>
        /// Calculate angle in degrees between given vectors
        /// </summary>
        /// <param name="Vector1"></param>
        /// <param name="Vector2"></param>
        /// <returns></returns>
        public static double AngleInDegreesBetweenVectors(Vector Vector1, Vector Vector2)
        {
            // Calculates an angle between 0 and Pi between two vectors 
            double angle = AngleInRadiansBetweenVectors(Vector1, Vector2);
            return RadToDeg(angle);
        }
        public static double AngleInDegreesBetweenVectors(double[] Vector1, Vector Vector2)
        {
            // Calculates an angle between 0 and Pi between two vectors 
            double angle = AngleInRadiansBetweenVectors(Vector1, Vector2);
            return RadToDeg(angle);
        }
        public static double AngleInDegreesBetweenVectors(Vector Vector1, double[] Vector2)
        {
            // Calculates an angle between 0 and Pi between two vectors 
            double angle = AngleInRadiansBetweenVectors(Vector1, Vector2);
            return RadToDeg(angle);
        }
        public static double AngleInDegreesBetweenVectors(double[] Vector1, double[] Vector2)
        {
            // Calculates an angle between 0 and Pi between two vectors 
            double angle = AngleInRadiansBetweenVectors(Vector1, Vector2);
            return RadToDeg(angle);
        }
        /// <summary>
        /// Calculates an angle between 0 and 2Pi depending if the rotation from the first to second vector is counter clockwise around on given axis of rotation
        /// </summary>
        /// <param name="Vector1"></param>
        /// <param name="Vector2"></param>
        /// <param name="AxisOfRotationVector"></param>
        /// <returns></returns>
        public static double AngleBetweenVectors(Vector Vector1, Vector Vector2, Vector AxisOfRotationVector)
        {
            // Calculates an angle between 0 and 2Pi between two vectors using AxisOfRotationVector to figure out if cross product is along or against this axis
            double Mag1 = Vector1.Magnitude();
            double Mag2 = Vector2.Magnitude();
            double dot = DotProduct(Vector1, Vector2);
            Vector CrossProductOfV1AndV2 = CrossProduct(Vector1, Vector2);
            double direction = DotProduct(AxisOfRotationVector, CrossProductOfV1AndV2);
            if ((Mag1 != 0.0d) && (Mag2 != 0.0d))
            {
                double angle = Math.Acos(dot / (Mag1 * Mag2));
                if (direction < 0.0d) angle = 2.0d * Math.PI - angle;
                return angle;
            }
            else
            {
                return 0.0d;
            }
        }
        
        #endregion

        public override string ToString()
        {
            string output = "";
            output += "Number of rows: " + this.Rows.ToString() + "\n";
            for (int i = 0; i < this.Rows; i++)
            {
                    output += "  Row " + i.ToString() + ": " + this.Values[i].ToString() + "\n";
            }

            return output;
        }
    }
}
