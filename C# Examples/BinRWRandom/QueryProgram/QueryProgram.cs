// APPLICATION:  BinRWRandom        PROGRAM:  QueryProgram
// AUTHOR:  D. Kaminski
// DESCRIPTION:  (see detailed notes about the app at the top of CreateProgram).
//      This program displays individual records from the binary file based on hard-coded
//      requests of specific id values (i.e., the primary key field on which the driect
//      address file is based.  It thus has to use BinFile class's RANDOM ACCESS version
//      of the ReadARec method, where RRN has to be sent in, to:
//          - calculate the offset
//          - do the seek
//          - actually READ the record
//          - SEPARATE it into fields and send those back as OUT parameters
//          - RETURN a bool value to indicate that the record with that id was
//              NOT IN THE FILE
//***************************************************************************************

using System;
using BinRWClasses;

namespace QueryProgram
{
    class QueryProgram
    {
        static void Main()
        {
            BinFile theFile = new BinFile('R');              // R for ReadMode

            Do1Query(theFile, 2);                   // ID 2 - OK
            Do1Query(theFile, 5);                   // ID 5 - OK
            Do1Query(theFile, 3);                   // ID 3 - not in file - a hole
            Do1Query(theFile, 4);                   // ID 4 - OK
            Do1Query(theFile, 35);                  // ID 35 - not in file - past EOF

            theFile.FinishWithObject();
        }
        //*******************************************************************************
        static void Do1Query(BinFile theFile, int targetId)
        {
            int id;
            string name;
            float gpa;

            bool validRec = theFile.ReadARec(out id, out name, out gpa, targetId);

            Console.Write("target ID:  {0} >> ", targetId);

            if (validRec)
                Console.WriteLine("id: {0:D2}, name: {1}, gpa: {2}", id, name, gpa);
            else
                Console.WriteLine("empty");
        }
    }
}