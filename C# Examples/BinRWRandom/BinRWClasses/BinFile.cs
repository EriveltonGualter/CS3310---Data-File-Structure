// APPLICATION:  BinaryReaderWriter         SEPARATE SHARABLE CLASS:  BinFile
// 
// DESCRIPTION:  This class handles the binary file used by the 3 programs.
//      It provides a constructor for opening the file, and methods to close the file,
//      do a WriteARec (RANDOM ACCESS version used by Create),
//      do a ReadARec (SEQUENTIAL ACCESS version used by Display,
//                      & RANDOM ACCESS version used by Query).
//
// THE FILE is both a BINARY file and a RANDOM ACCESS file - those are 2 separate,
//              independent choices the designer made.
//      Since it's a random access file, it needs FIXED-LENGTH RECORDS and so fixed-
//      length fields.
//              - That's automatic for int/float/long/.../char/bool fields since they're
//                  they're all the same size for every record (int's and floats are
//                  4 bytes, longs and doubles are 8 bytes, etc.).
//              - However, STRING fields must be padded/truncated to ensure that all
//                  records are fixed-length.
//
// VIEWING THE FILE:
//      1) use the DisplayProgram
//      2) use a HexEditor.  I've put a .pdf copy of the HexEditor's display of the
//          binary file is the top-level folder of the app.  See:
//                      XVI32 - BinaryFile.pdf
//          (XVI32 is the HexEditor - see the course webpage to download the software).
//
// NOTE:  This BinFile class is NOT THE SAME as the BinFile class in the prior example
//      application.  This example:
//          - uses a binary file which is also RANDOM ACCESS
//                  so it has FIXED-LENGTH records (with fixed-length fields)
//          - creates the file using random access
//                  so WriteARec needs an RRN parameter to calculate OFFSET for the SEEK
//          - also has a QueryProgram which uses random access
//                  so it needs a 2nd ReadARec for doing random access which needs an
//                  RRN parameter to calculate OFFSET for the SEEK
//          - needs to deal with possible EMPTY LOCATIONS ("holes") within the file
//                  due to the DIRECT ADDRESS file structure
//                  so the 1st ReadARec (for doing sequential access) needs to do
//                  SPECIAL HANDLING of any STRING fields because of the way they're
//                  stored
//***************************************************************************************

using System;
using System.IO;

namespace BinRWClasses
{
    public class BinFile
    {
        //*************************  ATTRIBUTES  ****************************************
        private static FileStream bFile;            // NEEDED FOR ITS Seek METHOD
        private static BinaryWriter bWriter;
        private static BinaryReader bReader;

        // -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -
        // The following are needed for Random Access
        // -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -
        private const int SIZE_OF_ID = sizeof(int);         // 4 BYTES
        private const int SIZE_OF_NAME = 8;
        private const int SIZE_OF_GPA = sizeof(float);      // 4 BYTES

        private const int SIZE_OF_REC = SIZE_OF_ID + (SIZE_OF_NAME + 1) + SIZE_OF_GPA;
        //                                      THE +1 IS FOR THE STRING-LENGTH BYTE

        //*************************  CONSTRUCTOR  ***************************************
        // This opens the file when the 2 programs create an object for this file.
        // The file is opened only ONCE (for a particular program), which is done before
        //      any actual I/O to the file happens.
        // For the parameter, Create sends in a 'W', Display & Query send in an 'R'.
        //*******************************************************************************
        public BinFile(char ReadOrWrite)
        {
            if (ReadOrWrite == 'R')
            {
                bFile = new FileStream(".\\..\\..\\..\\BinaryFile.bin",
                                            FileMode.Open, FileAccess.Read);
                bReader = new BinaryReader(bFile);
            }
            else
            {
                bFile = new FileStream(".\\..\\..\\..\\BinaryFile.bin",
                                            FileMode.Create, FileAccess.Write);
                bWriter = new BinaryWriter(bFile);
            }
        }
        //************************  FINISH WITH OBJECT  *********************************
        public void FinishWithObject()
        {
            bFile.Close();
        }
        //*************************  WRITE A REC (RANDOM ACCESS version) ****************
        public void WriteARec(string aLine, int rrn)
        {
            int id = Convert.ToInt32(aLine.Substring(0, 2));
            string name = aLine.Substring(2, 8);
            float gpa = Convert.ToSingle(aLine.Substring(10, 4));

            long offset = (rrn - 1) * SIZE_OF_REC;
            bFile.Seek(offset, SeekOrigin.Begin);

            bWriter.Write(id);
            bWriter.Write(name);
            bWriter.Write(gpa);
        }
        //*************************  READ A REC  ****************************************
        // 2 overloaded methods:
        //      1) for sequential access
        //      2) for random access (where the caller supplies the RRN
        //              used to calculate offset used for the Seek).
        // This reads 1 record's fields (in their proper binary format) from the file
        //      and returns them as OUT parameters.
        //*******************************************************************************

