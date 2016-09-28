/********************************
 * Hogan Charters
 * Test Driver Class Sept. 22nd
 * Kaminski
 * This class runs setup, pretty print,and user app using a varaity of files
 * ******************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountriesOfTheWorld
{
    class TestDriver
    {
        static void Main(string[] args)
        {
            String[] sample;
            int i;		//index variable
			//using rawdatasample runs the first three transactions
            for (i = 1; i < 4; i++)
            {
                sample = new String[] {"A1RawDataSample.csv"};
                CountriesOfTheWorld.Setup.Main(sample);
                sample = new String[] { "A1TransData" + i + ".txt" };
                CountriesOfTheWorld.UserApp.Main(sample);
                CountriesOfTheWorld.PrettyPrintUtility.Main(sample);
            }
            sample = new String[] { "A1RawData.csv" };
            CountriesOfTheWorld.Setup.Main(sample);
            sample = new String[] { "A1TransData4.txt" };
            CountriesOfTheWorld.UserApp.Main(sample);
        }
    }
}
