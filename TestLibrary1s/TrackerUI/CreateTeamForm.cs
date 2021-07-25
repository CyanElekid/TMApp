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
using TrackerUI.Interfaces;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private ITeamRequester callingForm;

        private List<PersonModel> availableTeamMembers = new List<PersonModel>();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;

            LoadData();
           // CreateSampleData();

            WireUpLists();
        }

        private void LoadData()
        {
            availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        }
        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Sindre", LastName = "Somethin" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Foo", LastName = "Stuff" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Sue", LastName = "Bar" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Lana", LastName = "Pet" });
        }

        private void WireUpLists()
        {
            /*     DataTable fullName = new DataTable();
                 fullName.Columns.Add(
         "FullName",
         typeof(string),
         $"{FirstName} {LastName}");*/
            //set to null to refresh ui
            selectMemberBox.DataSource = null;
            selectMemberBox.DataSource = availableTeamMembers;
            selectMemberBox.DisplayMember = "FullName";

            playerListBox.DataSource = null;
            playerListBox.DataSource = selectedTeamMembers;
            playerListBox.DisplayMember = "FullName";
        }
        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateMemberForm())
            {
                PersonModel model = new PersonModel(
                    firstNameTextBox.Text,
                    lastNameTextBox.Text,
                    emailTextBox.Text,
                    phoneNumberTextBox.Text);

                GlobalConfig.Connection.CreatePerson(model);
                selectedTeamMembers.Add(model);

                WireUpLists();

                lastNameTextBox.Text = "";
                firstNameTextBox.Text = "";
                emailTextBox.Text = "";
                phoneNumberTextBox.Text = "";

            }
            else
            {
                MessageBox.Show("Invalid form");
            }
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel person = (PersonModel)selectMemberBox.SelectedItem;

            if (person != null)
            {
                availableTeamMembers.Remove(person);
                selectedTeamMembers.Add(person);

                WireUpLists(); 
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            PersonModel person = (PersonModel)playerListBox.SelectedItem;
            if (person != null)
            {
                selectedTeamMembers.Remove(person);
                availableTeamMembers.Add(person);
                WireUpLists();
            }
        }
        //TODO: close form after creating team (eventually reset)
        private void createTeamButton_Click(object sender, EventArgs e)
        {
            if (ValidateTeamForm())
            {
                TeamModel team = new TeamModel(
                    teamNameBox.Text,
                    selectedTeamMembers);

                GlobalConfig.Connection.CreateTeam(team);

                callingForm.TeamComplete(team);

                this.Close();
            }
        }

        private bool ValidateMemberForm()
        {
            bool output = true;

            if (firstNameTextBox.Text.Length == 0)
            {
                output = false;
            }
            // TODO: check both and give feedback (remove else)
            else if (lastNameTextBox.Text.Length == 0)
            {
                output = false;
            }

            if (emailTextBox.Text.Length == 0)
            {
                output = false;
            }

            if (phoneNumberTextBox.Text.Length == 0)
            {
                output = false;
            }

            return output;
        }

        private bool ValidateTeamForm()
        {
            bool output = true;

            if (selectedTeamMembers.Count == 0)
            {
                output = false;
            }

            if (teamNameBox.Text.Length == 0)
            {
                output = false;
            }

            return output;
        }
    }
}
