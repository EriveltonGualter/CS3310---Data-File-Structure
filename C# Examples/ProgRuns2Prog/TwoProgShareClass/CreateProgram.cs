// PROGRAM:  CreateProgram in NEW APPLICATION:  ProgRuns2Prog
//      - see top comment in DriverProgram.
// AUTHOR:  D. Kaminski
// NO CHANGES were made to this code from the earlier app EXCEPT:
//      PUBLIC was added to the CLASS and to the Main METHOD.
// **************************************************************************************

using System;
using System.IO;

namespace TwoProgShareClass
{
    public class CreateProgram
    {
        public static void Main()
        {
            StreamReader inFile = new StreamReader(".//..//..//..//InFile.txt");

            TheFile f = new TheFile();

            string aLine;

            while (!inFile.EndOfStream)
            {
                aLine = inFile.ReadLine();
                f.WriteARec(aLine);
            }
            inFile.Close();
            f.CloseTheFile();

            Console.WriteLine("DONE creating the file");
        }
    }
}
