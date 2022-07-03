#include "ClientCommon.h"

char * Action_PerformGetInfo(CMDINFO *CmdInfo)
{
	char *message;
	if (stricmp(CmdInfo->subaction, "Service") == 0)
	{
		message = GetWindowsService();
		if ( message == NULL)
			return NULL;
	}
	else if (stricmp(CmdInfo->subaction, "Process") == 0)
	{
		message = GetWindowsProcesses();
		if (message == NULL)
			return NULL;
	}
	else if (stricmp(CmdInfo->subaction, "Softwares") == 0)
	{
		message = GetSoftwareInstalled();
		if (message == NULL)
			return NULL;
	}
	else if (stricmp(CmdInfo->subaction, "Disk") == 0)
	{
		message = GetLogicalDrivesInfo();
		if (message == NULL)
			return NULL;
	}

		return message;
}
unsigned long GetFileSize(char *filename)
{
	FILE * f = fopen(filename, "r");
	fseek(f, 0, SEEK_END);
	unsigned long len = (unsigned long)ftell(f);
	fclose(f);
	return len;

}

char * GetLogicalDrivesInfo()
{
	//char *cmd = "wmic /output:disk.out logicaldisk get size, freespace, caption";
	char *cmd = "powershell -executionpolicy bypass -command \"Get-WMIObject Win32_LogicalDisk\" > disk.out";
	system(cmd);
	long size = GetFileSize("disk.out");

	char *output = (char*)malloc(size);
	if (output == NULL)
		return NULL;

	FILE* fp = fopen("disk.out", "r");
	if (fp == NULL)
	{
		printf("ERROR: Failed to read disk.out!");
		return NULL;
	}

	int s = fread((void*)output, 1, size, fp);
	if (size >= s)
		return output;
	else
	{
		free(output);
		return NULL;
	}
}


char * GetSoftwareInstalled()
{
	char *cmd = "powershell -executionpolicy ByPass -Command \"Get-ItemProperty HKLM:\\Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\* | Select-Object DisplayName, DisplayVersion, Publisher, InstallDate\" > SoftwareList.out";
	system(cmd);
	long size = GetFileSize("SoftwareList.out");

	char *output = (char*)malloc(size);
	if (output == NULL)
		return NULL;

	FILE* fp = fopen("SoftwareList.out", "rb");
	if (fp == NULL)
	{
		printf("ERROR: Failed to read SoftwareList.out!");
		return NULL;
	}

	int s = fread((void*)output, 1, size, fp);
	if (s == size)
		return output;
	else
	{
		free(output);
		return NULL;
	}
}

char * GetWindowsService()
{
	char *cmd = "powershell -executionpolicy ByPass -Command Get-Service > GetService.out";
	system(cmd);
	long size = GetFileSize("GetService.out");
	
	char *output = (char*)malloc(size);
	if (output == NULL)
		return NULL;

	FILE* fp = fopen("GetService.out", "rb");
	if (fp == NULL)
	{
		printf("ERROR: Failed to read GetService.out!");
		return NULL;
	}
	
	int s = fread((void*)output, 1, size, fp);
	if (s == size)
		return output;
	else
	{
		free(output);
		return NULL;
	}
}

char * GetWindowsProcesses()
{
	char *output;
	char *cmd = "tasklist > tasklist.out";
	system(cmd);
	long size = GetFileSize("tasklist.out");

	output = (char*)malloc(size);
	if (output == NULL)
		return NULL;

	FILE* fp = fopen("tasklist.out", "rb");
	if (fp == NULL)
	{
		printf("ERROR: Failed to read tasklist.out!");
		return NULL;
	}

	int s = fread((void*)output, 1, size, fp);
	if (s == size)
		return output;
	else
	{
		free(output);
		return NULL;
	}
	return RSMC_SUCCESS;
}
