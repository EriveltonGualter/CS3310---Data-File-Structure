// PROGRAM:  NotCloseOutFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This program demonstrates the results of failing to CLOSE an output
//      file.  (Not closing an input file does not cause this problem.  However, good 
//      programming practice dictates that a program should explicitly close all files
//      that it opens to release system resources).
// WHAT HAPPENS:  If an output file isn't closed, the last group of records do not
//      actually get written to the file.  The program WRITES them (as "proven" by the
//      System.out.println immediately after the outFile.write).  However, when
//      writing to a FILE, the data is actually only written to an internal buffer set up
//      for that file.  When the buffer gets full, it's dumped out to the file.  The
//      buffer is also dumped to the file when the program closes the file.  So, ... if
//      the program doesn't close it, the buffer doesn't get flushed.
//      [NOTE:  Some languages/systems DO automatically "flush the buffer" when the
//              program finishes executing, even without an explicit close of the file].
// DEMONSTRATION:
//    1)Run this program (with the CLOSE (line 79) commented out).  Then check the 2 
//      output files in NotePad.  Also see the file sizes in the main project folder.
//    2) Uncomment outFile.close() and run the program again.  Check the 2 output files
//          in NotePad and note that all the records are now there.
// REMINDER / CAUTION:  System.out.println (and even the interactive debugger) can
//          "LIE TO YOU" (well, MISLEAD you anyway) !
// NOTE: This program demonstrates "proper" error handling for opening and closing files.
//          Subsequent examples will not include proper error handling in order to 
//          keep the focus on what is being demonstrated.

package notcloseoutfile;

import java.io.*;

public class NotCloseOutFile 
{
    public static void main(String[] args)
    {
        String inFileName = "InFile.txt";
        String outFileName = "OutFile.txt";
        
        //Sets up a file reader to read the file one character at a time
        FileReader inFile = null;
        try {
            inFile = new FileReader(inFileName);
        } catch (FileNotFoundException ex) {    //catch error opening input file
            System.out.println("Error opening " + inFileName + ": " + ex);
        }
        //Filter FileReader through a BufferedReader to read a line at a time
        BufferedReader input = new BufferedReader(inFile);
        
        //create FileWriter
        FileWriter outFile = null;     
        try {
            outFile = new FileWriter(outFileName);
            System.out.println("\nNow creating " + outFileName + "\n");
        } catch (IOException ex) {  //catch error creating/opening output file
            System.out.println("Error creating/opening " + outFileName + ": " + ex);
        }
        
        String line;
        try {
            while ((line = input.readLine()) != null)   //loop through input until End Of File
            {
                outFile.write(line + "\r\n");
                System.out.println(line);           //"prove" that it's been written
            }
        } catch (IOException ex) {
            System.out.println("Error reading from " + inFileName + ": " + ex);
        }
        
        System.out.println("\nOutFile has been created.\n\n" +
                "Check it in NotePad to see if all the records are there.\n" +
                "Also check the file sizes in the main project folder:\n\t" +
                "InFile: 2 KB, OutFile: 0 KB.");
        try {
            inFile.close();
        } catch (IOException ex) {
            System.out.println("Error closing " + inFileName + ": " + ex);
        }
        try {
            input.close();
            //outFile.close();      //OOPS!  Didn't close the file!
        } catch (IOException ex) {
            System.out.println("Error closing " + outFileName + ": " + ex);
        }
    }
}
