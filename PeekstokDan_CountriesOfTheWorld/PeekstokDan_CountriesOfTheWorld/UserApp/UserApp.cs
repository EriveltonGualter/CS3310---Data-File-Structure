/* Project:         Countries of the World
 * Module:          UserApp
 * Programmer:      Dan Peekstok
 * 
 * This module is used for anything that has to do with the user. It starts by telling the UI to 
 * write to the log file. After that, it makes 3 objects: DataTable, NameIndex, and UI. Then it 
 * reads the backup file into the arrays in both DataTable and NameIndex. Next, tells UI to start
 * reading from the transData file and sends the data to the other classes as needed. Finally, it 
 * calls FinishUp for dataTable, NameIndex, UI, and UserApp.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UserApp
{
    public class UserApp
    {
        //local variables

        //transData suffix is defaulted to the first TransData file
        private static string transDataLoc = "1", transCode, transString, output;
        private static OtherClasses.DataTable newTable;
        private static OtherClasses.NameIndex newIndex;
        private static StreamReader bkpReader;
        private static OtherClasses.UI newUI;
        //counter for backup and transactions
        private static int bkpCounter, transCounter;

        public static void Main(string[] args)
        {
            if (Setup.Setup.LogTruncated)
                OtherClasses.UI.UserAppStart(true);
            else
            {
                OtherClasses.UI.UserAppStart(false);
                Setup.Setup.LogTruncated = true;
            }


            if (args.Length > 0)
                transDataLoc = args[0];


            newTable = new OtherClasses.DataTable();
            newIndex = new OtherClasses.NameIndex();
            newUI = new OtherClasses.UI(transDataLoc);

            newUI.StartBkpRead();

            bkpReader = new StreamReader("Backup.txt");
            //I pass the bkpReader by reference so that a new object isn't created. 
            //This saves a bit of memory and run time
            bkpCounter = newTable.LoadTable(ref bkpReader);
            newIndex.LoadIndex(ref bkpReader);
            newUI.StartTransRead();

            transCounter = 0;

            while (newUI.ReadTrans(out transCode, out transString))
            {
                transCounter++;
                switch (transCode)
                {
                    case "SI":
                        newTable.SelectByIndex(transString, ref newUI);
                        break;
                    case "SN":
                        newIndex.SelectByName(transString, ref newTable, ref newUI);
                        break;
                    case "AI":
                        //prints the header for the top of the list of countries
                        newUI.PrintHeader("AI");

                        //x is the coutner that will print all the countries in order
                        //y is a counter that counts how many real countries are printed, not just blank lines
                        for (int x = 1, y = 0; y < bkpCounter; x++)
                        {
                            if (newTable.SelectAllByIndex(x, ref newUI))
                            {
                                //if its printing a real country, not a blank line
                                y++;
                            }
                        }
                        //prints the footer at the bottom of the list of countries
                        newUI.PrintFooter();
                        break;
                    case "AN":
                        //prints the header for the top of the list of countries
                        newUI.PrintHeader("AN");
                        for (int x = 0; x < bkpCounter; x++)
                        {
                            //prints all the countries sorted by name from the nameIndex array
                            newIndex.SelectAllByName(x, ref newTable, ref newUI);
                        }
                        //prints the footer at the bottom of the list of countries
                        newUI.PrintFooter();
                        break;
                    case "DN":
                        //will just print "not working yet"
                        newIndex.DeleteByName(transString, ref newUI);
                        break;
                    case "DI":
                        //will just print "not working yet"
                        newTable.DeleteByIndex(transString, ref newUI);
                        break;
                    case "IN":
                        //temporary string variable
                        newIndex.InsertCountry(transString, ref newTable, ref newUI);
                        //increases the counter if the country wasn't a replacement
                        bkpCounter = newIndex.Counter;
                        break;
                }
            }

            //calls finishup for the objects
            newUI.FinishUp(ref bkpReader);
            newTable.FinishUp();
            newIndex.FinishUp(false);
            newUI.UserAppFinishUp(transCounter);
        }
    }
}
