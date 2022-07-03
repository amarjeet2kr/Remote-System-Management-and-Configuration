using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSMCUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                Globals.ServerIP = args[0];
                // check if valid ip
                Globals.ValidateIP(Globals.ServerIP);
            }
            if (args.Length == 2)
            {
                Globals.Repos_User = args[1];
                if (String.IsNullOrWhiteSpace(Globals.Repos_User))
                {
                    Console.WriteLine("\nInvalid Repository user name provided! Cannot be empty|whitespaces");
                    System.Environment.Exit(1);
                }
            }
            if (args.Length == 3)
            {
                Globals.Repos_Password = args[2];
                if (String.IsNullOrWhiteSpace(Globals.Repos_User))
                {
                    Console.WriteLine("\nInvalid Repository use password provided! Cannot be empty|whitespaces");
                    System.Environment.Exit(1);
                }
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RSMCUI());
        }
    }
}
