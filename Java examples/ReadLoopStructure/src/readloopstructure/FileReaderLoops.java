// CLASS:  FileReaderLoops - used by ReadLoopStructure
//      - a class of STATIC methods, called by Main
// DESCRIPTION:  The methods here demonstrate the read loop structure to be used with
//              FileReader and BufferedReader's ReadLine method.
//      1st one) Read-Process loop works correctly
//      2nd one) Process-Read loop is WRONG - IT DOESN'T PROCESS THE LAST RECORD
// ************************************************************************************

package readloopstructure;

import java.io.*;

public class FileReaderLoops 
{
    // *************************** DECLARATIONS ***********************************
    // This variable/constant is used in several methods in this class, and so
    //      is defined here rather than as a local variable in all 3 methods below.
    // ****************************************************************************
    private static final String fileName = "InFile.txt";
    private static String aLine;

    // *************************** PUBLIC METHODS *********************************
    // ****************************************************************************
    public static void ReadProcessLoop() throws FileNotFoundException, IOException                 // G O O D
    {        
        //Sets up a file reader to read the file one character at a time
        FileReader inFile = new FileReader(fileName);

        //Filter FileReader through a BufferedReader to read a line at a time
        BufferedReader input = new BufferedReader(inFile);

        while ((aLine = input.readLine()) != null)   //loop through input until End Of File
        {
            System.out.println(aLine);
        }
        input.close();
        inFile.close();
    }
        // ****************************************************************************
    public static void ProcessReadLoop() throws FileNotFoundException, IOException                 // B A D
    {        
        //Sets up a file reader to read the file one character at a time
        FileReader inFile = new FileReader(fileName);

        //Filter FileReader through a BufferedReader to read a line at a time
        BufferedReader input = new BufferedReader(inFile);

        aLine = input.readLine();
        while (input.ready())   //check if BufferedReader has some content ready
        {           
            System.out.println(aLine);
            aLine = input.readLine();
        }
        input.close();
        inFile.close();
    }
}
