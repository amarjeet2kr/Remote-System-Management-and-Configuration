#define _CRT_SECURE_NO_WARNINGS

#include <Windows.h>
#include <tchar.h>
#include <stdio.h>
#include "ServerCommon.h"

#pragma comment(lib,"ws2_32.lib") //Winsock Library


SERVICE_STATUS        g_ServiceStatus = { 0 };
SERVICE_STATUS_HANDLE g_StatusHandle = NULL;
HANDLE                g_ServiceStopEvent = INVALID_HANDLE_VALUE;

VOID WINAPI ServiceMain(DWORD argc, LPTSTR *argv);
VOID WINAPI ServiceCtrlHandler(DWORD);

#define SERVICE_NAME  _T("RSMC Server Service")  

int _tmain(int argc, TCHAR *argv[])
{

	DWORD Status = E_FAIL;
	// initiate the use of winsock library
	WSAData wsaData; // initialize
	int Result = WSAStartup(MAKEWORD(2, 1), &wsaData);
	if (Result != NO_ERROR) // check for errors
	{
		OutputDebugString(_T("Error at WSAStartup()"));
		goto EXIT;
	}

	//strcpy()

	// Start a thread that will perform the main task of the service

	// create threat for managing communication with client and sending jobs to client
	HANDLE hThreadCommJob = CreateThread(NULL, 0, ServiceWorkerThread_ManageCommunicationAndJob, NULL, 0, NULL);

	// create thread for managing job status from clients
	HANDLE hThreadJobStatus = CreateThread(NULL, 0, ServiceWorkerThread_ManageJostStatus, NULL, 0, NULL);
	
	HANDLE hThreadUI = CreateThread(NULL, 0, ServiceWorkerThread_UI, NULL, 0, NULL);

	HANDLE hThread[] = { hThreadCommJob, hThreadJobStatus, hThreadUI };

	// Wait until our worker thread exits signaling that the service needs to stop
	WaitForMultipleObjects(2, hThread, TRUE, INFINITE);


EXIT:
	return 0;
}
