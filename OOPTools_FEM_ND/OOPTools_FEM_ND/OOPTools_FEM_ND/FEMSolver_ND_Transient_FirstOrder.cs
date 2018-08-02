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
    public class FEMSolver_ND_Transient_FirstOrder : FEMSolver_ND_Transient
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 12/26/2012
        ///
        /// Purpose: Root class for first order transient N-D solvers (C dU/dt + K U = F)
        /// Comments:
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public Vector U_o; //Initial conditions of temperature at the nodes
        public void Initialize_Solver_ND_Transient_FirstOrder(double TheDTime, int TheNumberOfStepsToStore, int TheNumberOfStepsToSkipOutput,  Vector TheU_o, string OutputDirectory)
        {
            Initialize_Solver_ND_Transient(TheDTime, TheNumberOfStepsToStore, TheNumberOfStepsToSkipOutput, OutputDirectory);
            
            U_o = TheU_o;
            Ut[0] = new Vector(U_o); 
        }
    }
}
