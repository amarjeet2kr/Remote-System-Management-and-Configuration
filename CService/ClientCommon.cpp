#define _WINSOCK_DEPRECATED_NO_WARNINGS
#define _CRT_SECURE_NO_WARNNINGS

#include <Windows.h>
#include <tchar.h>
#include <stdio.h>
#include "ClientCommon.h"

#ifdef CSERVICE_RSMC
extern HANDLE                g_ServiceStopEvent;
#endif

void print(_TCHAR *msg)
{
#ifdef CSERVICE_RSMC
	OutputDebugString(msg);
#else
	_tprintf(msg);
#endif 
}

SERVERINFO ServerInfo = {
	"127.0.0.1",
	55555,
	55556
};
int registered_flag = 0;

int GetIPAddress(SOCKET s, char *IP)
{
	sockaddr_in SockAddr;
	int addrlen = sizeof(SockAddr);

	if (getsockname(s, (LPSOCKADDR)&SockAddr, &addrlen) == SOCKET_ERROR)
	{
		return WSAGetLastError();
	}

	strcpy(IP, inet_ntoa(SockAddr.sin_addr));
	return RSMC_SUCCESS;
}


LSTATUS ReadRegistry(LPCSTR sPath, LPCSTR sKey, LPCSTR pBuffer, LPDWORD pBufferSize)
{
	HKEY hKey;
	LSTATUS nResult = ::RegOpenKeyExA(HKEY_LOCAL_MACHINE, sPath,
		0, KEY_READ | KEY_WOW64_64KEY, &hKey);

	if (nResult == ERROR_SUCCESS)
	{
		nResult = ::RegQueryValueExA(hKey, sKey, NULL, NULL,
			(LPBYTE)pBuffer, pBufferSize);

		RegCloseKey(hKey);
	}

	return (nResult);
}


int GetWinVersion(char *osname, char *osverion)
{
	DWORD size = 100;
	LSTATUS nResult = ReadRegistry("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion",
		"ProductName", osname, &size);
	nResult = ReadRegistry("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion",
		"ReleaseId", osverion, &size);
	return RSMC_SUCCESS;

}

int GetHostName(char *hostname)
{
	int retval = GetEnvironmentVariableA("COMPUTERNAME", hostname, 100);
	return RSMC_SUCCESS;
}

// connects to sever, looks for waiting job, if job found, it creates the job object
// with detail and returns to caller 0
// if there is no job, it return 1
// closes the connection
//
int ConectToServerGetWaitingJob(PJOB Job)
{
	// create socket
	print(_T("\nCreate socket ..."));
	SOCKET sockSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); // create a socket
	if (sockSocket == INVALID_SOCKET) // check for errors
	{
		//char buff[500]; 
		//sprintf(buff, "Error Creating socket, Error: %d", WSAGetLastError());
		print(_T("Error connecting to socket"));
		return RSMC_ERROR;
	}
		
	int err = 0;
	//GetIPAddress(sockSocket, SelfIP, &err);

	// connect to socket
	struct sockaddr_in ServerAddress;
	ServerAddress.sin_addr.s_addr = inet_addr(ServerInfo.ServerIP); // connect to the ipnuted IP
	ServerAddress.sin_family = AF_INET;
	ServerAddress.sin_port = htons(ServerInfo.CommPort); ///(55555); 
	
	print(_T("Connect to server socket ..."));
	if (connect(sockSocket, (SOCKADDR*)&ServerAddress, sizeof(ServerAddress)) == SOCKET_ERROR) // check for errors
	{
		//char buff[500]; 
		//sprintf(buff, "Error Creating socket, Error: %d", WSAGetLastError());
		print(_T("Failed to connect to server"));
		closesocket(sockSocket);
		return RSMC_ERROR;
	}

	char request_string[50] = "NewJob?";
	
	print(_T("\nsend reqest to server to check if pending job are available "));
	if (SendToServer(sockSocket, request_string)  == RSMC_ERROR)
	{
		//char buff[500]; 
		//sprintf(buff, "Error Creating socket, Error: %d", WSAGetLastError());
		print(_T("Failed to send request"));
		closesocket(sockSocket);
		return RSMC_ERROR;
	}

	char buffer[512];
	int result; 
	do
	{
		print(_T("\nrececive pending job answer from server"));
		result = recv(sockSocket, buffer, 512, 0);
		if (result <= 0)
			break;

		buffer[result] = '\0';
		break;

	} while (true);

	if (result > 0)
	{
		strcpy(Job->job_string, buffer);
	}
	   
	// update the job object
		
	closesocket(sockSocket);
	return RSMC_SUCCESS;
}

