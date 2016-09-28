// CLASS:  StrRdrLoops - used by ReadLoopStructure
//      - a STATIC class of STATIC methods, called by Main
// DESCRIPTION:  The methods here demonstrate the read loop structure to be used with
//              FileStream's Read and using numBytesRead = 0 as the EOF-check.
//      1st one) ProcessRead loop works correctly
//                  (the OPPOSITE of what works for StreamReader and EndOfStream)
//      2nd one) ReadProcess loop is WRONG - IT PROCESSES THE LAST RECORD TWICE
//      3rd one) Shorter loop works correctly
// WHAT'S A STATIC CLASS? (see note in the StrRdrLoops class)
// ************************************************************************************

using System;
using System.IO;
using System.Text;                      // needed for Encoding

namespace ReadLoopStructure
{
    static class FileStrLoops
    {
        // *************************** DECLARATIONS ***********************************
        // These variables/constant are used in several methods in this class, and so
        //      are defined here rather than as local variables in all 3 methods below.
        // ****************************************************************************
        private static string path = ".//..//..//..//";
        private static string fileName = "InFile.txt";

        private const int REC_SIZE = 2 + 8 + 4 + 2;   // id name gpa <CR><LF>
        private static byte[] aLineAsAByteArray = new byte[REC_SIZE];
        private static string aLineAsAString;

        // *************************** PUBLIC METHODS *********************************
        // ****************************************************************************
        public static void DoProcessReadLoop()                 // G O O D
        {
            FileStream inFile = new FileStream(path + fileName,
                                                FileMode.Open, FileAccess.Read);

            int numBytesRead = inFile.Read(aLineAsAByteArray, 0, REC_SIZE);
            while (numBytesRead != 0)
            {
                aLineAsAString = Encoding.Default.GetString(aLineAsAByteArray);
                ProcessTheRecord(aLineAsAString);

                numBytesRead = inFile.Read(aLineAsAByteArray, 0, REC_SIZE);
            }
            inFile.Close();
        }
        // ****************************************************************************
        public static void DoReadProcessLoop()                // B A D
        {
            FileStream inFile = new FileStream(path + fileName,
                                                FileMode.Open, FileAccess.Read);

            int numBytesRead = REC_SIZE;          // NEEDED SO WHILE LOOP STARTS
            while (numBytesRead != 0)
            {
                numBytesRead = inFile.Read(aLineAsAByteArray, 0, REC_SIZE);

                aLineAsAString = Encoding.Default.GetString(aLineAsAByteArray);
                ProcessTheRecord(aLineAsAString);
            }
            inFile.Close();
        }
        // ****************************************************************************
        public static void DoShorterLoop()                 // G O O D
        {
            FileStream inFile = new FileStream(path + fileName,
                                                FileMode.Open, FileAccess.Read);

            while (inFile.Read(aLineAsAByteArray, 0, REC_SIZE) != 0)
            {
                aLineAsAString = Encoding.Default.GetString(aLineAsAByteArray);
                ProcessTheRecord(aLineAsAString);
            }
            inFile.Close();
        }
        // *************************** PRIVATE METHOD *********************************
        // ****************************************************************************
        private static void ProcessTheRecord(string theLine)
        {
            int id = Convert.ToInt32(theLine.Substring(0, 2));
            string name = theLine.Substring(2, 8);
            float gpa = Convert.ToSingle(theLine.Substring(10, 4));
            Console.WriteLine("{0:D2}  {1}  {2}", id, name, gpa);
        }
    }
}
