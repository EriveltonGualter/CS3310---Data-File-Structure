// APPLICATION:  TwoProgShareClass
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This application is functionally the same as the prior demo example
//      (AppHas2Prog) except that the message which is displayed is defined in a 
//      separate class.  Both programs utilize the class, although the class is
//      defined within the ProgramTwo's package.  So ProgramOne must be 
//      allowed to reference that class.  
// HOW TO GIVE THIS PROGRAM ACCESS TO A CLASS IN ANOTHER PACKAGE:
//      1) Add "import PackageName.ClassName;" to the class
//          (in this case "import Package2.MessageClass;")
//      2) Use available public methods within the class, prefixed with the ClassName.
//          (ex/ MessageClass.DisplayMessage(); )
// **************************************************************************************

package Package1;

import Package2.MessageClass;

public class ProgramOne 
{
    public static void main(String[] args) 
    {
        MessageClass.DisplayMessage("Program One");
    }
}
