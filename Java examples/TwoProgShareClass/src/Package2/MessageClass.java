// CLASS:  MessageClass    used by ProgramOne AND ProgramTwo
//          (though it's physically in PACKAGE2 which contains ProgramTwo's class)
// DESCRIPTION:  This class prints a simple message to the console.
// **************************************************************************************

package Package2;

public class MessageClass 
{
    public static void DisplayMessage(String program)
    {
        System.out.format("This message was printed by %s from Package2's MessageClass\n", program);
    }
}
