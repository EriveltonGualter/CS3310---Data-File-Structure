// APPLICATION:  BinaryReaderWriter         SEPARATE SHARABLE CLASS:  BinFile
// 
// DESCRIPTION:  This class handles the binary file used by the 2 programs.
//      It provides methods for opening the file, closing the file and ReadARec and
//      WriteARec.
// NOTE:  If there were more record/field-handling in this app, then I would have
//      set up a SEPARATE CLASS for BinRec in addition to the BinFile class
//      (as was shown in an earlier example on the course webpage).
// -------------------------------------------------------------------------------------
// A BINARY FILE:  (read the DumpingABinaryFile.pdf document on the course website)
//      A file is referred to as a "binary file" when it is not a text file
//          (i.e., it doesn't contain only printable ASCII/Unicode characters).
//      A binary file
//          - contains fields which are int/double/. . . (non-char, non-string fields)
//          - does not have a <CR><LF> after each record (which is just a nice-ity for
//              human readers, really)
//          - can use fixed-length or variable-length records, but the program must know
//              how many bytes to read or write.
//              BinaryWriter has a Write method (not WriteLine) to be able to write out
//                  fields of any of the different data types - and since the field is
//                  a parameter, its size (in bytes) can be determined by the method.
//              BinaryReader has ReadInt32, ReadDouble, ReadSingle, . . . ReadChar,
//                  ReadChars(how many?) methods, but does not have a ReadLine method.
//                  The number of bytes is thus specified by the method used.
//      There are 3 TYPES OF BINARY FILES in C#:
//          1) a PC-specific one which can be created with BinaryWriter and read using
//              BinaryReader (DEMONSTRATED IN THIS APP).
//          2) a .NET-specific serialized binary file (shown another example app).
//          3) a generic binary file which can be tailored to be readable using any
//              language/OS/platform (shown in another example app).
//
// VIEWING A BINARY FILE:  A binary file can't just be printed or viewed in NotePad.
//      There are 2 ways to view its contents:
//      1) use a HexEditor (or OS dump utility) - see course website for details.
//      2) write a simple Dump/Display utility program.
//
// A NOTE on STORAGE in a BINARY FILE for THIS TYPE (#1) of binary file
//          - numeric multi-byte fields (int's, double's, float's, . . .) are stored as
//              LITTLE-ENDIAN on the file since that's how they're stored in memory
//              (see course website "Other Issues #4)
//          - string fields are stored with a preceeding 8-bit byte counter
// -------------------------------------------------------------------------------------
// THE BINARY FILE here: 
//      - the file name is hard-coded in the constructor, the callers don't supply it
//      - it contains fixed-length records, fixed-length fields, no field-separators
//          and no <LF><CR> (it uses Write, not WriteLine)
//      - contains the same fields in the same order as the input file: id, name, gpa
//      - however, for the binary file:
//              id is stored as an actual int (4 bytes)
//              name is stored as char's, same as the input file
//              gpa is stored as an actual float (4 bytes) with the decimal point
// To view the file,
//      1) run the DisplayProgram
//      2) view it in a HexEditor.  (There's also a pdf of the HexEditor's display - 
//          see "XVII32 - BinaryFile" in the main folder of this app.
//***************************************************************************************

using System;
using System.IO;

namespace BinRWClasses
{
    public class BinFile
    {
        //*************************  ATTRIBUTES  ****************************************
        private static FileStream bFile;
        private static BinaryWriter bWriter;
        private static BinaryReader bReader;
        
        //*************************  CONSTRUCTOR  ***************************************
        // This opens the file when the 2 programs create an object for this file.
        // The file is opened only ONCE (for a particular program), which is done before
        //      any actual I/O to the file happens.
        // For the parameter, Create sends in a 'W', Display sends in an 'R'.
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
        //************************  FINISH WITH OBJECT  ********************************
        public void FinishWithObject()
        {
            bFile.Close();
        }
        //*************************  WRITE A REC  ***************************************
        public void WriteARec(string aLine)
        {
            string[] field = aLine.Split(',');
            
            int id = Convert.ToInt32(field[0]);
            string name = field[1];
            float gpa = Convert.ToSingle(field[2]);

            bWriter.Write(id);
            bWriter.Write(name);
            bWriter.Write(gpa);
        }
        //*************************  READ A REC  ****************************************
        // What's sent back to the caller:
        //      - individual fields as OUT parameters
        //      - a bool RETURN value:  true (for successfully read), false for EOF.
        //*******************************************************************************
        public bool ReadARec(out int id, out string name, out float gpa)
        {
            try
            {
                id = bReader.ReadInt32();
                name = bReader.ReadString();
                gpa = bReader.ReadSingle();
                return true;
            }
            catch                                   // READ PAST EOF
            {
                id = 0;
                name = "";
                gpa = 0;
                return false;
            }
        }
    }
}
