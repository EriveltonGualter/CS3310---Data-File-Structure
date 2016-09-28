// AUTHOR:  D. Kaminski
// DESCRIPTION:  This dumps out the random file to the console.
// FILE ACCESS:  SEQUENTIAL ACCESS, so it calls the ReadARec method which doesn't
//          require an RRN.
// CASES: This program needs to distinguish between 3 cases:
//          1) ReadARec detected EOF (i.e., it returned a -1)
//          2) ReadARec detected an EMPTY location (i.e., it returned aLine, but aLine 
//              is "all-0-bits" which is determined by checking the first 2 bytes of 
//              the record, the ID field, which could never be 00 since that's an 
//              invalid ID)
//          3) ReadARec returned a good record (i.e., it returned aLine which contains
//              a good record)
// *************************************************************************************

package DumpProgram;

import java.io.*;
import randomaccess.RanFile;

public class DumpProgram 
{
    public static void main(String[] args) throws FileNotFoundException, IOException 
    {
        RanFile f = new RanFile();
        final String empty = "\0\0";         // ID FIELD "ALL 0 BITS" SO EMPTY LOC

        String aLine;
        int counter = 1;                     // LOCATIONS START AT 1 NOT 0

        while (!(aLine = f.ReadARec()).equals("-1")) // LOOP WHILE CHECKING FOR "EOF"
        {
            System.out.format("%02d >>  ", counter);
            if (aLine.substring(0,2).equals(empty))
                System.out.println(". . . EMPTY . . .");
            else
                System.out.println(aLine);
            counter++;
        }
        f.CloseFile();
    }
}
