using System;
using System.IO;
using System.Windows;
using System.Windows.Navigation;


namespace OtherGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            
            // for .NET Core you need to add UseShellExecute = true
            // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
            try
            {

                System.Diagnostics.Process.Start("C:\\Windows\\System32\\notepad.exe");
            }
            catch (Exception ex)
            {
                File.WriteAllText("NewFile.txt", ex.ToString());
            }

        }
    }
}