void PrepareResponseString(PJOB Job)
{
	//jobid:action:status:action_status:action_status_string
	// if action is getinfo then we should send getinfo command output separately
	sprintf(Job->response_stringToBeSent, "%d:%d:%d:%s", Job->jobid, Job->action, Job->action_status, Job->action_status_string);	
}

int ConnectToServerToGiveJobStatus(PJOB Job)
{
	// create socket
	print(_T("Create Socket ..."));
	SOCKET sockSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); // create a socket
	if (sockSocket == INVALID_SOCKET) // check for errors
	{
		//char buff[500]; 
		//sprintf(buff, "Error Creating socket, Error: %d", WSAGetLastError());
		print(_T("Error connecting to socket"));
		return RSMC_ERROR;
	}

	// connect to socket
	struct sockaddr_in ServerAddress;
	ServerAddress.sin_addr.s_addr = inet_addr(ServerInfo.ServerIP); // connect to the ipnuted IP
	ServerAddress.sin_family = AF_INET;
	ServerAddress.sin_port = htons(ServerInfo.StatusPort); // should the port also be the argument?

	print(_T("\n Connect to server ..."));
	if (connect(sockSocket, (SOCKADDR*)&ServerAddress, sizeof(ServerAddress)) == SOCKET_ERROR) // check for errors
	{
		//char buff[500]; 
		//sprintf(buff, "Error Creating socket, Error: %d", WSAGetLastError());
		print(_T("Failed to connect to server"));
		closesocket(sockSocket);
		return RSMC_ERROR;
	}

	PrepareResponseString(Job);

	print(_T("\nsend status update to sever..."));
	printf("\n response_string: %s", Job->response_stringToBeSent);
	if(SendToServer(sockSocket, Job->response_stringToBeSent) == RSMC_ERROR)
	{
		//char buff[500]; 
		//sprintf(buff, "Error Creating socket, Error: %d", WSAGetLastError());
		print(_T("Failed to send request"));
		closesocket(sockSocket);
		return RSMC_ERROR;
	}

	if (Job->action == ACTION_GETINFO)
	{
		// send the get_info_output
		if (SendToServer(sockSocket, Job->GetInfo_Output) == RSMC_ERROR)
		{
			print(_T("Failed to send getinfo_output"));
			closesocket(sockSocket);
			return RSMC_ERROR;
		}
	}

	// receive response on update
	char buffer[512];
	int result;
	do
	{
		print(_T("\nrececive pending job answer from server"));
		result = recv(sockSocket, buffer, 512, 0);
		if (result <= 0)
			break;

		buffer[result] = '\0';
		break;

	} while (true);

	if (result > 0)
	{
		printf("\nreponse received: %s", buffer);
		if (stricmp(buffer, "GetInfoDataMissing") == 0)
		{
			// GetInfo output was not received by server, handle it here
		}
		else if (stricmp(buffer, "ClientNotFouond") == 0)
		{
			// Client may not registered, client information not found in server 
		}
		else if (stricmp(buffer, "JobNotFound") == 0)
		{
			// Jobid not found in the server for which the response was sent
		}
		else if (stricmp(buffer, "ThankYou") == 0)
		{
			// Success fully status received and updated.
		}
	}
	// update the job object
	print(_T("Out of ConnectToServerToGiveJobStatus() - closing connection to server"));
	closesocket(sockSocket);
	return RSMC_SUCCESS;
}

void FreeJobMemory(PJOB Job)
{
	free(Job->GetInfo_Output);
}

