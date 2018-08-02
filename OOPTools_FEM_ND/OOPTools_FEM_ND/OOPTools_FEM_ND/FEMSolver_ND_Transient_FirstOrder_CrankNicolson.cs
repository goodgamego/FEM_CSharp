using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class FEMSolver_ND_Transient_FirstOrder_CrankNicolson : FEMSolver_ND_Transient_FirstOrder
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 12/26/2012
        ///
        /// Purpose: CrankNicholson solver for first order transient problem (C dU/dt + K U = F)
        /// Comments:
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public FEMSolver_ND_Transient_FirstOrder_CrankNicolson()
        {
            TheNumberOfVectorsToStorePerStep = 2;
        }
        public override void SolveFEMSystem()
        {
            //Set up the load vector and stiffness matrix
            int NDF = Unknowns_NDoF; //Get the size of the unknowns temperature (one per node)
            Vector F_old = new Vector(NDF); //Make global load vector (old)
            Vector F_new = new Vector(NDF); //Make global load vector (new)
            Matrix_Jagged K = new Matrix_Jagged(NDF, NDF); // Make global stiffness matrix
            Matrix_Jagged C = new Matrix_Jagged(NDF, NDF); // Make global thermal mass matrix

            //Assemble F, K, C
            int NE = Elements.Length; //Get the number of elements
            for (int i = 0; i < NE; i++)
            {
                Elements[i].CalculateAndAssemble_FeAndKeAndCe(ref F_old, ref K, ref C);
            }
            //Adjust for boundary conditions
            int NBE = BoundaryElements.Length;
            for (int i = 0; i < NBE; i++)
            {
                BoundaryElements[i].AdjustK(Times.Values[StartIndex], ref K);
            }
            K = 0.5 * K;

            C = C / DTime;
            Matrix_Jagged C1 = C - K;
            C = C + K; // Calculate Chat and store in C
            //Adjust C for boundary condition
            for (int i = 0; i < NBE; i++)
            {
                BoundaryElements[i].AdjustChat(Times.Values[StartIndex], ref C);
            }
            C.SolveLinearSystem_LUDecomp();//Run LU decomposition on C

            //Adjust F for BC
            for (int i = 0; i < NBE; i++)
            {
                BoundaryElements[i].AdjustF(Times.Values[StartIndex], ref F_old);
            }
            F_old = 0.5D * F_old;

            //Store initial time and temperature
            Vector U_old = new Vector(Ut[StartIndex]);
            Vector Fhat;
            Node_ND.Set_UnknownForNode(Nodes, U_old);
            StoreSensors(StartIndex);
            double RTime = Times.Values[StartIndex];
            for (int i = StartIndex; i < NumberOfStepsToStore; i++)
            {
                for (int j = 0; j < NumberOfStepsToSkipOutput; j++)
                {
                    RTime += DTime;//Increment to new time
                    Element_ND.Time = RTime; //Give new time to all elements
                    F_new = new Vector(NDF);
                    for (int k = 0; k < NE; k++)
                    {
                        Elements[k].CalculateAndAssemble_Fe(ref F_new); //Calculate new F
                    }
                    //Adjust new F for BC
                    for (int k = 0; k < NBE; k++)
                    {
                        BoundaryElements[k].AdjustF(RTime, ref F_new);
                    }
                    F_new = 0.5 * F_new;

                    Fhat = F_old + F_new + C1 * U_old; //Get Fhat
                    //Adust Fhat for BC
                    for (int k = 0; k < NBE; k++)
                    {
                        BoundaryElements[k].AdjustFhat(RTime, ref F_new);
                    }

                    U_old = C.SolveLinearSystem_BackSub(Fhat); //Solve for new temperatures
                    F_old = F_new; //Move new F to old F for next step
                }
                Times.Values[i + 1] = RTime;//Store time
                Ut[i + 1] = new Vector(U_old);//Store vector of node temperatures
                
                Solver_Output_Info.StoreAStep(i+1, Times.Values[i+1], Ut[i + 1], DTime,NumberOfStepsToSkipOutput);
                Solver_Output_Info.SaveToFile(Solver_Output_Info.Make_OuptutAddress());

                Node_ND.Set_UnknownForNode(Nodes, U_old);
                StoreSensors(i+1);
            }
        }
        
    }
}
