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
    public class FEMSolver_ND_Transient : FEMSolver_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 12/25/2012
        ///
        /// Purpose: Root class for transient N-D solvers (e.g., C dUdt + K U = F)
        /// Comments:
        ///
        /// Date modified:
        /// Modified by:
        /// Comments:
        /// </summary>
        public double DTime; //Time step size
        public int StartIndex;
        public int NumberOfStepsToStore; //Number of time increments to store
        public int NumberOfStepsToSkipOutput; //Number of time steps to skip before storing 
        public int TheNumberOfVectorsToStorePerStep; //Ouput per step
        public Vector Times; //Times associated with output
        public Vector[] Ut; //Output vectors
        public Element_ND[] Elements;
        public BoundaryElement_ND_Transient[] BoundaryElements;
        public FEMSolverOutput_Transient_SolverInfo Solver_Output_Info;
        public bool StoreOutput;
        public FEMSolver_ND_Transient()
        {
            TheNumberOfVectorsToStorePerStep = 1;
        }
        public FEMSolver_ND_Transient(double TheDTime, int TheNumberOfStepsToStore, int TheNumberOfStepsToSkipOutput, string OutputDirectory)
        {
            Initialize_Solver_ND_Transient(TheDTime, TheNumberOfStepsToStore, TheNumberOfStepsToSkipOutput, OutputDirectory);
        }
        public void Initialize_Solver_ND_Transient(double TheDTime, int TheNumberOfStepsToStore, int TheNumberOfStepsToSkipOutput, string OutputDirectory)
        {
            DTime = TheDTime;
            NumberOfStepsToStore = TheNumberOfStepsToStore;
            NumberOfStepsToSkipOutput = TheNumberOfStepsToSkipOutput;

            StartIndex = 0;

            Times = new Vector(NumberOfStepsToStore + 1);
            Ut = new Vector[NumberOfStepsToStore + 1];

            Solver_Output_Info = new FEMSolverOutput_Transient_SolverInfo(NumberOfStepsToStore, TheNumberOfVectorsToStorePerStep, OutputDirectory);
        }
        public virtual void SolveFEMSystem(int TheStartIndex)
        {
            StartIndex = TheStartIndex;
            SolveFEMSystem();
        }
        public override void LoadFromFile(string FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            FEMSolver_ND_Transient TheSolver = (FEMSolver_ND_Transient)formatter.Deserialize(stream);

            LoadFrom_Solver_ND_Static(TheSolver);

            stream.Close();
        }
        protected void LoadFrom_Solver_ND_Static(FEMSolver_ND_Transient TheSolver)
        {
            LoadFrom_Solver_ND(TheSolver);

            this.DTime = TheSolver.DTime;
            this.NumberOfStepsToStore = TheSolver.NumberOfStepsToStore;
            this.NumberOfStepsToSkipOutput = TheSolver.NumberOfStepsToSkipOutput;
            this.Times = TheSolver.Times;
            this.Ut = TheSolver.Ut;

            this.Elements = TheSolver.Elements;
            this.BoundaryElements = TheSolver.BoundaryElements;
        }
        public void StoreSensors(int Index)
        {
            int NS = Sensors.Length;
            for (int i = 0; i < NS; i++)
            {
                Sensors[i].GetSensorValue(Index);
            }
        }
    }

    [Serializable]
    public class SolverOutput_Values_AStep
    {
        public int Step_Index;
        public double Step_Time;
        public int Step_NumberOfOutput;
        public Vector[] Step_Us; //Output for step values (0 = U, 1 = V, ...)
        public string OutputAddress;
        public virtual void SaveToFile(string FileName)
        {
            OutputAddress = FileName;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);

            stream.Close();
        }
        public virtual void LoadFromFile(string FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            SolverOutput_Values_AStep TheStep = (SolverOutput_Values_AStep)formatter.Deserialize(stream);
            LoadFrom_SolverOutput_Values_AStep(TheStep);

            stream.Close();
        }
        protected void LoadFrom_SolverOutput_Values_AStep(SolverOutput_Values_AStep TheStep)
        {
            this.Step_Index = TheStep.Step_Index;
            this.Step_Time = TheStep.Step_Time;
            this.Step_Us = TheStep.Step_Us;
            this.OutputAddress = TheStep.OutputAddress;
        }
    }

    [Serializable]
    public class FEMSolverOutput_Transient_SolverInfo
    {
        public int Output_NumberOfOutputs;
        public Vector Output_DTimes; //Time step size
        public int Output_NumberOfOutputVectors;
        public int[] Output_NumberOfStepsToSkip;
        public string[] Output_Step_FileName;
        public bool[] Output_Step_StepNotStored;
        public int Output_Step_LastIndex;
        public string OutputAddress;
        public string Output_SolutionName;
        public string Output_DirectoryAddess;
        public FEMSolverOutput_Transient_SolverInfo(int TheNumberOfOutputs, int TheNumberOfOutputVectors, string OutputDirectory)
        {
            Initialize_Transient_SolverInfo(TheNumberOfOutputs, TheNumberOfOutputVectors, OutputDirectory);
        }
        public void Initialize_Transient_SolverInfo(int TheNumberOfOutputs, int TheNumberOfOutputVectors, string OutputDirectory)
        {
            Output_NumberOfOutputs = TheNumberOfOutputs;
            Output_NumberOfOutputVectors = TheNumberOfOutputVectors;
            Output_DTimes = new Vector(Output_NumberOfOutputs+1);
            Output_NumberOfStepsToSkip = new int[Output_NumberOfOutputs + 1];
            Output_Step_FileName = new string[Output_NumberOfOutputs + 1];
            Output_Step_StepNotStored = new bool[Output_NumberOfOutputs + 1];
            Output_Step_LastIndex = 0;
            Output_SolutionName = "OOPs_TransientSolution";
            Output_DirectoryAddess = OutputDirectory;
            OutputAddress = Make_OuptutAddress();
        }
        public void StoreAStep(int Index, double Time, Vector[] Us, double DTime, int Output_NumberOfStepsToSkip)
        {
            SolverOutput_Values_AStep TheStep = new SolverOutput_Values_AStep();

            TheStep.Step_Index = Index;
            TheStep.Step_Us = Us;
            TheStep.Step_Time = Time;
            TheStep.OutputAddress = Make_StepAddress(Index);

            TheStep.SaveToFile(Make_StepAddress(Index));

            Output_DTimes.Values[Index] = DTime;
            Output_Step_FileName[Index] = Make_StepName(Index);
            Output_Step_StepNotStored[Index] = false;
            Output_Step_LastIndex = Index;
        }
        public void StoreAStep(int Index, double Time, Vector U, double DTime, int Output_NumberOfStepsToSkip)
        {
            Vector[] Us = new Vector[1];
            Us[0] = U;
            StoreAStep(Index, Time, Us, DTime, Output_NumberOfStepsToSkip);
        }
        public void StoreAStep(int Index, double Time, Vector U, Vector V, double DTime, int Output_NumberOfStepsToSkip)
        {
            Vector[] Us = new Vector[2];
            Us[0] = U;
            Us[1] = V;
            StoreAStep(Index, Time, Us, DTime, Output_NumberOfStepsToSkip);
        }
        public string Make_StepName(int Index)
        {
            return Output_SolutionName + "_Step_" + Index.ToString() + ".ops";
        }
        public string Make_StepAddress(int Index)
        {
            return Output_DirectoryAddess + "\\" + Make_StepName(Index);
        }
        public string Make_OuptutAddress()
        {
            return Output_DirectoryAddess + "\\" + Output_SolutionName + "_Info.opi";
        }
        public SolverOutput_Values_AStep GetAStep(int Index)
        {
            SolverOutput_Values_AStep TheStep = new SolverOutput_Values_AStep();
            TheStep.LoadFromFile(Make_StepAddress(Index));
            return TheStep;
        }
        public SolverOutput_Values_AStep GetAStep(int Index, out Vector U)
        {
            SolverOutput_Values_AStep TheStep = GetAStep(Index);
            U = TheStep.Step_Us[0];
            return TheStep;
        }
        public SolverOutput_Values_AStep GetAStep(int Index, out Vector U, out Vector V)
        {
            SolverOutput_Values_AStep TheStep = GetAStep(Index);
            U = TheStep.Step_Us[0];
            V = TheStep.Step_Us[1];
            return TheStep;
        }
        public SolverOutput_Values_AStep GetLastStep(out int Index, out Vector U)
        {
            SolverOutput_Values_AStep TheStep = GetLastStep(out Index);
            U = TheStep.Step_Us[0];
            return TheStep;
        }
        public SolverOutput_Values_AStep GetLastStep(out int Index, out Vector U, out Vector V)
        {
            SolverOutput_Values_AStep TheStep = GetLastStep(out Index);
            U = TheStep.Step_Us[0];
            V = TheStep.Step_Us[1];
            return TheStep;
        }
        public SolverOutput_Values_AStep GetLastStep(out int Index)
        {
            SolverOutput_Values_AStep TheStep = new SolverOutput_Values_AStep();
            Index = Output_Step_LastIndex;
            TheStep.LoadFromFile(Make_StepAddress(Index));
            return TheStep;
        }
        public virtual void SaveToFile(string FileName)
        {
            OutputAddress = FileName;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);

            stream.Close();
        }
        public virtual void LoadFromFile(string FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            FEMSolverOutput_Transient_SolverInfo SolverInfo = (FEMSolverOutput_Transient_SolverInfo)formatter.Deserialize(stream);
            LoadFrom_SolverOutput_Values_AStep(SolverInfo);

            stream.Close();
        }
        protected void LoadFrom_SolverOutput_Values_AStep(FEMSolverOutput_Transient_SolverInfo SolverInfo)
        {
            this.Output_DTimes = SolverInfo.Output_DTimes;
            this.Output_NumberOfStepsToSkip = SolverInfo.Output_NumberOfStepsToSkip;
            this.Output_Step_FileName = SolverInfo.Output_Step_FileName;
            this.Output_Step_StepNotStored = SolverInfo.Output_Step_StepNotStored;
            this.Output_Step_LastIndex = SolverInfo.Output_Step_LastIndex;

            this.Output_DirectoryAddess = SolverInfo.Output_DirectoryAddess;
            this.Output_SolutionName = SolverInfo.Output_SolutionName;
            this.OutputAddress = SolverInfo.OutputAddress;
        }

    }
}
