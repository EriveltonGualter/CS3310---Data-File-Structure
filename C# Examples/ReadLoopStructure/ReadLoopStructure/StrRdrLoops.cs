// CLASS:  StrRdrLoops - used by ReadLoopStructure
//      - a STATIC class of STATIC methods, called by Main
// DESCRIPTION:  The methods here demonstrate the read loop structure to be used with
//              StreamReader's ReadLine method and EndOfStream property.
//      1st one) Read-Process loop works correctly
//      2nd one) Process-Read loop is WRONG - IT DOESN'T PROCESS THE LAST RECORD
//      3rd one) Shorter loop works correctly
// WHAT'S A STATIC CLASS?  It's a class used for the purpose of organizing code rather
//      than for OOP reasons.
//      A STATIC CLASS (like Main, Console, Math)
//          - is primarily a way of modularizing a program by grouping similar methods
//              into a separate static class and thus removing them from the Program
//              class (or having their code as part of a long Main)
//          - is used as a "class of services" (methods) provided to other
//          - contains only static members
//          - will NOT have an object instance declared for it
//          - so it does NOT contain storage for an object's attributes (instance
//                  variables) and thus no properties either
//          - does not a constructor since no object instance is being constructed
//                  (although a static constructor can be used to assign initial values
//                  when the program is executed)
//          - is called by specifying:   ClassName.MethodName
//                  rather than:         ObjectName.MethodName
//      vs. a REGULAR NON-STATIC CLASS, which
//          - is a way of defining a new datatype including:
//                  - what storage will be needed for any object instance of this class
//                      (instance variables for the object's attributes and thus 
//                       their properties)
//                  - what behaviors any object instance of this class can do
//                      (methods)
//          - for which an object instance of the class must be declared
//          - is called by specifying:   ObjectName.MethodName
// NOTE:  Instead of using a STATIC CLASS, one could just declare all methods in a
//      particular class as STATIC METHODS and accomplish the same thing.  However,
//      by declaring a static class, one ensures that the programmer can't accidentally
//      declare an instance object of that class.
// ************************************************************************************

using System;
using System.IO;

namespace ReadLoopStructure
{
    static class StrRdrLoops
    {
        // *************************** DECLARATIONS ***********************************
        // These variables/constant are used in several methods in this class, and so
        //      are defined here rather than as local variables in all 3 methods below.
        // ****************************************************************************
        private static string path = ".//..//..//..//";
        private static string fileName = "InFile.txt";

        private static string aLine;

        // *************************** PUBLIC METHODS *********************************
        // ****************************************************************************
        public static void DoReadProcessLoop()                 // G O O D
        {
            StreamReader inFile = new StreamReader(path + fileName);

            while (!inFile.EndOfStream)
            {
                aLine = inFile.ReadLine();
                ProcessTheRecord(aLine);
            }
            inFile.Close();
        }
        // ****************************************************************************
        public static void DoProcessReadLoop()                 // B A D
        {
            StreamReader inFile = new StreamReader(path + fileName);

            aLine = inFile.ReadLine();
            while (!inFile.EndOfStream)
            {
                ProcessTheRecord(aLine);
                aLine = inFile.ReadLine();
            }
            inFile.Close();
        }
        // ****************************************************************************
        public static void DoShorterLoop()                     // G O O D
        {
            StreamReader inFile = new StreamReader(path + fileName);

            while ((aLine = inFile.ReadLine()) != null)
            {
                ProcessTheRecord(aLine);
            }
            inFile.Close();
        }
        // *************************** PRIVATE METHOD *********************************
        // ****************************************************************************
        private static void ProcessTheRecord(string theLine)
        {
            int id = Convert.ToInt32(theLine.Substring(0, 2));
            string name = theLine.Substring(2, 8);
            float gpa = Convert.ToSingle(theLine.Substring(10, 4));
            Console.WriteLine("{0:D2}  {1}  {2}", id, name, gpa);
        }
    }
}
