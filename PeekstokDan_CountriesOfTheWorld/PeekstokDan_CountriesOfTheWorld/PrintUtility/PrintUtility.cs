/* Project:         Countries of the World
 * Module:          PrintUtility
 * Programmer:      Dan Peekstok
 *
 * This module prints the contents of the backup file to the Log file.
 * It is written without OOP because of the simplisity of the code
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PrintUtility
{
    public class PrintUtility
    {
            //private variables

        //holds the data of each country as it is read in
        private static string[] countryArray = new string[7];
        //flag that indicates the separation of the DataTable and the NameIndex
        private static bool indexFlag;
        public static void Main()
        {
            Console.WriteLine("print");
            StreamWriter writer;

            //truncates the log if it hasn't been done yet since the start of the program
            //if it has been done, it just opens it in append mode
            if (Setup.Setup.LogTruncated)
            {
                writer = File.AppendText("Log.txt");
                writer.WriteLine("STATUS > Log File opened");
            }
            else
            {
                writer = new StreamWriter(File.Open("Log.txt", FileMode.Truncate));
                writer.WriteLine("STATUS > Log File opened and trunicated");
                Setup.Setup.LogTruncated = true;
            }
            
            //
            StreamReader bkpReader = new StreamReader("Backup.txt");
            writer.WriteLine("STATUS > Backup File opened");

            writer.WriteLine("STATUS > PrintUtility started");

            
            //priming read
            string currentline = bkpReader.ReadLine();
            //header
            writer.WriteLine("N: " + OtherClasses.DataTable.NumOfCountries.ToString());
            writer.WriteLine("Max Id: " + OtherClasses.DataTable.MaxId.ToString());
            writer.WriteLine("LOC/ CDE ID- NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");    //header
            int counter = 0;
            indexFlag = false;
            while (!bkpReader.EndOfStream)
            {
                if (currentline == "Name Index Start")
                {
                    writer.WriteLine();
                    writer.WriteLine("NAME INDEX");
                    writer.WriteLine("LOC/ NAME-------------- PTR");
                    indexFlag = true;
                }
                else
                {
                    if (!indexFlag)
                    {
                        countryArray = currentline.Split(',');
                        if (countryArray[2].Length > 18)
                            countryArray[2] = countryArray[2].Substring(0, 18);

                        writer.WriteLine(String.Format("{0,3:000}/ {1,3} {2:000} {3,-18} {4,-13} {5,10:#,##0} {6,13:#,##0} {7,4:#0.0}", 
                            int.Parse(countryArray[1]), countryArray[0], int.Parse(countryArray[1]), countryArray[2], countryArray[3], 
                            long.Parse(countryArray[4]), long.Parse(countryArray[5]), double.Parse(countryArray[6])));
                    }
                    else
                    {
                        //shortens the name if too long
                        countryArray = currentline.Split(',');
                        if (countryArray[0].Length > 18)
                            countryArray[0] = countryArray[0].Substring(0, 18);

                        
                        writer.WriteLine(String.Format("{0, 3:000}/ {1, -18} {2, 3:000}", counter, countryArray[0], int.Parse(countryArray[1])));
                        counter++; //keps track of the location in the NameIndex Arrays
                    }
                }

                currentline = bkpReader.ReadLine();
            }
            writer.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");     //footer

            writer.WriteLine("STATUS > PrintUtility finished");

            bkpReader.Close();
            writer.WriteLine("STATUS > Backup File closed");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();

        }
    }
}
