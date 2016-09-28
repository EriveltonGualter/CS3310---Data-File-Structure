#include "stdafx.h"
#include <msclr\auto_gcroot.h>

using namespace System;
using namespace System::IO;

#define MAX_ARRAY_SIZE 100

namespace NameCP
{
	class CustomerPrQ
	{
	public:
		CustomerPrQ();
		int arrangeCustomerQ();
		int serveRemainingCustomers();
		int addCustomerToQ(String^ line);
		void serveACustomer();

		int jumpTheQPoints(String^ employeeStatus, String^ vipStatus, String^ age);
		int SubOfSmCh(int i, int N);
		void WalkDown(int i, int N);

	public:
		msclr::auto_gcroot<String^> name[MAX_ARRAY_SIZE];
		
		msclr::auto_gcroot<String^> employeeStatus;
		msclr::auto_gcroot<String^> vipStatus;
		msclr::auto_gcroot<String^> age;

		int heapNode[MAX_ARRAY_SIZE];

	private:
		msclr::auto_gcroot<System::IO::StreamReader^> inFile;
		int priorityValue;
		int nCustomer;

	};
}