using Cassia;
using System;
using System.Configuration;
using System.Linq;

namespace WindowsSessionManager
{
    public class SessionManagement
    {
        public int SessionTimeout { get; set; }
        public string[] WhiteList { get; set; }

        public SessionManagement()
        {
            SessionTimeout = ConfigurationManager.AppSettings["SessionTimeoutMinutes"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["SessionTimeoutMinutes"]) : 60;
            WhiteList = ConfigurationManager.AppSettings["WhiteList"]?.Split(',');
        }

        public void LogoffSessionTimeout()
        {
            try
            {
                ITerminalServicesManager manager = new TerminalServicesManager();
                using (ITerminalServer server = manager.GetLocalServer())
                {
                    server.Open();
                    foreach (ITerminalServicesSession session in server.GetSessions())
                    {
                        if (session.UserAccount == null)
                            continue;
                        if (WhiteList != null && WhiteList.Contains(session.UserName, StringComparer.OrdinalIgnoreCase))
                            continue;
                        if (session.ConnectionState != ConnectionState.Disconnected)
                            continue;
                        if (!session.DisconnectTime.HasValue)
                            continue;
                        if ((DateTime.Now - session.DisconnectTime.Value).TotalMinutes <= SessionTimeout)
                            continue;
                        session.Logoff();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}