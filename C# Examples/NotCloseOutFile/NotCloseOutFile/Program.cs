// PROGRAM:  NotCloseOutFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This program demonstrates the results of failing to CLOSE an output
//      file.  (Not closing an input file does not cause this problem.  However, good 
//      programming practice dictates that a program should explicitly close all files
//      that it opens to release system resources).
// WHAT HAPPENS:  If an output file isn't closed (in C#) the last group of records do not
//      actually get written to the file.  The program WRITES them (as "proven" by the
//      Console.WriteLine immediately after the outFile.WriteLine).  However, when
//      writing to a FILE, the data is actually only written to an internal buffer set up
//      for that file.  When the buffer gets full, it's dumped out to the file.  The
//      buffer is also dumped to the file when the program closes the file.  So, ... if
//      the program doesn't close it, the buffer doesn't get flushed.
//      [NOTE:  Some languages/systems DO automatically "flush the buffer" when the
//              program finishes executing, even without an explicit close of the file].
// DEMONSTRATION:
//    1)Run this program (with the CLOSE commented out).  Then check the 2 output files
//      in NotePad.  Also see the file sizes in the main project folder.
//      NOTE:  The buffer size is 1KB which is 1024 bytes.
//    2) Uncomment outFile.Close and run the program again.  Check the 2 output files
//          in NotePad and note that all the records are now there.
// REMINDER / CAUTION:  Console.WriteLine (and even the interactive debugger) can
//          "LIE TO YOU" (well, MISLEAD you anyway) !
// **************************************************************************************

using System;
using System.IO;

namespace NotCloseOutFile
{
    class Program
    {
        static void Main()
        {
            CopyAFile("InFile3",  "OutFile3");
            CopyAFile("InFile15", "OutFile15");

            Console.WriteLine("\nOutFile3 & OutFile15 have been created.\n\n" +
                "Check them in NotePad to see if all the records are there.\n" +
                "Also check the file sizes in the main project folder:\n\t" +
                "InFile3: 1 KB, OutFile3: 0 KB, InFile15: 2 KB, OutFile15: 1 KB.");
        }
        // ******************************************************************************
        static void CopyAFile(string inFileName, string outFileName)
        {
            string path = ".\\..\\..\\..\\";
            string extension = ".txt";

            StreamReader inFile = new StreamReader(path + inFileName + extension);
            StreamWriter outFile = new StreamWriter(path + outFileName + extension);

            Console.WriteLine("\nNow creating {0}\n", outFileName);

            string theLine;

            while (!inFile.EndOfStream)
            {
                theLine = inFile.ReadLine();
                outFile.WriteLine(theLine);
                Console.WriteLine(theLine);         // "PROVE" THAT IT'S BEEN WRITTEN
            }
            inFile.Close();
            //outFile.Close();                      // OOPS !  DIDN'T CLOSE THE FILE
        }
    }
}
