/***************************
 * Hogan Charters
 * Name Index Class - Sept 22nd
 * Kaminski
 * This class holds the arrays for all the fields and outputs the data to a backup file
 * ************************/
using System;
using System.IO;

namespace CountriesOfTheWorld
{
	public class NameIndexClass
	{
		//Private Class Fields
		private int MAX_ARRAY_SIZE = 250;	//Size of the arrays to be used in the class
		private int[] CountryID;		//This array holds the value of country ids
		private string[] CountryName;	//this array holds the string of the country names
		private int pointer;		//this variable is used asa pointer in the insert method
        private StreamWriter sw;	
        private String backup = "Backup.txt";		//this is the varaible that hold the name of the backup file
        private int i,x;		//these are basic index variables
        private String currentLine;		//this varibale holds the current line that has been read in by the stream reader
        private StreamReader sr;
        private String Begin = "NI";	//this is what the streamreader looks for at the begininng of a sentance
		private String[] currentArray;		//this array holds what the current line is when split
     	//the default constructors instantiate the arrays   
		public NameIndexClass ()
		{
			CountryID = new int[MAX_ARRAY_SIZE];
			CountryName = new string[MAX_ARRAY_SIZE];
		}
		//Inserts a country into the arrays
		public void Insert(String country, int id){
			pointer = id;
			CountryID [pointer] = id;
            CountryName [pointer] = country;
			Sort ();
            
		}
		//Writes to the backupfile closes the streamwriter
		public void CleanUp(){
            sw = new StreamWriter(backup, true);
            sw.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            for (i = 0; i < MAX_ARRAY_SIZE; i++)
            {
				//makes sure it does not write any blank spots to the backup file
                if (CountryID[i] != 0)
                {
                    sw.WriteLine("NI" + "," + CountryID[i] + "," + CountryName[i]);
                }
            }
            sw.WriteLine("*********************************************************************************************************************");
            sw.Close();
		}
		//this method imports all the data from the backup file
        public void RebootTable()
        {
            sr = new StreamReader(backup);
            while (!sr.EndOfStream)
            {
                currentLine = sr.ReadLine();
                if (currentLine.StartsWith(Begin))
                {
                    currentArray = currentLine.Split(',');
                    try
                    {
                        pointer = Int32.Parse(currentArray[1]);
                        CountryID[pointer] = pointer;
                        CountryName[pointer] = currentArray[2];
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: " + e.Message);
                        Console.ReadKey();
                    }


                }

            }
            Array.Sort(CountryName, CountryID);
            sr.Close();
        }
        public int FindName(string name)
        {
            i = -1;
            name = name.TrimEnd();
            for (x = 0; x < MAX_ARRAY_SIZE; x++)
            {
                if (name.Equals(CountryName[x], StringComparison.InvariantCultureIgnoreCase))
                {
                    i = CountryID[x];
                }
            }
                return i;
        }
		//Public getter methods
        public string GetName(int y)
        {
            return CountryName[y];
        }
        public int GetID(int y)
        {
            return CountryID[y];
        }
		//this method sorts countryname alphabeticlly while keeping id in the right position in comparasin to the name
        public void Sort()
        {
            Array.Sort(CountryName, CountryID);
        }
	}
}

