using System;
using System.Configuration;
using System.ServiceProcess;

namespace WindowsSessionManager
{
    public partial class MainService : ServiceBase
    {
        private readonly System.Timers.Timer _timer;
        private readonly SessionManagement _sessionManagement;
        public MainService()
        {
            InitializeComponent();
            ServiceName = Program.WindowsServiceManager.ServiceName;
            _sessionManagement = new SessionManagement();
            var timerIntervalMinutes = ConfigurationManager.AppSettings["TimerIntervalMinutes"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["TimerIntervalMinutes"]) : 5;
            _timer = new System.Timers.Timer(timerIntervalMinutes * 60 * 1000)
            {
                AutoReset = true,
                Enabled = false
            };
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerElapsed);
        }

        protected override void OnStart(string[] args) => StartProgram();

        protected override void OnContinue() => StartProgram();

        protected override void OnStop() => StopProgram();

        protected override void OnPause() => StopProgram();

        protected override void OnShutdown() => StopProgram();

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus) => true;

        private void StartProgram() => _timer.Start();

        private void StopProgram() => _timer.Stop();

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e) => _sessionManagement.LogoffSessionTimeout();
    }
}