        //*************************  READ A REC (SEQUENTIAL ACCESS version) *************
        // What's sent back to the caller:
        //      - individual fields as OUT parameters
        //              (or 0, "", 0.0 for empty locations or past eof case) 
        //      - a bool RETURN value:
        //              1) true for a successful read of a good record,
        //              2) true for an empty location - an "all 0 bits" hole in the file
        //              3) false for an empty location - a "read failed" case (past EOF)
        // NOTE case #2 - this ReadARec version's return value is for NotYetAtEOF
        // -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -   -
        // The if/else is needed for any STRING field if the file might have any empty
        //      locations ("all 0 bits"), because it's a Random Access file, (i.e.,
        //      it was created using Random Access rather than Sequential Access).
        // This is because BinaryReader expects string fields to appear in the file as:
        //      a 1-byte Length field followed by the the string itself.
        //      So an "all 0 bits" empty location will contain a 0 value for Length,
        //          so BinaryReader's ReadString method reads in 0 bytes for the string,
        //          so the filePositionPtr only gets "moved along" 0 bytes,
        //          so all subsequent fields and records in the file appear to be in
        //              the wrong place.
        //*******************************************************************************
        public bool ReadARec(out int id, out string name, out float gpa)
        {
            try
            {
                id = bReader.ReadInt32();
                if (id == 0)                    // TO SKIP OVER THE "all 0 bits"
                {                               // name FIELD (which looks like a null)
                    bReader.ReadChars(SIZE_OF_NAME + 1);
                    name = "";
                }
                else                            
                    name = bReader.ReadString();  // TO READ IN GOOD RECORD'S NAME FIELD
                gpa = bReader.ReadSingle();
                return true;
            }
            catch                                // READ PAST EOF
            {
                id = 0;
                name = "";
                gpa = 0;
                return false;
            }
        }
        //*************************  READ A REC (RANDOM ACCESS version) *****************
        // What's sent back to the caller:
        //      - individual fields as OUT parameters
        //              (or 0, "", 0.0 for empty locations or past eof case) 
        //      - a bool RETURN value:
        //              1) true for a successful read of a good record,
        //              2) false for an empty location - an "all 0 bits" hole in the file
        //              3) false for an empty location - a "read failed" case (past EOF)
        // NOTE case #2 - this ReadARec version's return value is for ReadAGoodRec
        //*******************************************************************************
        public bool ReadARec(out int id, out string name, out float gpa, int rrn)
        {
            long offset = (rrn - 1) * SIZE_OF_REC;
            bFile.Seek(offset, SeekOrigin.Begin);

            try
            {
                id = bReader.ReadInt32();
                name = bReader.ReadString();
                gpa = bReader.ReadSingle();
                if (name == "")
                    return false;               // IT'S AN EMPTY LOCATION ("ALL-0-BITS")
                else
                    return true;                // IT'S A GOOD RECORD
            }
            catch                               // IT'S AN EMPTY LOCATION ("READ FAILED")
            {                                   //          (TRIED READING PAST EOF)
                id = 0;
                name = "";
                gpa = 0;
                return false;
            }
        }
    }
}
