using System.ServiceProcess;

namespace ElevationService
{
    public partial class Service1 : ServiceBase
    {

    /// <summary>
    /// This is where Service functions begin.
    /// </summary>

    public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            CheckRequest.watchIt();
        }

        protected override void OnStop()
        {
        }

        
    }
}
