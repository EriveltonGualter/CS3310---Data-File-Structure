// Setup.cpp : main project file.

#include "stdafx.h"
#include "RawData.h"
#include "Output.h"
#include "DataTable.h"

using namespace System;
using namespace FileIN;
using namespace FileOUT;
using namespace DataNp;

int main(array<System::String ^> ^args)
{
	String^  Code;
	String^  Id;
	String^  Name;
	String^  Continent;
	String^  Area;
	String^  Population;
	String^  lifeExp;

	bool canWrite;
	int nCountries=0;
	
	System::IO::StreamWriter^ logFile;
	logFile = gcnew StreamWriter("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Log-StatusMessages.txt");
	logFile->WriteLine("STATUS > Setup Started");
	logFile->Close();

	InputFile inFile;
	OutputFile outFile;

	while (inFile.ReadARec(Code, Id, Name, Continent, Area, Population, lifeExp, canWrite))
	{
		if (canWrite)
		{
			outFile.WriteARec(Code, Id, Name, Continent, Area, Population, lifeExp);
			nCountries++;
		}
	}
	inFile.FinishWithFile();
	outFile.FinishWithFile();

	DataTable dataTb;

	while (dataTb.InputTable())
	{

	}
	logFile = gcnew StreamWriter("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Log-StatusMessages.txt",true);
	logFile->WriteLine("STATUS > Setup finished - {0} countries processed", nCountries.ToString());
	logFile->Close();
	return 0;
}
