// CLASS:  FCRCOutputFile - used by Main
// THE FILE:  A text file with fixed-length records,
//      stored in the top-level project folder.
// IMPORTANT:  Note that only a single RECORD OBJECT is declared.  As with the input
//      file, only a single output record is ever needed in memory at one time.
//      A record is built, written, and then no longer needed.
// **************************************************************************************

package fileclrecclfixlenrec;

import java.io.*;

public class FCRCOutputFile 
{
    // *************************  DECLARATIONS  *************************************
    // These objects are DECLARED here, rather than in the constructor so that any
    // method in this class can access the objects.
    // ******************************************************************************
    private static FileWriter outFile;
    private static FCRCOutputRec outRec;

    // *************************  CONSTRUCTOR  **************************************
    // The constructor method is run only ONCE and BEFORE any other methods run.
    // So this is the place to open the file, which'll be before WriteARec is done.
    // This is also the place where the SINGLE record object is set up.
    // ******************************************************************************
    public FCRCOutputFile() throws IOException
    {
        outFile = new FileWriter("OutputFile.txt");
        outRec = new FCRCOutputRec();
    }
    // *************************  METHODS  ******************************************

    // ******************************************************************************
    // This method calls the RECORD class's WriteARec method which actually builds
    //      the record from the individual fields and writes it to the file.
    //      (That's not the FILE's responsibility).
    // ******************************************************************************
    public void WriteARec(String id, String name, String major, String gpa) throws IOException
    {
        outRec.setId(id);
        outRec.setName(name);
        outRec.setMajor(major);
        outRec.setGpa(gpa);
        
        outRec.WriteARec(outFile);
    }
    // ******************************************************************************
    public void FinishWithFile() throws IOException
    {
        outFile.close();
        System.out.format("Wrote out %d records\n",FCRCOutputRec.count);
        System.out.println("\n\nView InputFile.txt & OutputFile.txt in NotePad " +
            "in the Project's main folder");
    }
}

