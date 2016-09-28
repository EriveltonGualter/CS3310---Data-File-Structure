// PROGRAM:  TDisplayProgram     in the PACKAGE:  Display
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This program displays a file.
//      The filename is specified as a parameter for Main.
// NOTE:  PUBLIC added to the class and to Main.
// **************************************************************************************

package display;

import java.io.*;

public class TDisplayProgram 
{
    public static void main(String[] args) throws FileNotFoundException, IOException 
    {
        String fileName = args[0];
        String extension = ".txt";
        
        //Sets up a file reader to read the file one character at a time
        FileReader inFile = new FileReader(fileName + extension);

        //Filter FileReader through a BufferedReader to read a line at a time
        BufferedReader input = new BufferedReader(inFile);
        
        String aLine;
        
        System.out.format("- - - - - - FILE: %s - - - - - -\n", fileName);

        while ((aLine = input.readLine()) != null)   //loop through input until End Of File
        {
            System.out.println(aLine);
        }
        input.close();
        inFile.close();  

        System.out.println("- - - - - - - - - - - - - - - - - - -\n");  
    }
}
