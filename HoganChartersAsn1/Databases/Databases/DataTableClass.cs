/******************
 * Hogan Charters
 * Data Table Class Sept 22nd
 * Kaminski
 * This class focuses on holding the data for all the current countries with fields to expand upon
 * ***************/
using System;
using System.IO;

namespace CountriesOfTheWorld
{
	public class DataTableClass
	{
		//Private Class Fields
		private int MAX_ARRAY_SIZE = 250;	//Size of the arrays to be used in the class
		private String[] CountryCode;		//Array to contain the country codes
		private int[] CountryID;			//Array to contain the country id
		private String[] CountryName;		//Array to contain the countries name
		private String[] Continent;			//Array to contain the continent the country is located is on
		private float[] SurfaceArea;		//Array of the surface area
		private long[] Population;			//Array of the countries population
		private float[] LifeExpec;			//Array of the countries life expectancy
		private int pointer;				//This is the id pointer to search with direct address
		private StreamWriter sw;			//This allows the class to write to a backup file
		private String backup = "Backup.txt";	//This is the files that the data is backed up to
        private int i;                      //This  is an index value to use for loops
        private StreamReader sr;            //this is used to read in the values from back up txt
        private String Begin = "DT";        //This is the comparative string used when reloading the table
        private String currentLine;         //This will be the current line from the stream reader
        private String[] currentArray;      //This array hold the data from backup.txt
        private int maxID;                  //Hold the max ID of the array
        private int n;                      //Where n is the amount of records
        
		//The Default constructor instatiates the arrays and sets the number of records and max id to 0
		public DataTableClass ()
		{
			CountryCode = new string[MAX_ARRAY_SIZE];
			CountryID = new int[MAX_ARRAY_SIZE];
			CountryName = new string[MAX_ARRAY_SIZE];
			Continent = new string[MAX_ARRAY_SIZE];
			SurfaceArea = new float[MAX_ARRAY_SIZE];
			Population = new long[MAX_ARRAY_SIZE];
			LifeExpec = new float[MAX_ARRAY_SIZE];
            maxID = 0;
            n = 0;
		}
		/// <summary>
		/// Insert information about the country into the apporpriate arrays
		/// </summary>
		/// <param name="code">Country Code.</param>
		/// <param name="id">Country ID Number.</param>
		/// <param name="name">Name of the Country.</param>
		/// <param name="cont">Continent Country is located on.</param>
		/// <param name="area">Surface Area of Country.</param>
		/// <param name="pop">Population of Country.</param>
		/// <param name="life">Life Expectancy for citizens of the Country .</param>
		public void Insert(string code, int id, string name, string cont, float area, long pop, float life){
			pointer = id;
			CountryCode [pointer] = code;
			CountryID [pointer] = id;
			CountryName [pointer] = name;
			Continent [pointer] = cont;
			SurfaceArea [pointer] = area;
			Population [pointer] = pop;
			LifeExpec [pointer] = life;
            if (id > maxID)
            {
                maxID = id;
            }
            n++;

		}

		//Writes to the backup file and closes out the stream reader
		public void CleanUp(){
			sw = new StreamWriter (backup);
            sw.WriteLine("M" + "," + maxID + "," + n);
            sw.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            for (i = 1; i < MAX_ARRAY_SIZE; i++)
            {
                    sw.WriteLine("DT" + "," + CountryCode[i] + "," + CountryID[i] + "," + CountryName[i] + ","
                        + Continent[i] + "," + SurfaceArea[i] + "," + Population[i] + "," + LifeExpec[i]);
            }
            sw.WriteLine("*********************************************************************************************************************");
            sw.Close();

		}
		//Reboot table reads in data from backup and sets it to the arrays
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
                        pointer = Int32.Parse(currentArray[2]);
                        if (pointer != 0)
                        {
                            CountryCode[pointer] = currentArray[1];
                            CountryID[pointer] = pointer;
                            CountryName[pointer] = currentArray[3];
                            Continent[pointer] = currentArray[4];
                            SurfaceArea[pointer] = Single.Parse(currentArray[5]);
                            Population[pointer] = Int64.Parse(currentArray[6]);
                            LifeExpec[pointer] = Single.Parse(currentArray[7]);
                            if (pointer > maxID)
                            {
                                maxID = pointer;
                            }
                            n++;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: " + e.Message);
                        Console.ReadKey();
                    }


                }
                
            }
            sr.Close();
        }
		//Public getter codes
        #region Public Getters
        public string GetCode(int y)
        {
            return CountryCode[y];
        }
        public int GetID(int y)
        {
            return CountryID[y];
        }
        public string GetName(int y)
        {
            return CountryName[y];
        }
        public string GetContinent(int y)
        {
            return Continent[y];
        }
        public float GetArea(int y)
        {
            return SurfaceArea[y];
        }
        public long GetPop(int y)
        {
            return Population[y];
        }
        public float GetLife(int y)
        {
            return LifeExpec[y];
        } 
        #endregion
	}
}

