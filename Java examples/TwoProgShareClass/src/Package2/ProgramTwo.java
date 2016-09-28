// APPLICATION:  TwoProgShareClass
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This program also uses the MessageClass, but does not have to  
//      "import Package2.MessageClass;", since it is in the same package as MessageClass.
// In order to run Program2, you must set it as the main class (as in the previous demo).
//      To change from one to the other:
//          1) Go to File: Project Properties
//          2) Under Categories, click Run
//          3) Look for Main Class: click Browse
//          4) Choose from the list - Package2:ProgramTwo
//      Now when you run, the MessageClass method is being called from ProgramTwo.
// **************************************************************************************

package Package2;

public class ProgramTwo 
{
    public static void main(String[] args) 
    {
        MessageClass.DisplayMessage("Program Two");        
    }
}
