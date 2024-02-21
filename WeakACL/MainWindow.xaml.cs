using System;
using System.IO;
using System.Security.AccessControl;
using System.Windows;

namespace WeakACL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private static readonly log4net.ILog log1 = log4net.LogManager.GetLogger("Logger1");
        //private static readonly log4net.ILog log2 = log4net.LogManager.GetLogger("Logger2");

        void CreateFolders()
        {
            //log1.Info("Hello logging world!");
            //log2.Info("Hello logging world!");
            string DebugPath = @"c:\ProgramData\ElevateApp\DebugLogs";
            if (!Directory.Exists(DebugPath))
            {
                Directory.CreateDirectory(DebugPath);
            }

            string DataPath = @"c:\ProgramData\ElevateApp\Data";
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }

            string DataCachePath = @"c:\ProgramData\ElevateApp\Data\Cache";
            if (!Directory.Exists(DataCachePath))
            {
                Directory.CreateDirectory(DataCachePath);
            }

            DirectoryInfo di = new DirectoryInfo(DataCachePath);
            DirectorySecurity ds = di.GetAccessControl(AccessControlSections.All);
            //DirectorySecurity ds = Directory.GetAccessControl();
            
            
            ds.SetAccessRuleProtection(true, false);
            ds.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));

            //Directory.SetAccessControl(DataCachePath, ds);

            di.SetAccessControl(ds);

            string ConfigPath = @"c:\ProgramData\ElevateApp\Config";
            if (!Directory.Exists(ConfigPath))
            {
                Directory.CreateDirectory(ConfigPath);
            }

            DirectoryInfo di1 = new DirectoryInfo(ConfigPath);
            DirectorySecurity ds1 = di1.GetAccessControl(AccessControlSections.All);
            //ds = Directory.GetAccessControl(ConfigPath);
            ds1.SetAccessRuleProtection(true, false);
            ds1.AddAccessRule(new FileSystemAccessRule(@"Administrators", FileSystemRights.FullControl, AccessControlType.Allow));

            di1.SetAccessControl(ds1);
            //Directory.SetAccessControl(ConfigPath, ds);

            /*}
            else
            {
                DirectorySecurity ds = Directory.GetAccessControl(ConfigPath);
                ds.SetAccessRuleProtection(true, false);
                ds.AddAccessRule(new FileSystemAccessRule(@"Administrators", FileSystemRights.FullControl, AccessControlType.Allow));
                Directory.SetAccessControl(ConfigPath, ds);
            }*/

            string ConfigFile = @"c:\ProgramData\ElevateApp\Config\Config-Arguments.config";
            StreamWriter f = File.AppendText(ConfigFile);
            f.Write("There is nothing in here, Go away!\n");
            f.Close();

            return;
        }

        private void CreateRegLog(object sender, RoutedEventArgs e)
        {
            //Create folder for log if not exists
            log4net.ILog log1 = log4net.LogManager.GetLogger("Logger1");

            log1.Info("[*]Logging Enabled");
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            log4net.ILog log1 = log4net.LogManager.GetLogger("Logger1");
            log1.Info("[*]Application Stopped!");
            Environment.Exit(1);
        }

        private void CreatePermLog(object sender, RoutedEventArgs e)
        {

            string LogFile = @"c:\ProgramData\ElevateApp\Logs\AppLog2.log";
            string LogFile2 = @"c:\ProgramData\ElevateApp\Logs\AppLog3.log";

            string LogPath = @"c:\ProgramData\ElevateApp\Logs\";
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
                //DirectorySecurity ds = Directory.GetAccessControl(LogPath2);
                //ds.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
                //Directory.SetAccessControl(LogPath2, ds);
            }

            //log2.Info("[*]Logging Enabled");
            StreamWriter f = File.AppendText(LogFile);
            f.Write("[*] Permissive Log file created\n");
            f.Close();

            DirectoryInfo di1 = new DirectoryInfo(LogFile);
            DirectorySecurity ds1 = di1.GetAccessControl(AccessControlSections.All);

            //DirectorySecurity ds1 = Directory.GetAccessControl(LogFile);
            

            ds1.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
            di1.SetAccessControl(ds1);

            try
            {
                DirectoryInfo di2 = new DirectoryInfo(LogFile2);
                DirectorySecurity ds2 = di2.GetAccessControl(AccessControlSections.All);
                ds1 = di2.GetAccessControl(AccessControlSections.All);
                ds1.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                di2.SetAccessControl(ds1);
                /*
                ds1 = Directory.GetAccessControl(LogFile2);
                ds1.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                Directory.SetAccessControl(LogFile2, ds1);*/
            }
            catch (DirectoryNotFoundException dirEx)
            {
                // Let the user know that the directory did not exist.
                Console.WriteLine("Directory not found: " + dirEx.Message);
            }
        }
        private void DeleteLogs(object sender, RoutedEventArgs e)
        {

            string logs = @"c:\ProgramData\ElevateApp\Logs";
            string crash = @"c:\ProgramData\ElevateApp\Logs\CrashData";

            // If directory does not exist, don't even try   
            if (Directory.Exists(crash))
            {
                Directory.Delete(crash, true);
            }
            if (Directory.Exists(logs))
            {
                Directory.Delete(logs, true);
            }

        }

        public MainWindow()
        {
            InitializeComponent();

            CreateFolders();

            log4net.ILog log1 = log4net.LogManager.GetLogger("Logger1");

            log1.Info("[*]Application Started!");

        }
    }
}
