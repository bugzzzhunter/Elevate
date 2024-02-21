using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ElevationService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void setFolderAndFiles()
        {
            string ElevatePath = @"c:\Windows\Temp\Elevate";
            if (!Directory.Exists(ElevatePath))
            {
                Directory.CreateDirectory(ElevatePath);
            }
            string ElevateAppPath = @"c:\ProgramData\ElevateApp";
            if (!Directory.Exists(ElevateAppPath))
            {
                Directory.CreateDirectory(ElevateAppPath);
            }

            const string commandFile = @"C:\Windows\Temp\Elevate\WhatsTheCommand.txt";

            if (!File.Exists(commandFile))
            {
                File.Create(commandFile).Dispose();
            }
            DirectoryInfo di1 = new DirectoryInfo(commandFile);
            DirectorySecurity ds1 = di1.GetAccessControl(AccessControlSections.All);
            //DirectorySecurity ds1 = Directory.GetAccessControl(LogFile);

            ds1.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
            di1.SetAccessControl(ds1);
        }
        static void Main()
        {

            setFolderAndFiles();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
