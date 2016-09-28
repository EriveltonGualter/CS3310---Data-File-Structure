// PROGRAM:  ExtraMissingCRLF
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This project demonstrates situations where the program CODE is CORRECT
//      but the DATA file has an error.  The 2 methods for reading/processing a file
//      are shown, taken from the prior demo example:
//          1) using FileReader and BufferedReaders's readLine
//              (so it uses a ReadProcess loop)
//          2) using FileInputStream's read & numBytesRead = -1 as the EOF-check
//              (so it uses a ProcessRead loop)
//      These are both run 4 times to see what happens when the data file is:
//          1) GoodInFile.txt  (i.e., every record ends with a <CR><LF>)
//          2) InFileMissingLastCRLF.txt
//          3) InFileWithExtraEmbeddedCRLF.txt
//          4) InFileWith2ExtraCRLFatEnd.txt
// LESSON #1:  "Never trust human-created input" - do error-checking in the program.
//      Even something as simple as hitting the ENTER key an extra time or two can
//      cause problems.
// LESSON #2:  When debugging, don't assume that it's the PROGRAM that's in error, it
//      could be the programs's input DATA.  Debugging techniques involve "moving the
//      bar" back to "where's the last place where you KNOW things are working right",
//      which could be as far back as the INPUT DATA.
// ************************************************************************************

package extramissingcrlf;

import java.io.*;

public class ExtraMissingCRLF 
{
    public static void main(String[] args) throws FileNotFoundException, IOException 
    {
        String file[] = {   "GoodInFile.txt",
                            "InFileMissingLastCRLF.txt",
                            "InFileWithExtraEmbeddedCRLF.txt",
                            "InFileWith2ExtraCRLFatEnd.txt" };
        for (int i = 0; i < file.length; i++)
        {
            System.out.println("\nUsing " + file[i]);
            System.out.println("\t\twith FileReader");
            UseFileReader(file[i]);
            System.out.println("\t\twith FileInputStream");
            UseFileInputStream(file[i]);
        }
    }
    
    static void UseFileReader(String fileName) throws FileNotFoundException, IOException
    {
        //Sets up a file reader to read the file one character at a time
        FileReader inFile = new FileReader(fileName);

        //Filter FileReader through a BufferedReader to read a line at a time
        BufferedReader input = new BufferedReader(inFile);
        
        String aLine;
        int i = 1;

        while ((aLine = input.readLine()) != null)   //loop through input until End Of File
        {
            System.out.printf("Record # %d: [%s]\n", i, aLine);
            i++;
        }
        input.close();
        inFile.close();      
    }
    
    static void UseFileInputStream(String fileName) throws FileNotFoundException, IOException
    {
        //create FileInputStream object
        FileInputStream inFile = new FileInputStream(fileName);
        
        final int REC_SIZE = 2 + 8 + 4 + 2;   // id name gpa <CR><LF>  
        byte aLineAsAByteArray[] = new byte[REC_SIZE];
        String aLineAsAString;
        int i = 1;

        int numBytesRead = inFile.read(aLineAsAByteArray, 0, REC_SIZE);
        while (numBytesRead != -1)
        {
            aLineAsAString = new String(aLineAsAByteArray); // CONVERT BYTEARRAY TO STRING
                                            // TO STRIP OFF THE <CR><LF> DO:
            aLineAsAString = aLineAsAString.substring(0, REC_SIZE - 2);
            System.out.printf("Record # %d: [%s]\n", i, aLineAsAString);

            numBytesRead = inFile.read(aLineAsAByteArray, 0, REC_SIZE);
            i++;
        }
        inFile.close();
    }
}