int RegisterWithServer()
{
	// create socket
	print(_T("\nCreate socket ..."));
	SOCKET sockSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); // create a socket
	if (sockSocket == INVALID_SOCKET) // check for errors
	{
		//char buff[500]; 
		//sprintf(buff, "Error Creating socket, Error: %d", WSAGetLastError());
		print(_T("\nError connecting to socket"));
		return RSMC_ERROR;
	}

	// connect to socket
	struct sockaddr_in ServerAddress;
	ServerAddress.sin_addr.s_addr = inet_addr(ServerInfo.ServerIP); // connect to the ipnuted IP
	ServerAddress.sin_family = AF_INET;
	ServerAddress.sin_port = htons(ServerInfo.CommPort); ///(55555); 

	printf("\nConnecting to server socket %s...", ServerInfo.ServerIP);
	if (connect(sockSocket, (SOCKADDR*)&ServerAddress, sizeof(ServerAddress)) == SOCKET_ERROR) // check for errors
	{
		//char buff[500]; 
		//sprintf(buff, "Error Creating socket, Error: %d", WSAGetLastError());
		printf("\nFailed to connect to server: error - %d", WSAGetLastError());
		closesocket(sockSocket);
		return RSMC_ERROR;
	}

	char request_string[100] = "Register?:Window 10:1803";
	char hostname[100], osname[100], version[100];
	GetHostName(hostname);
	GetWinVersion(osname, version);
	sprintf(request_string, "Register?:%s:%s:%s", hostname, osname, version);
	print(_T("\nRegistration request send. "));
	if (SendToServer(sockSocket, request_string) == RSMC_ERROR)
	{
		//char buff[500]; 
		//sprintf(buff, "Error Creating socket, Error: %d", WSAGetLastError());
		print(_T("Failed to send request"));
		closesocket(sockSocket);
		return RSMC_ERROR;
	}

	char buffer[512];
	int result;
	do
	{
		print(_T("\nRegistration request response:"));
		result = recv(sockSocket, buffer, 512, 0);
		if (result <= 0)
			break;

		buffer[result] = '\0';
		break;

	} while (true);

	if (result > 0)
	{
		printf("%s", buffer);
		if (stricmp(buffer, "registered") == 0)
		{
			print(_T("\nRegistration Successful"));
			return RSMC_SUCCESS;
		}
		else
		{
			printf("\nregistrataion status: %s", buffer);
			print(_T("\nRegistration failed"));
		}
	}

	return RSMC_ERROR;
}

DWORD WINAPI ServiceWorkerThread_ProcessServerRequest(LPVOID lpParam)
{
	// register the client first 
	printf("\n=>ServiceWorkerThread_ProcessServerRequest()");
	do
	{
		printf("\n=>RegisterWithServer()");
		int retval = RegisterWithServer();
		printf("\n<=RegisterWithServer()");

		if (retval == RSMC_SUCCESS)
		{
			registered_flag = 1;
			break;
		}
	} while (registered_flag == 0);

	print(_T("\nregistered with server successfully."));

	print(_T("\nStarting ProcessServerRequest Thread ..."));
#ifdef CSERVICE_RSMC
	//  Periodically check if the service has been requested to stop
	while (WaitForSingleObject(g_ServiceStopEvent, 0) != WAIT_OBJECT_0)
#else
	while (true)
#endif
	{
		printf("\nWaiting for next poll time to elapse ...");
		Sleep(60 * 1000);
		JOB Job;
		// connect to server and check if there is any job
		print(_T("\nConnecting to server for waiting job ..."));
		printf("\n=>ConectToServerGetWaitingJob()");
		int status = ConectToServerGetWaitingJob(&Job);
		printf("\n<=ConectToServerGetWaitingJob()");

		// if job is found process it
		if (status == RSMC_SUCCESS)
		{
			printf("\n Response for GetWaitingJob -> %s", Job.job_string);
			if ((stricmp(Job.job_string, "UnkownRequest") == 0) ||
				(stricmp(Job.job_string, "NoNewJob") == 0))
				continue;
			else
			{
				printf("\n=>ProcessJob()");
				ProcessJob(&Job);
				printf("\n<=ProcessJob()");
			}
		}

		// send the result/status of the job requested by server 
		print(_T("\nConnecting to server for updating job status and result ..."));
		status = ConnectToServerToGiveJobStatus(&Job);
		print(_T("Done."));

		FreeJobMemory(&Job);

		print(_T("ServiceWorkerThread_ManageJostStatus"));
	}

	return RSMC_SUCCESS;
}
