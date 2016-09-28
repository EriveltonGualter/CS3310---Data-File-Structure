// PROGRAM:  DisplayProgram     in the APPLICATION:  TestDriver
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This program displays a file.
//      The filename is specified as a parameter for Main.
// NOTE:  PUBLIC added to the class and to Main.
// **************************************************************************************

using System;
using System.IO;

namespace DisplayProject
{
    public class DisplayProgram
    {
        public static void Main(string[] args)
        {
            string path = ".//..//..//..//";
            string fileName = args[0];
            string extension = ".txt";

            StreamReader inFile = new StreamReader(path + fileName + extension);

            string aLine;

            Console.WriteLine("- - - - - - FILE: {0} - - - - - -", fileName);

            while (!inFile.EndOfStream)
            {
                aLine = inFile.ReadLine();
                Console.WriteLine(aLine);
            }
            inFile.Close();

            Console.WriteLine("- - - - - - - - - - - - - - - - - - -\n");
        }
    }
}
