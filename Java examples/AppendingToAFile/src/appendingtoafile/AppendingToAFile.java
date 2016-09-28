// PROGRAM:  AppendingToAFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This demonstrates using a separate class for all handling of a file,
//      including opening, writing to and closing it.   
// *************************************************************************************

package appendingtoafile;

import java.io.*;

public class AppendingToAFile 
{
    public static void main(String[] args) throws IOException 
    {
        LogFile logFileObj = new LogFile();
        
        //to read user input(more than one byte) we can redirect stdin
        BufferedReader in = new BufferedReader(new InputStreamReader(System.in));

        String name;
        String longerMessage;

        System.out.print("Enter a name:  ");
        name = in.readLine();
        logFileObj.WriteThis(name);

        System.out.print("Enter another name:  ");
        name = in.readLine();
        longerMessage = String.format("The 2nd name is:  %s", name);
        logFileObj.WriteThis(longerMessage);

        logFileObj.CloseFile();

        System.out.println("\n\nLook at LogFile.txt in NotePad");
        System.out.println("\nRUN THIS PROGRAM AGAIN a couple times\n");
    }
}
