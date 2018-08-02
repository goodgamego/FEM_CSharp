using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOPTools_Math;
using OOPTools_FEM_ND;

namespace ND_Elastic
{
    public class BoundaryElement_ND_Elastic_Line_SteadyState_DisplacementOnNode : BoundaryElement_ND_Static
    {
        public Matrix_Jagged Displacements; //Nodal values of Displacements
        public override void AdjustFAndK(ref Vector F, ref Matrix_Jagged K)
        {
            int NNPE = ElementNodes.Length;
            for (int i = 0; i < NNPE; i++)
            {
                Node_ND_Unknowns_VectorField Unknowns_i = (Node_ND_Unknowns_VectorField)ElementNodes[i].Unknowns;
                int[] Ip = Unknowns_i.UnknownDoFs; //position of unkonw in globle matrix
                F.Values[Ip[0]] = 1.0E20 * Displacements.Values[i][0];//penalty for Uix
                K.Values[Ip[0]][Ip[0]] = 1.0E20;
                F.Values[Ip[1]] = 1.0E20 * Displacements.Values[i][1];//penalty for Uiy
                K.Values[Ip[1]][Ip[1]] = 1.0E20;
            }
        }

    }
    public class BoundaryElement_ND_Elastic_Line_SteadyState_ForceOnNode : BoundaryElement_ND_Static
    {
        public Vector ForceX; //Force at X-axis
        public Vector ForceY; //Force at Y-axis
        public override void AdjustFAndK(ref Vector F, ref Matrix_Jagged K)
        {
            CalculateAndAssemble_Fe(ref F);
        }
        public override Vector IntegralArgument_Fe(Vector Xi)//override Fe
        {
            int NNPE = ElementNodes.Length;
            Vector N;
            Vector Temp1 = new Vector(NNPE);
            Vector Temp2 = new Vector(NNPE);
            double DsDXi = Calculate_NDLine_DsDXi(Xi, ElementNodes, out N);
            double t_x = Interpolator.Interpolate_Variable(ForceX.Values, N);
            double t_y = Interpolator.Interpolate_Variable(ForceY.Values, N);
            Temp1 = (t_x * DsDXi) * N;
            Temp2 = (t_y * DsDXi) * N;
            Vector BigFe=new Vector(NNPE*2);
            for (int i = 0; i < NNPE; i++)
            {
                BigFe.Values[i * 2] = Temp1.Values[i];
                BigFe.Values[i * 2+1] = Temp2.Values[i];
            }
            return BigFe;          
        }
        public override void Assemble(ref Vector F, Vector Fe)//override assemblage
        {
            int Ip;
            int NNPE = ElementNodes.Length;
            for (int p = 0; p < NNPE; p++)
            {
                Node_ND_Unknowns_VectorField Unknown_p = (Node_ND_Unknowns_VectorField)ElementNodes[p].Unknowns;
                int NDFp = Unknown_p.UnknownDoFs.Length;
                for (int i = 0; i < NDFp; i++)
                {
                    Ip = Unknown_p.UnknownDoFs[i];
                    F.Values[Ip] += Fe.Values[NDFp * p + i];
                }
            }
        }
    }
}
