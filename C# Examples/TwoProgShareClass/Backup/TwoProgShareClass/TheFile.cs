// CLASS:  TheFile    used by CreateProgram and by DisplayProgram
//                      (though it's physically in NAMESPACE:  TwoProgShareClass
//                          which contains CreateProgram class)
// DESCRIPTION:  This class handles all interaction with the SharedFile.txt.
//      CreateProgram uses WriteARec; DisplayProgram uses ReadARec.
// ERROR-CHECKING:  2 constructors are needed so that error-checking can be done to
//      ensure that DisplayProgram isn't run BEFORE CreateProgram.
// **************************************************************************************

using System;
using System.IO;
using System.Text;

namespace TwoProgShareClass
{
    public class TheFile
    {
        // *************************  "GLOBAL" DECLARATIONS  ****************************
        // These aren't instance variables.  They're declared here to make them
        // accessible to all methods in this class.
        // ******************************************************************************
        private static FileStream f;
        private const int REC_SIZE = 2 + 8 + 4 + 2;
        private static string fileNameWithPath = ".//..//..//..//SharedFile.txt";

        // *************************  CONSTRUCTOR  **************************************
        public TheFile()
        {
            f = new FileStream(fileNameWithPath, FileMode.Create, FileAccess.Write);
        }
        public TheFile(string s)
        {
            if (File.Exists(fileNameWithPath))
                f = new FileStream(fileNameWithPath, FileMode.Open, FileAccess.Read);
            else
                Console.WriteLine("ERROR:  CREATE the file before DISPLAYING it");
        }
        // *************************  METHODS  ******************************************
        // ******************************************************************************
        // Since aLine that's send in has no <CR><LF>, it's appended before conversion
        // to the byteArray buffer, since FileStream has a Write method, but no WriteLine
        // ******************************************************************************
        public void WriteARec(string aLine)
        {
            byte[] buffer = new byte[REC_SIZE];

            aLine = aLine + '\r' + '\n';                // ADD <CR><LF>

            for (int i = 0; i < aLine.Length; i++)      // CONVERT STRING TO BYTEARRAY
                buffer[i] = BitConverter.GetBytes(aLine[i])[0];

            f.Write(buffer, 0, REC_SIZE);
        }
        // ******************************************************************************
        // This method sends back 5 things to the caller:
        // 1) a true if an actual record has been read - a false if it "read past EOF"
        // 2) the record as aLine which NOT contain the <CR><LF> since that's what the
        //      caller wants, "just the DATA".
        // NOTE:  The extra Read of the 2 bytes is needed to move the "file position
        //      pointer" along 2 bytes so it's positioned at the start of the next
        //      record.
        // ******************************************************************************
        public bool ReadARec(out string aLine)
        {
            byte[] buffer = new byte[REC_SIZE];

            int numBytesRead = f.Read(buffer, 0, REC_SIZE - 2);     // JUST THE DATA

            if (numBytesRead != 0)
            {
                aLine = Encoding.Default.GetString(buffer);
                f.Read(buffer, 0, 2);                                // THE <CR><LF>                        
                return true;
            }
            else
            {
                aLine = "";
                return false;
            }
        }
        // ******************************************************************************
        // ******************************************************************************
        public void CloseTheFile()
        {
            f.Close();
        }
    }
}
