// PROGRAM:  AppendingToAFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This demonstrates using a separate class for all handling of a file,
//      including opening, writing to and closing it.   
// *************************************************************************************

using System;

namespace AppendingToAFile
{
    class Program
    {
        static void Main()
        {
            LogFile logFileObj = new LogFile();

            string name;
            string longerMessage;

            Console.Write("Enter a name:  ");
            name = Console.ReadLine();
            logFileObj.WriteThis(name);

            Console.Write("Enter another name:  ");
            name = Console.ReadLine();
            longerMessage = String.Format("The 2nd name is:  {0}", name);
            logFileObj.WriteThis(longerMessage);

            logFileObj.CloseFile();

            Console.WriteLine("\n\nLook at LogFile.txt in NotePad");
            Console.WriteLine("\nRUN THIS PROGRAM AGAIN a couple times\n");
        }
    }
}
