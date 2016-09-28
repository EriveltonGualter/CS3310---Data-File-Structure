// CLASS:  OutputRec - used by OutputFile CLASS
// A RECORD:   Fixed-length because of fixed-length fields.
// FIELDS:  The same fields as the InputRec except the fields are in a DIFFERENT order:
//          name (14 char's), gpa (4 char's), major (4 char's), id (4 char's), 
//                      input record:     11Li            Math3.21<CR><LF>
//                      output record:  Li            3.21Math  11<CR><LF>
//          (where <CR><LF> are 2 bytes:  CarriageReturn & LineFeed)
//      All are strings since this is a TEXT file.
//      But these are FIXED-LENGTH STRINGS vs. variable-length strings.
//          So an id of 11 is really "  11" (of length 4) and not "11".
// **************************************************************************************

using System;
using System.IO;

namespace FileClRecClFixLenRec
{
    class OutputRec
    {
        // *************************  DECLARATIONS  *************************************
        public static int count = 0;

        // *************************  ATTRIBUTES & PROPERTIES  **************************
        // Automatic properties are used since there was no special processing to put
        // inside them.  These will set up the attributes and generate the basic default
        // get/set procedures for the 3 variables.
        // ******************************************************************************
        public string Id { get; set; }
        public string Name { get; set; }
        public string Major { get; set; }
        public string Gpa { get; set; }

        // ******************************************************************************
        // This returns true if ReadARec successfully read a record.
        // It returns false if the ReadLine failed because it read past the EOF.
        // NOTE:  The sizes of the 4 fields need not be specified since they are already
        //      the appropriate fixed-length strings - e.g., Id is size 4 (e.g., "  11").
        //      Examine the OutputFile.txt in Notepad to demonstrate this to yourself.
        // ******************************************************************************
        public void WriteARec(StreamWriter outFile)
        {
            outFile.WriteLine("{0}{1}{2}{3}", Name, Gpa, Major, Id);
            count++;
        }
    }
}
