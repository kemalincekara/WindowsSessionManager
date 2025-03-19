using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.ServiceProcess;

namespace WindowsSessionManager
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            serviceProcessInstaller.Account = Program.WindowsServiceManager.Account;
            serviceProcessInstaller.Password = Program.WindowsServiceManager.Password;
            serviceProcessInstaller.Username = Program.WindowsServiceManager.Username;
            serviceInstaller.DelayedAutoStart = Program.WindowsServiceManager.DelayedAutoStart;
            serviceInstaller.Description = Program.WindowsServiceManager.Description;
            serviceInstaller.DisplayName = Program.WindowsServiceManager.DisplayName;
            serviceInstaller.ServiceName = Program.WindowsServiceManager.ServiceName;
            serviceInstaller.StartType = Program.WindowsServiceManager.StartType;

            Committed += new InstallEventHandler(ProjectInstaller_Committed);
            AfterInstall += new InstallEventHandler(ProjectInstaller_AfterInstall);
        }

        private void ProjectInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            try
            {
                using (ServiceController sc = new ServiceController(serviceInstaller.ServiceName, Environment.MachineName))
                {
                    if (sc.Status != ServiceControllerStatus.Running)
                        sc.Start();
                }
            }
            catch (Exception)
            {
                // TODO: Log
            }
        }

        private void ProjectInstaller_Committed(object sender, InstallEventArgs e)
        {
            try
            {
                using (ServiceController sc = new ServiceController(serviceInstaller.ServiceName))
                {
                    SetRecoveryOptions(sc.ServiceName);
                }
            }
            catch (Exception)
            {
                // TODO: Log
            }
        }

        private static void SetRecoveryOptions(string serviceName)
        {
            int exitCode;
            using (var process = new Process())
            {
                var startInfo = process.StartInfo;
                startInfo.FileName = "sc";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                // tell Windows that the service should restart if it fails
                startInfo.Arguments = string.Format("failure \"{0}\" reset= 0 actions= restart/60000", serviceName);

                process.Start();
                process.WaitForExit();

                exitCode = process.ExitCode;
            }

            if (exitCode != 0)
                throw new InvalidOperationException();
        }
    }
}