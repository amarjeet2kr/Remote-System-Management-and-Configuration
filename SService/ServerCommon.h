#pragma once

// action constants
#define ACTION_INSTALL 1
#define ACTION_UNINSTALL 2
#define ACTION_COPY 3
#define ACTION_GETINFO 4
#define ACTION_EXIT 9
#define ACTION_NONE 8
#define ACTION_INVALID -9

#define ACTION_STATUS_COMPLETE 10
#define ACTION_STATUS_INPROGRESS 11
#define ACTION_STATUS_NEW 13

#define RSMC_ERROR 1
#define RSMC_SUCCESS 0
#define RSMC_ERROR_MEM 2

#define MAX_APP_NAME 50
#define MAX_VERSION_STRING_SIZE 30
#define MAX_ACTION_STRING 20
#define MAX_RESPONSE_STRING 100

#define CLIENT_REGISTERED 1
#define CLIENT_DEREGISTERED 2
#define CLIENT_ERROR 3

#define JOB_STATUS_NEW 15
#define JOB_STATUS_COMPLETE 16
#define JOB_STATUS_NONE 17
#define JOB_STATUS_INPROGRESS 18

#define MAX_CLIENTS 100

struct _JobInfo
{
	int jobid;
	int action;
	char app[MAX_APP_NAME + 1];
	char version[MAX_VERSION_STRING_SIZE + 1];
	char subaction[MAX_ACTION_STRING + 1];
	char subaction_arg[MAX_ACTION_STRING + 1];
	char *GetInfo_Output;
	long GetInfo_Output_size;
	char action_status_string[MAX_RESPONSE_STRING + 1];
	int action_status;  // RSMC_SUCCESS or RSMC_ERROR
	int action_retval;
	char response_stringReceiveed[MAX_RESPONSE_STRING + 1];
	int final_job_status;
};

typedef struct _JobInfo CMDINFO, JOB, *PJOB;

struct _ClientInfo
{
	char ClientIP[16];
	char HostName[20];
	char OSName[50];
	char OSVersion[20];
	int CurrentJobId;
	int RegistrationStatus;
	int JobStatus;
	JOB Job;
};

typedef _ClientInfo CLIENTINFO, *PCLIENTINFO;

struct _ServerInfo
{
	char ServerIP[16];
	u_short  CommPort;
	u_short  StatusPort;
};
typedef _ServerInfo SERVERINFO, *PSERVERINFO;


int GetIPAddress(SOCKET s, char *IP);
int GetHostName(SOCKET s, char *IP, char *HostName);
int RegisterClient(char *IP, char *OsName, char *OsVersion);
DWORD WINAPI ServiceWorkerThread_ManageJostStatus(LPVOID lpParam);
DWORD WINAPI ServiceWorkerThread_ManageCommunicationAndJob(LPVOID lpParam);
DWORD WINAPI ServiceWorkerThread_UI(LPVOID lpParam);

void print(_TCHAR *msg);

int ProcessUIAction_Install(char *request);
int ProcessUIAction_Uninstall(char *request);
int ProcessUIAction_Copy(char *request);
int ProcessUIAction_GetInfo(char *request);
PCLIENTINFO GetClientRecord(const char *IP);