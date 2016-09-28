/* Project:         Countries of the World
 * Module:          Setup
 * Programmer:      Dan Peekstok
 * 
 * Setup deals with the input of raw data. If TestDriver hasn't been ran, Setup will truncate the Log file
 * before opening it. If the file was already truncated, then it just opens it in append mode to add status 
 * messages. Then it creates 3 objects: RawData, DataTable, and NameIndex. Next it loops through the RawData
 * file and sends it to RawData to get cleaned up before sending the data to the other two classes. When its 
 * done with the RawData file, it calls the FinishUp method for DataTable and NameIndex then closes.
 */ 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Setup
{
    public class Setup
    {
        private static bool logTruncated = false;
        private static string rawDataSuffix = "";

        public static void Main(string[] args)
        {
            Console.WriteLine("setup");
            StreamWriter writer;
            if (logTruncated)
            {
                writer = File.AppendText("Log.txt");
                writer.WriteLine("STATUS > Log File opened");
            }
            else
            {
                writer = new StreamWriter(File.Open("Log.txt", FileMode.Truncate));
                writer.WriteLine("STATUS > Log File opened and trunicated");
                logTruncated = true;
            }

            writer.WriteLine("STATUS > Setup started");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();

            int counter = 0;
            if (args.Length > 0)
                rawDataSuffix = args[0];

            OtherClasses.RawData inputRawData = new OtherClasses.RawData("A1RawData" + rawDataSuffix + ".csv");
            OtherClasses.DataTable table = new OtherClasses.DataTable();
            OtherClasses.NameIndex index = new OtherClasses.NameIndex();

            while (inputRawData.Input1Country())
            {
                counter++;
                table.InsertCountry(inputRawData.Code, inputRawData.ID, inputRawData.Name, inputRawData.Continent, inputRawData.SurfaceArea, inputRawData.Population, inputRawData.LifeExpect);
                index.InsertCountry(inputRawData.Name, inputRawData.ID);
            }

            table.FinishUp();
            index.FinishUp(true);

            writer = File.AppendText("Log.txt");
            writer.WriteLine("STATUS > Log File opened");
            writer.WriteLine("STATUS > Setup finished - " + counter.ToString() + " countries processed");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();
        }

        public static bool LogTruncated
        { get { return logTruncated; } set { logTruncated = value; } }
    }
}
