#include "clientcommon.h"

int Action_PerformInstall(CMDINFO * CmdInfo)
{
	char *cmd = "mount_repos.bat";
	system(cmd);

	char cmd_install[100];
	sprintf(cmd_install, "p:\\%s\\%s\\%s-install.bat", CmdInfo->app, CmdInfo->version, CmdInfo->app);
	printf("\nInstalling  ... Cmd:[%s]", cmd_install);
	int retval = system(cmd_install);
	if (retval == RSMC_ERROR)
	{
		printf("ERROR: %d", retval);
		return RSMC_ERROR;
	}

	printf("Done.\n");
	return RSMC_SUCCESS;
}

int Action_PerformUnInstall(CMDINFO * CmdInfo)
{
	char *cmd = "mount_repos.bat";
	system(cmd);

	char cmd_uninstall[100];
	sprintf(cmd_uninstall, "p:\\%s\\%s\\%s-uninstall.bat", CmdInfo->app, CmdInfo->version, CmdInfo->app);
	printf("\nUnInstalling ... Cmd:[%s]", cmd_uninstall);
	int retval = system(cmd_uninstall);
	if (retval == RSMC_ERROR)
	{
		printf("ERROR: %d", retval);
		return RSMC_ERROR;
	}

	printf("Done.\n");
	return RSMC_SUCCESS;
}
