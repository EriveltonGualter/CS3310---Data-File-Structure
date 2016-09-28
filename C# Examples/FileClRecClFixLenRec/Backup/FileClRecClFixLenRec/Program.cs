// PROGRAM:  FileClRecClFixLenRec
// AUTHOR:  D. Kaminski
// IMPORTANT:  The comments here are brief because it's assumed that you've already read 
//      and understood the previous demo example, FileClVarLenRec.
// DESCRIPTION:  This project demonstrates 2 concepts:
//      1) a FILE CLASS for each file, and a RECORD CLASS for the file's record-type.
//          This is done because there is somewhat more record manipulation/handling
//          than the prior example - so it's buried within the record's class.
//          [It uses a full OOP approach, but pays a slight run-time price because of
//          the taller "call/return hierarchy"].
//      2) FIXED-LENGTH RECORDS with FIXED-LENGTH FIELDS with NO field-separators since
//          the each exact field-length is known.
//          String-field values are left-justified within the field-size and space-
//          filled (or truncated) on the right.  Integer-field values are typically
//          right-justified and space-filled or zero-filled on the left - in this
//          example the output stays the same as the input data.
//      NOTE:  Again, options 1 and 2 above are independent decisions, as are their
//          alternative options used in the prior demo project.
// THE FILES:  The input and output files are simple sequential access text files,
//      e.g., the input file:         11Li            Math3.21
//                                  9876O'Leary       Math2.00
//                                   123Kopinski-JonesCS  3.97
//      The fields in the output file are in a DIFFERENT ORDER than the input file
//          (necessitating separating a line into individual fields).
// *************************************************************************************
// Main in this program is exactly the same as Main in the prior demo program.
// *************************************************************************************

using System;

namespace FileClRecClFixLenRec
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
