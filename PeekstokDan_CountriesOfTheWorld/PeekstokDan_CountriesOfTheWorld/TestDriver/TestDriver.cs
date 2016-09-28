/* Project:         Countries of the World
 * Date             9/15/2014
 * Module:          TestDriver
 * Programmer:      Dan Peekstok
 * 
 * This module runs the Main of the other 3 modules (Setup, UserApp, and PrintUtility) in this 
 * program. It truncates the Log file so that previous data will be erased before writing 
 * anything new. After that, it runs Setup with "A1RawDataSample.csv" and PrintUtility. Next, 
 * it has a for loop that runs UserApp with "A1TransData(1-3),txt". After running PrintUtility
 * and setup again, it runs UserApp a fourth time with "A1TransData4.txt". It then writes to the
 * Log file one last time before closing.
 */ 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestDriver
{
    public class TestDriver
    {
        public static void Main(string[] args)
        {
            StreamWriter writer = new StreamWriter(File.Open("Log.txt", FileMode.Truncate));
            writer.WriteLine("STATUS > Log File opened and trunicated");
            Setup.Setup.LogTruncated = true;
            writer.WriteLine("STATUS > Test Driver started");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();

            Setup.Setup.Main(new string[] { "Sample" });
            PrintUtility.PrintUtility.Main();
            for (int i = 1; i < 4; i++)
                UserApp.UserApp.Main(new string[] { i.ToString() });

            PrintUtility.PrintUtility.Main();
            Setup.Setup.Main(new string[] { "" });
            for (int i = 4; i == 4; i++)
                UserApp.UserApp.Main(new string[] { i.ToString() });


            writer = File.AppendText("Log.txt");
            writer.WriteLine("STATUS > Log File opened");
            writer.WriteLine("STATUS > Test Driver finished");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();
        }
    }
}
