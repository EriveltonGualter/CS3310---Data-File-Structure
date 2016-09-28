/************************
 * Hogan Charters
 * Raw Data Class
 * Kaminski - Sept 22nd
 * This class accesses the raw data file and pulls the relevant information 
 * and stores it in private fields
 * **********************/
using System;
using System.IO;

namespace CountriesOfTheWorld
{


	public class RawDataClass
	{
		//Fields that accept data from csv file

		private String NameOfCountry;		//Field to accept the name of the country
		private int CountryID;				//Field to accept the country ID
		private String CountryCode;			//Field to accept the country code
		private String Continent;			//Field to accept the country's continent
		//private String Region;			//Field to accept the country's region
		private float SurfaceArea;			//Field to accept the country's surface area
		//private int IndepYear;			//Field to accept the country's year of independance
		private long Population;				//Field to accept the country's population
		private float LifeExpec;			//Field to accept the country's life expectancy
		//private float GNP;				//Field to accept the country's gross domestic product(GNP)
		//private float GNPOld;				//Field to accept the country's old gross domestic product
		//private String LocalName;			//Field to accept the country's local name
		//private String GovernmentForm;	//Field to accept the country's form of government
		//private String HeadOfState;		//Field to accept the country's head of state
		//private int Capital;				//Field to accept the country's capital
		//private String CountryCode2;		//Field to accept the country's secondary country code
		private String currentLine;			//Field that holds the current line by the stream reader
		private StreamReader sr;			//This is the stream reader for the class
		private String Begin = "INSERT";	// This hold the variable to comapare the beginning of the line
		private String[] currentArray;		//Array that holds the split current line
		private int i; 						//General Index(es)

		//this is the default constructor which will do nothing
		public RawDataClass ()
		{

		}
		//this method accepts an input file name and intiates the stream reader
		public RawDataClass(String inputFile){
			try{
				sr = new StreamReader(inputFile);
			}
			catch(Exception e){
				Console.WriteLine ("Exception: " + e.Message);
			}
		}
		//these methods are used for getting the private fields of the class
		public String GetNameOfCountry(){return NameOfCountry;}
		public int GetCountryID(){return CountryID;}
		public String GetCountryCode(){return CountryCode;}
		public String GetContinent(){return Continent;}
		public float GetSurfaceArea(){return SurfaceArea;}
		public long GetPopulation(){return Population;}
		public float GetLifeExpec(){return LifeExpec;}

		//this method reads a line from teh file then makes sure it is a insert
		//it then splits the file and reads the values into the feilds
		public bool InputCountry(){
			currentLine = sr.ReadLine();
			bool added = false;
			if(currentLine.StartsWith(Begin)){
				currentLine = currentLine.Remove (0, 30);
				currentArray = currentLine.Split (',');
				WordCleaner();
				added = true;
				try{
					NameOfCountry = currentArray[2];
					CountryID = Int32.Parse(currentArray[1]);
					CountryCode = currentArray[0];
					Continent = currentArray[3];
					SurfaceArea = Single.Parse(currentArray[5]);
					Population = Int64.Parse(currentArray[7]);
					LifeExpec = Single.Parse(currentArray[8]);
					added = true;

				}
				catch(Exception e){
					Console.WriteLine ("Exception: " + e.Message);
                    Console.ReadKey();
					return false;
				}
			}
			return added;
		}
		//checks to see if you have reached the end of the file
		public Boolean IsEnded(){
			Boolean ended;
			if (sr.EndOfStream) {
				ended = true;
			} else {
				ended = false;
			}
			return ended;

		}
		//makes sure the streamreader is closed
		public void CleanUp(){
			sr.Close ();
			//sw.Close ();
		}
		//The word cleaner makes sure to replace all ' in the strings
		public void WordCleaner(){
			for (i=0; i < currentArray.Length; i++) {
				if (currentArray [i].Contains("\'")) {
					currentArray[i] = currentArray [i].Replace ("\'"  , "");
				}

			}
		}
	}
}

