// CLASS:  CustomerPrQ              in PROGRAM:  CrownOrganizer-A4
// AUTHOR:  Erivelton Gualter dos Santos
// DESCRIPTION:  This class handles allaccessing og the nodes and atributes 
// *************************************************************************************

#include "stdafx.h"
#include "CustomerPrQ.h"

NameCP::CustomerPrQ::CustomerPrQ()
{
	inFile = gcnew StreamReader("C:\\Users\\EriveltonDell\\Documents\\Western Michigan University\\CS3310 - Data & File Structure\\CrowdOrganizer-A4\\CrowdOrganizer-A4\\Files\\LineAt6Am.txt");
	priorityValue = 101;
	nCustomer = 0;
}

int NameCP::CustomerPrQ::arrangeCustomerQ()
{
	String^ theLine = inFile->ReadLine();
	while (!inFile->EndOfStream)
	{
		if (String::Compare(theLine, 0, "//", 0, 2) != 0)
		{
			array<String^>^ field;
			field = theLine->Split(',');

			for (int i = (nCustomer-1)/2; i >= 0; i--)
				WalkDown(i, nCustomer);

			this->name[nCustomer] = field[0];

			heapNode[nCustomer] = jumpTheQPoints(field[1], field[2], field[3]);

			nCustomer++;

		}

		theLine = inFile->ReadLine();
	}
	for (int i = (nCustomer - 1) / 2; i >= 0; i--)
		WalkDown(i, nCustomer);
	inFile->Close();

	return nCustomer;
}

int NameCP::CustomerPrQ::serveRemainingCustomers()
{
	return nCustomer;
}

int NameCP::CustomerPrQ::addCustomerToQ(String^ line)
{
	int point;

	array<String^>^ field;
	field = line->Split(',');

	for (int i = (nCustomer - 1) / 2; i >= 0; i--)
		WalkDown(i, nCustomer);

	this->name[nCustomer] = field[1];
	point = heapNode[nCustomer] = jumpTheQPoints(field[2], field[3], field[4]);

	nCustomer++;

	return point;
}

void NameCP::CustomerPrQ::serveACustomer()
{
	heapNode[0] = heapNode[nCustomer - 1];
	name[0] = name[nCustomer - 1];
	nCustomer--;
	for (int i = (nCustomer - 1) / 2; i >= 0; i--)
		WalkDown(i, nCustomer);
}

int  NameCP::CustomerPrQ::jumpTheQPoints(String^ employeeStatus, String^ vipStatus, String^ age)
{
	int value = priorityValue;

	if (employeeStatus == "employee")
		value -= 25;
	if (employeeStatus == "owner")
		value -= 80;
	if (vipStatus == "vip")
		value -= 5;
	if (vipStatus == "superVIP")
		value -= 10;
	if (age != "")
	{
		if (Convert::ToInt32(age, 10) >= 65)
			value -= 15;
		if (Convert::ToInt32(age, 10) >= 80)
			value -= 15;
	}


	this->priorityValue++;

	return value;
}

int NameCP::CustomerPrQ::SubOfSmCh(int i, int N)
{
	if (((2*i + 2) > (N - 1)) || (heapNode[2 * i + 1] <= heapNode[2 * i + 2]))
		return 2*i + 1;
	else
		return 2*i + 2;
}

void NameCP::CustomerPrQ::WalkDown(int i, int N)
{
	int SmCh = SubOfSmCh(i, N);
	while ( ((2 * i + 1) <= (N - 1)) && (heapNode[i] > heapNode[SmCh]))
	{
		int swap = heapNode[i];
		msclr::auto_gcroot<String^> sname = name[i];

		heapNode[i] = heapNode[SmCh];
		heapNode[SmCh] = swap;
		
		name[i] = name[SmCh];
		name[SmCh] = sname;
			
		i = SmCh;
		SmCh = SubOfSmCh(i,N);
	}
}