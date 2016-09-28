// CLASS:  OutputFile - used by Main
// THE FILE:  A text file with variable-length records using CSL,
//      stored in the top-level project folder.
// A RECORD:  Variable-length because of variable-length fields (using CSL).
//      The fields are in a DIFFERENT ORDER than the input file:
//                  input record:   11,Li,Math,3.21
//                  output record:  Li,3.21,Math,11
//      A WriteLine is used (rather than a Write) since we want a <CR><LF> at the end
//          of each line.
// FIELDS:  name, gpa, major, id
//      All are strings since this is a TEXT file.
// **************************************************************************************

using System;
using System.IO;

namespace FileClVarLenRec
{
    class OutputFile
    {
        // *************************  DECLARATIONS  *************************************
        // outFile is DECLARED here, rather than in the constructor so that any method in
        //      this class can access the object (i.e., the file).
        // ******************************************************************************
        private static StreamWriter outFile;
        public static int count = 0;

        // *************************  CONSTRUCTOR  **************************************
        // The constructor method is run only ONCE and BEFORE any other methods run.
        // So this is the place to open the file, which'll be before WriteARec is done.
        // ******************************************************************************
        public OutputFile()
        {
            outFile = new StreamWriter(".//..//..//..//OutputFile.txt");
        }
        // *************************  METHODS  ******************************************

        // ******************************************************************************
        public void WriteARec(string id, string name, string major, string gpa)
        {
            outFile.WriteLine("{0},{1},{2},{3}", name, gpa, major, id);
            count++;
        }
        // ******************************************************************************
        public void FinishWithFile()
        {
            outFile.Close();
            Console.WriteLine("Wrote out {0} records", count);

            Console.WriteLine("\n\nView InputFile.txt & OutputFile.txt in NotePad " +
                "in the Project's main folder");
        }
    }
}
