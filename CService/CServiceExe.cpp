#define _CRT_SECURE_NO_WARNINGS

#include <Windows.h>
#include <tchar.h>
#include <stdio.h>

#include "ClientCommon.h"

#pragma comment(lib,"ws2_32.lib") //Winsock Library

extern SERVERINFO ServerInfo;

int main(int argc, char *argv[])
{
	DWORD Status = E_FAIL;

	if (argc < 2)
	{
		printf("\nRMSC Client Console\n");
		printf("Usage: %s <RMSC Server IP>\n", argv[0]);
		return 1;
	}

	strcpy(ServerInfo.ServerIP, argv[1]);

	// initiate the use of winsock library
	WSAData wsaData; // initialize
	printf("RSMC Server IP provided: %s", ServerInfo.ServerIP);

	print(_T("\nStarting RSMC Client ..."));
	print(_T("\nRuning WSAStartup()..."));
	int Result = WSAStartup(MAKEWORD(2, 1), &wsaData);
	if (Result != NO_ERROR) // check for errors
	{
		print(_T("Error at WSAStartup()"));
		return 1;
	}
	print(_T("Done."));
	// Start a thread that will perform the main task of the service
	// create thread for managing job status from clients
	HANDLE hThread = CreateThread(NULL, 0, ServiceWorkerThread_ProcessServerRequest, NULL, 0, NULL);

	// Wait until our worker thread exits signaling that the service needs to stop
	WaitForSingleObject(hThread, INFINITE);

	return 0;
}


