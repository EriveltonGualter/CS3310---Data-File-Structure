/* Project:         Countries of the World
 * Module:          DataTable
 * Programmer:      Dan Peekstok
 * 
 * Called by both Setup and UserApp, this class holds all the information for each country
 * in 7 different parellel arrays.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OtherClasses
{
    public class DataTable
    {

        //local variables

        //used to hold variables that are used by PrintUtility. printed at the top of the output
        private static int _counter;
        private static int _maxId;

        //size of the seven arrays that hold country data
        private static int size = 250;

        //parellel arrays that hold country data
        private string[] _code = new string[size];
        private int[] _id = new int[size];              //country id
        private string[] _name = new string[size];      //country name  
        private string[] _cont = new string[size];      //continent
        private long[] _sa = new long[size];            //surface area
        private long[] _pop = new long[size];           //population
        private double[] _le = new double[size];        //life expantancy

        //counter that holds the total amount of stored countries
        private int counter = 0;
        //streamwriters that are used for writing to the log and backup files
        private StreamWriter writer, writeToBkp;

        //constructor that opens and writes to the log file
        public DataTable()
        {
            Console.WriteLine("DataTable");
            writer = File.AppendText("Log.txt");
            writer.WriteLine("STATUS > Log File opened");
            writer.WriteLine("STATUS > Data Table started");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();
        }

        //opens the backup file and calls WriteToBkp()
        //then closes the two files
        public void FinishUp()
        {
            _counter = counter;
            writer = File.AppendText("Log.txt");
            writer.WriteLine("STATUS > Log File opened");
            writeToBkp = new StreamWriter(File.Open("Backup.txt", FileMode.Truncate));
            writer.WriteLine("STATUS > Backup File opened");

            WriteToBackup();

            writeToBkp.Close();
            writer.WriteLine("STATUS > Backup File closed");
            writer.WriteLine("STATUS > Data Table finished");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();
        }

        //dummy stub that returns not working
        public void DeleteByIndex(string input, ref UI newUI)
        {
            newUI.PrintOuputToLog("[SORRY, Delete by Index module not yet working]", input);
        }

        //used by setup and nameindex to store country data and then increment the counter
        public void InsertCountry(string code, int id, string name, string cont, long sa, long pop, double le)
        {
            _code[id] = code;
            _id[id] = id;
            _name[id] = name;
            _cont[id] = cont;
            _sa[id] = sa;
            _pop[id] = pop;
            _le[id] = le;
            counter++;
            if (id > _maxId)
                _maxId = id;
        }

        //called by userapp to load the arrays from the backup file
        public int LoadTable(ref StreamReader bkpReader)
        {
            int bkpCounter = 0;
            string[] tempArray = new string[7];
            string currentLine = bkpReader.ReadLine();
            while (currentLine != "Name Index Start")
            {
                tempArray = currentLine.Split(',');
                InsertCountry(tempArray[0], int.Parse(tempArray[1]), tempArray[2],
                    tempArray[3], long.Parse(tempArray[4]), long.Parse(tempArray[5]), double.Parse(tempArray[6]));
                bkpCounter++;
                currentLine = bkpReader.ReadLine();
            }
            return bkpCounter;
        }

        //overloaded for SelectByName and called by NameIndex
        //accepts the country id and returns that country's data formatted for output
        public string SelectByIndex(int id)
        {
            string tempName = _name[id];
            if (tempName.Length > 18)
                tempName = _name[id].Substring(0, 18);
            return String.Format("{0,3} {1:000} {2,-18} {3,-13} {4,10:#,##0} {5,13:#,##0} {6,4:#0.0}",
                _code[id], id, tempName, _cont[id], _sa[id], _pop[id], _le[id]);
        }

        //overloaded for SelectByIndex and called by userapp
        //accepts a string and parses the data. If that country exists, it returns that data and true
        //otherwise it returns false
        public void SelectByIndex(string input, ref UI newUI)
        {
            int id = 0;
            string index = input.Substring(3, input.Length - 3).Trim();
            try
            {
                id = int.Parse(index);
                if (id > 0 && id <= _maxId && _code[id] != null)
                {

                    string tempName = _name[id];
                    if (tempName.Length > 18)
                        tempName = _name[id].Substring(0, 18);
                    newUI.PrintOuputToLog(String.Format("{0,3} {1:000} {2,-18} {3,-13} {4,10:#,##0} {5,13:#,##0} {6,4:#0.0}",
                                _code[id], id, tempName, _cont[id], _sa[id], _pop[id], _le[id]), input);
                }
                else
                {
                    newUI.PrintOuputToLog("ERROR, invalid country id", input);
                }
            }
            catch
            {
                newUI.PrintOuputToLog("ERROR, invalid country id", input);
            }
        }

        //called by nameindex to see if a country exists. Used when checking for an overwritten country
        public bool CountryExists(int id, out string name)
        {
            if (_code[id] == null)
            {
                name = null;
                return false;
            }
            else
            {
                name = _name[id];
                return true;
            }
        }

        //called by userapp when requested. returns the selected index. if its empty, then it returns an output say so
        public bool SelectAllByIndex(int id, ref UI newUI)
        {
            if (_code[id] == null)
            {
                newUI.PrintOuputToLog("    " + id.ToString("000") + " EMPTY");
                return false;
            }
            else
            {
                string tempName = _name[id];
                if (tempName.Length > 18)
                    tempName = _name[id].Substring(0, 18);
                newUI.PrintOuputToLog(String.Format("{0,3} {1:000} {2,-18} {3,-13} {4,10:#,##0} {5,13:#,##0} {6,4:#0.0}",
                            _code[id], id, tempName, _cont[id], _sa[id], _pop[id], _le[id]));
                return true;
            }
        }

        //called by FinishUp. Writes the contents of the 7 arrays to the backup file
        private void WriteToBackup()
        {
            for (int x = 0; x < size; x++)
            {
                if (_code[x] != null) //wont write empty spaces in the array to the backup file
                    writeToBkp.WriteLine(_code[x] + "," + _id[x] + "," + _name[x] + "," + _cont[x] + "," + _sa[x].ToString() + "," + _pop[x].ToString() + "," + _le[x].ToString());
            }
        }

        //used to set the size of the arrays if more countries need to be added later
        public static int SetSize
        { set { size = value; } }

        //used by PrintUtility. returns the greatest stored ID value
        public static int MaxId
        { get { return _maxId; } }

        //used by PrintUtility. returns the amount of countries stored
        public static int NumOfCountries
        { get { return _counter; } }
    }
}
