#include "ClientCommon.h"

int Action_PerformCopy(CMDINFO *CmdInfo)
{
	char *cmd = "mount_repos.bat";
	char *repos_copyfolder = "p:\\copyfolder";
	char *destination_folder = ".\\copiedfiles";
	// mount the repository drive
	system(cmd);

	// copy the file
	char cmd_copy[512];
	sprintf(cmd_copy, "copy %s\\%s %s /y", repos_copyfolder, CmdInfo->app, destination_folder);
	printf("\nCopying %s from repository...", CmdInfo->app);
	int retval = system(cmd_copy);
	if (retval == ERROR)
	{
		printf("ERROR: %d", retval);
		return ERROR;
	}

	printf("Done.\n");
	return RSMC_SUCCESS;
}