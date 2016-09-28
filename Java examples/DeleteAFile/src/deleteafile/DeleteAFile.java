// PROGRAM:  DeleteAFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This demonstrates:
//      - checking the size of the file
//      - delete a file from within a program
//      - checking whether a file exists
// **************************************************************************************

package deleteafile;

import java.io.*;

public class DeleteAFile 
{
    public static void main(String[] args) throws IOException 
    {
        File testFile = new File("AFile.txt");
        //                                          CREATE THE FILE
        FileWriter f = new FileWriter(testFile);
        f.write("Kalamazoo\r\n");
        f.write("Michigan\r\n");
        f.close();
        System.out.println("Created AFile.txt & wrote 2 records & closed it.");

        //                                          MANUALLY CHECK IF IT EXISTS
        System.out.println("\n\n(PAUSING EXECUTION NOW)" +
            "\n\tCheck solution folder for AFile.txt (& view it in NotePad)." +
            "\n(When done, hit ENTER to continue)");
        System.in.read();

        //                                          PROGRAM CHECKS IF IT EXISTS
        System.out.print("\nChecking from within the program whether file exists:  ");
        if (testFile.exists())
            System.out.println("YES it does.");
        else
            System.out.println("NO it doesn't.");

        //                                          DETERMINE FILE'S SIZE
        //FileInfo fileInfo = new FileInfo(fileName);
        System.out.format("\n\nThe filesize (in bytes, including <CR><LF>) is:  %d",
            testFile.length());

        //                                          DELETE THE FILE
        System.out.println("\n\nWill now DELETE the file from within the program.");
        testFile.delete();

        //                                          NOW CHECK IF FILE EXISTS
        System.out.println("\n\n(PAUSING EXECUTION AGAIN)" +
            "\n\tCheck solution folder for AFile.txt - it should be DELETED." +
            "\n(When done, hit ENTER to continue)");
        System.in.read();

        //                                          AGAIN, PROGRAM CHECKS FOR IT
        System.out.print("\nChecking from within the program whether file exists:  ");
        if (testFile.exists())
            System.out.println("YES it does.\n");
        else
            System.out.println("NO it doesn't.\n");
    }
}
