using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Threading;
using System.Diagnostics;

namespace RSMCUI
{
    class RSMCPipe
    {
        public static void WriteToPipe(PipeStream pipe, string msg)
        {
            byte[] bytes = Encoding.Default.GetBytes(msg);
            pipe.Write(bytes, 0, bytes.Length);
        }

        public static byte[] ReadPipe(PipeStream pipe)
        {
            byte[] buffer = new byte[1024 * 4];
            using (var ms = new MemoryStream())
            {
               var readBytes = pipe.Read(buffer, 0, buffer.Length);
               ms.Write(buffer, 0, readBytes);
               return ms.ToArray();
            } 
        }

        public static string ReadFromPipe(PipeStream pipe)
        {
            var result = ReadPipe(pipe);
            string msg = Encoding.UTF8.GetString(result);
            return msg;
        }

        public static string[] GetClientsArray(string clientlist_string)
        {
            char[] sep = { '|' };
         
            string[] clients = clientlist_string.Split(sep);

            return clients;
        }
        
        public static string[] GetClientDetail(string client_detail_string)
        {
            char[] sep = { ':' };
            string[] client_details = client_detail_string.Split(sep);
            return client_details;
        }

        public static int SendAction(string request, int timeout)
        {
            var pipeClient = new NamedPipeClientStream(".", "rsmcuipipe",
                                         PipeDirection.InOut, PipeOptions.None,
                                         TokenImpersonationLevel.Impersonation);
            try
            {
                pipeClient.Connect(timeout);

                WriteToPipe(pipeClient, request);

                pipeClient.Close();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }
        public static string RefreshClients(int timeout)
        {
            var pipeClient = new NamedPipeClientStream(".", "rsmcuipipe",
                                         PipeDirection.InOut, PipeOptions.None,
                                         TokenImpersonationLevel.Impersonation);
            try
            {
                pipeClient.Connect(timeout);

                string request = "refresh";
                WriteToPipe(pipeClient, request);

                string clientlist_string = ReadFromPipe(pipeClient);
                pipeClient.Close();

                //clientlist_string = "10.16.1.20:defaulthost:Windows 10:1083:Install:COMPLETE:Successfully complete|10.16.1.21:defaulthost1:Windows 7:1083:UnInstall:COMPLETE:Successfully complete";
                return clientlist_string;
            }
            catch (Exception e)
            {
               // string clientlist_string = "10.16.1.20:defaulthost:Windows 10:1083:Install:COMPLETE:Successfully complete|10.16.1.21:defaulthost1:Windows 7:1083:UnInstall:COMPLETE:Successfully complete";
                return "None";
                //return clientlist_string;
            }
        }

    }
}
