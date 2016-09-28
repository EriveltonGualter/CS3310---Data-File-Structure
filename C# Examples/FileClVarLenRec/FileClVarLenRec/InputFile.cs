// CLASS:  InputFile - used by Main
// THE FILE:  A text file with variable-length records using CSL,
//      stored in the top-level project folder.
// A RECORD:  Variable-length because of variable-length fields (using CSL), e.g.,
//                      11,Li,Math,3.21
// FIELDS:  id, name, major, gpa
//      All are strings since this is a TEXT file.
//      If the caller expected strings/ints/floats back, then ReadARec should do the
//      Converts and return the appropriate variables.
// **************************************************************************************

using System;
using System.IO;

namespace FileClVarLenRec
{
    class InputFile
    {
        // *************************  DECLARATIONS  *************************************
        // inFile is DECLARED here, rather than in the constructor so that any method in
        //      this class can access the object (i.e., the file).
        // ******************************************************************************
        private static StreamReader inFile;
        public static int count = 0;

        // *************************  CONSTRUCTOR  **************************************
        // The constructor method is run only ONCE and BEFORE any other methods run.
        // So this is the place to open the file, which'll be before ReadARec is done.
        // ******************************************************************************
        public InputFile()
        {
            inFile = new StreamReader(".//..//..//..//InputFile.txt");
        }
        // *************************  METHODS  ******************************************

        // ******************************************************************************
        // This method sends back 5 things to the caller:
        //      1) a true if an actual record has been read
        //          or a false if it "read past EOF"
        //      2) the 4 fields as separate OUT parameters.
        // This method assumes that the caller wants the 4 individual fields,
        //      rather than the whole record as a single unit.
        // ******************************************************************************
        public bool ReadARec(out string id,
                                out string name,
                                out string major,
                                out string gpa)
        {
            string theLine = inFile.ReadLine();

            if (theLine != "")
            {
                string[] field = theLine.Split(',');
                id = field[0];
                name = field[1];
                major = field[2];
                gpa = field[3];
                count++;
                return true;
            }
            else
            {
                id = name = major = gpa = "";
                return false;
            }
        }
        // ******************************************************************************
        public void FinishWithFile()
        {
            inFile.Close();
            Console.WriteLine("Read in {0} records", count);
        }
    }
}
