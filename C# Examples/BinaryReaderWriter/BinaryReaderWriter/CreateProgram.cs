// APPLICATION:  BinaryReaderWriter     PROGRAM:  CreateProgram
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This application creates a binary file and then reads that binary file
//          - handled in 2 separate programs within this app;
//          - and both programs use sequential access of the file;
//          - and all actual binary file handling is done within the BinFile class.
//              (NOTICE HOW THAT SIMPLIFIED THE 2 PROGRAMS - all issues/messiness/
//                  handling/... is buried within the BinFile class)
// NEW CONCEPTS:
//      1) This app uses the FCL's BinaryWriter and BinaryReader classes - used inside
//          the BinFile class.
//      2) BinFile class is in a SEPARATE PROJECT from either of the 2 PROGRAMS
//          (see "HOW TO..." instructions below).
//      3) A BINARY FILE (see notes at the top of BinFile class).
//
// INPUT FILE:
//      This is just a plain text file with no special handling.  So no separate class
//              is set up to handle it - the FCL's StreamReader's class is used to
//              handle it right here in CreateProgram's Main.
//      It's a csv file and so has variable-length records with variable-length fields:
//              id, name, gpa
//
// HOW TO SET UP A SEPARATE PROJECT FOR SHARED CLASSES (vs. for another program):
//      - right-click on Solution '...' in the Solution Explorer
//      - select Add . . . New Project
//      - select Class Library and call it:  BinRWClasses
//          (This is a general namespace where SEVERAL CLASSES could be placed if there
//              were several which were to be shared among various programs within
//              this app.  For this example, there's only 1, BinFile class).
//      - rename Class1.cs (in Solution Explorer) to BinFile 
//          and select YES when asked about consistent renaming
//      - add a using BinRWClasses; at the top of the 2 programs which need to access it
//
// NOTE:  After adding another project for the DisplayProgram, I
//      - renamed both Program.cs entries (in Solution Explorer) to CreateProgram and
//          DisplayProgram (and selected YES when asked about consistent renaming)
//      - gave both programs the right to reference the BinRWClasses classes/methods/...
//          by doing:
//              - right-click on the project name (in Solution Explorer)
//              - select Add Reference
//              - select the Project tab
//              - select BinRWClasses
//
// DEBATABLE:  Who should be responsible for:
//      1) taking the inputLine apart into fields and converting them into their
//          proper (binary) data types:  Create program or a method in BinFile class?
//          In this app it's left to a BinFile method.  Create program just passes in
//          a line of data (an input file's RECORD).
//          This could've instead been set up to have Create do either:
//              - the field-separating and send in those individual text string fields
//          OR  - the field-separating and the converting into numerica data types
//                  and sending those binary int/float/string/... fields to WriteARec
//      2) dealing with the binary file's individual fields on reading a record:
//          a method inside BinFile class or the Display program?
//          In this app BinFile's ReadARec sends back the individual fields in their
//          plain binary format (int's, float's...) and let's the Display program deal
//          with their individual fields as it needs to.
//***************************************************************************************

using System;
using System.IO;
using BinRWClasses;

namespace BinaryReaderWriter
{
    class CreateProgram
    {
        static void Main()
        {
            StreamReader inFile = new StreamReader(".\\..\\..\\..\\InputFile.txt");
            BinFile outFile = new BinFile('W');              // W for WriteMode

            string inputLine;

            while (! inFile.EndOfStream)
            {
                inputLine = inFile.ReadLine();
                outFile.WriteARec(inputLine);
            }

            Console.WriteLine("The Binary File was created.");
            Console.WriteLine("Run DumpProgram to view it.");
            Console.WriteLine("Also view it in a HexEditor: \n\t" +
                "(see XVI32 - BinaryFile.pdf in the app folder)."); 

            outFile.FinishWithObject();
            inFile.Close();
        }
    }
}
