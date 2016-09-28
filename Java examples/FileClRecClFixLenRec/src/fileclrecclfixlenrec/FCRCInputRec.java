// CLASS:  FCRCInputRec - used by InputFile CLASS
// A RECORD:  Fixed-length because of fixed-length fields,
//      e.g., the input file:         11Li            Math3.21
//                                  9876O'Leary       Math2.00
//                                   123Kopinski-JonesCS  3.97
// FIELDS:  id (4 char's), name (14 char's), major (4 char's), gpa (4 char's)
//      All are strings since this is a TEXT file.
// **************************************************************************************

package fileclrecclfixlenrec;

import java.io.*;

public class FCRCInputRec 
{
    // *************************  DECLARATIONS  *************************************
    // NOTE:  These need not be static since there is only a single object of this
    //        class type.
    // ******************************************************************************
    public static int count = 0;

    private static int sizeOfId = 4;
    private static int sizeOfName = 14;
    private static int sizeOfMajor = 4;
    private static int sizeOfGpa = 4;

    // *************************  ATTRIBUTES & PROPERTIES  **************************
    // Automatic properties are used since there was no special processing to put
    // inside them.  These will set up the attributes and generate the basic default
    // get/set procedures for the 3 variables.
    // ******************************************************************************
    private String privateId;
    public final String getId()
    {
    	return privateId;
    }
    public final void setId(String value)
    {
    	privateId = value;
    }
    
    private String privateName;
    public final String getName()
    {
    	return privateName;
    }
    public final void setName(String value)
    {
    	privateName = value;
    }
    
    private String privateMajor;
    public final String getMajor()
    {
    	return privateMajor;
    }
    public final void setMajor(String value)
    {
    	privateMajor = value;
    }
    
    private String privateGpa;
    public final String getGpa()
    {
    	return privateGpa;
    }
    public final void setGpa(String value)
    {
    	privateGpa = value;
    }

    // ******************************************************************************
    // This returns true if ReadARec successfully read a record.
    // It returns false if the ReadLine failed because it read past the EOF.
    // 
    // One could divide theLine into fields using hard-coding - e.g.,
    //      Id = theLine.Substring(0, 4);           // start in col 0, get 4 char's
    //      Name = theLine.Substring(4, 14);
    //      Major = theLine.Substring(18, 4);
    //      Gpa = theLine.Substring(22, 4);
    // However, to help make this more generic for handling records with lots of
    //      fields & to reduce programmer arithmetic errors, the technique below is
    //      used.
    // ******************************************************************************
    public boolean ReadARec(BufferedReader inFile) throws IOException
    {
        String theLine = inFile.readLine();
        if (theLine.length() != 0)
        {
            int startCol = 0;
            setId(theLine.substring(startCol, sizeOfId));

            startCol += sizeOfId;
            setName(theLine.substring(startCol, (startCol + sizeOfName)));

            startCol += sizeOfName;
            setMajor(theLine.substring(startCol, (startCol + sizeOfMajor)));

            startCol += sizeOfMajor;
            setGpa(theLine.substring(startCol, (startCol + sizeOfGpa)));

            count++;
            return true;
        }
        else
        {
            setId("");
            setName("");
            setMajor("");
            setGpa("");
            return false;
        }
    }
}
