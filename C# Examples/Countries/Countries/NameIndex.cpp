#include "stdafx.h"
#include "NameIndex.h"

NameNp::NameIndex::NameIndex()
{
}

bool NameNp::NameIndex::InputTable()
{
	String^ theLine = rawDataFile->ReadLine();
	if (!rawDataFile->EndOfStream)
	{
		if (String::Compare(theLine, 0, "DT", 0, 2) == 0)
		{
			array<String^>^ field;
			field = theLine->Split('>');

			int adr = Convert::ToInt32(field[1], 10);

			this->DPR[adr] = field[2];
			this->KV[adr] = field[3];

			count++;
		}
		return true;
	}
	for (int i = 0; i < MAX_ARRAY_SIZE; i++)
	{
		if (DPR[i] || KV[i])
			backupFile->WriteLine("NI>{0}>{1}", DPR[i]->ToString(), KV[i]->ToString());
	}
	return false;
}

void NameNp::NameIndex::openfile()
{
	rawDataFile = gcnew StreamReader("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\OutputFile.txt");
	backupFile = gcnew StreamWriter("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Backup.txt", true);
	backupFile->WriteLine("************************* Name Index *************************");
}

void NameNp::NameIndex::closefile()
{
	backupFile->Close();
	rawDataFile->Close();
}