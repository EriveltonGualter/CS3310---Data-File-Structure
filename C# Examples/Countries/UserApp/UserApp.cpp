// UserApp.cpp : main project file.

#include "stdafx.h"
#include "UI.h"
#include "NameIndex-UA.h"

using namespace System;
using namespace UInp;
using namespace NameNp;

int main(array<System::String ^> ^args)
{
	UI ui;
	ui.openFile();
	ui.ReadFile();
	ui.closeFile();

	NameIndex nameIndex;
	nameIndex.openfile();
	while (nameIndex.InputTable()){}
	nameIndex.closefile();
	

    return 0;
}
