#include "stdafx.h"
#include <vector>
#include <msclr\auto_gcroot.h>

using namespace System;
using namespace System::IO;

namespace FileOUT
{
	class OutputFile
	{
	public:
		OutputFile();
	private:
		msclr::auto_gcroot<System::IO::StreamWriter^> outFile;
	public:
		int count;
		void WriteARec(String^ &Code, String^ &Id, String^ &Name, String^ &Continent, String^ &Area, String^ &Population, String^ &lifeExp);
		void FinishWithFile();
	};
};

//`Code` char(3) NOT NULL default '',
//`Id` int(3) NOT NULL default '0',
//`Name` char(52) NOT NULL default '',
//`Continent` enum('Asia', 'Europe', 'North America', 'Africa', 'Oceania', 'Antarctica', 'South America') NOT NULL default 'Asia',
//`Region` char(26) NOT NULL default '',
//`SurfaceArea` float(10, 2) NOT NULL default '0.00',
//`IndepYear` smallint(6) default NULL,
//`Population` int(11) NOT NULL default '0',
//`LifeExpectancy` float(3, 1) default NULL,
//`GNP` float(10, 2) default NULL,
//`GNPOld` float(10, 2) default NULL,
//`LocalName` char(45) NOT NULL default '',
//`GovernmentForm` char(45) NOT NULL default '',
//`HeadOfState` char(60) default NULL,
//`Capital` int(11) default NULL,
//`Code2` char(2) NOT NULL default '',