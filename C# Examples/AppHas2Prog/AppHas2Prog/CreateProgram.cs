// APPLICATION:  AppHas2Prog        PROGRAM 1:  CreateProgram
//                                  PROGRAM 2:  DisplayProgram
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This app contains 2 separate PROGRAMS - one Creates "SharedFile.txt",
//      the other reads & displays it on the console.
// THE USER RUNS THE PROGRAMS:  Each program is run MANUALLY by the user - there is NO
//      overall DRIVER to automate this.  (That's shown in a later demo example).
//      The user is supposed to run CreateProgram before running DisplayProgram.
//      However, DisplayProgram checks whether the SharedFile exists before proceeding.
// TO CREATE A 2ND PROGRAM IN THIS APP:
//      1) right-click on the Solution:  'AppHas2Prog' in the Solution Explorer window
//      2) select Add, then New Project
//      3) select Console Application and name it DisplayTheFile
//      4) both programs have a class called Program, and both  files are called
//          Program.cs (hence the tabs in the IDE for the 2 code blocs are the same).
//          For clarity, change both of these duplications by doing:
//              - in the Solution Explorer window, click on Program.cs and rename it
//                  (and click the Yes button to "You are renaming...")
//              - do the same thing for the other program/
// COMPILING THE PROGRAMS:  "Build / Build Solution" will compile both programs.
// EXECUTING THE PROGRAMS:  "Debug / Start without Debugging" will only execute ONE
//      program, whichever is set as the "Startup Project".  (Check to to see which
//          class name is bolded in the Solution Explorer). 
//      To change from one to the other, while viewing the desired program file, do:
//              "Project / Set as Startup Project"
//              then run that program as normal.
//      Note that a new Console window opens for each program run.
// DEMONSTRATION:
//      1) delete SharedFile.txt if it exists
//      2) run DisplayProgram - note that it gives an ERROR message 
//      3) run CreateProgram, then run DisplayProgram
// **************************************************************************************

using System;
using System.IO;

namespace AppHas2Prog
{
    class CreateProgram
    {
        static void Main()
        {
            string path = ".//..//..//..//";

            StreamReader inFile = new StreamReader(path + "InFile.txt");
            StreamWriter outFile = new StreamWriter(path + "SharedFile.txt");

            string aLine;
            
            while (! inFile.EndOfStream)
            {
                aLine = inFile.ReadLine();
                outFile.WriteLine(aLine);
            }
            inFile.Close();
            outFile.Close();

            Console.WriteLine("OK, DONE creating SharedFile.txt");
        }
    }
}
