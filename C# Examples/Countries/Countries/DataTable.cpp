#include "stdafx.h"
#include "DataTable.h"

DataNp::DataTable::DataTable()
{
	rawDataFile = gcnew StreamReader("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\OutputFile.txt");
	backupFile = gcnew StreamWriter("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Backup.txt");
	backupFile->WriteLine("************************* BACKUP FILE *************************");
}

bool DataNp::DataTable::InputTable()
{
	String^ theLine = rawDataFile->ReadLine();
	if (!rawDataFile->EndOfStream)
	{
		array<String^>^ field;
		field = theLine->Split('>');

		int adr = Convert::ToInt32(field[1], 10);

		this->Code[adr] = field[0];
		this->Id[adr] = field[1];
		this->Name[adr] = field[2];
		this->Continent[adr] = field[3];
		this->Area[adr] = field[4];
		this->Population[adr] = field[5];
		this->lifeExp[adr] = field[6];

		count++;
		return true;
	}
	for (int i = 0; i < MAX_ARRAY_SIZE; i++)
	{
		if (this->Code[i])
		{
			backupFile->WriteLine("DT>{0}>{1}>{2}>{3}>{4}>{5}>{6}",
				Code[i]->ToString(),
				Id[i]->ToString(),
				Name[i]->ToString(),
				Continent[i]->ToString(),
				Area[i]->ToString(),
				Population[i]->ToString(),
				lifeExp[i]->ToString());
		}
	}

	return false;
}