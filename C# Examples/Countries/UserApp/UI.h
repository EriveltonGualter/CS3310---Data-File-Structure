#include "stdafx.h"
#include <msclr\auto_gcroot.h>

using namespace System;
using namespace System::IO;
using namespace System::Collections;

#define MAX_ARRAY_SIZE 250

namespace UInp
{
	class UI
	{
	public:
		UI();
		void ReadFile();
		void openFile();
		void closeFile();
		int nTrans = 0;

	private:
		msclr::auto_gcroot<System::IO::StreamReader^> backup;
		msclr::auto_gcroot<System::IO::StreamWriter^> logFile;


	private:
		msclr::auto_gcroot<System::IO::StreamReader^> TransData;

		msclr::auto_gcroot<String^> Code[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Id[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Name[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Continent[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Area[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> Population[MAX_ARRAY_SIZE];
		msclr::auto_gcroot<String^> lifeExp[MAX_ARRAY_SIZE];

	};
};