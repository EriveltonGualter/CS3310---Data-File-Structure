// PROGRAM:  ConvFixVarFields
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This builds upon the concepts in the prior 2 demo examples - reading/
//      writing fixed-length records and variable-length records (because they have
//      fixed-length fields and variable-length fields).  In those examples, the input
//      and output files were both of the same type - this isn't necessary, they can
//      be converted during processing.
// NUMERIC TYPES vs. STRINGS:
//      Convertionally we users expect:
//              - String fields have left-justified the actual data
//                  and space-fill (or truncate) on the right
//              - numeric fields have right-justified the actual data
//                  and zero-fill on the left
//      In the first 2 sets of conversions, everything is treated as strings, including
//          the id numbers.
//      In the second 2 sets of conversions, the id's are treated as numeric fields.
// **************************************************************************************

package convfixvarfields;

public class ConvFixVarFields 
{
    public static void main(String[] args) 
    {
        System.out.println("These treat all fields as just STRINGS\n");
        System.out.println("Fixed-length record         --> Variable-length record");
        RecTypeConversion.ConvertFixToVar("11  Li            Math3.21");
        RecTypeConversion.ConvertFixToVar("9876O'Leary       Math2.00");
        RecTypeConversion.ConvertFixToVar("123 Kopinski-JonesCS  3.97");

        System.out.println("\n\nVariable-length record      --> Fixed-length record");
        RecTypeConversion.ConvertVarToFix("11,Li,Math,3.21");
        RecTypeConversion.ConvertVarToFix("9876,O'Leary,Math,2.00");
        RecTypeConversion.ConvertVarToFix("123,Kopinski-Jones,CS,3.97");

        System.out.println("-------------------------------------------------------");
        System.out.println("\nDealing with integers as numeric:\n");

        String s1 = "1   ";
        String s2 = "123 ";
        String s3 = "1234";
        String s4 = "   4";
        String s5 = "  34";
        System.out.println("To right-justify & left-0-fill fixed-length strings:\n");
        System.out.println(String.format("\"%s\" to \"%4s\"", s1, s1.trim().replace(' ','0')));
        System.out.println(String.format("\"%s\" to \"%4s\"", s2, s2.trim().replace(' ','0')));
        System.out.println(String.format("\"%s\" to \"%4s\"", s3, s3.trim().replace(' ','0')));
        System.out.println(String.format("\"%s\" to \"%4s\"", s4, s4.trim().replace(' ','0')));
        System.out.println(String.format("\"%s\" to \"%4s\"", s5, s5.trim().replace(' ','0')));

        System.out.println("\nTo convert these strings to variable-length strings:\n");
        System.out.format("\"%s\" to \"%s\"\n", s1, s1.trim());
        System.out.format("\"%s\" to \"%s\"\n", s2, s2.trim());
        System.out.format("\"%s\" to \"%s\"\n", s3, s3.trim());
        System.out.format("\"%s\" to \"%s\"\n", s4, s4.trim());
        System.out.format("\"%s\" to \"%s\"\n", s5, s5.trim());
        //remove leading zeroes
        String s6 = "0004";
        System.out.format("\"%s\" to \"%s\"\n", s6, s6.replaceFirst("^0+(?!$)", ""));
    }
}
