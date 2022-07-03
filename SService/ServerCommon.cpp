#define _WINSOCK_DEPRECATED_NO_WARNINGS
#define _CRT_SECURE_NO_WARNNINGS

#include <Windows.h>
#include <tchar.h>
#include <stdio.h>
#include <iostream>
#include <string>
#include <ctime>
#include <sstream>
#include <vector>
#include <tchar.h>

#include "ServerCommon.h"
//#include "winsock.h"
using namespace std;

#ifdef SERVICE_RSMC
extern HANDLE g_ServiceStopEvent;
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

// Number of clients that we can handle
CLIENTINFO Clients[100];
int TotalClientCount = 0;
char *getinfo_output_ptr = NULL;
// splits a given string based on delimter provided
// e.g.:   "1:appname:version" split into 
// 1
// appname
// version
//
vector<string> split(const string &s, char delim) {
	vector<string> result;
	stringstream ss(s);
	string item;

	while (getline(ss, item, delim)) {
		result.push_back(item);
	}
	return result;
}

int GetIPAddress(SOCKET s, char *IP)
{
	sockaddr_in SockAddr;
	int addrlen = sizeof(SockAddr);

	if (getpeername(s, (LPSOCKADDR)&SockAddr, &addrlen) == SOCKET_ERROR)
	{
		return WSAGetLastError();
	}

	strcpy(IP, inet_ntoa(SockAddr.sin_addr));
	return RSMC_SUCCESS;
}

int GetHostName(SOCKET s, char *IP, char *HostName)
{
	strcpy(HostName, "default host");
	return RSMC_SUCCESS;
}

char * GetJobStatusString(int job_status)
{
	switch (job_status)
	{
	case JOB_STATUS_NONE:
		return "None";
	case JOB_STATUS_COMPLETE:
		return "Complete";
	case JOB_STATUS_NEW:
		return "Waiting";
	case JOB_STATUS_INPROGRESS:
		return "In Progress";
	default:
		return "UNKNOWN";
	}
}

int RegisterClient(const char *IP, const char *Hostname, const char *OsName, const char *OsVersion)
{

	PCLIENTINFO cp;

	cp = GetClientRecord(IP);
	if (cp == NULL)
	{
		printf("\nRegistering new client");
		if (TotalClientCount < MAX_CLIENTS)
		{
			printf("\nIP = [%s]", IP);
			strcpy(Clients[TotalClientCount].ClientIP, IP);
			strcpy(Clients[TotalClientCount].HostName, Hostname);
			Clients[TotalClientCount].RegistrationStatus = CLIENT_REGISTERED;
			strcpy(Clients[TotalClientCount].OSName, OsName);
			strcpy(Clients[TotalClientCount].OSVersion, OsVersion);
			Clients[TotalClientCount].Job.action = ACTION_NONE;
			Clients[TotalClientCount].Job.action_status = JOB_STATUS_NONE;
			strcpy(Clients[TotalClientCount].Job.action_status_string, "None");
			Clients[TotalClientCount].Job.jobid = 1;
			TotalClientCount++;
			return RSMC_SUCCESS;
		}
	}
	else
	{
		printf("\nUpdating the existing client");
		if (TotalClientCount < MAX_CLIENTS)
		{
			printf("\nIP = [%s]", IP);
			strcpy(cp->ClientIP, IP);
			strcpy(cp->HostName, Hostname);
			cp->RegistrationStatus = CLIENT_REGISTERED;
			strcpy(cp->OSName, OsName);
			strcpy(cp->OSVersion, OsVersion);
			cp->Job.action = ACTION_NONE;
			cp->Job.action_status = JOB_STATUS_NONE;
			strcpy(cp->Job.action_status_string, "None");
			cp->Job.jobid = 1;
			return RSMC_SUCCESS;
		}
	}
	print(_T("Cannot register more clients, Max Limit reached"));
	return RSMC_ERROR;
}

