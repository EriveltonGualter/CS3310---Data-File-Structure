/* CLASS:  RanAccCkforEmpty
 * AUTHOR:  Dr. Kaminski
 * *****************************************************************************************
 */
using System;
using System.IO;

namespace RanAccCkforEmpty
{
    class Program
    {
        static void Main()
        {
            const int RAN_REC_SIZE = sizeof(int) + 8 + sizeof(float);

            StreamReader inFile = new StreamReader(".//..//..//..//DataFiles//InFile.txt");

            string inputLine;

            FileStream ranFile = new FileStream(".//..//..//..//DataFiles//RandomFile.bin",
                        FileMode.Create, FileAccess.ReadWrite);
            BinaryReader ranReader = new BinaryReader(ranFile);
            BinaryWriter ranWriter = new BinaryWriter(ranFile);     

            int id;
            char[] name = new char[8];
            float gpa;

            long offset;
            int RRN;
            int idInRanFile;
            bool pastEOF;

            //- - - - - - - - - - - - - - - - - - - - - - - - CREATE THE FILE - - - - - - - - -
            while (!inFile.EndOfStream)
            {
                inputLine = inFile.ReadLine();          // INPUT HAS <CR><LF>, SO ReadLine OK

                id = Convert.ToInt32(inputLine.Substring(0, 2));
                name = inputLine.Substring(2, 8).ToCharArray();
                gpa = Convert.ToSingle(inputLine.Substring(10, 4));

                RRN = id;                               // use ID for DIRECT ADDRESS
                
                offset = (RRN - 1) * RAN_REC_SIZE;
                ranFile.Seek(offset, 0);
                idInRanFile = 0;                // NEED SEPARATE FIELD FROM ID SINCE PLAIN
                                                //    ID ALREADY FILLED IN WITH INPUT DATA

                pastEOF = CheckIfEmptyLoc(ranReader, out idInRanFile);

                if (pastEOF || idInRanFile == 0)        // i.e., it's an EMPTY LOCATION
                {                
                    ranFile.Seek(offset, 0);
                    ranWriter.Write(id);
                    ranWriter.Write(name);
                    ranWriter.Write(gpa);
                    ranWriter.Flush();                  // SINCE BOTH READING & WRITING
                }
                else
                    Console.WriteLine("ERROR FOR ID {0} - LOCATION NOT EMPTY", id);
            }
            //- - - - - - - - - - - - - - - - - - - - - - - - PRINT THE FILE - - - - - - - - -\
           
            Console.WriteLine("\nOK, RandomFile.bin file created - Here it is:");
            Console.WriteLine("   (and check it in the HexEditor)");

            ranFile.Seek(0, 0);
            int i = 1;

            while (ReadARec(ranReader, out id, out name, out gpa))
            {
                if (id == 0)
                    Console.WriteLine("{0:00}>>  EMPTY LOCATION", i);
                else
                    Console.WriteLine("{0:00}>>  {1}  {2}  {3}",
                        i, id, new string(name), gpa);
                i++;
            }
            inFile.Close();
            ranFile.Close();

            Console.Write("\n\nHit ENTER to quit");
            Console.ReadLine();
        }
        //***************************************************************************
        static bool CheckIfEmptyLoc(BinaryReader file, out int id)
        {
            try
            {
                id = file.ReadInt32();
                return false;
            }
            catch
            {
                id = 0;
                return true;
            }
        }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        static bool ReadARec(BinaryReader file,
                        out int id, out char[] name, out float gpa)
        {
            try
            {
                id = file.ReadInt32();
                name = file.ReadChars(8);
                gpa = file.ReadSingle();
                return true;
            }
            catch
            {
                id = 0;
                name = null;
                gpa = 0.0f;
                return false;
            }
        }
    }
}
