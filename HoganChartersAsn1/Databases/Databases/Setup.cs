/****************************************
 * Hogan Charters
 * Date: Sept 11th 2014
 * CS 3310
 * Donna Kaminski
 * This is the setup program to execute the rawdata, datatable, and name index class
 * which read data from the files and output it into a log and the backup
 * *************************************/
using System;
using System.IO;

namespace CountriesOfTheWorld
{
	class Setup
	{
		public static void Main(string[] args){
            String dataFile;		//This is the variable that holds the name for the raw data file
            if (args[0] == null)
            {
                   dataFile = "A1RawDataSample.csv";
            }
            else
            {
                dataFile = args[0];
            }
			
			//Variables for control of program
			String log = "Log.txt";		//This contains the file name for the log file
			StreamWriter sw = new StreamWriter (log, true);		//This instatiates the stremwriter into the class
			sw.WriteLine ("STATUS > Log FILE Opened");
			sw.WriteLine("STATUS > Setup Started");
			RawDataClass rdc = new RawDataClass (dataFile);		//this instatiates the rawdataclass with the raw data file as the parameter
			sw.WriteLine("STATUS > RawData FILE Opened (" + dataFile + ")");
			DataTableClass dtc = new DataTableClass ();		//instantiates the data tabel class
			NameIndexClass nic = new NameIndexClass ();		//instantiates the name index class
			int CountryCount = 0;		//index count for countries added

			//This loop runs through the raw data files and inputs the data into the DataTableClass and NameIndexClass
			while (!rdc.IsEnded()) {
				if (rdc.InputCountry ()) {
					CountryCount++;
				}
				nic.Insert (rdc.GetNameOfCountry (), rdc.GetCountryID ());
				dtc.Insert (rdc.GetCountryCode(), rdc.GetCountryID(), rdc.GetNameOfCountry(), rdc.GetContinent(), rdc.GetSurfaceArea(), rdc.GetPopulation(), rdc.GetLifeExpec());
			}
            nic.Sort();
			rdc.CleanUp();
            dtc.CleanUp();
            nic.CleanUp();
			sw.WriteLine ("STATUS > Setup Finished - " + CountryCount + " countries proccessed");
			sw.WriteLine ("STATUS > Log FILE closed");
			sw.Close ();
		}
    }
}

