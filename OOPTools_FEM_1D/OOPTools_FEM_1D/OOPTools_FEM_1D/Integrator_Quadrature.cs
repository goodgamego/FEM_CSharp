using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OOPTools_Math;

namespace OOPTools_FEM_1D
{
    public class Integrator_Quadrature
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 10/7/2012
        ///
        /// Purpose:    Conduct quadrature on scalar, vector, or matrix functions (argument of integral)
        /// Comments:   Need to designate function as delegate
        ///             Function only takes the quadrature point as a scalar argument
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public QuadratureRule QRule;
        public delegate double Function_Scalar(double Xi);
        public delegate Vector Function_Vector(double Xi);
        public delegate Matrix Function_Matrix(double Xi);
        public Integrator_Quadrature()
        {
        }
        public Integrator_Quadrature(QuadratureRule qRule)
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
    }
}
