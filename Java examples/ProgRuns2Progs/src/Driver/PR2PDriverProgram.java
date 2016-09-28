// APPLICATION:  ProgRuns2Prog      NEW PROGRAM:  PR2PDriverProgram
// ORIGINAL APPLICATION:  TwoProgShareClass had:     PROGRAM:  ProgramOne
//                                                   PROGRAM:  ProgramTwo
//                                                   CLASS:    MessageClass
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This app is the same as the previous example, TwoProgShareClass.
//      However, in that example, the user had to run each program separately, manually.
//      This app adds a DRIVER PROGRAM which automates this process, running the 2
//      programs automatically (in in the correct order).
// HOW TO ADD THIS NEW PROGRAM:
//      - I copied the steps to make TwoProgShareClass but named it ProgRuns2Progs:
//          (I also renamed all the files (and thus classes), but ONLY to differentiate
//           between the two programs's classes - not because it was necessary).
//      - To add another package & program I did:
//              1) right-click on the Project in the Project Explorer
//              2) select New, then Java Package, and name it Driver
//              3) right-click the Driver Package that was just created
//              4) select New, then Java Main Class, and name it PR2PDriverProgram
// HOW TO GIVE THIS NEW PROGRAM ACCESS TO THE OTHER 2 PROGRAMS:
//      - Call the package-name.program-name.main()
//          (Ex/ "Package1.PR2PProgramOne.main(args);"
//      - Conversely, you could "import Package1.PR2PProgramOne;", then simply call
//          "PR2PProgramOne.main(args);"
// TO RUN THE DRIVER: (as shown in previous demos)
//          1) Go to File: Project Properties
//          2) Under Categories, click Run
//          3) Look for Main Class: click Browse
//          4) Choose from the list - Driver.PR2PDriverProgram
//      - Now "Run Main Project" (F6)
// **************************************************************************************

package Driver;

public class PR2PDriverProgram 
{
    public static void main(String[] args) 
    {
        Package1.PR2PProgramOne.main(args);
        Package2.PR2PProgramTwo.main(args);
    }
}
