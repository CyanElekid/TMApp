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
using TestLibrary1.FunctionLibrary;
using TestLibrary1.Models;

namespace TrackerUI
{
    public partial class TournamentViewerForm : Form
    {
        private TournamentModel tournament;
        private List<int> roundNumber = new List<int>();
        List<MatchupModel> currentRound = new List<MatchupModel>();

        // bindingsource? bindinglist?

        public TournamentViewerForm(TournamentModel tournamentModel)
        {
            InitializeComponent();

            tournament = tournamentModel;
            LoadFormData();
            LoadRounds();
            WireUpRoundList();
            WireUpMatchupList();
        }

        private void LoadFormData()
        {
            tournamentNameLabel.Text = tournament.TournamentName;

        }

        private void WireUpRoundList()
        {
            roundBox.DataSource = null;
            roundBox.DataSource = roundNumber;
        }

        private void WireUpMatchupList()
        {
            matchupListBox.DataSource = null;
            matchupListBox.DataSource = currentRound;
            matchupListBox.DisplayMember = "DisplayName";
        }

        private void LoadRounds()
        {
            int roundsCount = tournament.Rounds.Count;
            for (int i = 1; i<= roundsCount; i++)
            {
                roundNumber.Add(i);
            }
        }

        private List<MatchupModel> LoadRound(int i)
        {
            List<MatchupModel> output = new List<MatchupModel>();

            output = tournament.Rounds[i - 1];
            return output;
        }

        private void roundBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups();
        }

        private void LoadMatchups()
        {
            List<MatchupModel> output = new List<MatchupModel>();
            int round = (int)roundBox.SelectedItem;
            if (!unplayedCheckbox.Checked)
            {
                output = LoadRound(round);
            }
            else
            {
                foreach(MatchupModel m in LoadRound(round))
                {
                    if(m.Winner == null)
                    {
                        output.Add(m);
                    }
                }
            }
            currentRound = output;
            WireUpMatchupList();
            DisplayMatchupInfo();
        }

        private void DisplayMatchupInfo() 
        {
            bool isVisible = (currentRound.Count > 0);

            teamOneLabel.Visible = isVisible;
            teamTwoLabel.Visible = isVisible;
            score1Label.Visible = isVisible;
            score2Label.Visible = isVisible;
            score1TextBox.Visible = isVisible;
            score2TextBox.Visible = isVisible;
            scoreButton.Visible = isVisible;
            vsLabel.Visible = isVisible;
        }

        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchup();
        }

        private void LoadMatchup()
        {
            MatchupModel model = (MatchupModel)matchupListBox.SelectedItem;
            if (model == null)
            {
                return;
            }
            for (int i = 0; i <model.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (model.Entries[0].TeamCompeting != null)
                    {
                        teamOneLabel.Text = model.Entries[0].TeamCompeting.TeamName;
                        score1TextBox.Text = model.Entries[0].Score.ToString();
                    }
                    else
                    {
                        teamOneLabel.Text = "TBD";
                        score1TextBox.Text = "";
                    }
                }
                if (i == 1)
                {
                    if (model.Entries[1].TeamCompeting != null)
                    {
                        teamTwoLabel.Text = model.Entries[1].TeamCompeting.TeamName;
                        score2TextBox.Text = model.Entries[1].Score.ToString();
                    }
                    else
                    {
                        teamTwoLabel.Text = "TBD";
                        score2TextBox.Text = "";
                    }
                }
                else if (model.Entries.Count == 1)
                {
                    teamTwoLabel.Text = "<Bye>";
                    score2TextBox.Text = "0";
                }
            }
        }

        private void unplayedCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups();
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            MatchupModel model = (MatchupModel)matchupListBox.SelectedItem;

            if (model == null)
            {
                return;
            }
            if (!IsValid())
            {
                return;
            }
            for (int i = 0; i < model.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (model.Entries[0].TeamCompeting != null)
                    {
                        model.Entries[0].Score = double.Parse(score1TextBox.Text);
                    }
                }
                if (i == 1)
                {
                    if (model.Entries[1].TeamCompeting != null)
                    {
                        model.Entries[1].Score = double.Parse(score2TextBox.Text);
                    }

                }
            }
            try
            {
                MatchupHelper.UpdateTournamentResults(tournament);
            }
            catch (Exception ex )
            {

                MessageBox.Show($"Application had the following error: {ex.Message}");
                return;
            }

            LoadMatchups();
        }

        private bool IsValid()
        {
            bool output = true;
            string errorMessage = "";
            //Starts different so that we don't get false positives on tie
            double teamOneScore = 0;
            double teamTwoScore = -1;

            bool ScoreOneValid = double.TryParse(score1TextBox.Text, out teamOneScore);
            bool ScoreTwoValid = double.TryParse(score2TextBox.Text, out teamTwoScore);
            if( !ScoreOneValid)
            {
                output = false;
                errorMessage+=$"Team one score is not a valid number. \n";
            }

            if (!ScoreTwoValid)
            {
                output = false;
                errorMessage += $"Team two score is not a valid number.\n";
            }


            if (teamOneScore == teamTwoScore && errorMessage == "")
            {
                output = false;
                errorMessage+=$"Please do some tiebreaker \n";
            }

            if(errorMessage != "")
            {
                MessageBox.Show($"Input error: \n{errorMessage}");
            }

            return output;
        }
    }
}
