#include "stdafx.h"
#include "RawData.h"

FileIN::InputFile::InputFile()
{
	inFile = gcnew StreamReader("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Inputs\\TestSample.txt");
	System::IO::StreamWriter^ logFile;
	logFile = gcnew StreamWriter("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Log-StatusMessages.txt",true);
	logFile->WriteLine("STATUS > RawData File opened (A1RawDataSample.csv)");
	logFile->Close();
}

bool FileIN::InputFile::ReadARec(String^ &Code, String^ &Id, String^ &Name, String^ &Continent, String^ &Area, String^ &Population, String^ &lifeExp, bool &write)
{

	String^ theLine = inFile->ReadLine();
	if (!inFile->EndOfStream)
	{
		if (String::Compare(theLine, 0, "INS", 0, 3) == 0)
		{
			array<String^>^ field;
			field = theLine->Split(',');

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

			this->Code[count] = field[0];
			this->Id[count] = field[1];
			this->Name[count] = field[2];
			this->Continent[count] = field[3];
			this->Area[count] = field[5];
			this->Population[count] = field[7];
			this->lifeExp[count] = field[8];

			Code = field[0];
			Id = field[1];
			Name = field[2];
			Continent = field[3];
			Area = field[5];
			Population = field[7];
			lifeExp = field[8];

			count++;
			write = true;
			return true;
		}
		write = false;
		return true;
	}
	else
	{
		//Code = Id = Name = "";
		write = false;
		System::IO::StreamWriter^ logFile;
		logFile = gcnew StreamWriter("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\C# Examples\\Countries\\Files\\Outputs\\Log-StatusMessages.txt", true);
		logFile->WriteLine("STATUS > RawData File closed (A1RawDataSample.csv)");
		logFile->Close();
		return false;
	}
}

void FileIN::InputFile::FinishWithFile()
{
	inFile->Close();
	Console::WriteLine("Read in {0} records", count);
}

bool FileIN::InputFile::readIsDone()
{
	return (this->inFile->EndOfStream);
}

