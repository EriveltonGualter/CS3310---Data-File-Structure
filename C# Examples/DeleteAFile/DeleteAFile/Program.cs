// PROGRAM:  DeleteAFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This demonstrates:
//      - checking the size of the file
//      - delete a file from within a program
//      - checking whether a file exists
// **************************************************************************************

using System;
using System.IO;

namespace DeleteAFile
{
    class Program
    {
        static void Main()
        {
            string fileName = ".//..//..//..//AFile.txt";
            //                                          CREATE THE FILE
            StreamWriter f = new StreamWriter(fileName);
            f.WriteLine("Kalamazoo");
            f.WriteLine("Michigan");
            f.Close();
            Console.WriteLine("Created AFile.txt & wrote 2 records & closed it.");

            //                                          MANUALLY CHECK IF IT EXISTS
            Console.WriteLine("\n\n(PAUSING EXECUTION NOW)" +
                "\n\tCheck solution folder for AFile.txt (& view it in NotePad)." +
                "\n(When done, hit ENTER to continue)");
            Console.ReadLine();

            //                                          PROGRAM CHECKS IF IT EXISTS
            Console.Write("\nChecking from within the program whether file exists:  ");
            if (File.Exists(fileName))
                Console.WriteLine("YES it does.");
            else
                Console.WriteLine("NO it doesn't.");

            //                                          DETERMINE FILE'S SIZE
            FileInfo fileInfo = new FileInfo(fileName);
            Console.WriteLine("\n\nThe filesize (in bytes, including <CR><LF>) is:  {0}",
                fileInfo.Length);

            //                                          DELETE THE FILE
            Console.WriteLine("\n\nWill now DELETE the file from within the program.");
            File.Delete(fileName);

            //                                          NOW CHECK IF FILE EXISTS
            Console.WriteLine("\n\n(PAUSING EXECUTION AGAIN)" +
                "\n\tCheck solution folder for AFile.txt - it should be DELETED." +
                "\n(When done, hit ENTER to continue)");
            Console.ReadLine();

            //                                          AGAIN, PROGRAM CHECKS FOR IT
            Console.Write("\nChecking from within the program whether file exists:  ");
            if (File.Exists(fileName))
                Console.WriteLine("YES it does.\n");
            else
                Console.WriteLine("NO it doesn't.\n");
        }
    }
}
