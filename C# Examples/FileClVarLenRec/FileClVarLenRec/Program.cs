// PROGRAM:  FileClVarLenRec
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This project demonstrates 2 concepts:
//      1) a FILE CLASS for each file, but NO RECORD CLASS for the file's record-type.
//          ReadARec and WriteARec methods are part of their file's class.  This is done
//          because there is little actual record manipulation/handling (though this is
//          not, technically, a full OOP approach).  Alternatively, a separate record
//          class could have been used for each file - this is used in the next demo
//          project, FileClRecClVarLenRec.         
//      2) VARIABLE-LENGTH RECORDS with VARIABLE-LENGTH FIELDS vs. fixed-length records/
//          fields (as used in the next demo project, FileClRecClVarLenRec).
//          This necessitates using field-separators, typically a comma, and is referred
//          too as a CSL (Comma-Separated-List) file.
//      NOTE:  Options 1 and 2 above are independent choices, as are their alternative
//          options used in the next demo project.  Either could be used with the other.
// THE FILES:  The input and output files are simple sequential access CSL text files,
//      e.g., the input file:       11,Li,Math,3.21
//                                  9876,O'Leary,Math,2.00
//                                  123,Kopinski-Jones,CS,3.97
//      The fields in the output file are in a DIFFERENT ORDER than the input file
//          (necessitating separating a line into individual fields).
// OOP APPROACH:  The actual file/record-handling work is hidden from Main, and provided
//      as services (methods) in the file's class.
//      NOTE:  Some languages provide built-in commands/methods which automatically DO
//          much of the file/record manipulation/conversion, so a simple method-call
//          would be used right in the program itself rather than setting up a separate
//          file class.
// *************************************************************************************
// NOTE:  No   using System.IO   in this Program class since Main does no file handling.
// READ LOOP STRUCTURE:  The ReadARec method (in this project) is written so that its
//      EOF-check is based on the "Read-past-EOF" (vs. "Peek-ahead").  That is, ReadARec
//      returns a bool:
//          - true if it DID successfully ReadARec,
//          - false if it failed to read in a record - i.e., it read past EOF
//      So, the loop structure needs to be a ProcessRead loop (with priming Read), or
//          the equivalent "Read-in-the-While-condition" loop structure.
// PROGRAM STRUCTURE:  Notice the HIERARCHICAL structure of the program.
//      - Main is the DRIVER/CONTROLLER (boss) which calls methods in the other classes.
//      - Methods in other classes just provide SERVICES (workers) which the controller
//          calls as needed.  Methods in "sibling" classes do NOT call methods in a
//          sibling's class (though they could call methods in a "child" class if 
//          there were any, which there aren't in this project).
// *************************************************************************************

using System;

namespace FileClVarLenRec
{
    class Program
    {
        static void Main()
        {
            InputFile inFile = new InputFile();
            OutputFile outFile = new OutputFile();

            string id;
            string name;
            string major;
            string gpa;

            while (inFile.ReadARec(out id, out name, out major, out gpa))
            {
                outFile.WriteARec(id, name, major, gpa);
            }
            inFile.FinishWithFile();
            outFile.FinishWithFile();
        }
    }
}
