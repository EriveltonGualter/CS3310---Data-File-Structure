// CLASS:  FCInputFile - used by Main
// THE FILE:  A text file with variable-length records using CSL,
//      stored in the top-level project folder.
// A RECORD:  Variable-length because of variable-length fields (using CSL), e.g.,
//                      11,Li,Math,3.21
// FIELDS:  id, name, major, gpa
//      All are strings since this is a TEXT file.
//      If the caller expected strings/ints/floats back, then ReadARec should do the
//      Converts and return the appropriate variables.
// **********************************************************************************

package fileclvarlenrec;

import java.io.*;

class FCInputFile 
{
    // *************************  DECLARATIONS  *************************************
    // inFile is DECLARED here, rather than in the constructor so that any method in
    //      this class can access the object (i.e., the file).
    // ******************************************************************************
    private static FileReader input;
    private static BufferedReader inFile;
    public static int count = 0;

    // *************************  CONSTRUCTOR  **************************************
    // The constructor method is run only ONCE and BEFORE any other methods run.
    // So this is the place to open the file, which'll be before ReadARec is done.
    // ******************************************************************************
    public FCInputFile() throws FileNotFoundException
    {
        input = new FileReader("InputFile.txt");
        inFile = new BufferedReader(input);
    }
    
    // *************************  METHODS  ******************************************

    // ******************************************************************************
    // This method sends back to the caller:
    //      - a true if an actual record has been read
    //      - or a false if it "read past EOF"
    // Since Java cannot pass variables by reference, the 4 fields are declared in 
    //      main, and edited as necessary within this method.  In C#, they could be 
    //      passed by reference (as separate OUT parameters).
    // This method assumes that the caller wants the 4 individual fields,
    //      rather than the whole record as a single unit.
    // ******************************************************************************
    public boolean ReadARec() throws IOException
    {
        String theLine = inFile.readLine();

        if (theLine.length() != 0)
        {
            String[] field = theLine.split(",");
            FileClVarLenRec.id = field[0];
            FileClVarLenRec.name = field[1];
            FileClVarLenRec.major = field[2];
            FileClVarLenRec.gpa = field[3];
            count++;
            return true;
        }
        else
        {
            FileClVarLenRec.id = FileClVarLenRec.name = 
                        FileClVarLenRec.major = FileClVarLenRec.gpa = "";
            return false;
        }
    }
    // ******************************************************************************
    public void FinishWithFile() throws IOException
    {
        inFile.close();
        System.out.format("Read in %d records\n",count);
    }
}
