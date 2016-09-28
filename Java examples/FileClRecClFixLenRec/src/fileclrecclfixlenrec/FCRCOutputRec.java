// CLASS:  FCRCOutputRec - used by OutputFile CLASS
// A RECORD:   Fixed-length because of fixed-length fields.
// FIELDS:  The same fields as the InputRec except the fields are in a DIFFERENT order:
//          name (14 char's), gpa (4 char's), major (4 char's), id (4 char's), 
//                      input record:     11Li            Math3.21<CR><LF>
//                      output record:  Li            3.21Math  11<CR><LF>
//          (where <CR><LF> are 2 bytes:  CarriageReturn & LineFeed)
//      All are strings since this is a TEXT file.
//      But these are FIXED-LENGTH STRINGS vs. variable-length strings.
//          So an id of 11 is really "  11" (of length 4) and not "11".
// **************************************************************************************

package fileclrecclfixlenrec;

import java.io.*;

public class FCRCOutputRec
{
	// *************************  DECLARATIONS  *************************************
	public static int count = 0;

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
	// NOTE:  The sizes of the 4 fields need not be specified since they are already
	//      the appropriate fixed-length strings - e.g., Id is size 4 (e.g., "  11").
	//      Examine the OutputFile.txt in Notepad to demonstrate this to yourself.
	// ******************************************************************************
	public final void WriteARec(FileWriter outFile) throws IOException
	{
                outFile.write(getName() + getGpa() + getMajor() + getId() + "\r\n");
		count++;
	}
}
