// CLASS:  InputFile - used by Main
// THE FILE:  A text file with fixed-length records,
//      stored in the top-level project folder.
// IMPORTANT:  Note that only a single RECORD OBJECT is declared.  When processing files,
//      typically only ONE INPUT RECORD is ever in memory at once:  it's read in, then
//      processed, and is then no longer needed.  So the next record is read into the
//      VERY SAME memory location (allocated for that object).
//      Thus only a SINGLE RECORD OBJECT should be declared.
// **************************************************************************************

using System;
using System.IO;

namespace FileClRecClFixLenRec
{
    class InputFile
    {
        // *************************  DECLARATIONS  *************************************
        // These objects are DECLARED here, rather than in the constructor so that any
        // method in this class can access the objects.
        // ******************************************************************************
        private static StreamReader inFile;
        private static InputRec inRec;

        // *************************  CONSTRUCTOR  **************************************
        // The constructor method is run only ONCE and BEFORE any other methods run.
        // So this is the place to open the file, which'll be before ReadARec is done.
        // This is also the place where the SINGLE record object is set up.
        // ******************************************************************************
        public InputFile()
        {
            inFile = new StreamReader(".//..//..//..//InputFile.txt");
            inRec = new InputRec();
        }
        // *************************  METHODS  ******************************************

        // ******************************************************************************
        // This method sends back 5 things to the caller:
        //      1) a true if an actual record has been read
        //          or a false if it "read past EOF"
        //      2) the 4 fields as separate OUT parameters.
        // This method assumes that the caller wants the 4 individual fields,
        //      rather than the whole record as a single unit.
        // This method calls the RECORD class's ReadARec method which actually reads in
        //      a record and divides it into the individual fields.  (That's not the
        //      FILE's responsibility).
        // ******************************************************************************
        public bool ReadARec(out string id,
                                out string name,
                                out string major,
                                out string gpa)
        {
            bool gotARec = inRec.ReadARec(inFile);

            id = inRec.Id;
            name = inRec.Name;
            major = inRec.Major;
            gpa = inRec.Gpa;

            return gotARec;
        }
        // ******************************************************************************
        public void FinishWithFile()
        {
            inFile.Close();
            Console.WriteLine("Read in {0} records", InputRec.count);
        }
    }
}
