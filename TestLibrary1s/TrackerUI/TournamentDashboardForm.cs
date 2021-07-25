using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestLibrary1;
using TestLibrary1.Models;

namespace TrackerUI
{
    public partial class TournamentDashboardForm : Form
    {
        List<TournamentModel> tournaments = GlobalConfig.Connection.GetTournament_All();
        public TournamentDashboardForm()
        {

            InitializeComponent();

            WireUpLists();
        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {
            TournamentModel tm = (TournamentModel)loadTournamentComboBox.SelectedItem;
            TournamentViewerForm form = new TournamentViewerForm(tm);
            form.Show();
        }

        private void WireUpLists()
        {
            loadTournamentComboBox.DataSource = null;
            loadTournamentComboBox.DataSource = tournaments;
            loadTournamentComboBox.DisplayMember = "TournamentName";
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournamentForm form = new CreateTournamentForm();
            form.Show();
        }
    }
}
