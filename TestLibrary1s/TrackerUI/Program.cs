using System;
using System.Windows.Forms;

namespace TrackerUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Initialize database/text files connection
            TestLibrary1.GlobalConfig.InitializeConnections("sql");

            //Application.Run(new CreateTournamentForm());
            Application.Run(new TournamentDashboardForm());
        }
    }
}
