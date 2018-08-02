using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOPTools_FEM_ND;
using OOPTools_Math;

namespace ND_Elastic
{
    public class Element_ND_Elastic : Element_ND_VectorField
    {
        public Function_Scalar_X rho; //density
        public Function_Vector_X b;
        public double langmuda;
        public double miu;
        public static OutputTypes SelectedOutputType;
        public override Matrix_Jagged IntegralArgument_Ke(Vector Xi)
        {
            int NNPE = ElementNodes.Length;
            double Det_Jac;
            Vector X;
            Matrix_Jagged DNDX;
            Matrix_Jagged Ke_Arg = new Matrix_Jagged(NNPE * 2, NNPE * 2);
            Interpolator.Calculate_X_DNDX_DetJacobian(Xi, ElementNodes, out X, out DNDX, out Det_Jac);
            for (int p = 0; p < NNPE; p++)//p defines node number
            {
                Node_ND_Unknowns_VectorField Unknown_p = (Node_ND_Unknowns_VectorField)ElementNodes[p].Unknowns;
                int NDFp = Unknown_p.UnknownDoFs.Length;
                for (int i = 0; i < NDFp; i++) //loop at x direction
                {
                    for (int q = 0; q < NNPE; q++)//
                    {
                        Node_ND_Unknowns_VectorField Unknown_q = (Node_ND_Unknowns_VectorField)ElementNodes[q].Unknowns;
                        int NDFq = Unknown_q.UnknownDoFs.Length;
                        for (int j = 0; j < NDFq; j++)//loop at y direction 
                        {
                            double temp = 0;
                            for (int m = 0; m < 2; m++)//loop at x direction 
                                for (int n = 0; n < 2; n++)//loop at y direction 
                                {
                                    double Eimjn = 0;
                                    Eimjn = GetE(i, m, j, n);//obtain constitutive matrix
                                    temp += DNDX.Values[p][m] * Eimjn * DNDX.Values[q][n]; 
                                }
                            Ke_Arg.Values[NDFp * p + i][NDFq * q + j] = temp * Det_Jac; //store Ke                           
                        }
                    }
                }

            }
            return Ke_Arg;
        }
        public override OOPTools_Math.Vector IntegralArgument_Fe(OOPTools_Math.Vector Xi)
        {
            int NNPE = ElementNodes.Length;
            double Det_Jac;
            Vector X;
            Vector N;
            Vector B = new Vector(2);
            Interpolator.Calculate_X_N_DetJacobian(Xi, ElementNodes, out X, out N, out Det_Jac);
            B = b(X);
            Vector F_Arg = new Vector(NNPE * 2);
            for (int i = 0; i < NNPE; i++)
            {
                F_Arg.Values[2 * i] = N.Values[i] * B.Values[0]*rho(X);//nodes at x direction
                F_Arg.Values[2 * i + 1] = N.Values[i] * B.Values[1] * rho(X);//nodes at y direction
            }
            return F_Arg;
        }
        public double GetE(int i, int j, int k, int l)
        {
            int[] sign = new int[] { 0, 0, 0, 0, 0, 0 };//difine which class to be applied 
            if (i == j)
                sign[0] = 1;
            if (k == l)
                sign[1] = 1;
            if (i == k)
                sign[2] = 1;
            if (j == l)
                sign[3] = 1;
            if (i == l)
                sign[4] = 1;
            if (j == k)
                sign[5] = 1;
            double E = 0.0D;
            E = langmuda * sign[0] * sign[1] + miu * (sign[2] * sign[3] + sign[4] * sign[5]);
            return E;
        }
        public enum OutputTypes
        {
            Displacement_x, Displacement_y
        }
        public override void Set_ElementNodal_DisplayValuesFromUnknowns()
        {
            switch (SelectedOutputType)
            {
                case OutputTypes.Displacement_x:
                    Set_ElementNodal_DisplayValues_Displacement_x();
                    break;
                case OutputTypes.Displacement_y:
                    Set_ElementNodal_DisplayValues_Displacement_y();
                    break;
            }
        }
        private void Set_ElementNodal_DisplayValues_Displacement_x()
        {
            int NNPE = ElementNodes.Length;
            for (int i = 0; i < NNPE; i++)
            {
                Node_ND_Unknowns_VectorField Unknown_i = (Node_ND_Unknowns_VectorField)ElementNodes[i].Unknowns;
                ElementNodes[i].Unknowns.DisplayValue = Unknown_i.Unknowns.Values[0];
            }
        }
        private void Set_ElementNodal_DisplayValues_Displacement_y()
        {
            int NNPE = ElementNodes.Length;
            for (int i = 0; i < NNPE; i++)
            {
                Node_ND_Unknowns_VectorField Unknown_i = (Node_ND_Unknowns_VectorField)ElementNodes[i].Unknowns;
                ElementNodes[i].Unknowns.DisplayValue = Unknown_i.Unknowns.Values[1];
            }
        }


    }
}
