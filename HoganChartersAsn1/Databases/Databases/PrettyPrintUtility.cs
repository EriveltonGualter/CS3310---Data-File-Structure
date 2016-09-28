/****************
 * Hogan Charters
 * Pretty Print Utility Class Sept. 22nd
 * Kaminski
 * This class prints the data from backup to the log in the specified format
 * ***************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CountriesOfTheWorld
{
    class PrettyPrintUtility
    {
        public static void Main(string[] args)
        {
            string backup = "Backup.txt";		//File name for the backup text
            string log = "Log.txt";		//This is the file name for the log file
			StreamReader sr = new StreamReader(backup);		//instaiates the stream reader
            int counter = 0;		//counter is the amount of counrties currently in the backup file
            string currentLine;		//this holds the current string from the streamreader
            StreamWriter sw = new StreamWriter(log,true);		//instatiates the stream writer
            int DataLoc;		//this is a counter for the current locatation in the datatable
            int NameLoc;		//this is a counter fo the current location in the name index
            string[] currentArray;		//this holds the array of current line when split
            string output;		//this string holds the formated string to write to the log file
            string name; 		//this holds the countries name
            bool spaced;		//this bool tells me if the datatable has been run through
            int id = -1;		//this holds the parsed id
            float area = -1;	//this holds the parsed area
            long population = -1;		//this holds the parsed population
            float lifeexpec = -1;		//this holds the parsed life expectancy
            int maxID;		//this hold the max id of the country to make sure it does not show empty
            DataLoc = 0;	
            NameLoc = 0;
            maxID = 0;
            currentLine = sr.ReadLine();
            currentArray = currentLine.Split(',');

            try
            {
                maxID = Int32.Parse(currentArray[1]);
                counter = Int32.Parse(currentArray[2]);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.ReadKey();
            }

            sw.WriteLine("N: " + counter);
            sw.WriteLine("DATA STORAGE");
            sw.WriteLine("LOC/ CDE ID- NAME-------------- CONTINENT---- -------AREA ---POPULATION LIFE");
            spaced = false;
            while (!sr.EndOfStream)
            {
                currentLine = sr.ReadLine();
				//this if statement is used to read data from the data table class in backup and write to log
                if (currentLine.StartsWith("DT") && DataLoc < maxID)
                {
                    currentArray = currentLine.Split(',');
                    DataLoc++;
                    name = currentArray[3];
                    if (name.Length > 18)
                    {
                        name = name.Substring(0, 18);
                    }
                    try
                    {
                        id = Int32.Parse(currentArray[2]);
                        area = Single.Parse(currentArray[5]);
                        population = Int64.Parse(currentArray[6]);
                        lifeexpec = Single.Parse(currentArray[7]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: " + e.Message);
                        Console.ReadKey();
                    }
					//format string
                    output = String.Format("{0,3:D3}/ {1,3} {2,3:D3} {3,-18} {4,-13} {5,11:N0} {6,13:N0} {7,4}", DataLoc, currentArray[1], id,
                        name, currentArray[4], area, population, lifeexpec);
                    sw.WriteLine(output);

                }
				//writes the space between data table and name index
                if (DataLoc == maxID && NameLoc == 0 && !spaced)
                {
                    sw.WriteLine();
                    sw.WriteLine("NAME INDEX");
                    sw.WriteLine("LOC/ NAME--------------- PTR");
                    spaced = true;

                }
				//writes the nameindex info to the log file
                if (currentLine.StartsWith("NI"))
                {
                    currentArray = currentLine.Split(',');
                    NameLoc++;
                    name = currentArray[2];
                    if (name.Length > 18)
                    {
                        name = name.Substring(0, 18);
                    }
                    try
                    {
                        id = Int32.Parse(currentArray[1]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception: " + e.Message);
                        Console.ReadKey();
                    }
                    output = String.Format("{0,3:D3}/ {1,-19} {2,3:D3}", NameLoc, name, id);
                    sw.WriteLine(output);
                }
                
            }
            sw.Close();
            sr.Close();
        }
    }
}