int FindNewJob(char *IP, char *job_string)
{ 
	PCLIENTINFO pClientInfo = GetClientRecord(IP);
	if (pClientInfo == NULL)
	{
		sprintf(job_string, "ClientNotFound");
		return RSMC_ERROR;
	}

	if (pClientInfo->JobStatus == JOB_STATUS_NEW && 
		pClientInfo->Job.action_status == ACTION_STATUS_NEW)
	{
		printf("\nJob Action: %d", pClientInfo->Job.action);
		switch (pClientInfo->Job.action)
		{
		case ACTION_INSTALL:
			//jobid:action:appname:version
			sprintf(job_string, "%d:%d:%s:%s", pClientInfo->Job.jobid, pClientInfo->Job.action, pClientInfo->Job.app, pClientInfo->Job.version);
			break;
		case ACTION_UNINSTALL:
			//jobid:action:appname:version
			sprintf(job_string, "%d:%d:%s:%s", pClientInfo->Job.jobid, pClientInfo->Job.action, pClientInfo->Job.app, pClientInfo->Job.version);
			break;
		case ACTION_COPY:
			//jobid:action:filename
			sprintf(job_string, "%d:%d:%s", pClientInfo->Job.jobid, pClientInfo->Job.action, pClientInfo->Job.app);
			break;
		case ACTION_GETINFO:
			//jobid:action:subaction
			sprintf(job_string, "%d:%d:%s", pClientInfo->Job.jobid, pClientInfo->Job.action, pClientInfo->Job.subaction);
			break;
		}

		pClientInfo->Job.action_status = ACTION_STATUS_INPROGRESS;
		pClientInfo->JobStatus = JOB_STATUS_INPROGRESS;
	}
	else
	{
		sprintf(job_string, "NoNewJob");
	}

	return RSMC_SUCCESS;
}

int ProcessClientRequest(SOCKET s, char *request_string, char *response_string)
{
	
	if (strstr(request_string, "Register?") != NULL)
	{
		vector<string> list = split(request_string, ':');
		char IP[16];
		int err = 0;
		char Hostname[20];
		GetIPAddress(s, IP);

		if (RegisterClient(IP, list.at(1).c_str(), list.at(2).c_str(), list.at(3).c_str()) == RSMC_SUCCESS)
		{
			strcpy(response_string, "Registered");
		}
		else
			strcpy(response_string, "Register_failed");
	}
	else if (strstr(request_string, "NewJob?") != NULL)
	{
		char IP[16];
		GetIPAddress(s, IP);

		FindNewJob(IP, response_string); // no need to check the returnvalue, it would set the reponse string accordingly
	}
	else
	{
		strcpy(response_string, "UnkownRequest");
		return RSMC_ERROR;
	}

	return RSMC_SUCCESS;
}

