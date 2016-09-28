#include "stdafx.h"
#include <msclr\auto_gcroot.h>

using namespace System;
using namespace System::IO;
using namespace System::Collections;

#define MAX_ARRAY_SIZE 250
#define MAX_OPTIONS_SIZE 6

enum Option {
	Code,
	Id,
	Name,
	Continent,
	Area,
	Population,
	lifeExp
};

namespace FileIN
{
	class InputFile
	{
	private:
		msclr::auto_gcroot<System::IO::StreamReader^> inFile;
		msclr::auto_gcroot<System::IO::StreamWriter^> table;
		int count = 0;

	public:
		msclr::auto_gcroot<String^> Code[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Id[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Name[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Continent[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Area[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Population[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> lifeExp[MAX_ARRAY_SIZE];

	public:
		InputFile();
		bool ReadARec(String^ &Code, String^ &Id, String^ &Name, String^ &Continent, String^ &Area, String^ &Population, String^ &lifeExp, bool &write);
		void FinishWithFile();
		bool readIsDone();
	};
};
