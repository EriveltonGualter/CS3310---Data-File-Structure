// APPLICATION:  BinaryReaderWriter     PROGRAM:  DisplayProgram
// AUTHOR:  D. Kaminski
// DESCRIPTION:  (see detailed notes about the app at the top of CreateProgram)
//      This program displays the binary file.  It relies on BinFile class's ReadARec
//      method to:
//          - actually READ the record
//          - SEPARATE it into fields and send those back as OUT parameters
//          - RETURN the EOF value as a bool
// NOTE:  This program doesn't really need to access the numeric fields as int or float,
//      but it is shown here for demonstration purposes.
//***************************************************************************************

using System;
using BinRWClasses;

namespace DisplayTheFile
{
    class DisplayProgram
    {
        static void Main()
        {
            BinFile theFile = new BinFile('R');              // R for ReadMode

            int id;
            string name;
            float gpa;

            int counter = 1;

            while (theFile.ReadARec(out id, out name, out gpa))
            {
                Console.Write("{0:D2} > ", counter);
                if (name == "")
                    Console.WriteLine("empty");
                else
                    Console.WriteLine("id: {0}, name: {1}, gpa: {2}", id, name, gpa);
                counter++;
            }
            theFile.FinishWithObject();
        }
    }
}
