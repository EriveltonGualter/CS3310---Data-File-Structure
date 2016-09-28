// PROGRAM:  ExtraMissingCRLF
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This project demonstrates situations where the program CODE is CORRECT
//      but the DATA file has an error.  The 2 methods for reading/processing a file
//      are shown, taken from the prior demo example:
//          1) using StreamReader's ReadLine & EndOfStream
//              (so it uses a ReadProcess loop)
//          2) using FileStream's Read & numBYtesRead = 0 as the EOF-check
//              (so it uses a ProcessRead loop)
//      These are both run 4 times to see what happens when the data file is:
//          1) GoodInFile.txt  (i.e., every record ends with a <CR><LF>)
//          2) InFileMissingLastCRLF.txt
//          3) InFileWithExtraEmbeddedCRLF.txt
//          4) InFileWith2ExtraCRLFatEnd.txt
// LESSON #1:  "Never trust human-created input" - do error-checking in the program.
//      Even something as simple as hitting the ENTER key an extra time or two can
//      cause problems.
// LESSON #2:  When debugging, don't assume that it's the PROGRAM that's in error, it
//      could be the programs's input DATA.  Debugging techniques involve "moving the
//      bar" back to "where's the last place where you KNOW things are working right",
//      which could be as far back as the INPUT DATA.
// ************************************************************************************

using System;
using System.IO;
using System.Text;                      // needed for Encoding

namespace ExtraMissingCRLF
{
    class Program
    {
        static void Main()
        {
            string path = ".//..//..//..//";
            string[] file = {   "GoodInFile",
                                "InFileMissingLastCRLF",
                                "InFileWithExtraEmbeddedCRLF",
                                "InFileWith2ExtraCRLFatEnd" };
            string ext = ".txt";

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("\nUsing {0}", file[i]);
                Console.WriteLine("\t\twith StreamReader");
                UseStreamReader(path + file[i] + ext);
                Console.WriteLine("\t\twith FileStream");
                UseFileStream(path + file[i] + ext);
            }
        }
        // ****************************************************************************
        static void UseStreamReader(string fileName)
        {
            StreamReader inFile = new StreamReader(fileName);

            string aLine;
            int i = 1;

            while (!inFile.EndOfStream)
            {
                aLine = inFile.ReadLine();
                Console.WriteLine("Record # {0}: [{1}]", i, aLine);
                i++;
            }
            inFile.Close();
        }
        // ****************************************************************************
        static void UseFileStream(string fileName)
        {
            FileStream inFile = new FileStream(fileName,FileMode.Open, FileAccess.Read);

            const int sizeOfRec = 2 + 8 + 4 + 2;
            byte[] aLineAsAByteArray = new byte[sizeOfRec];
            string aLineAsAString;
            int i = 1;

            int numBytesRead = inFile.Read(aLineAsAByteArray, 0, sizeOfRec);
            while (numBytesRead != 0)
            {
                aLineAsAString = Encoding.Default.GetString(aLineAsAByteArray);
                                                // TO STRIP OFF THE <CR><LF> DO:
                aLineAsAString = aLineAsAString.Substring(0, sizeOfRec - 2);
                Console.WriteLine("Record # {0}: [{1}]", i, aLineAsAString);

                numBytesRead = inFile.Read(aLineAsAByteArray, 0, sizeOfRec);
                i++;
            }
            inFile.Close();
        }
    }
}
