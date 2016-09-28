// APPLICATION:  RanAccCkforEmpty       PROGRAM:  RanAccCkforEmpty
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This creates a BINARY random access file from data coming from an input 
//      file.  (See example 7a for more on binary files.)  The program checks the 
//      location before writing data to ensure that it is empty, and to check for 
//      duplicate RRNs.
// FILE STRUCTURE (for the random access file):  DIRECT ADDRESS
//      This means that the location where a record is placed in the file, (the record's
//      "relative record number" or RRN) is taken DIRECTLY from a field in the record.
// FILE ACCESS:  RANDOM ACCESS, so writing a record requires an RRN.
//      This is used since the records in the inFile are in no particular order, in
//      terms of ID.
// EMPTY LOCATIONS ("HOLES"):  Some locations will never have a record written to them.
//      This includes 2 types of locations, which each have a different way of the
//      program detecting them:
//          1) locations within the allocated file space for this file, between other
//              good records - the "all-0-bits" case.  (The system initializes the
//              file space allocated for this file to all-0-bits which is interpreted
//              as all '\0' characters in a char or string field, or as the value 0 in
//              an int or float field).
//          2) locations past the EOF, beyond the file space allocated for this file -
//              the "past EOF" case
// HOLE-CHECKING:  In this example, the CheckIfEmptyLoc method tries to read an int from
//      the location where the next record is going to be written.  If it succeeds, we 
//      know that the location is not empty, thus we have a duplicate RRN.  If it fails,
//      the exception is handled, and we know that the location is empty and is OK to be
//      written to.
// *************************************************************************************

package ranaccckforempty;

import java.io.*;

public class RanAccCkforEmpty 
{
    public static int id;
    public static char[] name = new char[8];
    public static float gpa;
    public static int idInRanFile;
        
    public static void main(String[] args) throws IOException 
    {
        //one record includes 1 int, 8 chars, & 1 float
        final int RAN_REC_SIZE = (Integer.SIZE / Byte.SIZE) + 8 + (Float.SIZE / Byte.SIZE); 
        //(SIZE is in bits, so we have to divide by size of byte to get number of bytes)
        
        //Sets up a file reader to read the file one character at a time
        FileReader inFile = new FileReader("DataFiles//InFile.txt");

        //Filter FileReader through a BufferedReader to read a line at a time
        BufferedReader input = new BufferedReader(inFile);
        
        String inputLine;
        
        //delete binary file if it exists - need to start fresh with each run
        new File("DataFiles//RandomFile.bin").delete();
                
        RandomAccessFile ranFile = new RandomAccessFile("DataFiles//RandomFile.bin", "rws");
        //"rws": Open for reading and writing (as with "rw") and also require that every 
        //  update to the file's content or metadata be written synchronously to the 
        //  underlying storage device. (no need for flushing)

        long offset;
        int RRN;
        boolean pastEOF;

        //- - - - - - - - - - - - - - - - - - - - - - - - CREATE THE FILE - - - - - - - - -
        while ((inputLine = input.readLine()) != null) // INPUT HAS <CR><LF>, SO readLine OK
        {
            id = Integer.parseInt(inputLine.substring(0, 2));
            name = inputLine.substring(2, 10).toCharArray();
            gpa = Float.parseFloat(inputLine.substring(10, 14));

            RRN = id;                               // use ID for DIRECT ADDRESS
               
            offset = (RRN - 1) * RAN_REC_SIZE;
            ranFile.seek(offset);
            idInRanFile = 0;                // NEED SEPARATE FIELD FROM ID SINCE PLAIN
                                            //    ID ALREADY FILLED IN WITH INPUT DATA

            pastEOF = CheckIfEmptyLoc(ranFile);

            if (pastEOF || idInRanFile == 0)        // i.e., it's an EMPTY LOCATION
            {      
                ranFile.seek(offset);
                ranFile.writeInt(id);
                for (int i=0; i<name.length; i++)
                {
                    ranFile.write((int)name[i]); //write char array as bytes
                }
                ranFile.writeFloat(gpa);
            }
            else
                System.out.format("ERROR FOR ID %d - LOCATION NOT EMPTY\n", id);
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - PRINT THE FILE - - - - - - - - -\
           
        System.out.println("\nOK, RandomFile.bin file created - Here it is:");
        System.out.println("   (and check it in the HexEditor)");

        ranFile.seek(0); //start back at the beginning for printing
        int i = 1;

        while (ReadARec(ranFile))
        {
            if (id == 0)
                System.out.format("%02d>>  EMPTY LOCATION\n", i);
            else
                System.out.format("%02d>>  %d  %s  %.2f\n", 
                        i, id, new String(name), gpa);
            i++;
        }
        inFile.close();
        ranFile.close();
    }
    //***************************************************************************
    static boolean CheckIfEmptyLoc(RandomAccessFile file)
    {
        try
        {
            idInRanFile = file.readInt();
            return false; //not empty
        }
        catch (Exception e)
        {
            idInRanFile = 0;
            return true; //empty
        }
    }
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    static boolean ReadARec(RandomAccessFile file)
    {
        byte[] tempName = new byte[8];
        String tempString;
        try
        {
            id = file.readInt();
            //with Java, we can't read directly into a char array, so instead we can:
            file.read(tempName); //read 8 chars into a byte array
            tempString = new String(tempName); //convert byte array to string using 
                                                                  //default charset
            name = tempString.toCharArray(); //convert string to char array
            gpa = file.readFloat();
            return true;
        }
        catch (Exception e)
        {
            id = 0;
            name = null;
            gpa = 0.0f;
            return false;
        }
    }
}
