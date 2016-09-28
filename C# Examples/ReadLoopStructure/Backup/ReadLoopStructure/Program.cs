// PROGRAM:  ReadLoopStructure
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This program demonstrates 2 (+1) read-loop structures (in pseudocode):
//          1) Read-Process:            while (! EOF)
//                                      {   Read in 1 record
//                                          Process that record
//                                      }
//          2) Process-Read with a priming-Read:    Read in 1 record
//                                                  while (! EOF)
//                                                  {   Process that record
//                                                      Read in another record
//                                                  }
//      plus a shorter alternative version for these:
//                                      while (Read a record & check it's ! EOF)
//                                      {   Process that record
//                                      }
// WHY IS THIS IMPORTANT?  If the wrong loop structure is used, based on the particular
//      method/command used for reading and for EOF-checking, then the program either
//      processes the LAST RECORD TWICE or it DOESN'T PROCESS THE LAST RECORD AT ALL.  
// IMPORTANT IN ANY LANGUAGE:  This issue applies across all languages, not just C#.  So
//      when using a new language (or a new C# "Read" method) for file processing, check
//      how its "Read" command/method/function and its EOF-checking work, so you can use
//      the appropriate loop structure.  Many languages have include both types.
//      Similarly, when manually processing a result set from a database query (with a
//      cursor), check which structure is appropriate to use.
// NOTE:  "Read" here refers to whatever command/method/function is used to "input a
//      record", whether part of the language's library, or programmer-defined:  Read,
//      ReadLine, GetLine, GetNBytes, ReadARec, etc.  Also, the same method name may
//      behave differently depending on the class it's in, e.g., in C#, StreamReader,
//      FileStream, BinaryReader).
// WHICH ONE TO USE:
//      The appropriate one is based on when the EOF-test-condition becomes true.
//      Is it:
//          1) on reading the final good record (i.e., "peek ahead" approach)
//              --> use Read-Process    (so there are N reads altogether)
//      OR  2) on reading past EOF (i.e., "read failed" or "0 bytes actually read in")
//              --> use Process-Read    (so there are N+1 reads altogether)
//      This depends on:
//          - which "Read" method is used (e.g., StreamReader's ReadLine?  FileStream's
//              Read? a user-defined ReadARec?)
//          - what that method returns (e.g., the record?  the number of bytes read?
//              a bool to indicate EOF or not?)
//          - what the method's side effects are (e.g., on an "EOF-switch like
//              StreamReader's EndOfStream method)
//      Examine the 4 main code examples below to see how this works with C#.
// ALTERNATIVE/SHORTER VERSION:  Programmers will often use a shorter version of the
//      loop structure, doing 2 things in the while's condition:
//          1) reading in a record
//          2) testing whether it's EOF or not (and thus exit the loop or not)
//      This is only possible if the "Read" returns a value that can be checked for the
//      EOF condition.
//          - Since FileStream's Read returns the number of bytes read,
//              this can be checked for ! 0.
//          - Since StreamReader's ReadLine returns the record read (i.e., string aLine),
//              this can be checked for ! null.
// FILE PROCESSING IN GENERAL:  When processing a file (with sequential access), one
//      generally only needs a single record in memory at once - NOT THE WHOLE FILE.
//      A single record is read in, it's processed in some way, then it is no longer
//      needed. So, there should only be a SINGLE STORAGE LOCATION for storing a record,
//      and a new record is read into that location, replacing the prior contents.
// SPECIAL CASE ISSUE:  Always use a PREtest loop, not a POSTtest loop.  This allows for
//      the special case where there are no records in the file (e.g., MacDonald's sales
//      for Christmas Day).
// DISCLAIMER:  Since this is a DEMO program, I have written the 6 methods as distinct
//      chunks of code.  If an actual app needed to process a file 6 times, efficiency
//      dictates that the program reads through the file ONCE, and for each record read,
//      calls the various "process-the-record" methods for that record.  Also, if several
//      methods had such commonalities as these do, good programming practice dictates
//      that a single more generic method be used and called multiple times with 
//      appropriate parameters.
// ************************************************************************************
// THE FILE:  (stored in the main project folder):
//      01Kopinski3.94     (with a <CR><LF> at the end of this line)
//      02Patel   3.45     (with a <CR><LF> at the end of this line)
//      03DoeJones2.97     (with a <CR><LF> at the end of this line)
// ************************************************************************************

using System;

namespace ReadLoopStructure
{
    class Program
    {
        static void Main()
        {
            //                      NOTE:  No object declarations
            //                      NOTE:  the call is CLASS.Method, not OBJECT.Method
            //                              since these are STATIC methods
            Console.WriteLine("Using StreamReader's ReadLine");

            Console.WriteLine("\nRead-Process Loop");
            StrRdrLoops.DoReadProcessLoop();
            Console.WriteLine("\nProcess-Read Loop (with priming Read) - BAD");
            StrRdrLoops.DoProcessReadLoop();
            Console.WriteLine("\nSHORTER version of Read-Process");            
            StrRdrLoops.DoShorterLoop();

            Console.WriteLine("------------------------------------------------\n");

            Console.WriteLine("Using FileStream's Read");

            Console.WriteLine("\nProcess-Read Loop (with priming Read)");
            FileStrLoops.DoProcessReadLoop();
            Console.WriteLine("\nRead-Process Loop - BAD");
            FileStrLoops.DoReadProcessLoop();
            Console.WriteLine("\nSHORTER version of Process-Read (with priming Read)");
            FileStrLoops.DoShorterLoop();
        }
    }
}
