#include "ClientCommon.h"

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

//
// Process the server request and extract action and relevant data for each action
// request may look like this:
//   1:appname:version
//   2:appname
//   3:filename
//   4:Service or 4:Process
// Input: ActionString
// Output: Fill the CmdInfo passed with extracted Action and its relevant data
// returns: SUCCESS on successful extraction else ERROR on failure
///
int GetActionDetails(char *ActionString, CMDINFO* CmdInfo)
{
	vector<string> list = split(ActionString, ':');
	int retval = RSMC_SUCCESS;

	CmdInfo->jobid = atoi(list.at(0).c_str());
	CmdInfo->action = atoi(list.at(1).c_str());

	printf("\n jobid: %d, action = %d", CmdInfo->jobid, CmdInfo->action);
	switch (CmdInfo->action)
	{
		case ACTION_INSTALL:
			strcpy(CmdInfo->app, list.at(2).c_str());
			strcpy(CmdInfo->version, list.at(3).c_str());
			printf("\nApp: %s, version: %s", CmdInfo->app, CmdInfo->version);
			break;
		case ACTION_UNINSTALL:
			strcpy(CmdInfo->app, list.at(2).c_str());
			strcpy(CmdInfo->version, list.at(3).c_str());

			break;
		case ACTION_COPY:
			strcpy(CmdInfo->app, list.at(2).c_str());
			break;
		case ACTION_GETINFO:
			strcpy(CmdInfo->subaction, list.at(2).c_str());
			break;
		default:
			retval = RSMC_ERROR;
			break;
	}
	return retval;
}

int SendToServer(SOCKET s, char *response)
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

int ProcessJob(PJOB Job)
{
	char ActionString[512];

	strcpy(ActionString, Job->job_string);
	
	int retval = RSMC_SUCCESS;
	char *GetInfoOutput = NULL;

	printf("\n ActionString = %s", ActionString);
	retval = GetActionDetails(ActionString, Job);
	if (retval == RSMC_ERROR)
	{
		printf("\nError: Invalid Action sent by server!");
		//SendResponseToServer(s, "ERROR: Invalid Request, Cannot process!");
		Job->action_status = ACTION_INVALID;

		return RSMC_ERROR;
	}
	else
	{
		switch (Job->action)
		{
			case ACTION_INSTALL:
				retval = Action_PerformInstall(Job);
				
				if (retval == RSMC_SUCCESS)
					strcpy(Job->action_status_string, "SUCCESS - Installation Successfull!");
				else
					strcpy(Job->action_status_string, "ERROR - Installation failed!");
				
				Job->action_status = retval;

				break;

			case ACTION_UNINSTALL:
				retval = Action_PerformUnInstall(Job);
				
				if (retval == RSMC_SUCCESS)
					strcpy(Job->action_status_string, "SUCCESS - UnInstallation Successfull!");
				else
					strcpy(Job->action_status_string, "ERROR - UnInstallation failed!");

				Job->action_status = retval;
				
				break;

			case ACTION_COPY:
				retval = Action_PerformCopy(Job);
				
				if (retval == RSMC_SUCCESS)
					strcpy(Job->action_status_string, "SUCCESS - Copy Successfull!");
				else
					strcpy(Job->action_status_string, "ERROR - Copy failed!");
				
				Job->action_status = retval;

				break;

			case ACTION_GETINFO:
				char *Info;
				Info = Action_PerformGetInfo(Job);

				if (retval == RSMC_SUCCESS)
				{
					strcpy(Job->action_status_string, "SUCCESS - GetInfo Successfull!");
					Job->GetInfo_Output = Info;
				}
				else
					strcpy(Job->action_status_string, "ERROR - GetInfo failed!");

				Job->action_status = retval;
				break;

			default:
				// we should never reach here,
				// if reached this is a critical problem in the code
				printf("\nERROR: Invalid Action requested.");
				break;
		}
	}
	
	return RSMC_SUCCESS;
}
