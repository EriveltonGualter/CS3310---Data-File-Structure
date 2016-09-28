// PROGRAM:  UsingFileStream
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This demonstrates using FileStream for reading and writing text files.
// THE FILES:  InFile and OutFile are sequential access text file.
// THE RECORDS:  Both files have FIXED-LENGTH RECORDS (lines) containing id, name, gpa.
//      However, the field-order is different in the 2 files.
//              InFile:   id (2 char), name (8 char), gpa (4 char) plus <CR><LF>
//              OutFile:  gpa (4 char), name (8 char), id (2 char) plus <CR><LF>
//      This means that each input record has to be subdivided into individual fields
//          so as to access the fields, rather than just processing the whole record
//          as a single unit (i.e., aLine).
//          [This is done in order to show how such conversion can be done].
//      However, there was no need to convert the string id and string gpa into int
//          and float fields since no numeric processing needed to be done.
// FILESTREAM:  This class's Read and Write methods access a BYTE ARRAY rather than
//      a STRING which StreamReader and StreamWriter use.  Their parameters include:
//          1) the byte array which will hold the record:       buffer 
//          2) the offset in the buffer:                        0
//          3) the number of bytes to read in or write out:     REC_SIZE
//              (which is 2 + 8 + 4 + the extra 2 for the <CR><LF>)
// SOME ISSUES WHEN USING FILESTREAM:
//      - The byte array (buffer) is generally converted into a string for easier
//          processing of the record's fields.  For example, numeric fields will often
//          need to be converted into int's, float's, etc. for arithmetic processing.
//      - FileStream is used here with FIXED-LENGTH records (i.e., every record is
//          exactly REC_SIZE long) rather than VARIABLE-LENGTH records since REC_SIZE
//          must be specified in the Read/Write calls.
//      - Alternatively, if VARIABLE-LENGTH records were used, each data record in the
//          input file would have to be preceeded with a record SIZE field (which would
//          have to be a FIXED-SIZE FIELD) which would have to be read in first, then
//          FileStream's Read would use this for its record size parameter.
//          Similarly, the output file would need a preceeding-byte-count for each
//          record if it will later be read using FileStream.
//      - The methods are Read and Write, not ReadLine and WriteLine.  So,
//              - when calculating REC_SIZE, the 2 bytes for <CR><LF> must be included
//              - Read's buffer DOES include the <CR><LF>
//              - when writing to a file, the <CR><LF> must be put into the end of the
//                  buffer for a record, if those are to be included in the file.
//                  Since Read's buffer already includes them, they don't need to be
//                  appended for this example
// IMPORTANT NOTE:  It will be necessary to use FileStream later for handling
//      RANDOM ACCESS files.  It will also be useful for generic BINARY files.
// **************************************************************************************

using System;
using System.IO;
using System.Text;              // needed for the Encoding class

namespace UsingFileStream
{
    class Program
    {
        static void Main()
        {
            const int REC_SIZE = 16;

            FileStream inFile  = new FileStream(".//..//..//..//InFile.txt",
                                                FileMode.Open, FileAccess.Read);
            FileStream outFile = new FileStream(".//..//..//..//OutFile.txt",
                                                FileMode.Create, FileAccess.Write);
            byte[] buffer = new byte[REC_SIZE];
            string aLine;
            string id, name, gpa, endOfLine;
                                                        
            int numBytesRead = inFile.Read(buffer, 0, REC_SIZE);    // READ 1ST RECORD
            while (numBytesRead != 0)                   // START THE LOOP
            {
                                                        // CONVERT BYTEARRAY TO STRING
                aLine = Encoding.Default.GetString(buffer);
                                                        
                id = aLine.Substring(0, 2);             // SEPARATE STRING INTO FIELDS
                name = aLine.Substring(2, 8);
                gpa = aLine.Substring(10, 4);
                endOfLine = aLine.Substring(14, 2);
                                                        
                Console.WriteLine("{0}{1}{2}", id, name, gpa);    // USE THE FIELDS
                                                        
                aLine = name + id + gpa + endOfLine;    // COMBINE FIELDS INTO A STRING
                                                        
                for (int i = 0; i < aLine.Length; i++)  // CONVERT STRING TO BYTEARRAY
                    buffer[i] = BitConverter.GetBytes(aLine[i])[0];
                                                     
                outFile.Write(buffer, 0, REC_SIZE);                 // WRITE A RECORD
                                                        
                numBytesRead = inFile.Read(buffer, 0, REC_SIZE);    // READ A RECORD
            }
            inFile.Close();
            outFile.Close();
        }
    }
}