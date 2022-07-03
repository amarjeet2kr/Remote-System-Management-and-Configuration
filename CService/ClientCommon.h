#pragma once

#define _WINSOCK_DEPRECATED_NO_WARNINGS
#define _CRT_SECURE_NO_WARNNINGS

#include <Windows.h>
#include <iostream>
#include <string>
#include <stdio.h>
#include <ctime>
#include <sstream>
#include <vector>
#include <tchar.h>

#pragma comment(lib,"ws2_32.lib") //Winsock Library

using namespace std;

// action constants
#define ACTION_INSTALL 1
#define ACTION_UNINSTALL 2
#define ACTION_COPY 3
#define ACTION_GETINFO 4
#define ACTION_EXIT 9
#define ACTION_INVALID -9

#define RSMC_ERROR 1
#define RSMC_SUCCESS 0

#define MAX_APP_NAME 50
#define MAX_VERSION_STRING_SIZE 30
#define MAX_ACTION_STRING 20
#define MAX_RESPONSE_STRING 512

struct _JobInfo
{
	int jobid;
	int action;
	char app[MAX_APP_NAME+1];
	char version[MAX_VERSION_STRING_SIZE+1];
	char subaction[MAX_ACTION_STRING+1];
	char subaction_arg[MAX_ACTION_STRING+1];
	char job_string[512];
	char *GetInfo_Output;
	char action_status_string[MAX_RESPONSE_STRING+1];
	int action_status;  // RSMC_SUCCESS or RSMC_ERROR
	char response_stringToBeSent[MAX_RESPONSE_STRING+1];
};
typedef struct _JobInfo CMDINFO, JOB, *PJOB;

struct _ServerInfo
{
	char ServerIP[16];
	u_short  CommPort;
	u_short  StatusPort;
};
typedef _ServerInfo SERVERINFO, *PSERVERINFO;

//function declearation
int Action_PerformInstall(CMDINFO * CmdInfo);
int Action_PerformUnInstall(CMDINFO * CmdInfo);
int Action_PerformCopy(CMDINFO *CmdInfo);
char * Action_PerformGetInfo(CMDINFO *CmdInfo);

char * GetWindowsService(void);
char * GetWindowsProcesses(void);
char * GetSoftwareInstalled(void);
char * GetLogicalDrivesInfo();

int SendToServer(SOCKET s, char *response);
// common functions - helper
vector<string> split(const string &s, char delim);

DWORD WINAPI ServiceWorkerThread_ProcessServerRequest(LPVOID lpParam);
VOID WINAPI ServiceCtrlHandler(DWORD CtrlCode);
int GetIPAddress(SOCKET s, char *IP);
int GetHostName(char *Hostname);
void print(_TCHAR *msg);

int ConectToServerGetWaitingJob(PJOB Job);
int ProcessJob(PJOB Job);
int ConnectToServerToGiveJobStatus(PJOB Job);
void PrepareResponseString(PJOB Job);
void FreeJobMemory(PJOB Job);