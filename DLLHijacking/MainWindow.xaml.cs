using System.Windows;
using System.Runtime.InteropServices;


namespace DLLHijacking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    class loadDll
    {
        public const string DllFilePath = @"hijack.dll";
        //Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";C:/ProgramData/DemoApp/Dll");

        [DllImport(DllFilePath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]

        private extern static void dllMain();
        public static void DllMain()
        {
            dllMain();
            //File.AppendAllText(@"C:\\Program\ Files\ (x86)\\bugzzzhunter\\Elevate\\logWatcher2.txt", "Inside spawn function!\n");
        }

    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";C:/ProgramData/ElevateApp/Dll");

            try
            {
                loadDll.DllMain();
            }
            catch {
                MessageBox.Show("Error loading Dll!");
            }
        }
    }
}