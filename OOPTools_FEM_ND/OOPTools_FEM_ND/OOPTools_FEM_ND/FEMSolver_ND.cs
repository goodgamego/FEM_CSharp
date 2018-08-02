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
    public class FEMSolver_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 11/12/2012
        ///
        /// Purpose: Root class for N-D solvers
        /// Comments:
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public int Unknowns_NDoF; //Number of (scalar) unknowns
        public Node_ND[] Nodes; //The nodes in the problem
        public Sensor_ND[] Sensors; //Value at specific points defined by the sensor
        public StopWatchTimer Timer_Assemble, Timer_Solve;
        public FEMSolver_ND()
        {
        }
        public void InitializeSolver()
        {
        }
        public virtual void SolveFEMSystem()
        {
            //user needs to add this for the method used
        }
        public static void Assemble_Parallel(int NE, Element_ND[] Elements, out Vector[] Fes, out Matrix_Jagged[] Kes)
        {
            Vector[] Fe = new Vector[NE];
            Matrix_Jagged[] Ke = new Matrix_Jagged[NE];

            ParallelOptions op = new ParallelOptions();
            op.MaxDegreeOfParallelism = Environment.ProcessorCount-1;
            

            Parallel.For(0, NE, op, i =>
            {
                Fe[i] = Elements[i].Calculate_Fe();
                Ke[i] = Elements[i].Calculate_Ke();
            });
            Fes = Fe;
            Kes = Ke;  
        }
        public virtual void SolveFEMSystem_Parallel()
        {
            //user needs to add this for the method used
        }
        public Vector[] Get_Nodal_X()
        {
            int NN = Nodes.Length;
            Vector[] X = new Vector[NN];
            for (int i = 0; i < NN; i++)
            {
                X[i] = Nodes[i].X;
            }
            return X;
        }
        public override string ToString()
        {
            int NS = Sensors.Length;
            string OutputString = "";
            for (int i = 0; i < NS; i++)
            {
                OutputString += Sensors[i].ToString();
            }
            return OutputString;
        }
        public virtual void SaveToFile(string FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);

            stream.Close();

        }
        public virtual void LoadFromFile(string FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            FEMSolver_ND TheSolver = (FEMSolver_ND)formatter.Deserialize(stream);
            LoadFrom_Solver_ND(TheSolver);

            stream.Close();
        }
        protected void LoadFrom_Solver_ND(FEMSolver_ND TheSolver)
        {
            this.Unknowns_NDoF = TheSolver.Unknowns_NDoF;
            this.Nodes = TheSolver.Nodes;
            this.Sensors = TheSolver.Sensors;
        }
    }
}
