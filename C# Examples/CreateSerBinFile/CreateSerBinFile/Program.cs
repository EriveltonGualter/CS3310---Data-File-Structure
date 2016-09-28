// PROGRAM:  CreateSerBinFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This application creates a SERIALIZED binary file rather than a PLAIN
//      binary file (using InputFile.txt).
// A SERIALIZED file contains META-data, i.e., extra data about the actual data so the
//      data file is "self-interpretable" because the meta data indicates the data types
//      (and other information) regarding the actual data in the file).
//      See Deitel & Deitel's "Visual C# 2008: How To Program" book, chapter 19
//      for more details about Serialization (and from which this example was based on).
// VIEWING THE FILE:  Use a HexEditor to view the SerBinFile.bin and compare that to the
//      BinaryFile.bin in the prior BinaryReaderWriter application.
//***************************************************************************************

using System;
using System.IO;

namespace CreateSerBinFile
{
    class Program
    {
        static void Main()
        {
            StreamReader inFile = new StreamReader(".\\..\\..\\..\\InputFile.txt");
            SerBinFile serBinFile = new SerBinFile(".\\..\\..\\..\\SerBinFile.bin");

            string inputLine;

            while (!inFile.EndOfStream)
            {
                inputLine = inFile.ReadLine();
                serBinFile.WriteARec(inputLine);
            }

            Console.WriteLine("The Serialized Binary File was created.");
            Console.WriteLine("View it in a HexEditor: \n\t" +
                "(see XVI32 - SerBinaryFile.pdf in the app folder).");

            serBinFile.CloseIt();
            inFile.Close();
        }
    }
}
