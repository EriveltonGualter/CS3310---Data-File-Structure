/* Project:         Countries of the World
 * Module:          NameIndex
 * Programmer:      Dan Peekstok
 * 
 * Called by both Setup and UserApp, this class stores all the countries in arrays
 * that are sorted alphabetically by name. This class also deals with DataTable when 
 * requested to return a specific countries data. 
 */ 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OtherClasses
{
    public class NameIndex
    {
        //private local variables 

        //size of the two arrays that hold country data
        private static int size = 250;
        //coutner to know how many countries are currently stored       
        private int counter = 0;
        //parellel arrays that hold the data of the countries
        //sorted name array (key value)
        private string[] _names = new string[size];
        //id for the coutnry in the same index (Data Retreival Point)
        private int[] _ids = new int[size];
        //streamwriters that are used in a few methods. 
        private StreamWriter writer, writeToBkp;

        //constructor that creates the NameIndex object and writes to the log
        public NameIndex()
        {
            Console.WriteLine("NameIndex");
            writer = File.AppendText("Log.txt");
            writer.WriteLine("STATUS > Log File opened");
            writer.WriteLine("STATUS > Name Index started");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();
        }

        //overloaded for generic inserts from raw data
        //places the data into the arrays and incrments the counter
        public void InsertCountry(string name, int id)
        {
            _names[counter] = name;
            _ids[counter] = id;
            counter++;
        }

        //overloaded for inserts from trans file
        //first cleans up the data. 
        //second rounds the area if its a decimal
        //third checks to see if the selected id value is in use and replaces if necessary
        //fourth sends to DataTable and cleans up the output
        //finally re-sorts the names
        public void InsertCountry(string input, ref DataTable newTable, ref UI newUI)
        {
            string output;
            input = RawData.CleanUpData(input.Substring(3, input.Length - 3));
            string[] inputStringArray = input.Split(',');

            //rounds decimal areas to the nearest whole number
            long area;
            if (!long.TryParse(inputStringArray[5], out area))
                area = long.Parse(Math.Round(decimal.Parse(inputStringArray[5])).ToString());

            input = inputStringArray[0] + "," + inputStringArray[1] + "," + inputStringArray[2] + "," + inputStringArray[3] + ","
                + inputStringArray[5] + "," + inputStringArray[7] + "," + inputStringArray[8];

            string name;
            int id, position;
            if (newTable.CountryExists(int.Parse(inputStringArray[1]), out name) || Search(inputStringArray[2].ToUpper(), out id, out position))
            {
                output = "ERROR, invalid country name or id. Already exists";
            }
            else
            {

                _names[counter] = inputStringArray[2];
                _ids[counter] = int.Parse(inputStringArray[1]);
                counter++;

                newTable.InsertCountry(inputStringArray[0], int.Parse(inputStringArray[1]), inputStringArray[2], inputStringArray[3],
                    area, long.Parse(inputStringArray[7]), double.Parse(inputStringArray[8]));



                Sort(0, counter - 1);

                output = "OK, country inserted (in data storage & name index)";
            }

            newUI.PrintOuputToLog(output, "IN " + input);
        }

        //swap method used in sorting. 
        private void Swap(int currentPosition, int newPosition)
        {
            string swapName = _names[currentPosition];
            int swapId = _ids[currentPosition];
            _names[currentPosition] = _names[newPosition];
            _ids[currentPosition] = _ids[newPosition];
            _names[newPosition] = swapName;
            _ids[newPosition] = swapId;
        }

        //quicksort that places the name array into alphabetical order
        //written for a project last semester for CIS 237 (at KVCC)
        //reused the code with a variable name change
        //Aras Vitkus and I worked together to write it
        private void Sort(int low, int high)
        {
            if ((high - low) >= 1)
            {
                int selected = low;
                int max = high;
                int min = low + 1;
                int current = high;


                while (current != selected)
                {
                    if (selected < current)
                    {
                        if (_names[current].CompareTo(_names[selected]) < 0)
                        {
                            Swap(selected, current);
                            max = current - 1;
                            selected = current;
                            current = min;
                        }
                        else
                        {
                            current--;
                        }

                    }
                    else
                    {
                        if (_names[current].CompareTo(_names[selected]) > 0)
                        {
                            Swap(selected, current);
                            selected = current;
                            min = current + 1;
                            current = max;

                        }
                        else
                        {
                            current++;
                        }
                    }
                }
                Sort(low, (selected - 1));
                Sort((selected + 1), high);
            }
        }

        //binary search for the name of a country
        //returns the id and position
        //position is used for replacing in the event of a duplicate
        //written for a project last semester for CIS 237 (at KVCC) 
        private bool Search(string input, out int id, out int position)
        {
            int first = 0;
            int last = counter - 1;
            int middle;
            position = -1;
            bool found = false;

            while (!found && first <= last)
            {
                middle = (first + last) / 2;
                if (string.Compare(input, _names[middle].ToUpper()) == 0)
                {
                    found = true;
                    position = middle;
                }
                else if (string.CompareOrdinal(input, _names[middle].ToUpper()) > 0)
                    first = middle + 1;
                else if (string.CompareOrdinal(input, _names[middle].ToUpper()) < 0)
                    last = middle - 1;
            }

            if (found)
            {
                id = _ids[position];
                return true;
            }
            else
            {
                id = 0;
                return false;
            }
        }

        //dummy stub that just returns not working
        public void DeleteByName(string input, ref UI newUI)
        {
            newUI.PrintOuputToLog("[SORRY, Delete By Name module not yet working]", input);
        }

        //used by UserApp when requested
        //returns the data of all countries sorted by name
        //data is formatted and ready to output
        public void SelectAllByName(int index, ref DataTable newTable, ref UI newUI)
        {
            newUI.PrintOuputToLog(newTable.SelectByIndex(_ids[index]));
        }

        //used by UserApp when requested
        //uses search to find the country
        //if it exists, returns the country's data formatted for ourput
        //else returns false and a blank message
        public void SelectByName(string input, ref DataTable newTable, ref UI newUI)
        {
            int id, position;
            input = input.Substring(3, input.Length - 3).TrimEnd();
            if (Search(input.ToUpper(), out id, out position))
                newUI.PrintOuputToLog(newTable.SelectByIndex(id), "SN " + input);
            else
                newUI.PrintOuputToLog("ERROR, invalid country name", "SN " + input);
        }

        //called by UserApp to store a sorted name index
        public void LoadIndex(ref StreamReader bkpReader)
        {
            string[] tempArray = new string[2];
            string currentLine = bkpReader.ReadLine();
            while (!bkpReader.EndOfStream)
            {
                tempArray = currentLine.Split(',');
                InsertCountry(tempArray[0], int.Parse(tempArray[1]));
                currentLine = bkpReader.ReadLine();
            }
        }

        //called by FinishUp. writes the contents of the two arrays to the backup file.
        private void WriteToBackup()
        {
            //marker in the backup file to differentiate dataTable entries from nameIndex entries
            writeToBkp.WriteLine("Name Index Start");

            for (int x = 0; x <= counter; x++)
            {
                writeToBkp.WriteLine(_names[x] + "," + _ids[x]);
            }
        }

        //if the needSort variable is true it calls the Sort ,ethod
        //writes to the log file, opens the backup file and calls WriteToBackup(),
        //then closes the two files
        public void FinishUp(bool needSort)
        {
            if (needSort)
                Sort(0, counter - 1);

            writer = File.AppendText("Log.txt");
            writer.WriteLine("STATUS > Log File opened");
            writeToBkp = File.AppendText("Backup.txt");
            writer.WriteLine("STATUS > Backup File opened");

            WriteToBackup();

            writeToBkp.Close();
            writer.WriteLine("STATUS > Backup File closed");
            writer.WriteLine("STATUS > Name Index finished");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();
        }

        //This will be used to adjust the size of the arrays should more countries need to be stored.
        public int SetSize
        { set { size = value; } }

        //called by UserApp for accurate loop counters that don't go passed the stored amount of countries
        public int Counter
        { get { return counter; } }
    }
}
