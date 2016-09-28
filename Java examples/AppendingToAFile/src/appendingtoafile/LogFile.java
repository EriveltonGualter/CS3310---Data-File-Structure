// CLASS:  LogFile              in PROGRAM:  AppendingToAFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This class handles all accessing of the LogFile including opening it,
//      writing to it and closing it.  It checks with the user to determine whether to
//      open it in APPEND mode or OVER-WRITE mode.
// *************************************************************************************

package appendingtoafile;

import java.io.*;

class LogFile 
{
    // -------------------------- DECLARATIONS -------------------------------
    // This is declared HERE rather than in the constructor so it's accessible
    //      from any method in this class.
    // -----------------------------------------------------------------------
    static FileWriter file;
    static BufferedWriter logFile;
    //to read user input(more than one byte) we can redirect stdin
    BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
        
    // --------------------------- CONSTRUCTOR -------------------------------
    // This "open" happens only ONCE, BEFORE any writing is done.
    // The true says to create the file if it doesn't exist, or APPENDING to
    //      it if it DOES exist.
    // -----------------------------------------------------------------------

    public LogFile() throws IOException
    {
        System.out.print("Do you want to APPEND to or OVER-WRITE the LogFile?" +
            "(Enter A or O):  ");
            
        String option = in.readLine();
            
        if ("A".equals(option.trim()) || "a".equals(option.trim()))
            file = new FileWriter("LogFile.txt", true);
        else
            file = new FileWriter("LogFile.txt");
        
        logFile = new BufferedWriter(file);
        
        System.out.println("OK, LogFile is now opened");
    }
    // ---------------------------- METHODS ----------------------------------
    public void WriteThis(String message) throws IOException
    {
        logFile.write(message);
        logFile.newLine();
        System.out.format("OK, this was written to LogFile:  %s\n", message);
    }
    // -----------------------------------------------------------------------
    public void CloseFile() throws IOException
    {
        logFile.close();
        System.out.println("OK, LogFile is now closed");
    }
}
