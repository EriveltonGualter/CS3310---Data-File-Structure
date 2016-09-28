// APPLICATION:  RandomAccess       CLASS:  RanFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This contains methods for handling the random access file including
//      methods for:
//          - writing to the file (using RANDOM ACCESS) - used by CreateProgram
//          - reading the file (using RANDOM ACCESS)    - used by QueryProgram
//          - reading the file (using SEQUENTIAL ACCESS)- used by DumpProgram
//      Any method needing random access needs to have the RRN (Relative Record Number)
//          sent in.
// QUESTION:  Would it ever be necessary to have a WriteARec which did SEQUENTIAL
//      ACCESS?  That would only be needed if Create's input file was a key-sequential
//      file on the primary key for this direct address file.  That is, the input file
//      would have to have already been sorted on ID.  Instead, the input file in this
//      example is a serial file with respect ot ID (i.e., not ordered on ID) - it's a
//      sequential access file, but it's not a KEY-SEQUENTIAL file.
// DISCLAIMER:  This example does not use a separate RECORD class.  This is to simplify
//      the example to highlight the important new concepts.  It is also appropriate
//      in this case since there is really very little record handling/processing.
//      Normally one would probably set up a separate RanRec class similar to what
//      was done in an earlier demo example, FileClRecClFixLenRec.
// *************************************************************************************

using System;
using System.IO;
using System.Text;

namespace RandomAccess
{
    public class RanFile
    {
        // **************************** DECLARATIONS ************************************
        FileStream f;
        private const int REC_SIZE = 2 + 8 + 4;     // JUST THE DATA PORTION, NO +2 FOR
                                                    //      THE <CR><LF>
        private const string empty = "\0\0";
            // **************************** CONSTRUCTOR *************************************
        public RanFile(string createFile)
        {
            f = new FileStream(".//..//..//..//RandomFile.txt",
                        FileMode.Create, FileAccess.Write);
        }
        public RanFile()
        {
            f = new FileStream(".//..//..//..//RandomFile.txt",
                        FileMode.Open, FileAccess.Read);
        }
        // **************************** METHODS *****************************************

        // ******************************************************************************
        // RANDOM ACCESS WriteARec (no RRN needed)
        // NOTE:  aLine does not contain a <CR><LF> since StreamReader's ReadLine strips
        //          it off and just returns the DATA portion of the record.  That's OK
        //          since the EMPTY locations don't have a <CR><LF> either.
        // ******************************************************************************
        public void WriteARec(string aLine, int RRN)        // RANDOM ACCESS
        {
            byte[] buffer = new byte[REC_SIZE];

            for (int i = 0; i < aLine.Length; i++)          // STRING --> BYTEARRAY
                buffer[i] = BitConverter.GetBytes(aLine[i])[0];

            int offset = (RRN - 1) * REC_SIZE;
            f.Seek(offset, SeekOrigin.Begin);             

            f.Write(buffer, 0, REC_SIZE);
        }
        // ******************************************************************************
        // SEQUENTIAL ACCESS ReadARec (no RRN needed)
        // sends back 2 things:
        //      1) returns 
        //              true - a good record is being returned
        //              true - an empty location is being returned
        //              false - detected EOF
        //         NOTE:  To DUMP, a good record and an empty (internal) location
        //                  mean the same thing, i.e., keep looping (it's not yet EOF)
        //      2) the record (or empty location) or "" if EOF)
        // ******************************************************************************
        public bool ReadARec(out string aLine)              // SEQUENTIAL ACCESS
        {
            byte[] buffer = new byte[REC_SIZE];

            int numBytesRead = f.Read(buffer, 0, REC_SIZE);
            if (numBytesRead == 0)                          // READ PAST EOF
            {
                aLine = "";
                return false;
            }
            else
            {
                aLine = Encoding.Default.GetString(buffer); // BYTEARRAY --> STRING
                return true;                                // RETURNING A RECORD
            }
        }
        // ******************************************************************************
        // RANDOM ACCESS ReadARec - RRN sent in
        // sends back 2 things:
        //      1) returns 
        //              true - a good record is being returned
        //              false - an empty location is being returned
        //              false - detected EOF
        //         NOTE:  To QUERY, an empty location & EOF mean the same thing,
        //                  i.e., there's no record at that RRN
        //      2) the record (or empty location) or "" if EOF
        // ******************************************************************************
        public bool ReadARec(int RRN, out string aLine)     // RANDOM ACCESS
        {
            byte[] buffer = new byte[REC_SIZE];

            int offset = (RRN - 1) * REC_SIZE;
            f.Seek(offset, SeekOrigin.Begin);             

            int numBytesRead = f.Read(buffer, 0, REC_SIZE);
            if (numBytesRead == 0)                         // PAST EOF, SO NO RECORD HERE
            {
                aLine = "";
                return false;
            }
            else
            {
                aLine = Encoding.Default.GetString(buffer); // BYTEARRAY --> STRING
                if (aLine.Substring(0, 2) == empty)         // IT'S AN EMPTY LOCATION
                    return false;
                else
                    return true;                            // RETURNING A GOOD RECORD
            }
        }
        // ******************************************************************************
        public void CloseFile()
        {
            f.Close();
        }
    }
}
