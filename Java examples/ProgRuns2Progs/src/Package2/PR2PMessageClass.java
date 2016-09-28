// CLASS:  MessageClass in NEW APPLICATION:  ProgRuns2Prog
//      - see top comment in DriverProgram.
// AUTHOR:  D. Kaminski
// NO CHANGES were made to this code from the earlier app.
// **************************************************************************************

package Package2;

public class PR2PMessageClass 
{
    public static void DisplayMessage(String program)
    {
        System.out.format("This message was printed by %s from Package2's MessageClass\n", program);
    }
}
