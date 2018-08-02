using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class Integrator_ND_Quadrature
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 10/7/2012
        ///
        /// Purpose:    Conduct quadrature on scalar, vector, or matrix functions (argument of integral)
        /// Comments:   Need to designate function as delegate
        ///             Function only takes the quadrature point as a vector argument
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public QuadratureRule_ND QRule;
        public delegate double Function_Scalar(Vector Xi);
        public delegate Vector Function_Vector(Vector Xi);
        public delegate Matrix Function_Matrix(Vector Xi);
        public delegate Matrix_Jagged Function_Matrix_Jagged(Vector Xi);
        public Integrator_ND_Quadrature()
        {
        }
        public Integrator_ND_Quadrature(QuadratureRule_ND qRule)
        {
            QRule = qRule;
        }
        public virtual double IntegrateFunction(Function_Scalar Func)
        {
            double integral = QRule.wi[0] * Func(QRule.Xi[0]);
            for (int i = 1; i <QRule.NIP; i++)
            {
                integral += QRule.wi[i] * Func(QRule.Xi[i]);
            }
            return integral;
        }
        public virtual Vector IntegrateFunction(Function_Vector Func)
        {
            Vector integral = QRule.wi[0] * Func(QRule.Xi[0]);
            for (int i = 1; i < QRule.NIP; i++)
            {
                integral += QRule.wi[i] * Func(QRule.Xi[i]);
            }
            return integral;
        }
        public virtual Matrix IntegrateFunction(Function_Matrix Func)
        {
            Matrix integral = QRule.wi[0] * Func(QRule.Xi[0]);
            for (int i = 1; i < QRule.NIP; i++)
            {
                integral += QRule.wi[i] * Func(QRule.Xi[i]);
            }
            return integral;
        }
        public virtual Matrix_Jagged IntegrateFunction(Function_Matrix_Jagged Func)
        {
            Matrix_Jagged integral = QRule.wi[0] * Func(QRule.Xi[0]);
            for (int i = 1; i < QRule.NIP; i++)
            {
                integral += QRule.wi[i] * Func(QRule.Xi[i]);
            }
            return integral;
        }
    }
}
