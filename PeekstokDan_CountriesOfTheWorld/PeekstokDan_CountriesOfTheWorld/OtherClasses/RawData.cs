/* Project:         Countries of the World
 * Module:          RawData
 * Programmer:      Dan Peekstok
 * 
 * In the constructor, it opens and appends the Log file. Then it also opens the RawData file that
 * was passed in as a parameter. It has private variables and getter for all of the parts of data 
 * that will be sent to DataTable and Nameindex.
 * 
 * CleanUpData is a static, public method that is called internally and by NameIndex when processing 
 * an Insert. It removes all the unneeded parts of the string like the ' and the front 30 characters.
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OtherClasses
{
    public class RawData
    {
        //private local variables

        private StreamReader rawDataReader;
        private string _code;       //three character country code
        private int _id;            //country id
        private string _name;       //country name  
        private string _cont;       //continent
        private long _sa;           //surface area
        private long _pop;          //population
        private double _le;         //life expantancy

        //Constructor that takes the data file location as a parameter
        //opens the log file and RawData file and writes to the log
        public RawData(string dataLoc)
        {
            Console.WriteLine("RawData");
            rawDataReader = new StreamReader(dataLoc);
            StreamWriter writer = File.AppendText("Log.txt");
            writer.WriteLine("STATUS > Log File opened");
            writer.WriteLine("STATUS > RawData File opened (" + dataLoc + ")");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();
        }

        //called by Setup when it wants the next country's data
        //reads the file until it finds a good line or the end of the file.
        //calls CleanUpData then stores the data in backing variables
        public bool Input1Country()
        {
            string currentLine = rawDataReader.ReadLine();
            if (rawDataReader.EndOfStream)
            {
                FinishUp();
                return false;
            }

            while (!rawDataReader.EndOfStream)
            {
                //will run if the line contains proper data not just the header or footer
                if (currentLine.StartsWith("INSERT INTO `Country` VALUES"))
                {
                    currentLine = CleanUpData(currentLine);
                    string[] tempInput = currentLine.Split(',');
                    _code = tempInput[0];
                    _id = int.Parse(tempInput[1]);
                    _name = tempInput[2];
                    _cont = tempInput[3];
                    if (!long.TryParse(tempInput[5], out _sa))
                        _sa = long.Parse(Math.Round(decimal.Parse(tempInput[5])).ToString());
                    _pop = long.Parse(tempInput[7]);
                    _le = double.Parse(tempInput[8]);
                    return true;
                }
                else
                    currentLine = rawDataReader.ReadLine();
            }

            return false;

        }

        //called by Input1Country and NameIndex when receiving an insert command
        //removes all unused parts of the string. 
        public static string CleanUpData(string currentLine)
        {
            currentLine = currentLine.Substring(31, currentLine.Length - 33);      //removes the first 30 characters and the last 2
            while (currentLine.IndexOf('\'') != -1)                             //removes all ' from the data
            {
                currentLine = currentLine.Remove(currentLine.IndexOf('\''), 1);
            }
            return currentLine;
        }

        //called when its gets to the end of the RawData file
        //writes to the log file and closes the RawData file
        private void FinishUp()
        {
            StreamWriter writer = File.AppendText("Log.txt");
            writer.WriteLine("STATUS > Log File Opened");
            rawDataReader.Close();
            writer.WriteLine("STATUS > RawData FILE closed");
            writer.WriteLine("STATUS > Log File Closed");
            writer.Close();
        }

        //Getters for all the backing variables.         
        public string Code
        { get { return _code; } }

        public int ID
        { get { return _id; } }

        public string Name
        { get { return _name; } }

        public string Continent
        { get { return _cont; } }

        public long SurfaceArea
        { get { return _sa; } }

        public long Population
        { get { return _pop; } }

        public double LifeExpect
        { get { return _le; } }
    }
}
