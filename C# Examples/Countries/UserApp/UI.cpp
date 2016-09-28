#include "stdafx.h"
#include "UI.h"


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

UInp::UI::UI()
{
	logFile = gcnew StreamWriter("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Log-StatusMessages.txt", true);
	logFile->WriteLine("STATUS > TransData FILE opened A1TransData4.txt");
}

void UInp::UI::ReadFile()
{
	String^ LineBU;
	String^ theLine;
	String^ param;

	int paramInt;

	//array< array< String^ >^ >^ tableData(gcnew array< array< String^ >^ >(MAX_ARRAY_SIZE));
	while (!TransData->EndOfStream)
	{
		param = theLine = this->TransData->ReadLine();
		array< array< String^ >^ >^ tableData(gcnew array< array< String^ >^ >(MAX_ARRAY_SIZE));
		array<String^>^ names = gcnew array<String^>(MAX_ARRAY_SIZE);
		array<int>^ ids = gcnew array<int>(MAX_ARRAY_SIZE);
		int i = 0;
		this->backup = gcnew StreamReader("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Backup.txt", true);
		while (!backup->EndOfStream)
		{
			LineBU = backup->ReadLine();
			if (String::Compare(LineBU, 0, "*", 0, 1) != 0 & String::Compare(LineBU, 0, " ", 0, 1) != 0)
			{
				if (String::Compare(LineBU, 0, "DT", 0, 2) == 0)
				{
					array<String^>^ field;
					field = LineBU->Split('>');

					field[3]->Trim();
					field[5] = norm1(field[5]);
					field[6] = norm1(field[6]);
					field[7] = norm2(field[7]);

					tableData[i] = field;

					names->SetValue(field[3]->ToLower(), i);
					ids->SetValue(Convert::ToInt32(field[2], 10), i);
				}
			}
			i++;
		}
		backup->Close();
		theLine = theLine->Substring(0, 2);
		if (param->Length > 2)
		{
			param = param->Substring(3, param->Length - 3);
			int letter = 0;
			int digit = 0;
			int punctuation = 0;

			for each (Char ch in param) {
				if (Char::IsDigit(ch))
					digit++;
				else if (Char::IsLetter(ch))
					letter++;
			}
			if (digit > 0 & letter == 0)
				paramInt = Convert::ToInt32(param->Trim(), 10);

		}
		if (theLine == "IN")
		{
			array<String^>^ field;
			field = param->Split(',');

			array<String^>^ trash;
			trash = field[0]->Split('(');
			field[0] = trash[1];
			trash = field[15]->Split(')');
			field[15] = trash[0];

			for (int i = 0; i < 16; i++)
			{
				if (String::Compare(field[i], 0, "'", 0, 1) == 0)
				{
					field[i] = field[i]->Remove(field[i]->Length - 1);
					field[i] = field[i]->Remove(0, 1);
				}
			}

			logFile->WriteLine("IN  {0}, {1}, {2}, {3}, {4}, {5}, {6}",
				field[0]->ToString(),
				field[1]->ToString(),
				field[2]->ToString(),
				field[3]->ToString(),
				field[5]->ToString(),
				field[7]->ToString(),
				field[8]->ToString());

			Array::Sort(ids, tableData);
			int result(0);
			result = Array::BinarySearch(ids, Convert::ToInt32(field[1], 10));

			if (result > 0)                                 // Check the result
			{
				logFile->WriteLine("  ERROR, the id is already used");
			}
			else
			{
				msclr::auto_gcroot<System::IO::StreamWriter^> newBackup;
				newBackup = gcnew StreamWriter("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Backup.txt", true);
				newBackup->WriteLine("DT>{0}>{1}>{2}>{3}>{4}>{5}>{6}",
					field[0]->ToString(),
					field[1]->ToString(),
					field[2]->ToString(),
					field[3]->ToString(),
					field[5]->ToString(),
					field[7]->ToString(),
					field[8]->ToString());
				newBackup->WriteLine("NI>{0}>{1}>{2}>{3}>{4}>{5}>{6}",
					field[0]->ToString(),
					field[1]->ToString(),
					field[2]->ToString(),
					field[3]->ToString(),
					field[5]->ToString(),
					field[7]->ToString(),
					field[8]->ToString());
				newBackup->Close();
				logFile->WriteLine("  OK, country inserted (in data storage & name index)");
			}

		nTrans++;
		}

		if (theLine == "DN")
		{
			logFile->WriteLine("DN");
			logFile->WriteLine("   SORRY, Delete By Name module not yet working");
			nTrans++;
		}

		if (theLine == "DI")
		{
			logFile->WriteLine("DI");
			logFile->WriteLine("   SORRY, Delete By Id module not yet working");
			nTrans++;
		}

		if (theLine == "SN")
		{
			logFile->WriteLine("SN {0}", param);
			logFile->WriteLine("  CDE ID- NAME---------------------------------------- CONTINENT---- ------AREA ---POPULATION LIFE");

			Array::Sort(names, tableData);
			int result(0);
			result = Array::BinarySearch(names, param->Trim()->ToLower());

			if (result < 0)                                 // Check the result
			{
				logFile->WriteLine("  ERROR, invalid country name");
			}
			else
				logFile->WriteLine("  {0} {1} {2} {3} {4} {5} {6}",
				tableData[result][1],
				tableData[result][2]->Trim()->PadLeft(3, '0'),
				tableData[result][3]->Trim()->PadRight(44, ' '),
				tableData[result][4]->Trim()->PadRight(13, ' '),
				tableData[result][5]->Trim()->PadLeft(10, ' '),
				tableData[result][6]->Trim()->PadLeft(13, ' '),
				tableData[result][7]->Trim()->PadLeft(4, ' '));
			nTrans++;
		}

		if (theLine == "SI")
		{
			logFile->WriteLine("SI {0}", param->ToString());
			logFile->WriteLine("  CDE ID- NAME---------------------------------------- CONTINENT---- ------AREA ---POPULATION LIFE");
			Array::Sort(ids, tableData);

			int result(0);
			result = Array::BinarySearch(ids, Convert::ToInt32(param, 10));

			if (result < 0 || Convert::ToInt32(param, 10) == 0)                                 // Check the result
			{
				logFile->WriteLine("  ERROR, invalid country id");
			}
			else
				logFile->WriteLine("  {0} {1} {2} {3} {4} {5} {6}",
					tableData[result][1],
					tableData[result][2]->Trim()->PadLeft(3, '0'),
					tableData[result][3]->Trim()->PadRight(44, ' '),
					tableData[result][4]->Trim()->PadRight(13, ' '),
					tableData[result][5]->Trim()->PadLeft(10, ' '),
					tableData[result][6]->Trim()->PadLeft(13, ' '),
					tableData[result][7]->Trim()->PadLeft(4, ' '));
			nTrans++;
		}

		if (theLine == "AN")
		{
			logFile->WriteLine("AN");
			logFile->WriteLine("  CDE ID- NAME---------------------------------------- CONTINENT---- ------AREA ---POPULATION LIFE");
			Array::Sort(names, tableData);
			for (int i = 0; i < MAX_ARRAY_SIZE; i++)
			{
				if (names[i])
					logFile->WriteLine("  {0} {1} {2} {3} {4} {5} {6}",
					tableData[i][1],
					tableData[i][2]->Trim()->PadLeft(3, '0'),
					tableData[i][3]->Trim()->PadRight(44, ' '),
					tableData[i][4]->Trim()->PadRight(13, ' '),
					tableData[i][5]->Trim()->PadLeft(10, ' '),
					tableData[i][6]->Trim()->PadLeft(13, ' '),
					tableData[i][7]->Trim()->PadLeft(4, ' '));
			}
			nTrans++;
			logFile->WriteLine("  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
		}

		if (theLine == "AI")
		{
			logFile->WriteLine("AI");
			logFile->WriteLine("  CDE ID- NAME---------------------------------------- CONTINENT---- ------AREA ---POPULATION LIFE");
			Array::Sort(ids, tableData);
			for (int i = 0; i < MAX_ARRAY_SIZE; i++)
			{
				if (ids[i])
					logFile->WriteLine("  {0} {1} {2} {3} {4} {5} {6}",
					tableData[i][1],
					tableData[i][2]->Trim()->PadLeft(3, '0'),
					tableData[i][3]->Trim()->PadRight(44, ' '),
					tableData[i][4]->Trim()->PadRight(13, ' '),
					tableData[i][5]->Trim()->PadLeft(10, ' '),
					tableData[i][6]->Trim()->PadLeft(13, ' '),
					tableData[i][7]->Trim()->PadLeft(4, ' '));
			}
			nTrans++;
			logFile->WriteLine("  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
		}
	}
}

void UInp::UI::openFile()
{
	this->TransData = gcnew StreamReader("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Inputs\\A1TransData4.txt");
	this->backup = gcnew StreamReader("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Backup.txt", true);
}

void UInp::UI::closeFile()
{
	this->TransData->Close();
	this->logFile->Close();
	this->backup->Close();

	logFile = gcnew StreamWriter("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Log-StatusMessages.txt", true);
	logFile->WriteLine("STATUS > TransData FILE closed {0} Transaction processed", nTrans.ToString());
	logFile->Close();
}