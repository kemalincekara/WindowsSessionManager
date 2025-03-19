using System;
using System.Collections;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;

namespace WindowsSessionManager
{
    public class WindowsServiceManager
    {
        public bool DelayedAutoStart { get; set; }
        public ServiceStartMode StartType { get; set; }
        public ServiceAccount Account { get; set; }
        public string Username { get; set; } = null;
        public string Password { get; set; } = null;
        public string ServiceName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string AppDir { get; set; }
        public string InstallAssembly { get; set; }
        public string LogFile { get; set; }

        public WindowsServiceManager()
        {
            DelayedAutoStart = false;
            StartType = ServiceStartMode.Automatic;
            Account = ServiceAccount.LocalSystem;
            Username = null;
            Password = null;
            AppDir = Path.GetDirectoryName(Application.ExecutablePath);
            InstallAssembly = Path.GetFileName(Application.ExecutablePath);
            LogFile = Path.Combine(AppDir, "WindowsServiceLog.log");
        }

        public void Install()
        {
            IDictionary savedState = new Hashtable();
            string[] commandLine = new[]
            {
                //"/ShowCallStack=true",
                //"/LogToConsole=true",
                $@"/LogFile=""{LogFile}"""
                //$@"/InstallStateDir=""{AppDir}"""
            };
            using (AssemblyInstaller installer = new AssemblyInstaller(InstallAssembly, commandLine)
            {
                UseNewContext = true
            })
            {
                try
                {
                    installer.Install(savedState);
                    installer.Commit(savedState);
                }
                catch (Exception ex)
                {
                    try
                    {
                        installer.Rollback(savedState);
                    }
                    catch (Exception rollbackEx)
                    {
                        throw new AggregateException(ex, rollbackEx);
                    }
                    throw;
                }
            }

            Start();
        }

        public void Uninstall()
        {
            Stop();
            IDictionary savedState = new Hashtable();
            string[] commandLine = new[] {
                //"/ShowCallStack=true",
                //"/LogToConsole=true",
                $@"/LogFile=""{LogFile}"""
            };
            using (AssemblyInstaller installer = new AssemblyInstaller(InstallAssembly, commandLine)
            {
                UseNewContext = true
            })
            {
                try
                {
                    installer.Uninstall(savedState);
                    installer.Commit(savedState);
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public ServiceControllerStatus? Status()
        {
            using (var sc = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == ServiceName))
            {
                return sc?.Status;
            }
        }

        public void Start()
        {
            using (var sc = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == ServiceName))
            {
                if (sc != null && sc.Status == ServiceControllerStatus.Stopped)
                    sc.Start();
            }
        }

        public void Stop()
        {
            using (var sc = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == ServiceName))
            {
                if (sc != null && sc.Status == ServiceControllerStatus.Running)
                    sc.Stop();
            }
        }
    }
}
