// APPLICATION:  TestDriver including       PROGRAM:  DriverProgram
//                                          PROGRAM:  DisplayProgram
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This program calls the DisplayProgram multiple times to test it with a
//      variety of different test files.  DisplayProgram's Main takes a stringArray as
//      its argument.  In this example, only 1 parameter is needed.  But the array makes
//      it possible to send in multiple parameters.        
// **************************************************************************************

using System;

namespace TestDriver
{
    class DriverProgram
    {
        static void Main()
        {
            string[] args = new string[1];

            for (int i = 1; i <= 4; i++)
            {
                args[0] = "InFile" + i;
                DisplayProject.DisplayProgram.Main(args);
            }
        }
    }
}