DWORD WINAPI ServiceWorkerThread_ManageCommunicationAndJob(LPVOID lpParam)
{
	print(_T("\n In ServiceWorkerThread_ManageCommunicationAndJob()"));
#ifdef SERVICE_RSMC
	//  Periodically check if the service has been requested to stop
	while (WaitForSingleObject(g_ServiceStopEvent, 0) != WAIT_OBJECT_0)
#else
	while (1)
#endif
	{

		SOCKET sockSocket, acceptSocket;
		struct sockaddr_in service;

		// create socket
		print(_T("\n Create socket ..."));
		sockSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); // create a socket
		if (sockSocket == INVALID_SOCKET) // check for errors
		{
			OutputDebugString(_T("Error connecting to socket!"));
			//return 0;
		}

		// bind to socket
		service.sin_addr.s_addr = INADDR_ANY;
		service.sin_family = AF_INET;
		service.sin_port = htons(ServerInfo.CommPort);
		print(_T("\n bind to socket ..."));
		if (bind(sockSocket, (SOCKADDR*)&service, sizeof(service)) == SOCKET_ERROR) // cheking for errors
		{
			print(_T("Error binding to socket!"));
			closesocket(sockSocket);
			//return 0;
		}

		do
		{
			if (listen(sockSocket, 10) == SOCKET_ERROR) // check for errors
			{
				print(_T("Error at listen(): "));
				closesocket(sockSocket);
				break;
			}

			//accept connection
			int servlen = sizeof(service);
			print(_T("\nMCJ Waiting for client to connect..."));
			acceptSocket = accept(sockSocket, (SOCKADDR*)&service, &servlen);
			if (acceptSocket == INVALID_SOCKET)
			{
				print(_T("Error at accept(): "));
				closesocket(sockSocket);
				break;
			}
							
			char Buffer[512];
			char RBuffer[512];
			
			int res = recv(acceptSocket, Buffer, 512, 0);
			Buffer[res] = '\0';
			printf("\nrecevied data: %s", Buffer);
			int retval = ProcessClientRequest(acceptSocket, Buffer, RBuffer);

			printf("\nsending data: %s", RBuffer);
			if (send(acceptSocket, RBuffer, strlen(RBuffer), 0) == SOCKET_ERROR)
			{
				print(_T("send error: "));
				break;
			}

#ifdef SERVICE_RSMC
		} while (WaitForSingleObject(g_ServiceStopEvent, 0) != WAIT_OBJECT_0);
#else
		} while (true);
#endif

		closesocket(sockSocket);
	}
	print(_T("\nOut of ServiceWorkerThread_ManageCommunicationAndJob()"));
	return ERROR_SUCCESS;
}

PCLIENTINFO GetClientRecord(const char *IP)
{
	for (int i = 0; i < TotalClientCount; i++)
	{
		if ((strcmp(Clients[i].ClientIP, IP) == 0))
		{
			return &Clients[i];
		}
	}
	return NULL;
}

int ProcessClientStatusUpdateRequest(SOCKET s, char *status_string, char *response_string)
{
	char *getinfo_output = NULL;
	long output_size;
	vector<string> list = split(status_string, ':');

	// find the ip addrerss of the client first
	char IP[16];
	GetIPAddress(s, IP);

	// check if action is GETINFO and It was successful, then receive the GETINFO OUTPUT
	if (atoi(list.at(1).c_str()) == ACTION_GETINFO)
	{
		if (atoi(list.at(2).c_str()) == RSMC_SUCCESS)
		{
			char buffer[MAX_RESPONSE_STRING + 1];
			int rec = recv(s, buffer, MAX_RESPONSE_STRING, MSG_PEEK);
			if (WSAGetLastError() == WSAEMSGSIZE)
			{

			}

			if (rec > 0)
			{
				getinfo_output = (char*)malloc(rec);
				output_size = rec;
				if (getinfo_output == NULL)
				{
					return RSMC_ERROR_MEM;
				}
				int rec2 = recv(s, getinfo_output, rec, 0);
				if (!(rec2 > 0))
				{
					strcpy(response_string, "GetInfoDataMissing");
					return RSMC_ERROR;
				}
				char filename[50];
				sprintf(filename, "c:\\repos\\getinfo_%s_.out", IP );

				FILE *fp = fopen(filename, "w");
				if (fp != NULL)
				{
					int size = rec;
					char * s = getinfo_output;
					while (size > 0)
					{
						fputc(*s,fp);
						s++;
						size--;
					}

				}
				fclose(fp);
			}
		}
	}

	

	PCLIENTINFO pClient = GetClientRecord(IP);
	if ( pClient == NULL)
	{
		strcpy(response_string, "ClientNotFound");
		return RSMC_ERROR;
	}

	int tjobid = atoi(list.at(0).c_str());
	if (tjobid != pClient->Job.jobid)
	{
		strcpy(response_string, "JobNotFound");
		return RSMC_ERROR;
	}

	pClient->JobStatus = JOB_STATUS_COMPLETE;
	pClient->Job.action_status = ACTION_STATUS_COMPLETE;
	pClient->Job.action_retval = atoi(list.at(2).c_str());
	strcpy(pClient->Job.action_status_string, list.at(3).c_str());

	if (pClient->Job.action == ACTION_GETINFO)
	{
		pClient->Job.GetInfo_Output = getinfo_output;
		pClient->Job.GetInfo_Output_size = output_size;
	}

	strcpy(response_string, "ThankYou");

	return RSMC_SUCCESS;
}



