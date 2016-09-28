#include "stdafx.h"
#include "Output.h"

using namespace System;
using namespace System::IO;

FileOUT::OutputFile::OutputFile()
{
	outFile = gcnew StreamWriter("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\OutputFile.txt");
}

void FileOUT::OutputFile::WriteARec(String^ &Code, String^ &Id, String^ &Name, String^ &Continent, String^ &Area, String^ &Population, String^ &lifeExp)
{
	outFile->WriteLine("{0}>{1}>{2}>{3}>{4}>{5}>{6}", Code, Id, Name, Continent, Area, Population, lifeExp);

	count++;
}

void FileOUT::OutputFile::FinishWithFile()
{
	outFile->Close();
	Console::WriteLine("Wrote out {0} records", count);

	Console::WriteLine("\n\nView InputFile.txt & OutputFile.txt in NotePad " +
		"in the Project's main folder");
}
