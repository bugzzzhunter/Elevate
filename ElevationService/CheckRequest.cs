using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ElevationService
{

    class externalSpawn
    {

        private const string DllFilePath = "Spawner.dll";
        //private const string DllFilePath = Path.Combine(strWorkPath, @"\Spawner.dll");

        [DllImport(DllFilePath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private extern static void spawn(string module);
        public static void Spawn(string module)
        {
            spawn(module);
            //File.AppendAllText(@"C:\\Program\ Files\ (x86)\\bugzzzhunter\\Elevate\\logWatcher2.txt", "Inside spawn function!\n");
        }
    }
    class CheckRequest
    {

        public static string folder = @"C:\Windows\Temp\Elevate\";
        public static string fileName = "WhatsTheCommand.txt";
        public static string commandFilePath = folder + fileName;

        //public static string folder1 = @".\";

        //static string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        //static string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
        //public static string fileName1 = "\\logWatcher2.txt";
        //public static string fullPath = strWorkPath + fileName1;
        public static string fullPath = @"c:\ProgramData\ElevateApp\logWatcher2.txt";
        public static void parseCommand()
        {
            
            Console.WriteLine("Parsing command!\n");

            string readText = File.ReadAllText(commandFilePath);
            File.AppendAllText(fullPath, "Command Read: " + readText + "\n");
            Console.WriteLine("File Read: {0}\n", readText);
            
            if (readText.StartsWith("Module:"))
            {
                string module = readText.Split(':')[1];
                File.AppendAllText(fullPath, "Module Requested: " + module + "\n");
                Console.WriteLine("Module Read: {0}\n", module);
                //processRequest.spawnGUI();
                //externalSpawn Spawn1 = new externalSpawn();
                //List<String> Exelist = new List<String>();

                // Adding elements to List
                //Exelist.Add("OpenHackGUI");
                try
                {
                    //if (Exelist.Contains(module))
                    {
                        externalSpawn.Spawn(module);
                    }
                }
                catch(Exception e)
                {
                    PrintException(e);
                    File.AppendAllText(fullPath, e.ToString());
                }
                File.AppendAllText(fullPath, "Finally Safe!\n");
                /*File.AppendAllText(fullPath, "V is: "+ v +"!\n");
                if (v)
                {
                    File.AppendAllText(fullPath, "Module run success!\n"); 
                    Console.WriteLine("Success!"); 
                }*/

            }

        }


        public static void watchIt()
        {
            
            //File.WriteAllLines(fullPath, authors);
            var watcher = new FileSystemWatcher(@"C:\Windows\Temp\Elevate");

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            //watcher.Deleted += OnDeleted;
            //watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.Filter = "WhatsTheCommand.txt";
            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = true;

            //Console.WriteLine("Press enter to exit.");
            //Console.ReadLine();
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {

            if (e.ChangeType != WatcherChangeTypes.Changed)
                {
                    return;
                }
            //string[] FullPathArray = e.FullPath.Split();
            File.AppendAllText(fullPath, "Modified: "+e.FullPath+"\n");
            Console.WriteLine($"Changed: {e.FullPath}");
            parseCommand();
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            //string[] FullPathArray = e.FullPath.Split();
            //File.AppendAllText(logFile.fullPath, e.FullPath);
            File.AppendAllText(fullPath, "Created: "+e.FullPath + "\n");
            Console.WriteLine(value);
            //parseCommand();
        }

        /*
        private static void OnDeleted(object sender, FileSystemEventArgs e) =>
                Console.WriteLine($"Deleted: {e.FullPath}");

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
                Console.WriteLine($"Renamed:");
                Console.WriteLine($"    Old: {e.OldFullPath}");
                Console.WriteLine($"    New: {e.FullPath}");
        }*/

        private static void OnError(object sender, ErrorEventArgs e) =>
                PrintException(e.GetException());

        private static void PrintException(Exception ex)
        {
                if (ex != null)
                {
                    Console.WriteLine($"Message: {ex.Message}");
                    Console.WriteLine("Stacktrace:");
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine();
                    PrintException(ex.InnerException);
                }
        }

    }
}