int SendToClient(SOCKET s, char *response)
{
	std::string message = response;

	print(_T("\nSending response ..."));
	if (send(s, message.c_str(), message.length(), 0) == SOCKET_ERROR)
	{
		//std::cout << "Error at send(): " << WSAGetLastError() << std::endl;
		print(_T("Errot sending message."));
		return RSMC_ERROR;
	}
	print(_T("Done.\n"));
	return RSMC_SUCCESS;
}


DWORD WINAPI ServiceWorkerThread_ManageJostStatus(LPVOID lpParam)
{
	SOCKET sockSocket, acceptSocket;
	struct sockaddr_in service;

	print(_T("\nIn ServiceWorkerThread_ManageJostStatus()"));
#ifdef SERVICE_RSMC
	//  Periodically check if the service has been requested to stop
	while (WaitForSingleObject(g_ServiceStopEvent, 0) != WAIT_OBJECT_0)
#else
	while (true)
#endif
	{
		// create socket
		print(_T("\nCreate socket ..."));
		sockSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP); // create a socket
		if (sockSocket == INVALID_SOCKET) // check for errors
		{
			print(_T("Error connecting to socket!"));
			//return 0;
		}

		// bind to socket
		service.sin_addr.s_addr = INADDR_ANY;
		service.sin_family = AF_INET;
		service.sin_port = htons(55556);
		print(_T("\nbind to socket ..."));
		if (bind(sockSocket, (SOCKADDR*)&service, sizeof(service)) == SOCKET_ERROR) // cheking for errors
		{
			print(_T("Error binding to socket!"));
			closesocket(sockSocket);
			return 0;
		}

		do
		{
			// listen
			print(_T("\nlisten to socket ..."));
			if (listen(sockSocket, 10) == SOCKET_ERROR) // check for errors
			{
				print(_T("Error at listen(): "));
				closesocket(sockSocket);
				return 0;
			}

			//accept connection
			int servlen = sizeof(service);
			print(_T("\nMJS-Waiting for client to connect..."));
			acceptSocket = accept(sockSocket, (SOCKADDR*)&service, &servlen);
			if (acceptSocket == INVALID_SOCKET)
			{
				print(_T("Error at accept(): "));
				closesocket(sockSocket);
				return 0;
			}

			char StatusBuffer[MAX_RESPONSE_STRING+1];
			char ResponseString[MAX_RESPONSE_STRING + 1];
			do
			{

				print(_T("\nClient connected ...,receiving data..."));
				
				int res = recv(acceptSocket, StatusBuffer, MAX_RESPONSE_STRING, 0);
				if (res > 0) {
					print(_T("recevied job status!"));
					break;
				}
			} while (true);
			
			ProcessClientStatusUpdateRequest(acceptSocket, StatusBuffer, ResponseString);
			SendToClient(acceptSocket, ResponseString);
		
#ifdef SERVICE_RSMC
		} while (WaitForSingleObject(g_ServiceStopEvent, 0) != WAIT_OBJECT_0);
#else
		} while (true);
#endif
		closesocket(sockSocket);
	}
	return RSMC_SUCCESS;
}

#define PIPE_NAME "\\\\.\\pipe\\rsmcuipipe"
#define PIPE_BUFFSIZE 512

