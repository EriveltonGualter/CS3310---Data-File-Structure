/* Project:         Countries of the World
 * Module:          UI
 * Programmer:      Dan Peekstok
 * 
 * This module is used by UserApp for writing status messages and data to the log file. 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OtherClasses
{
    public class UI
    {
        //local variables
        private StreamWriter writer;
        private StreamReader transReader;
        private string currentLine;

        //constructor that takes the transdata suffix as an arguement
        //this doesn't close the log writer since it is used exclusively by this module
        //until the FinishUp method is called
        public UI(string transDataLoc)
        {
            Console.WriteLine("UI");
            writer = File.AppendText("Log.txt");
            writer.WriteLine("STATUS > Log File opened");
            transReader = new StreamReader("A1TransData" + transDataLoc + ".txt");
            writer.WriteLine("STATUS > Trans Data File opened (A1TransData" + transDataLoc + ".txt)");            
        }

        //called by userApp to write a status message to the log file
        public void StartBkpRead()
        {
            writer.WriteLine("STATUS > Backup File opened");
        }

        //called by userApp to wrtie a status message to the log file   
        public void StartTransRead()
        {
            writer.WriteLine("STATUS > Trans Data processing started");
        }

        //used to open the log file for userApp. If the file hasn't been truncated yet, it 
        //does so. Otherwise it just opens it and writes a status message
        public static void UserAppStart(bool ifTuncated)
        {
            StreamWriter writer;
            if (ifTuncated)
            {
                writer = File.AppendText("Log.txt");
                writer.WriteLine("STATUS > Log File opened");
            }
            else 
            {
                writer = new StreamWriter(File.Open("Log.txt", FileMode.Truncate));
                writer.WriteLine("STATUS > Log File opened and trunicated");
            }
            writer.WriteLine("STATUS > UserApp started");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();
        }

        //prints a header that is used before either AI or AN. 
        public void PrintHeader(string code)
        {
            writer.WriteLine(code);
            writer.WriteLine("CDE ID- NAME-------------- CONTINENT---- ------AREA ---POPULATION LIFE");
        }

        //prints a footer after either Ai or AN is called
        public void PrintFooter()
        {
            writer.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        }

        //overloaded print method. only accepts one arguement, the ouput. 
        //used by AN and AI
        public void PrintOuputToLog(string output)
        {
            writer.WriteLine(output);
        }

        //overloaded print method accepts the original input and the output as arguements
        //used by SI, SN, DI, DN, and IN.
        public void PrintOuputToLog(string output, string input)
        {
            writer.WriteLine(input);
            writer.WriteLine("   " + output);
        }

        //used to read one transaction at a time. If the reader hits the end of the stream,
        //it returns false. Othewise it returns the transavtion line
        public bool ReadTrans(out string transCode, out string transString)
        {
            currentLine = transReader.ReadLine();
            if (transReader.EndOfStream)
            {
                transCode = "";
                transString = "";
                return false;
            }
            else 
            {
                transCode = currentLine.Substring(0, 2);
                transString = currentLine;
                return true;                
            }
            
        } 

        //called by userApp after processing all transactions
        //recieves the bkpReader in by reference. This allows it to be closed from here instead of in UserApp
        //closes the backupReader, the transReader, and the log file while writing status messages
        public void FinishUp(ref StreamReader bkpReader)
        {     
            writer.WriteLine("STATUS > Trans Data processing finished");
            bkpReader.Close();
            writer.WriteLine("STATUS > Backup File closed");            
            transReader.Close();
            writer.WriteLine("STATUS > Trans Data File closed");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();
        }

        //called by UserApp before its done. 
        //opens the log file, writes a status message, then closes the log
        public void UserAppFinishUp(int transCounter)
        {
            writer = File.AppendText("Log.txt");
            writer.WriteLine("STATUS > Lof File opened");
            writer.WriteLine("STATUS > UserApp finished - (" + transCounter.ToString() + " transactions processed)");
            writer.WriteLine("STATUS > Log File closed");
            writer.Close();
        }
    }
}
