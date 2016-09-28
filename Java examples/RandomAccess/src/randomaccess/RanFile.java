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

package randomaccess;

import java.io.*;

public class RanFile 
{
    // **************************** DECLARATIONS ************************************
    RandomAccessFile f;

    private final int REC_SIZE = 2 + 8 + 4;     // JUST THE DATA PORTION, NO +2 FOR
                                                //      THE <CR><LF>
    private final String empty = "\0\0";
   
    // **************************** CONSTRUCTOR *************************************
    public RanFile(String createFile) throws FileNotFoundException
    {
        f = new RandomAccessFile("RandomFile.txt", "rw");//open for reading & writing
    }
    public RanFile() throws FileNotFoundException
    {
        f = new RandomAccessFile("RandomFile.txt", "r");//open for reading ONLY
    }
    // **************************** METHODS *****************************************

    // ******************************************************************************
    // RANDOM ACCESS WriteARec (no RRN needed)
    // NOTE:  aLine does not contain a <CR><LF> since StreamReader's ReadLine strips
    //          it off and just returns the DATA portion of the record.  That's OK
    //          since the EMPTY locations don't have a <CR><LF> either.
    // ******************************************************************************
    public void WriteARec(String aLine, int RRN) throws IOException  // RANDOM ACCESS
    {
        byte[] buffer = new byte[REC_SIZE];

        buffer = aLine.getBytes();                      // STRING --> BYTEARRAY

        int offset = (RRN - 1) * REC_SIZE;              // FIND OFFSET
        
        f.seek(offset);

        f.write(buffer, 0, REC_SIZE);     // WRITE REC BEGINNING AT OFFSET
    }
    // ******************************************************************************
    // SEQUENTIAL ACCESS ReadARec (no RRN needed)
    // returns: 
    //      1) the record (or empty location)
    //          OR
    //      2) the string "-1" if EOF
    //         NOTE:  To DUMP, a good record and an empty (internal) location
    //                  mean the same thing, i.e., keep looping (it's not yet EOF)
    // ******************************************************************************
    public String ReadARec() throws IOException     // SEQUENTIAL ACCESS
    {
        byte[] buffer = new byte[REC_SIZE];

        int numBytesRead = f.read(buffer, 0, REC_SIZE);
        if (numBytesRead == -1)                          // EOF
        {
            return "-1";        // signal for EOF
        }
        else
        {
            String aLine = new String(buffer);          // BYTEARRAY --> STRING
            return aLine;                               // RETURNING A RECORD
        }
    }
    // ******************************************************************************
    // RANDOM ACCESS ReadARec - RRN sent in
    // returns: 
    //      1) the record
    //          OR
    //      2) the string "-1" if empty location OR EOF
    //         NOTE:  To QUERY, an empty location & EOF mean the same thing,
    //                  i.e., there's no record at that RRN
    // ******************************************************************************
    public String ReadARec(int RRN) throws IOException // RANDOM ACCESS
    {
        byte[] buffer = new byte[REC_SIZE];

        int offset = (RRN - 1) * REC_SIZE;
        
        f.seek(offset);
        int numBytesRead = f.read(buffer, 0, REC_SIZE);

        if (numBytesRead == -1)                       // PAST EOF, SO NO RECORD HERE
        {
            return "-1";        // signal for EOF
        }
        else
        {
            String aLine = new String(buffer);          // BYTEARRAY --> STRING
            if (aLine.substring(0, 2).equals(empty))    // IT'S AN EMPTY LOCATION
                return "-1";
            else
                return aLine;                           // RETURNING A GOOD RECORD
        }
    }
    // ******************************************************************************
    public void CloseFile() throws IOException
    {
        f.close();
    }
}
