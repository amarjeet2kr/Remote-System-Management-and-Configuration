using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace RSMCUI
{
    class Globals
    {
        public static string ServerIP = "127.0.0.1";
        public static string Repos_User = "repos_user";
        public static string Repos_Password = "test123#";

        public static bool ValidateIP(string IP)
        {

            if (String.IsNullOrWhiteSpace(IP))
            {
                Console.WriteLine("ERROR: IP Address can not be empty or WhiteSpaces!");
                System.Environment.Exit(0);
            }

            string val = IP.Trim();
            string ipPattern = @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$";
            MatchCollection matches = Regex.Matches(val, ipPattern);
            if (matches.Count != 1)
            {
                Console.WriteLine("ERROR: Invalid IP Address provided!");
                System.Environment.Exit(0);
            }
            return true;
        }

        public static int RunCommand(string command, string argument, bool waitforexit = true)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = argument,
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    CreateNoWindow = true,
                }
            };

            proc.Start();
            if (waitforexit)
            {
                proc.WaitForExit();

                return proc.ExitCode;
            }
            return 0;
        }

        public static List<string> GetSoftwareList(string filename)
        {
            List<string> swlist = new List<string>();

            using (StreamReader conffile = new StreamReader(filename, true))
            {
                String line;
                while ((line = conffile.ReadLine()) != null)
                {
                    if (line.StartsWith("//") || String.IsNullOrWhiteSpace(line))
                        continue;

                    swlist.Add(line);
                }
            }

            return swlist; // returns lines which is <softwarename> <version>
        }

        public static List<string> GetCopyFileList(string filename)
        {
            List<string> cflist = new List<string>();

            using (StreamReader conffile = new StreamReader(filename, true))
            {
                String line;
                while ((line = conffile.ReadLine()) != null)
                {
                    if (line.StartsWith("//") || String.IsNullOrWhiteSpace(line))
                        continue;

                    cflist.Add(line);
                }
            }

            return cflist; // returns lines which is <softwarename> <version>
        }

        public static void ConnectToRepos()
        {
            string cmdarg = "use \\\\" + Globals.ServerIP + "\\repos /user:" + Globals.Repos_User + " " + Globals.Repos_Password;

            // net use to mount
            Globals.RunCommand("net.exe", cmdarg);
        }

        public static void DisconnectFromRepos()
        {
            string cmdarg = "use \\\\" + Globals.ServerIP + "\\repos /delete /y";
            Globals.RunCommand("net", cmdarg);

        }
        public static int GetSoftwareListConf()
        {
            string cmdarg;
            //check if swlist.conf exits
            FileInfo fi = new System.IO.FileInfo("swlist.conf");
            if (!fi.Exists)
            {
                DisconnectFromRepos();
                ConnectToRepos();

                // copy the swlist.conf file
                cmdarg = "\\\\" + Globals.ServerIP + "\\repos\\swlist.conf";
                Globals.RunCommand("copy", cmdarg);

                DisconnectFromRepos();
            }

            return 0;
        }

        public static int GetSoftwareListConfFresh()
        {
            string cmdarg;

            DisconnectFromRepos();
            ConnectToRepos();

            // copy the swlist.conf file
            cmdarg = "\\\\" + Globals.ServerIP + "\\repos\\swlist.conf";
            Globals.RunCommand("copy", cmdarg + " /y");

            DisconnectFromRepos();

            return 0;
        }

        public static int GetCopyFileListConf()
        {
            string cmdarg;
            //check if swlist.conf exits
            FileInfo fi = new System.IO.FileInfo("cflist.conf");
            if (!fi.Exists)
            {
                ConnectToRepos();

                // copy the swlist.conf file
                cmdarg = "\\\\" + Globals.ServerIP + "\\repos\\cflist.conf";
                Globals.RunCommand("copy", cmdarg + " /y");

                DisconnectFromRepos();
            }

            return 0;
        }

        public static int GetCopyFileListConfFresh()
        {
            string cmdarg;

            DisconnectFromRepos();
            ConnectToRepos();

            // copy the swlist.conf file
            cmdarg = "\\\\" + Globals.ServerIP + "\\repos\\cflist.conf";
            Globals.RunCommand("copy", cmdarg + " /y");

            DisconnectFromRepos();


            return 0;
        }
    }


}