int CreateUIPIPE(HANDLE *hPipe)
{

	*hPipe = CreateNamedPipe(
		PIPE_NAME,             // pipe name 
		PIPE_ACCESS_DUPLEX,    // read/write access 
		PIPE_TYPE_BYTE | PIPE_READMODE_BYTE |  PIPE_WAIT,    // blocking mode // message-read mode     // message type pipe
		PIPE_UNLIMITED_INSTANCES, // max. instances  
		PIPE_BUFFSIZE,                  // output buffer size 
		PIPE_BUFFSIZE,                  // input buffer size 
		0,                        // client time-out 
		NULL);                    // default security attribute 

	if (hPipe == INVALID_HANDLE_VALUE)
	{
		_tprintf(TEXT("\nCreateNamedPipe failed, GLE=%d.\n"), GetLastError());
		return RSMC_ERROR;
	}
	return RSMC_SUCCESS;
}

char* GetActionString(int action)
{
	switch (action)
	{
	case ACTION_INSTALL:
		return "INSTALL";

	case ACTION_UNINSTALL: 
		return "UNINSTALL";

	case ACTION_COPY:
		return "COPY";

	case ACTION_GETINFO:
		return "GETINFO";

	case ACTION_NONE:
		return "NONE";

	default:
		return "UNKNOWN";
	}
}


int GetAllClientsString(char *ReplyBuffer)
{
	int TotalClients = TotalClientCount;
	int i = 0;
	if (TotalClients > 0)
	{
		do
		{
			char Buffer[PIPE_BUFFSIZE + 1];

			if (i != 0)
			{
				strcat(ReplyBuffer, "|");
			}
			sprintf(Buffer, "%s:%s:%s:%s:N/A:N/A:%s:%s:%s", Clients[i].ClientIP, Clients[i].HostName, Clients[i].OSName, 
				                                    Clients[i].OSVersion, GetActionString(Clients[i].Job.action), 
													GetJobStatusString(Clients[i].JobStatus), Clients[i].Job.action_status_string);
			strcat(ReplyBuffer, Buffer);
			i++;
		} while (i < TotalClients);
		return i;
	}
	strcpy(ReplyBuffer, "None");
	return i;
}

DWORD WINAPI ServiceWorkerThread_UI(LPVOID lpParam)
{
#ifdef SERVICE_RSMC
	//  Periodically check if the service has been requested to stop
	while (WaitForSingleObject(g_ServiceStopEvent, 0) != WAIT_OBJECT_0)
#else
	while (1)
#endif
	{
		bool fSuccess;
		bool fConnected;

		DWORD dataread;
		DWORD datasize;
		DWORD datawritten;

		char RequestBuffer[PIPE_BUFFSIZE + 1];

		HANDLE hPipe;
		printf("\nCreating UI Communication PIPE..");
		CreateUIPIPE(&hPipe);

		//wait for client to connect to pipe
		printf("\nConnect to Named pipe");
		fConnected = ConnectNamedPipe(hPipe, NULL);
		if (fConnected == 0)
		{
			printf("\nConnectNamePipe() failed, Error: %d", GetLastError());
		}
		else
		{
			printf("\nUI Client conected to pipe");
			memset(RequestBuffer, '\0', PIPE_BUFFSIZE);
			fSuccess = ReadFile(hPipe, RequestBuffer, PIPE_BUFFSIZE, &dataread, NULL);
			printf("\nNoted Data on pipe from UI");
			if (!fSuccess || dataread == 0)
			{
				if (GetLastError() == ERROR_BROKEN_PIPE)
				{
					printf("\nUI Client disconnected.");
				}
				else
				{
					printf("\nPipe read failed. %d", GetLastError());
				}
			}
			else
			{
				char * ReplyBufferFull = (char*)malloc(1024*4);
				memset(ReplyBufferFull, '\0', 1024 * 4);
				if (ReplyBufferFull == NULL)
				{
					printf("\nRun out of memory!");
					exit(1);
				}
				printf("\nRead from Pipe: %s", RequestBuffer);
		
				if (strstr(RequestBuffer, "refresh") )
				{
					// UI asking for refresh
					printf("\nREFRESH request from UI");
					printf("\nBefore: [%s]", ReplyBufferFull);
					
					GetAllClientsString(ReplyBufferFull);
					printf("\nData to UI:[%s]", ReplyBufferFull);
					datasize = strlen(ReplyBufferFull);
					//Write to fiile
					fSuccess = WriteFile(
						hPipe,        // handle to pipe 
						ReplyBufferFull,     // buffer to write from 
						datasize, // number of bytes to write 
						&datawritten,   // number of bytes written 
						NULL);        // not overlapped I/O 

					if (!fSuccess || datasize != datawritten)
					{
						printf("\nWrite to pipe failed.");
					}
					free(ReplyBufferFull);
					continue;
				}

				vector<string> list = split(RequestBuffer, ':');
				char req[10];
				strcpy(req, list.at(0).c_str());
				printf("\nreq = %s", req);
				if (stricmp(req, "uninstall") == 0)
				{
					printf("\nUNINSTALL request from UI");
					ProcessUIAction_Uninstall(RequestBuffer);
				}
				else if (stricmp(req, "install") == 0)
				{
					printf("\nINSTALL request from UI");
					ProcessUIAction_Install(RequestBuffer);
				}
				else if (stricmp(req, "copy") == 0)
				{
					printf("\nCopy request from UI");
					ProcessUIAction_Copy(RequestBuffer);
				}
				else if (stricmp(req, "getinfo") == 0)
				{
					printf("\nGETINFO request from UI");
					ProcessUIAction_GetInfo(RequestBuffer);
				}
			}
			
		} // if (fconnected)
		printf("\nClosing Pipe");
		CloseHandle(hPipe);
	} // while loop

	return ERROR_SUCCESS;
}

