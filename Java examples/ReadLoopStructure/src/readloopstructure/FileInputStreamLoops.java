// CLASS:  FileInputStreamLoops - used by ReadLoopStructure
//      - a class of STATIC methods, called by Main
// DESCRIPTION:  The methods here demonstrate the read loop structure to be used with
//              FileInputStream's Read and using numBytesRead = -1 as the EOF-check.
//      1st one) ProcessRead loop works correctly
//                  (the OPPOSITE of what works for FileReader)
//      2nd one) ReadProcess loop is WRONG - IT PROCESSES THE LAST RECORD TWICE
//      3rd one) Shorter loop works correctly
// ************************************************************************************

package readloopstructure;

import java.io.*;

public class FileInputStreamLoops 
{
    // *************************** DECLARATIONS ***********************************
    // This variable/constant is used in several methods in this class, and so
    //      is defined here rather than as a local variable in all 3 methods below.
    // ****************************************************************************
    private static final String fileName = "InFile.txt";
    private static String aLine;
    
    private static final int REC_SIZE = 2 + 8 + 4 + 2;   // id name gpa <CR><LF>
    private static byte aLineAsAByteArray[] = new byte[REC_SIZE];

    // *************************** PUBLIC METHODS *********************************
    // ****************************************************************************
    public static void ProcessReadLoop() throws FileNotFoundException, IOException                 // G O O D
    { 
        //create FileInputStream object
        FileInputStream inFile = new FileInputStream(fileName);
        
        int numBytesRead = inFile.read(aLineAsAByteArray, 0, REC_SIZE);        
        while (numBytesRead != -1)                  // START THE LOOP
        {                                    
            aLine = new String(aLineAsAByteArray);             // CONVERT BYTEARRAY TO STRING     
            System.out.print(aLine); 
                                                        
            numBytesRead = inFile.read(aLineAsAByteArray, 0, REC_SIZE);
        }
        inFile.close();
    }
    // ****************************************************************************
    public static void ReadProcessLoop() throws FileNotFoundException, IOException                 // B A D
    { 
        //create FileInputStream object
        FileInputStream inFile = new FileInputStream(fileName);
        
        int numBytesRead = REC_SIZE;          // NEEDED SO WHILE LOOP STARTS        
        while (numBytesRead != -1)
        {      
            numBytesRead = inFile.read(aLineAsAByteArray, 0, REC_SIZE);
                        
            aLine = new String(aLineAsAByteArray);             // CONVERT BYTEARRAY TO STRING     
            System.out.print(aLine); 
        }
        inFile.close();
    }
    // ****************************************************************************
    public static void ShorterLoop() throws FileNotFoundException, IOException                 // G O O D
    { 
        //create FileInputStream object
        FileInputStream inFile = new FileInputStream(fileName);
              
        while ((inFile.read(aLineAsAByteArray, 0, REC_SIZE)) != -1)
        {                                    
            aLine = new String(aLineAsAByteArray);             // CONVERT BYTEARRAY TO STRING     
            System.out.print(aLine); 
        }
        inFile.close();
    }
}
