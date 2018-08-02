using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OOPTools_Math;

namespace OOPTools_FEM_ND
{
    [Serializable]
    public class Sensor_ND
    {
        /// <summary>
        /// Developed by: Mehrdad Negahban
        /// Date: 12/25/2012
        ///
        /// Purpose:    Multi-dimensional root sensor 
        /// Comments:   Xi is isoparametric location of sensor in element 
        ///             Element is the element the sensor gets its value from
        ///             Value is the value of the element
        ///
        /// Date modified:
        /// Modified by: 
        /// Comments:
        /// </summary>
        public double Sensor_Value;
        public Vector Sensor_Values;
        public Element_ND Sensor_Element;
        public Vector Sensor_Xi;
        public Vector n; //unit normal (used when a normal needs to be given)
        public Sensor_ND()
        {
        }
        public Sensor_ND(int NumberOfSteps)
        {
            Sensor_Values = new Vector(NumberOfSteps + 1);
        }
        public virtual void GetSensorValue()
        {
        }
        
        public virtual void GetSensorValue(int Index)
        {
            GetSensorValue();
            Sensor_Values.Values[Index] = Sensor_Value;
        }
        public Vector Calculate_X()
        {
            return Sensor_Element.Calculate_X(Sensor_Xi);
        }
        public override string ToString()
        {
            string OutputString = "";
            OutputString += "X = " + Calculate_X().ToString() + "\n";
            if (Sensor_Values == null)
            {
                OutputString += "  Value = " + Sensor_Value.ToString() + "\n";
            }
            else
            {
                OutputString += "  Value = " + Sensor_Values.ToString() + "\n";
            }
            return OutputString;
        }
    }
}
