// APPLICATION:  AppHas2Prog
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This app contains 2 separate PROGRAMS - each program consists of a 
//      simple message printed to the console.
// THE USER RUNS THE PROGRAMS:  Each program is run MANUALLY by the user - there is NO
//      overall DRIVER to automate this.  (That's shown in a later demo example).
// TO CREATE A 2ND PROGRAM IN THIS APP (in a seperate package):
//      1) Open the Projects window on the left hand side of the window (if it's not
//          already open)
//      2) Right-click on the project name (AppHas2Prog), select New, then Java Package
//      3) Name the package (Program2)
//      4) Now right-click the package, select New, then Java Main Class
//      5) Name your class (Program2)
// EXECUTING THE PROGRAMS:  "Run Main Project" will only execute ONE program, generally
//          the first one created, which is automatically set as the main class.
//      To change from one to the other:
//          1) Go to File: Project Properties
//          2) Under Categories, click Run
//          3) Look for Main Class: click Browse
//          4) Choose from the list - Package2:Program2
//      Now when you run, you will see the output from Program 2 ONLY.
// **************************************************************************************

package Package1;

public class Program1 
{
    public static void main(String[] args) 
    {
        System.out.println("Hello from Program 1!");
    }
}
