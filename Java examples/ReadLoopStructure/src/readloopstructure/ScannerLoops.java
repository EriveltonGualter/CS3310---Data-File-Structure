// CLASS:  ScannerLoops - used by ReadLoopStructure
//      - a class of STATIC methods, called by Main
// DESCRIPTION:  The methods here demonstrate the read loop structure to be used with
//              Scanner and its hasNextLine method.
//      1st one) Read-Process loop works correctly
//      2nd one) Process-Read loop is WRONG - IT DOESN'T PROCESS THE LAST RECORD
// ************************************************************************************

package readloopstructure;

import java.io.*;
import java.util.Scanner;

public class ScannerLoops 
{
    // *************************** DECLARATIONS ***********************************
    // This variable/constant is used in several methods in this class, and so
    //      is defined here rather than as a local variable in all 3 methods below.
    // ****************************************************************************
    private static final String fileName = "InFile.txt";
    private static String aLine;

    // *************************** PUBLIC METHODS *********************************
    // ****************************************************************************
    public static void ReadProcessLoop() throws FileNotFoundException                 // G O O D
    {
        File inFile = new File(fileName);
        Scanner scanner = new Scanner(inFile);

        while (scanner.hasNextLine())
        {
            aLine = scanner.nextLine();
            System.out.println(aLine);            
        }
        scanner.close();
    }
    // ****************************************************************************
    public static void ProcessReadLoop() throws FileNotFoundException                 // B A D
    {
        File inFile = new File(fileName);
        Scanner scanner = new Scanner(inFile);

        aLine = scanner.nextLine();
        while (scanner.hasNextLine())
        {   
            System.out.println(aLine); 
            aLine = scanner.nextLine();           
        }
        scanner.close();
    }
}
