using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

using OOPTools_Math;
using OOPTools;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class FEMSolver_ND_Static_SparseMatrix : FEMSolver_ND_Static
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 12/25/2012
        ///
        /// Purpose: Root class for static sparse N-D solvers (KU=F)
        /// Comments:
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public override void SolveFEMSystem()
        {
            //Set up the load vector and stiffness matrix
            int NDF = Unknowns_NDoF; //Get the size of the unknowns temperature (one per node)
            Vector F = new Vector(NDF); //Make global load vector
            MatrixSparseLinkedList K = new MatrixSparseLinkedList(NDF); // Make global stiffness matrix

            Timer_Assemble = new StopWatchTimer();
            Timer_Assemble.StartTimer();
            //Assemble F and K
            int NE = Elements.Length; //Get the number of elements
            for (int i = 0; i < NE; i++)
            {
                Elements[i].CalculateAndAssemble_FeAndKe(ref F, ref K);
            }
            //Adjust for boundary conditions
            int NBE = BoundaryElements.Length;
            for (int i = 0; i < NBE; i++)
            {
                BoundaryElements[i].AdjustFAndK(ref F, ref K);
            }
            Timer_Assemble.StopTimer();

            Timer_Solve = new StopWatchTimer();
            Timer_Solve.StartTimer();
            //Solve system
            U = K.SolveLinearSystem(F);
            Timer_Solve.StopTimer();
        }
        private Object F_Lock = new Object();
        private Object K_Lock = new Object();
        public override void SolveFEMSystem_Parallel()
        {
            //Set up the load vector and stiffness matrix
            int NDF = Unknowns_NDoF; //Get the size of the unknowns temperature (one per node)
            Vector F = new Vector(NDF); //Make global load vector
            MatrixSparseLinkedList K = new MatrixSparseLinkedList(NDF); // Make global stiffness matrix

            //Assemble F and K
            int NE = Elements.Length; //Get the number of elements

            ParallelOptions op = new ParallelOptions();
            op.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Timer_Assemble = new StopWatchTimer();
            Timer_Assemble.StartTimer();
             Parallel.For(0, NE, op, i =>
            {
                Vector Fe = Elements[i].Calculate_Fe();
                Matrix_Jagged Ke = Elements[i].Calculate_Ke();
                lock (K_Lock) { Elements[i].Assemble(ref K, Ke); }
                lock (F_Lock) { Elements[i].Assemble(ref F, Fe); }
                
            });
 
            //Adjust for boundary conditions
            int NBE = BoundaryElements.Length;
            for (int i = 0; i < NBE; i++)
            {
                BoundaryElements[i].AdjustFAndK(ref F, ref K);
            }
            Timer_Assemble.StopTimer();

            Timer_Solve = new StopWatchTimer();
            Timer_Solve.StartTimer();
            //Solve system
            U = new Vector();
            U.Values = K.SolveLinearSystem_Parallel(F.Values);
            Timer_Solve.StopTimer();
        }

    }
}