int ProcessUIAction_Install(char *RequestBuffer)
{
	vector<string> list = split(RequestBuffer, ':');

	PCLIENTINFO pClientInfo = GetClientRecord(list.at(1).c_str());

	pClientInfo->JobStatus = JOB_STATUS_NEW;
	pClientInfo->Job.action = ACTION_INSTALL;
	strcpy(pClientInfo->Job.app, list.at(2).c_str());
	strcpy(pClientInfo->Job.version, list.at(3).c_str());
	pClientInfo->Job.action_status = ACTION_STATUS_NEW;

	return RSMC_SUCCESS;
}

int ProcessUIAction_Uninstall(char *RequestBuffer)
{
	vector<string> list = split(RequestBuffer, ':');

	PCLIENTINFO pClientInfo = GetClientRecord(list.at(1).c_str());

	pClientInfo->JobStatus = JOB_STATUS_NEW;
	pClientInfo->Job.action = ACTION_UNINSTALL;
	strcpy(pClientInfo->Job.app, list.at(2).c_str());
	strcpy(pClientInfo->Job.version, list.at(3).c_str());
	pClientInfo->Job.action_status = ACTION_STATUS_NEW;

	return RSMC_SUCCESS;
}

int ProcessUIAction_Copy(char *RequestBuffer)
{
	vector<string> list = split(RequestBuffer, ':');

	PCLIENTINFO pClientInfo = GetClientRecord(list.at(1).c_str());

	pClientInfo->JobStatus = JOB_STATUS_NEW;
	pClientInfo->Job.action = ACTION_COPY;
	strcpy(pClientInfo->Job.app, list.at(2).c_str());
	pClientInfo->Job.action_status = ACTION_STATUS_NEW;

	return RSMC_SUCCESS;
}

int ProcessUIAction_GetInfo(char *RequestBuffer)
{
	vector<string> list = split(RequestBuffer, ':');

	PCLIENTINFO pClientInfo = GetClientRecord(list.at(1).c_str());

	pClientInfo->JobStatus = JOB_STATUS_NEW;
	pClientInfo->Job.action = ACTION_GETINFO;
	strcpy(pClientInfo->Job.subaction, list.at(2).c_str());
	pClientInfo->Job.action_status = ACTION_STATUS_NEW;

	return RSMC_SUCCESS;
}