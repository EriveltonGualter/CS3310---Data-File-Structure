// CLASS:  FCOutputFile - used by Main
// THE FILE:  A text file with variable-length records using CSL,
//      stored in the top-level project folder.
// A RECORD:  Variable-length because of variable-length fields (using CSL).
//      The fields are in a DIFFERENT ORDER than the input file:
//                  input record:   11,Li,Math,3.21
//                  output record:  Li,3.21,Math,11
// FIELDS:  name, gpa, major, id
//      All are strings since this is a TEXT file.
// **************************************************************************************

package fileclvarlenrec;

import java.io.*;

class FCOutputFile 
{
    // *************************  DECLARATIONS  *************************************
    // outFile is DECLARED here, rather than in the constructor so that any method in
    //      this class can access the object (i.e., the file).
    // ******************************************************************************
    private static FileWriter outFile;
    public static int count = 0;

    // *************************  CONSTRUCTOR  **************************************
    // The constructor method is run only ONCE and BEFORE any other methods run.
    // So this is the place to open the file, which'll be before WriteARec is done.
    // ******************************************************************************
    public FCOutputFile() throws IOException
    {
        outFile = new FileWriter("OutputFile.txt");
    }
    // *************************  METHODS  ******************************************

    // ******************************************************************************
    public void WriteARec(String id, String name, String major, String gpa) throws IOException
    {
        outFile.write(name + "," + gpa + "," + major + "," + id + "\r\n");
        count++;
    }
    // ******************************************************************************
    public void FinishWithFile() throws IOException
    {
        outFile.close();
        System.out.format("Wrote out %d records\n",count);
        System.out.println("\n\nView InputFile.txt & OutputFile.txt in NotePad " +
            "in the Project's main folder");
    }
}
