// APPLICATION:  TwoProgShareClass      PROGRAM 1:  CreateProgram
//                                      PROGRAM 2:  DisplayProgram
//                                      CLASS:  TheFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  See comments at the top of CreateProgram.
// HOW TO GIVE THIS PROGRAM ACCESS TO A CLASS IN ANOTHER NAMESPACE:
//      1) (while viewing the DisplayProgram.cs code)
//         under Project drop-down menu, select Add Reference
//      2) under the Projects tab, select TwoProgShareClass (i.e., the other namespace)
//      3) add "public" to TheFile's class
//      4) add "using TwoProgShareClass" directive at the top here
// **************************************************************************************

using System;
using TwoProgShareClass;

namespace DisplayTheFile
{
    class DisplayProgram
    {
        static void Main()
        {
            TheFile f = new TheFile("Check That File Exists");

            string aLine;

            Console.WriteLine("DISPLAY the file:");

            while (f.ReadARec(out aLine))
            {
                Console.WriteLine(aLine);
            }
            f.CloseTheFile();
        }
    }
}
