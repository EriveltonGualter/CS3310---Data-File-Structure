// CLASS:  RecTypeConversion - a class used by ConvFixVarFields program
// **************************************************************************************

package convfixvarfields;

public class RecTypeConversion 
{
    // *****************************************************************************
    public static void ConvertFixToVar(String fixedLenRec)
    {
        System.out.println(fixedLenRec);

        String id, name, major, gpa;

        id = fixedLenRec.substring(0, 4);           // SPLIT RECORD INTO 4 FIELDS
        name = fixedLenRec.substring(4, 18);        // (start in col 4, get 14 char)
        major = fixedLenRec.substring(18, 22);
        gpa = fixedLenRec.substring(22, 26);
        
        id = id.replaceAll(" +$", "");              // TRUNCATE THE RIGHT-END SPACES
        name = name.replaceAll(" +$", "");
        major = major.replaceAll(" +$", "");
        gpa = gpa.replaceAll(" +$", "");
            
        String varLenRec =                          // BUILD THE VAR-LENGTH RECORD
            String.format("%s,%s,%s,%s", id, name, major, gpa);

        System.out.format("\t\t\t\t%s\n", varLenRec);
    }
    // *****************************************************************************
    public static void ConvertVarToFix(String varLenRec)
    {
        System.out.println(varLenRec);

        String id, name, major, gpa;
            
        String[] field = varLenRec.split(",");      // SPLIT RECORD INTO 4 FIELDS
        id = field[0];
        name = field[1];
        major = field[2];
        gpa = field[3];

        id = padRight(id,4);                        // PAD THE RIGHT-END WITH SPACES
        name = padRight(name,14);
        major = padRight(major,4);
        gpa = padRight(gpa,4);
                                            
        String fixedLenRec =                        // BUILD THE FIXED-LENGTH RECORD
            String.format("%s%s%s%s", id, name, major, gpa);

        System.out.format("\t\t\t\t%s\n", fixedLenRec);
    }
    
    // *****************************************************************************
    private static String padRight(String s, int n) //custom function to pad Strings
    {
        return String.format("%1$-" + n + "s", s);
    }
}
