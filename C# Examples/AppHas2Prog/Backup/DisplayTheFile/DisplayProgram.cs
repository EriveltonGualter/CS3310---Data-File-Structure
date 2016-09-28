// PROJECT:  AppHas2Prog        PROGRAM 1:  CreateProgram
//                              PROGRAM 2:  DisplayProgram
// AUTHOR:  D. Kaminski
// DESCRIPTION:  See comments at the top of CreateProgram.
// **************************************************************************************

using System;
using System.IO;

namespace DisplayTheFile 
{
    class DisplayProgram
    {
        static void Main()
        {
            string fileName = ".//..//..//..//SharedFile.txt";

            if (File.Exists(fileName))
            {
                StreamReader inFile = new StreamReader(fileName);

                string aLine;

                Console.WriteLine("Here is SharedFile.txt:");

                while (!inFile.EndOfStream)
                {
                    aLine = inFile.ReadLine();
                    Console.WriteLine(aLine);
                }
                inFile.Close();
            }
            else
                Console.WriteLine("ERROR:  Run CreateProgram before DisplayProgram");
        }
    }
}