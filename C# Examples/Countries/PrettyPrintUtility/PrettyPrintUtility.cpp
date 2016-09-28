// PrettyPrintUtility.cpp : main project file.

#include "stdafx.h"

using namespace System;
using namespace System::IO;

#define MAX_ARRAY_SIZE 250

String^ norm1(String^ nnorm)
{
	String^ newstring;
	int l = nnorm->Length;
	int d = l / 3;
	int m = l % 3;

	if (nnorm != "0")
	{
		newstring = nnorm->Substring(0, m);
		int p = m;
		for (int i = 0; i < d; i++)
		{
			if (m == 0)
				newstring = newstring + nnorm->Substring(p, 3) + ",";
			else
				newstring = newstring + "," + nnorm->Substring(p, 3);
			p = p + 3;
		}
		if (newstring->Substring(newstring->Length - 1, 1) == ",")
			newstring = newstring->Substring(0, newstring->Length - 1);
	}
	else
		newstring = "000,000,000";

	return newstring;
}

String^ norm2(String^ nnorm)
{
	String^ newstring = nnorm;
	if (nnorm == "0")
		newstring = "00.0";
	if (nnorm->Length == 2)
		newstring = nnorm + ".0";
	return newstring;
}

int main(array<System::String ^> ^args)
{
	System::IO::StreamWriter^ logFile;
	logFile = gcnew StreamWriter("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Log-StatusMessages.txt", true);
	logFile->WriteLine("STATUS > PrettyPrintUtility started");

	StreamWriter^ pretty;
	StreamReader^ backup;
	backup = gcnew StreamReader("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Backup.txt");

	array< array< String^ >^ >^ tableData(gcnew array< array< String^ >^ >(MAX_ARRAY_SIZE));
	array<String^>^ names = gcnew array<String^>(MAX_ARRAY_SIZE);
	array<int>^ ids = gcnew array<int>(MAX_ARRAY_SIZE);

	int i = 0;
	int storage = 0;
	String^ theLine;
	while (!backup->EndOfStream)
	{
		theLine = backup->ReadLine();
		if (String::Compare(theLine, 0, "*", 0, 1) != 0 & String::Compare(theLine, 0, " ", 0, 1) != 0)
		{
			if (String::Compare(theLine, 0, "DT", 0, 2) == 0)
			{
				array<String^>^ field;
				field = theLine->Split('>');

				field[3]->Trim();
				field[5] = norm1(field[5]);
				field[6] = norm1(field[6]);
				field[7] = norm2(field[7]);

				tableData[i] = field;
				ids->SetValue(Convert::ToInt32(field[2], 10), i);

				storage++;
			}
		}
		i++;
	}

	logFile->WriteLine("N: {0}", storage.ToString());
	logFile->WriteLine("DATA STORAGE");
	logFile->WriteLine("LOC/ CDE ID- NAME---------------------------------------- CONTINENT---- ------AREA ---POPULATION LIFE");
	Array::Sort(ids, tableData);
	int loc = 0;
	for (int i = 0; i < MAX_ARRAY_SIZE; i++)
	{
		if (ids[i])
		{
			loc++;
			logFile->WriteLine("{0}/ {1} {2} {3} {4} {5} {6} {7}",
				loc.ToString()->PadLeft(3, '0'),
				tableData[i][1],
				tableData[i][2]->Trim()->PadLeft(3, '0'),
				tableData[i][3]->Trim()->PadRight(44, ' '),
				tableData[i][4]->Trim()->PadRight(13, ' '),
				tableData[i][5]->Trim()->PadLeft(10, ' '),
				tableData[i][6]->Trim()->PadLeft(13, ' '),
				tableData[i][7]->Trim()->PadLeft(4, ' '));
		}
	}
	logFile->WriteLine("");
	logFile->WriteLine("NAME INDEX");
	logFile->WriteLine("LOC/ NAME---------------------------------------- PTR");
	Array::Sort(ids, tableData);
	loc = 0;
	for (int i = 0; i < MAX_ARRAY_SIZE; i++)
	{
		if (ids[i])
		{
			loc++;
			logFile->WriteLine("{0}/ {1} {2}",
				loc.ToString()->PadLeft(3, '0'),
				tableData[i][3]->Trim()->PadRight(44, ' '),
				i.ToString()->PadLeft(3, '0'));
		}
	}
	logFile->WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
	logFile->WriteLine("STATUS > PrettyPrintUtility finished");
	logFile->Close();

	return 0;
}