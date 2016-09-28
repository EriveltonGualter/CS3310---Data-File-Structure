/*******************
 * Hogan Charters
 * UI class sept. 22nd
 * Kaminski
 * This class processes all the transaction from the transaction files
 * *************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace CountriesOfTheWorld
{
    public class UI
    {
		//Private Fields
        private StreamWriter sw;		
        private string log = "Log.txt";		//this hold the name of the log file
        private StreamReader sr;
        private String transFile;		//this hold the value of the transaction file
        private String currentLine;		//this holds the current life from the streamreader
		private String[] currentArray;		//this holds the array of the split current line
        private String comp;		//this is a general string variable used for various purposes
        private int i,x;		//index varaibles
        private NameIndexClass nic;
        private DataTableClass dtc;
        private string code;		//code of country
        private int id;		//id of the country
        private string name;		//name of the country
        private string continent;		//name of the coninent
        private float area;		//size of the area
        private long population;		//size of the population
        private float life;		//life expectancy of the country
        private string output;		//output string that gets written to the log file
        private int counter;		//counts the number of transactionsd
        private int MAX_ARRAY_SIZE = 250;		//max array size allowed

		//The default constructor which is set to do nothing
		public UI(){
		}

		//this constructor accepts a transaction file name and opens the reader and writers 
        public UI(string TransactionFile)
        {
            transFile = TransactionFile;	
            counter = 0;		
            sw = new StreamWriter(log, true);
            sw.WriteLine("STATUS > UserApp started");
            sw.WriteLine("STATUS > Log FILE Opened");
            sr = new StreamReader(transFile);
            sw.WriteLine("STATUS > TransData FILE Opened (" + transFile + ")");
            nic = new NameIndexClass();
            dtc = new DataTableClass();
            dtc.RebootTable();		
            nic.RebootTable();
        }
		//This method reads a line from the trans action file the processes the transaction with a switch
        public void ReadTransaction()
        {
            currentLine = sr.ReadLine();
            currentArray = currentLine.Split(null);
            switch (currentArray[0])
            {
                case "AN":
                    AllByName();
                    break;
                case "AI":
                    AllByID();
                    break;
                case "SN":
                    SelectByName();
                    break;
                case "SI":
                    SelectByID();
                    break;
                case "DN":
                    DeleteByName();
                    break;
                case "DI":
                    DeleteByID();
                    break;
                case "IN":
                    Insert();
                    break;
                default:
                    break;
            }


        }
		//this method checks whether or not we have reached the end of the file
        public Boolean IsEnded()
        {
            Boolean ended;
            if (sr.EndOfStream)
            {
                ended = true;
            }
            else
            {
                ended = false;
            }
            return ended;

        }
		//this closes out the log files and executes the cleanup for data table and name index
        public void CleanUp()
        {
            sw.WriteLine("STATUS > TransData FILE closed");
            sw.WriteLine("STATUS > UserApp finished - " + counter + " transactions processed");
            sw.WriteLine("STATUS > Log FILE closed");
            sw.Close();
            sr.Close();
            dtc.CleanUp();
            nic.CleanUp();
        }
		//This method will select and display a country by name
        private void SelectByName()
        {
            comp = currentLine.Remove(0,3);		//removes the first three characters from the string
            i = nic.FindName(comp);
            sw.WriteLine(currentLine);
            if (i >= 0)
            {
                code = dtc.GetCode(i);
                id = dtc.GetID(i);
                name = dtc.GetName(i);
                if (name.Length > 18)
                {
                    name = name.Substring(0, 18);
                }
                continent = dtc.GetContinent(i);
                area = dtc.GetArea(i);
                population = dtc.GetPop(i);
                life = dtc.GetLife(i);
                output = String.Format("   {0,3} {1,3:D3} {2,-18} {3,-13} {4,11:N0} {5,13:N0} {6,4}", code, id, name, continent, area, population, life);            
            }
            else
            {
				output = "   ERROR, invalid country name";
            }
            counter++;
            sw.WriteLine(output);
            
        }
		//selects and displays a country by name
        private void SelectByID()
        {
            comp = currentArray[1];
            sw.WriteLine(currentLine);
            try
            {
                i = Int32.Parse(comp);
                code = dtc.GetCode(i);
                id = dtc.GetID(i);
                name = dtc.GetName(i);
                if (name.Length > 18)
                {
                    name = name.Substring(0, 18);
                }
                continent = dtc.GetContinent(i);
                area = dtc.GetArea(i);
                population = dtc.GetPop(i);
                life = dtc.GetLife(i);
                output = String.Format("   {0,3} {1,3:D3} {2,-18} {3,-13} {4,11:N0} {5,13:N0} {6,4}", code, id, name, continent, area, population, life);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                output = "   ERROR, invalid country ID";
            }
            counter++;
            sw.WriteLine(output);
        }
		//this prints out all countries in the arrays arranged by name
        private void AllByName()
        {
            sw.WriteLine(currentLine);
            sw.WriteLine("   CDE ID- NAME-------------- CONTINENT---- -------AREA ---POPULATION LIFE");
            for (x = 0; x < MAX_ARRAY_SIZE; x++)
            {
                if (nic.GetName(x) != null)
                {
                    i = nic.GetID(x);
                    code = dtc.GetCode(i);
                    id = dtc.GetID(i);
                    name = dtc.GetName(i);
                    if (name.Length > 18)
                    {
                        name = name.Substring(0, 18);
                    }
                    continent = dtc.GetContinent(i);
                    area = dtc.GetArea(i);
                    population = dtc.GetPop(i);
                    life = dtc.GetLife(i);
                    output = String.Format("   {0,3} {1,3:D3} {2,-18} {3,-13} {4,11:N0} {5,13:N0} {6,4}", code, id, name, continent, area, population, life);
                    sw.WriteLine(output);
                }
            }
        }
		//prints out all countries in array by order of id
        private void AllByID()
        {
            sw.WriteLine(currentLine);
            sw.WriteLine("   CDE ID- NAME-------------- CONTINENT---- -------AREA ---POPULATION LIFE");
            for (i = 0; i < MAX_ARRAY_SIZE; i++)
            {
                if (dtc.GetID(i) != 0)
                {
                    code = dtc.GetCode(i);
                    id = dtc.GetID(i);
                    name = dtc.GetName(i);
                    if (name.Length > 18)
                    {
                        name = name.Substring(0, 18);
                    }
                    continent = dtc.GetContinent(i);
                    area = dtc.GetArea(i);
                    population = dtc.GetPop(i);
                    life = dtc.GetLife(i);
                    output = String.Format("   {0,3} {1,3:D3} {2,-18} {3,-13} {4,11:N0} {5,13:N0} {6,4}", code, id, name, continent, area, population, life);
                    sw.WriteLine(output);
                }
            }
        }
		//this will delete a country by name
        private void DeleteByName()
        {
            sw.WriteLine(currentLine);
            sw.WriteLine("[SORRY, Delete By Name module not yet working]");
        }
		//this will delete a country by id
        private void DeleteByID()
        {
            sw.WriteLine(currentLine);
            sw.WriteLine("[SORRY, Delete By ID module not yet working]");
        }
		//inserts a country into both the name index and data table class
        private void Insert()
        {
            sw.WriteLine(currentLine);
            counter++;
            currentLine = currentLine.Remove(0, 33);
            currentArray = currentLine.Split(',');
            WordCleaner();
                
                try
                {
                    name = currentArray[2];
                    id = Int32.Parse(currentArray[1]);
                    code = currentArray[0];
                    continent = currentArray[3];
                    area = Single.Parse(currentArray[5]);
                    population = Int64.Parse(currentArray[7]);
                    life = Single.Parse(currentArray[8]);
                    dtc.Insert(code, id, name, continent, area, population, life);
                    nic.Insert(name, id);
                    sw.WriteLine("   OK, country inserted (in data storage & name index)");
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                    Console.ReadKey();
                    sw.WriteLine("   ERROR, invalid country");
              
                }
        }
		//the method trys to remove ' from and input string
        public void WordCleaner()
        {
            for (i = 0; i < currentArray.Length; i++)
            {
                if (currentArray[i].Contains("\'"))
                {
                    currentArray[i] = currentArray[i].Replace("\'", "");
                }

            }
        }
    }
}
