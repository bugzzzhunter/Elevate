using System.IO;
using System.Windows;

namespace MainGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string commandFile = @"C:\Windows\Temp\Elevate\WhatsTheCommand.txt";

        /// <summary>
        /// The GUI code is below
        /// </summary>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            File.Delete(commandFile);
            base.OnClosing(e);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hack_GUI_Click(object sender, RoutedEventArgs e)
        {

            File.WriteAllText(commandFile, "Module:HackGUI.exe");

        }

        private void Weak_File_Folder_Permissions_Click(object sender, RoutedEventArgs e)
        {

            File.WriteAllText(commandFile, "Module:WeakACL.exe");

        }

        private void DLL_Hijacking_Click(object sender, RoutedEventArgs e)
        {

            File.WriteAllText(commandFile, "Module:DLLHijacking.exe");

        }


    }

}
