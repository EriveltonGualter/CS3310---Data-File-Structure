// APPLICATION:  RandomAccess       PROGRAM:  DumpProgram
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This dumps out the random file to the console.
// FILE ACCESS:  SEQUENTIAL ACCESS, so it calls the ReadARec method which doesn't
//          require an RRN.
// CASES: This program needs to distinguish between 3 cases:
//          1) ReadARec detected EOF (i.e., it returned a false)
//          2) ReadARec detected an EMPTY location (i.e., it returned a true since
//              it is returning aLine, but aLine is "all-0-bits" which is determined
//              by checking the first 2 bytes of the record, the ID field, which could
//              never be 00 since that's an invalid ID)
//          3) ReadARec returned a good record (i.e., it returned a true and
//              aLine contains a good record)
// *************************************************************************************

using System;
using System.IO;
using RandomAccess;

namespace DumpProgram
{
    class DumpProgram
    {
        static void Main()
        {
            RanFile f = new RanFile();
            const string empty = "\0\0";         // ID FIELD "ALL 0 BITS" SO EMPTY LOC

            string aLine;
            int counter = 1;                    // LOCATIONS START AT 1 NOT 0

            while (f.ReadARec(out aLine))
            {
                Console.Write("{0:D2} >>  ", counter);
                if (aLine.Substring(0,2) == empty)
                    Console.WriteLine(". . . EMPTY . . .");
                else
                    Console.WriteLine("{0}", aLine);
                counter++;
            }
            f.CloseFile();
        }
    }
}
