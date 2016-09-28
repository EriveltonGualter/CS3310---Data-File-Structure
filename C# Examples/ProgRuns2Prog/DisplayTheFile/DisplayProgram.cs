// PROGRAM:  DisplayProgram in NEW APPLICATION:  ProgRuns2Prog
//      - see top comment in DriverProgram.
// AUTHOR:  D. Kaminski
// NO CHANGES were made to this code from the earlier app EXCEPT:
//      PUBLIC was added to the CLASS and to the Main METHOD.
// **************************************************************************************

using System;
using TwoProgShareClass;

namespace DisplayTheFile
{
    public class DisplayProgram
    {
        public static void Main()
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
