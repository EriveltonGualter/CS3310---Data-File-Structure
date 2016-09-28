// APPLICATION:  TestDriver including       PROGRAM:  TDriverProgram
//                                          PROGRAM:  TDisplayProgram
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This program calls the TDisplayProgram multiple times to test it with a
//      variety of different test parameters.  TDisplayProgram's Main takes a stringArray 
//      as its argument.  In this example, only 1 parameter is needed.  But the array 
//      makes it possible to send in multiple parameters.        
// **************************************************************************************

package driver;

import java.io.*;

class TDriverProgram 
{
    public static void main(String[] args) throws FileNotFoundException, IOException
    {
        //Remember - this does not have to be an array - it's only used to demonstrate 
        //that you can use arrays to pass multiple parameters
        String[] paramArgs = new String[1];
        
        for (int i = 1; i <= 4; i++)
        {
            paramArgs[0] = "InFile" + i;
            display.TDisplayProgram.main(paramArgs);
        }
    }
}
