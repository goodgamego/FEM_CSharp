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

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class FEMSolver_ND_Static : FEMSolver_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 12/25/2012
        ///
        /// Purpose: Root class for static N-D solvers (KU=F)
        /// Comments:
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public Element_ND[] Elements;
        public BoundaryElement_ND_Static[] BoundaryElements;
        public Vector U; //Solution
        public FEMSolver_ND_Static()
        {
        }
        public override void SolveFEMSystem()
        {
            //Set up the load vector and stiffness matrix
            int NDF = Unknowns_NDoF; //Get the size of the unknowns temperature (one per node)
            Vector F = new Vector(NDF); //Make global load vector
            Matrix_Jagged K = new Matrix_Jagged(NDF, NDF); // Make global stiffness matrix

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

            //Solve system
            U = K.SolveLinearSystem(F);
        }
        private Object this_F_Lock = new Object();
        private Object this_K_Lock = new Object();
        public override void SolveFEMSystem_Parallel()
        {
            //Set up the load vector and stiffness matrix
            int NDF = Unknowns_NDoF; //Get the size of the unknowns temperature (one per node)
            Vector F = new Vector(NDF); //Make global load vector
            Matrix_Jagged K = new Matrix_Jagged(NDF, NDF); // Make global stiffness matrix

            //Assemble F and K
            int NE = Elements.Length; //Get the number of elements

            ParallelOptions op = new ParallelOptions();
            op.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Parallel.For(0, NE, op, i =>
            {
                Vector Fe = Elements[i].Calculate_Fe();
                Matrix_Jagged Ke = Elements[i].Calculate_Ke();
                lock (this_K_Lock) { Elements[i].Assemble(ref K, Ke); }
                lock (this_F_Lock) { Elements[i].Assemble(ref F, Fe); }
            });
            //Adjust for boundary conditions
            int NBE = BoundaryElements.Length;
            for (int i = 0; i < NBE; i++)
            {
                BoundaryElements[i].AdjustFAndK(ref F, ref K);
            }

            //Solve system
            U = K.SolveLinearSystem(F);
        }
        public override void LoadFromFile(string FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            FEMSolver_ND_Static TheSolver = (FEMSolver_ND_Static)formatter.Deserialize(stream);

            LoadFrom_Solver_ND_Static(TheSolver);

            stream.Close();
        }
        protected void LoadFrom_Solver_ND_Static(FEMSolver_ND_Static TheSolver)
        {
            LoadFrom_Solver_ND(TheSolver);

            this.Elements = TheSolver.Elements;
            this.BoundaryElements = TheSolver.BoundaryElements;
            this.U = TheSolver.U;
        }
    }
}
