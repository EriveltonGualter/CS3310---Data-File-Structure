#include "stdafx.h"
#include <msclr\auto_gcroot.h>

using namespace System;
using namespace System::IO;
using namespace System::Collections;

#define MAX_ARRAY_SIZE 250

namespace NameNp
{
	class NameIndex
	{
	public:
		NameIndex();
		bool InputTable();

	public:
		void openfile();
		void closefile();
		msclr::auto_gcroot<System::IO::StreamReader^> rawDataFile;
		msclr::auto_gcroot<System::IO::StreamWriter^> backupFile;
		int count = 0;

		msclr::auto_gcroot<String^> KV[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> DPR[MAX_ARRAY_SIZE];

	};
};
