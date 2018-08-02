using System;
using System.Collections.Generic;
using System.Text;

namespace OOPTools
{
    [Serializable]
    public static class WriteToDisplay
    {
        static DisplayMode CurrentMode;
        /// <summary>
        /// Select the display mode to use to write strings
        /// </summary>
        /// <param name="NewDisplayMode"></param>
        static public void SetDisplayMode(DisplayMode NewDisplayMode)
        {
            CurrentMode = NewDisplayMode;
        }
        /// <summary>
        /// Write the string to selected display
        /// </summary>
        /// <param name="info"></param>
        static public void WriteInfo(string info)
        {
            switch (CurrentMode)
            {
                case DisplayMode.Console:
                    Console.Write(info);
                    break;
                case DisplayMode.Window:
                    break;
                case DisplayMode.off:
                    break;
                default:
                    break;
            }
        }
        public enum DisplayMode
        {
            Console,
            Window,
            off
        }
    }

    
     
}
