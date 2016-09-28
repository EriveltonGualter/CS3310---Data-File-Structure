// APPLICATION:  BinRWRandom        PROGRAM:  CreateProgram
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This application creates a binary file which is also a RANDOM ACCESS
//      file (Query uses Random Access).  Create also uses RANDOM ACCESS when inserting
//      records into the file (since the order of the input file is NOT in the order
//      needed to create it using SEQUENTIAL ACCESS).
// RANDOM ACCESS FILE STRUCTURE:  A direct address file structure based on ID as the key.
//      That is, the record with ID 04 goes in location 4 in the file.
// RRN's start with 1, not 0.
//***************************************************************************************

using System;
using System.IO;
using BinRWClasses;

namespace BinRWRandom
{
    class CreateProgram
    {
        static void Main()
        {
            StreamReader inFile = new StreamReader(".\\..\\..\\..\\InputFile.txt");
            BinFile outFile = new BinFile('W');              // W for WriteMode

            string inputLine;
            int id;                         // id is used as the RRN for random access
                                            // since the file's DIRECT ADDRESS on ID

            while (!inFile.EndOfStream)
            {
                inputLine = inFile.ReadLine();
                id = int.Parse(inputLine.Substring(0, 2));
                outFile.WriteARec(inputLine, id);       // sending in the RRN too, so 
                                                        // it's calling a RANDOM ACCESS
                                                        // version of WriteARec
            }
            Console.WriteLine("The Binary File was created.");
            Console.WriteLine("Run DumpProgram to view it.");
            Console.WriteLine("Also view it in a HexEditor: \n\t" +
                "(see XVI32 - BinaryFile.pdf in the app folder).");
            Console.WriteLine("\nRun QueryProgram to see that random access works.");

            outFile.FinishWithObject();
            inFile.Close();
        }
    }
}
