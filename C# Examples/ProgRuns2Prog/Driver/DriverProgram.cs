// APPLICATION:  ProgRuns2Prog      NEW PROGRAM:  DriverProgram
// ORIGINAL APPLICATION:  TwoProgShareClass had:     PROGRAM:  CreateProgram
//                                                   PROGRAM:  DisplayProgram
//                                                   CLASS:    TheFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This app is the same as the previous example, TwoProgShareClass.
//      However, in that example, the user had to run each program separately, manually.
//      This app adds a DRIVER PROGRAM which automates this process, running the 2
//      programs automatically (in in the correct order).
// HOW TO ADD THIS NEW PROGRAM:
//      - I made a copy of TwoProgShareClass and renamed the top-level folder:
//          ProgRuns2Prog
//      - After opening the .sln file of the new app, for clarity, I changed the overall
//          solution name in Solution Explorer to ProgRuns2Prog
//      - To add another project/program I did:
//              1) right-click on the Solution in the Solution Explorer
//              2) select Add, then New Project
//              3) select Console Application and name it Driver
//              4) for clarity, I renamed the new Program.cs (in Driver) in the Solution
//                  Explorer - calling it DriverProgram (& Yes to "You are renaming...")
// HOW TO GIVE THIS NEW PROGRAM ACCESS TO THE OTHER 2 PROGRAMS:
//      - add PUBLIC to the class and to the Main method in both CreateProgram and
//          DisplayProgram
//      - (while viewing the DriverProgram.cs code)
//          under Project drop-down menu, select Add Reference
//      - under the Projects tab, select TwoProgShareClass and then do that for
//          DisplayTheFile
// TO RUN THE DRIVER:
//      - for DriverProgram do:  Project / Set As StartUp Project
//      - do Debug / Start without Debugging (as normal)
// **************************************************************************************

using System;

namespace Driver
{
    class DriverProgram
    {
        static void Main()
        {
            TwoProgShareClass.CreateProgram.Main();
            DisplayTheFile.DisplayProgram.Main();
        }
    }
}
