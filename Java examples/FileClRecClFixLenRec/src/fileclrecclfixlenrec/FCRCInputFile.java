// CLASS:  FCRCInputFile - used by Main
// THE FILE:  A text file with fixed-length records,
//      stored in the top-level project folder.
// IMPORTANT:  Note that only a single RECORD OBJECT is declared.  When processing files,
//      typically only ONE INPUT RECORD is ever in memory at once:  it's read in, then
//      processed, and is then no longer needed.  So the next record is read into the
//      VERY SAME memory location (allocated for that object).
//      Thus only a SINGLE RECORD OBJECT should be declared.
// **************************************************************************************

package fileclrecclfixlenrec;

import java.io.*;

public class FCRCInputFile 
{
    // *************************  DECLARATIONS  *************************************
    // These objects are DECLARED here, rather than in the constructor so that any 
    // method in this class can access the objects.
    // ******************************************************************************
    private static FileReader input;
    private static BufferedReader inFile;
    private static FCRCInputRec inRec;

    // *************************  CONSTRUCTOR  **************************************
    // The constructor method is run only ONCE and BEFORE any other methods run.
    // So this is the place to open the file, which'll be before ReadARec is done.
    // This is also the place where the SINGLE record object is set up.
    // ******************************************************************************
    public FCRCInputFile() throws FileNotFoundException
    {
        input = new FileReader("InputFile.txt");
        inFile = new BufferedReader(input);
        inRec = new FCRCInputRec();
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
    // This method calls the RECORD class's ReadARec method which actually reads in
    //      a record and divides it into the individual fields.  (That's not the
    //      FILE's responsibility).
    // ******************************************************************************
    public boolean ReadARec() throws IOException
    {
            boolean gotARec = inRec.ReadARec(inFile);
            FileClRecClFixLenRec.id = inRec.getId();
            FileClRecClFixLenRec.name = inRec.getName();
            FileClRecClFixLenRec.major = inRec.getMajor();
            FileClRecClFixLenRec.gpa = inRec.getGpa();

            return gotARec;
    }
    // ******************************************************************************
    public void FinishWithFile() throws IOException
    {
        inFile.close();
        System.out.format("Read in %d records\n", FCRCInputRec.count);
    }
}

