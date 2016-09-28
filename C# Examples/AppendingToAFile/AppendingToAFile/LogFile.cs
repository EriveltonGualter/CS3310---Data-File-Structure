// CLASS:  LogFile              in PROGRAM:  AppendingToAFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This class handles all accessing of the LogFile including opening it,
//      writing to it and closing it.  It checks with the user to determine whether to
//      open it in APPEND mode or OVER-WRITE mode.
// *************************************************************************************

using System;
using System.IO;

namespace AppendingToAFile
{
    class LogFile
    {
        // -------------------------- DECLARATIONS -------------------------------
        // This is declared HERE rather than in the constructor so it's accessible
        //      from any method in this class.
        // -----------------------------------------------------------------------
        static StreamWriter logFile;
        
        // --------------------------- CONSTRUCTOR -------------------------------
        // This "open" happens only ONCE, BEFORE any writing is done.
        // The true says to create the file if it doesn't exist, or APPENDING to
        //      it if it DOES exist.
        // -----------------------------------------------------------------------

        public LogFile()
        {
            Console.Write("Do you want to APPEND to or OVER-WRITE the LogFile?" +
                "(Enter A or O):  ");
            
            string option = Console.ReadLine();
            
            if (option == "A" || option == "a")
                logFile = new StreamWriter(".//..//..//..//LogFile.txt", true);
            else
                logFile = new StreamWriter(".//..//..//..//LogFile.txt");

            Console.WriteLine("OK, LogFile is now opened");
        }
        // ---------------------------- METHODS ----------------------------------
        public void WriteThis(string message)
        {
            logFile.WriteLine(message);
            Console.WriteLine("OK, this was written to LogFile:  {0}", message);
        }
        // -----------------------------------------------------------------------
        public void CloseFile()
        {
            logFile.Close();
            Console.WriteLine("OK, LogFile is now closed");
        }
    }
}
