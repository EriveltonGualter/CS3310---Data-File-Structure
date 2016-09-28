// CrowdOrganizer-A4.cpp : main project file.

// PROGRAM:  CrownOrganizer-A4
// AUTHOR:  Erivelton Gualter dos Santos
// DESCRIPTION:  It handles the class CustomerPrQ. Also, 
//      including opening, writing to and closing it.   
// *************************************************************************************

#include "stdafx.h"
#include "CustomerPrQ.h"

using namespace System;
using namespace System::IO;

using namespace NameCP;

int main(array<System::String ^> ^args)
{
	StreamWriter^ logFile;
	logFile = gcnew StreamWriter("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\CrowdOrganizer-A4\\CrowdOrganizer-A4\\Files\\Log.txt");
	logFile->WriteLine(">>> program starting");

	int nodes;

	CustomerPrQ customerPrQ;
	
	StreamReader^ inEvent;
	inEvent = gcnew StreamReader("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\CrowdOrganizer-A4\\CrowdOrganizer-A4\\Files\\Events.txt");

	String^ theLine = inEvent->ReadLine();
	while (!inEvent->EndOfStream)
	{
		if (String::Compare(theLine, 0, "//", 0, 2) != 0)
		{
			if (String::Compare(theLine, 0, "O", 0, 1) == 0)
			{
				logFile->WriteLine("STORE IS OPENING");
				nodes = customerPrQ.arrangeCustomerQ();
				logFile->WriteLine(">>> initial heap built containing {0} nodes", Convert::ToString(nodes)); 
				logFile->WriteLine("");
			}
			if (String::Compare(theLine, 0, "C", 0, 1) == 0)
			{
				logFile->WriteLine("");
				logFile->WriteLine("STORE IS CLOSING");
				int rnodes;
				rnodes = customerPrQ.serveRemainingCustomers();
				logFile->WriteLine(">>> heap currently has {0} nodes remaining", Convert::ToString(rnodes));

				while (customerPrQ.serveRemainingCustomers())
				{
					logFile->WriteLine("SERVING: {0} ({1})", customerPrQ.name[0]->ToString()->Trim()->PadRight(20, ' '), Convert::ToString(customerPrQ.heapNode[0]));
					customerPrQ.serveACustomer();
				}

				logFile->WriteLine(">>> heap is now empty");

			}
			if (String::Compare(theLine, 0, "N", 0, 1) == 0)
			{
				array<String^>^ field;
				field = theLine->Split(',');
				int point = customerPrQ.addCustomerToQ(theLine);
				logFile->WriteLine("ADDING:  {0} ({1})", field[1]->Trim()->PadRight(20, ' '), point);
			}
			if (String::Compare(theLine, 0, "S", 0, 1) == 0)
			{
				logFile->WriteLine("SERVING: {0} ({1})", customerPrQ.name[0]->ToString()->Trim()->PadRight(20, ' '), Convert::ToString(customerPrQ.heapNode[0]));
				customerPrQ.serveACustomer();	
			}
		}
		theLine = inEvent->ReadLine();
	}

	logFile->WriteLine(">>> program terminating");

	inEvent->Close();
	logFile->Close();

	return 0;
}
