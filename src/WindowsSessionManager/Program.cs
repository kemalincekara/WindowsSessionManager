using System;
using System.ServiceProcess;
using System.Text;

namespace WindowsSessionManager
{
    static class Program
    {
        public static WindowsServiceManager WindowsServiceManager = new WindowsServiceManager()
        {
            ServiceName = "WindowsSessionManager",
            DisplayName = "Windows Session Manager by Ubden",
            Description = "Windows Session Manager by Ubden"
        };

        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                ArgumentsHandler(args);
                return;
            }

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MainService()
            };
            ServiceBase.Run(ServicesToRun);
        }

        private static void ArgumentsHandler(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                HelpMessage();
                return;
            }
            switch (args[0])
            {
                case "-i":
                case "--install":
                    WindowsServiceManager.Install();
                    Console.WriteLine("Servis başarıyla yüklendi.");
                    break;
                case "-u":
                case "--uninstall":
                    WindowsServiceManager.Uninstall();
                    Console.WriteLine("Servis başarıyla kaldırıldı.");
                    break;
                case "-s":
                case "--start":
                    WindowsServiceManager.Start();
                    Console.WriteLine("Servis başarıyla başlatıldı.");
                    break;
                case "-t":
                case "--stop":
                    WindowsServiceManager.Stop();
                    Console.WriteLine("Servis başarıyla durduruldu.");
                    break;
                default:
                    HelpMessage();
                    break;
            }
        }

        private static void HelpMessage()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Windows Session Manager by Ubden");
            builder.AppendLine("Kullanım: WindowsSessionManager.exe [-i] [-u] [-s] [-t]");
            builder.AppendLine("  -i, --install\tServisi yükler.");
            builder.AppendLine("  -u, --uninstall\tServisi kaldırır.");
            builder.AppendLine("  -s, --start\tServisi başlatır.");
            builder.AppendLine("  -t, --stop\tServisi durdurur.");
            builder.AppendLine("  -?, -h, --help\tBu yardımı gösterir.");
            Console.WriteLine(builder.ToString());
        }
    }
}
