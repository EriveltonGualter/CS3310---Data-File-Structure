// PROGRAM:  CreateSerBinFile       CLASS:  SerBinRed
// AUTHOR:  D. Kaminski
// DESCRIPTION:  This class handles the record building for the Serialized Binary File.
//***************************************************************************************

using System;

namespace CreateSerBinFile
{
    [Serializable]              // NOTE THIS

    class SerBinRec
    {
        //*************************  FIELDS IN THE RECORD  ******************************
        // automatic properties
        // ******************************************************************************
        public int Id { get; set; }
        public string Name { get; set; }
        public float Gpa { get; set; }
        //*************************  CONSTRUCTORS  **************************************
        public SerBinRec(int id, string name, float gpa)
        {
            Id = id;
            Name = name;
            Gpa = gpa;
        }
    }
}
