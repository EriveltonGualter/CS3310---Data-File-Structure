// CLASS:  OutputFile - used by Main
// THE FILE:  A text file with fixed-length records,
//      stored in the top-level project folder.
// IMPORTANT:  Note that only a single RECORD OBJECT is declared.  As with the input
//      file, only a single output record is ever needed in memory at one time.
//      A record is built, written, and then no longer needed.
// **************************************************************************************

using System;
using System.IO;

namespace FileClRecClFixLenRec
{
    class OutputFile
    {
        // *************************  DECLARATIONS  *************************************
        // These objects are DECLARED here, rather than in the constructor so that any
        // method in this class can access the objects.
        // ******************************************************************************
        private static StreamWriter outFile;
        private static OutputRec outRec;

        // *************************  CONSTRUCTOR  **************************************
        // The constructor method is run only ONCE and BEFORE any other methods run.
        // So this is the place to open the file, which'll be before WriteARec is done.
        // This is also the place where the SINGLE record object is set up.
        // ******************************************************************************
        public OutputFile()
        {
            outFile = new StreamWriter(".//..//..//..//OutputFile.txt");
            outRec = new OutputRec();
        }
        // *************************  METHODS  ******************************************

        // ******************************************************************************
        // This method calls the RECORD class's WriteARec method which actually builds
        //      the record from the individual fields and writes it to the file.
        //      (That's not the FILE's responsibility).
        // ******************************************************************************
        public void WriteARec(string id, string name, string major, string gpa)
        {
            outRec.Id = id;
            outRec.Name = name;
            outRec.Major = major;
            outRec.Gpa = gpa;

            outRec.WriteARec(outFile);
        }
        // ******************************************************************************
        public void FinishWithFile()
        {
            outFile.Close();
            Console.WriteLine("Wrote out {0} records", OutputRec.count);

            Console.WriteLine("\n\nView InputFile.txt & OutputFile.txt in NotePad " +
                "in the Project's main folder.");
        }
    }
}
