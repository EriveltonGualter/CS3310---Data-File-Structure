// PROGRAM:  ConvFixVarFields
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This builds upon the concepts in the prior 2 demo examples - reading/
//      writing fixed-length records and variable-length records (because they have
//      fixed-length fields and variable-length fields).  In those examples, the input
//      and output files were both of the same type - this isn't necessary, they can
//      be converted during processing.
// NUMERIC TYPES vs. STRINGS:
//      Convertionally we users expect:
//              - string fields have left-justified the actual data
//                  and space-fill (or truncate) on the right
//              - numeric fields have right-justified the actual data
//                  and zero-fill on the left
//      In the first 2 sets of conversions, everything is treated as strings, including
//          the id numbers.
//      In the second 2 sets of conversions, the id's are treated as numeric fields.
// **************************************************************************************

using System;

namespace ConvFixVarFields
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("These treat all fields as just STRINGS\n");
            Console.WriteLine("Fixed-length record         --> Variable-length record");
            RecTypeConversion.ConvertFixToVar("11  Li            Math3.21");
            RecTypeConversion.ConvertFixToVar("9876O'Leary       Math2.00");
            RecTypeConversion.ConvertFixToVar("123 Kopinski-JonesCS  3.97");

            Console.WriteLine("\n\nVariable-length record      --> Fixed-length record");
            RecTypeConversion.ConvertVarToFix("11,Li,Math,3.21");
            RecTypeConversion.ConvertVarToFix("9876,O'Leary,Math,2.00");
            RecTypeConversion.ConvertVarToFix("123,Kopinski-Jones,CS,3.97");

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("\nDealing with integers as numeric:\n");

            string s1 = "1   ";
            string s2 = "123 ";
            string s3 = "1234";
            string s4 = "   4";
            string s5 = "  34";
            Console.WriteLine("To right-justify & left-0-fill fixed-length strings:\n");
            Console.WriteLine("\"{0}\" to \"{1}\"", s1, s1.Trim().PadLeft(4, '0'));
            Console.WriteLine("\"{0}\" to \"{1}\"", s2, s2.Trim().PadLeft(4, '0'));
            Console.WriteLine("\"{0}\" to \"{1}\"", s3, s3.Trim().PadLeft(4, '0'));
            Console.WriteLine("\"{0}\" to \"{1}\"", s4, s4.Trim().PadLeft(4, '0'));
            Console.WriteLine("\"{0}\" to \"{1}\"", s5, s5.Trim().PadLeft(4, '0'));

            Console.WriteLine("\nTo convert these strings to variable-length strings:\n");
            Console.WriteLine("\"{0}\" to \"{1}\"", s1, s1.Trim());
            Console.WriteLine("\"{0}\" to \"{1}\"", s2, s2.Trim());
            Console.WriteLine("\"{0}\" to \"{1}\"", s3, s3.Trim());
            Console.WriteLine("\"{0}\" to \"{1}\"", s4, s4.Trim());
            Console.WriteLine("\"{0}\" to \"{1}\"", s5, s5.Trim());

            string s6 = "0004";
            Console.WriteLine("\"{0}\" to \"{1}\"", s6, s6.TrimStart('0'));
        }
    }
}