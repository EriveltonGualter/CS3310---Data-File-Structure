// CLASS:  RecTypeConversion - a static class used by ConvFixVarFields program
// **************************************************************************************

using System;

namespace ConvFixVarFields
{
    static class RecTypeConversion
    {
        // *****************************************************************************
        public static void ConvertFixToVar(string fixedLenRec)
        {
            Console.WriteLine("{0}", fixedLenRec);

            string id, name, major, gpa;

            id = fixedLenRec.Substring(0, 4);           // SPLIT RECORD INTO 4 FIELDS
            name = fixedLenRec.Substring(4, 14);        // (start in col 4, get 14 char)
            major = fixedLenRec.Substring(18, 4);
            gpa = fixedLenRec.Substring(22, 4);

            id = id.TrimEnd();                          // TRUNCATE THE RIGHT-END SPACES
            name = name.TrimEnd();
            major = major.TrimEnd();
            gpa = gpa.TrimEnd();
            
            string varLenRec =                          // BUILD THE VAR-LENGTH RECORD
                String.Format("{0},{1},{2},{3}", id, name, major, gpa);

            Console.WriteLine("\t\t\t\t{0}", varLenRec);
        }
        // *****************************************************************************
        public static void ConvertVarToFix(string varLenRec)
        {
            Console.WriteLine("{0}", varLenRec);

            string id, name, major, gpa;
            
            string[] field = varLenRec.Split(',');      // SPLIT RECORD INTO 4 FIELDS
            id = field[0];
            name = field[1];
            major = field[2];
            gpa = field[3];

            id = id.PadRight(4);                        // PAD THE RIGHT-END WITH SPACES
            name = name.PadRight(14);
            major = major.PadRight(4);
            gpa = gpa.PadRight(4);
                                            
            string fixedLenRec =                        // BUILD THE FIXED-LENGTH RECORD
                String.Format("{0}{1}{2}{3}", id, name, major, gpa);

            Console.WriteLine("\t\t\t\t{0}", fixedLenRec);
        }
    }
}
