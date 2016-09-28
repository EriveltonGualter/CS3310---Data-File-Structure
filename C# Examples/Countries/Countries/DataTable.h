#include "stdafx.h"
#include <msclr\auto_gcroot.h>

using namespace System;
using namespace System::IO;
using namespace System::Collections;

#define MAX_ARRAY_SIZE 250

namespace DataNp
{
	class DataTable
	{
	public:
		DataTable();
		bool InputTable();

	private:
		msclr::auto_gcroot<System::IO::StreamReader^> rawDataFile;
		msclr::auto_gcroot<System::IO::StreamWriter^> backupFile;
		int count = 0;

		msclr::auto_gcroot<String^> Code[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Id[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Name[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Continent[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Area[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Population[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> lifeExp[MAX_ARRAY_SIZE];


	};
};
