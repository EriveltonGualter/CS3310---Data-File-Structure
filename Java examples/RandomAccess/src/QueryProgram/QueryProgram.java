// APPLICATION:  RandomAccess       PROGRAM:  QueryProgram
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This queries the random file using a small "driver" to test queries for
//      ID 1 through ID 8.  Some queries find good records, some find empty locations
//      (including BOTH kinds of "holes":  the "all-0-bits" and the "past EOF" cases).
// FILE ACCESS:  RANDOM ACCESS, so it calls the ReadARec method which requires an RRN.
// CASES: This program needs to distinguish between only 2 cases:
//          1) ReadARec detected an empty location (i.e., it returned a -1)
//              which could be either an "all-0-bits" or a "past EOF" case
//          2) ReadARec returned a good record (i.e., it returned aLine which contains
//              a good record)
// *************************************************************************************

package QueryProgram;

import java.io.*;
import randomaccess.RanFile;

public class QueryProgram 
{
    public static void main(String[] args) throws FileNotFoundException, IOException 
    {
        for (int i = 1; i <= 8; i++)
            QueryForId(i);                      // ID IS THE 1st 2 DIGITS IN THE REC
    }
    // *****************************************************************************
    static void QueryForId(int RRN) throws FileNotFoundException, IOException
    {
        RanFile f = new RanFile();

        String aLine = f.ReadARec(RRN);

        if (aLine.equals("-1")) // NO RECORD
            System.out.format("Location %02d is EMPTY\n", RRN);
        else
            System.out.format("Location %02d contains:  %s\n", RRN, aLine);
        
        f.CloseFile();    
    }
}
