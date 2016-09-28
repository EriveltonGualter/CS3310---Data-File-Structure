// APPLICATION:  TwoProgShareClass  PROGRAM 1:  CreateProgram
//                                  PROGRAM 2:  DisplayProgram
//                                  CLASS:  TheFile
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This application is functionally the same as the prior demo example
//      (AppHas2Prog) except that the file which is created and displayed is defined
//      in a separate class.  Both programs utilize the class, although the class is
//      defined within the CreateProgram's namespace (project).  So the DisplayProgram
//      must be allowed to reference that class.  (See top comment in DisplayProgram
//      for instructions).
// NOTE ON <CR><LF>:  StreamReader's ReadLine inputs one line, including the <CR><LF>,
//      but only RETURNS the DATA portion of the line WITHOUT the <CR><LF> (as
//      programmers using StreamReader typically want).  However, WriteARec is using
//      FileStream which doesn't have a WriteLine, only a Write which writes out the
//      exact bytes specified.  But look at that method in TheFile class to see how it
//      handles this. 
// **************************************************************************************

using System;
using System.IO;

namespace TwoProgShareClass
{
    class CreateProgram
    {
        static void Main()
        {
            StreamReader inFile = new StreamReader(".//..//..//..//InFile.txt");

            TheFile f = new TheFile();

            string aLine;

            while (!inFile.EndOfStream)
            {
                aLine = inFile.ReadLine();
                f.WriteARec(aLine);
            }
            inFile.Close();
            f.CloseTheFile();

            Console.WriteLine("DONE creating the file");
        }
    }
}
