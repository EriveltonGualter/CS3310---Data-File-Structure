// APPLICATION:  RandomAccess       PROGRAM:  QueryProgram
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This queries the random file using a small "driver" to test queries for
//      ID 1 through ID 8.  Some queries find good records, some find empty locations
//      (including BOTH kinds of "holes":  the "all-0-bits" and the "past EOF" cases).
// FILE ACCESS:  RANDOM ACCESS, so it calls the ReadARec method which requires an RRN.
// CASES: This program needs to distinguish between only 2 cases:
//          1) ReadARec detected an empty location (i.e., it returned a false)
//              which could be either an "all-0-bits" or a "past EOF" case
//          2) ReadARec returned a good record (i.e., it returned a true and
//              aLine contains a good record)
// *************************************************************************************

using System;
using System.IO;
using RandomAccess;

namespace QueryProgram
{
    class QueryProgram
    {
        static void Main() 
        {
            for (int i = 1; i <= 8; i++)
                QueryForId(i);                      // ID IS THE 1st 2 DIGITS IN THE REC
        }
        // *****************************************************************************
        static void QueryForId(int RRN)
        {
            RanFile f = new RanFile();

            string aLine;

            bool gotARec = f.ReadARec(RRN, out aLine);

            if (!gotARec)
                Console.WriteLine("Location {0:D2} is EMPTY", RRN);
            else
                Console.WriteLine("Location {0:D2} contains:  {1}", RRN, aLine);

            f.CloseFile();
        }
    }
}
