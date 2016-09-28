// PROGRAM:  CreateSerBinFile       CLASS:  SerBinFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This class handles all file IO for the Serialized Binary File.
//      This class is much DIFFERENT than the BinFile class in the earlier example
//      (BinaryReaderWriter) which created a PLAIN Binary file:
//      - additional libraries are declared here to allow for serialization
//      - a BinaryFormatter object is declared
//      - the record is built from the fields by a methoddifferently (in WriteARec)
//      - the actual writing of the record is done by formatter.Serialize method
//          (in the BinaryFormatter class)
//***************************************************************************************

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;       // NOTE THIS
using System.Runtime.Serialization;                         // NOTE THIS

namespace CreateSerBinFile
{
    class SerBinFile
    {
        //*************************  STATIC MEMBERS  ************************************
        private static FileStream serBinFile;
        private static SerBinRec serBinRec;
        private BinaryFormatter formatter = new BinaryFormatter();

        //*************************  CONSTRUCTOR  ***************************************
        public SerBinFile(string fileName)
        {
            serBinFile = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        }
        //************************  CLOSE FILE  *****************************************
        public void CloseIt()
        {
            serBinFile.Close();
        }
        //*************************  WRITE A REC  ***************************************
        public void WriteARec(string aLine)
        {
                                        // SEPARATE THE FIELDS & CONVERT TO "BINARY"
            string[] field = aLine.Split(',');

            int id = Convert.ToInt32(field[0]);
            string name = field[1];
            float gpa = Convert.ToSingle(field[2]);

                                        // GATHER THE FIELDS FOR THE BINARY RECORD
            serBinRec = new SerBinRec(id, name, gpa);

                                        // WRITE THE RECORD TO FileStream, BUT FIRST
                                        //   SERIALIZE THE RECORD serBinRec OBJECT
            formatter.Serialize(serBinFile, serBinRec);
        }
    }
}
