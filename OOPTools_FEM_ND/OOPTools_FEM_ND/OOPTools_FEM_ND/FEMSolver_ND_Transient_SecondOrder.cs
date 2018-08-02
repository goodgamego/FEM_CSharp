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
    public class FEMSolver_ND_Transient_SecondOrder : FEMSolver_ND_Transient_FirstOrder
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 12/27/2012
        ///
        /// Purpose: Root class for second order transient N-D solvers (M d^2U/dt^2 + C dU/dt + K U = F)
        /// Comments:
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public Vector V_o; //Initial velocity (dU/dt)
        public Vector[] Vt; // V=dU/dt
        public FEMSolver_ND_Transient_SecondOrder()
        {
            TheNumberOfVectorsToStorePerStep = 2;
        }
        public void InitializeFEMSolver_ND_Transient_SecondOrder(double TheDTime, int TheNumberOfStepsToStore, int TheNumberOfStepsToSkipOutput, Vector TheU_o, Vector TheV_o, string OutputDirectory)
        {
            Initialize_Solver_ND_Transient_FirstOrder(TheDTime, TheNumberOfStepsToStore, TheNumberOfStepsToSkipOutput, TheU_o, OutputDirectory);

            V_o = TheV_o;
            Vt[0] = new Vector(V_o);
        }
    }
}
