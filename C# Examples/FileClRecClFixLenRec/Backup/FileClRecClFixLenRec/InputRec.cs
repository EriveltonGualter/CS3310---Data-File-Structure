// CLASS:  InputRec - used by InputFile CLASS
// A RECORD:  Fixed-length because of fixed-length fields,
//      e.g., the input file:         11Li            Math3.21
//                                  9876O'Leary       Math2.00
//                                   123Kopinski-JonesCS  3.97
// FIELDS:  id (4 char's), name (14 char's), major (4 char's), gpa (4 char's)
//      All are strings since this is a TEXT file.
// **************************************************************************************

using System;
using System.IO;

namespace FileClRecClFixLenRec
{
    class InputRec
    {
        // *************************  DECLARATIONS  *************************************
        // NOTE:  These need not be static since there is only a single object of this
        //      class type.
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
        public string Id { get; set; }
        public string Name { get; set; }
        public string Major { get; set; }
        public string Gpa { get; set; }

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
        public bool ReadARec(StreamReader inFile)
        {
            string theLine = inFile.ReadLine();
            if (theLine != "")
            {
                int startCol = 0;
                Id = theLine.Substring(startCol, sizeOfId);

                startCol += sizeOfId;
                Name = theLine.Substring(startCol, sizeOfName);

                startCol += sizeOfName;
                Major = theLine.Substring(startCol, sizeOfMajor);

                startCol += sizeOfMajor;
                Gpa = theLine.Substring(startCol, sizeOfGpa);

                count++;
                return true;
            }
            else
            {
                Id = Name = Major = Gpa = "";
                return false;
            }
        }
    }
}
