/**************************
 * Hogan Charters
 * User App Class Sept 22nd
 * Kaminski
 * The purpose of this class is to read in a transaction file and to run all the trasactions
 * **************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CountriesOfTheWorld
{
    class UserApp
    {
        public static void Main(string[] args){
            String transFile = "A1TransData1.txt";	//Default  Transaction file
			//accepts an argument string if there is one
            if (args[0] != null)
            {
                transFile = args[0];
            }
            UI ui = new UI(transFile);	
            //while the file has not ended processes transactions
            while (!ui.IsEnded())
            {
                ui.ReadTransaction();
            }
            
            ui.CleanUp();
        }

    }
}
