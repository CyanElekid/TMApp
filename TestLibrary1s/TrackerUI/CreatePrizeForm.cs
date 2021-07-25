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
using TestLibrary1.Interfaces;
using TestLibrary1.Models;
using TrackerUI.Interfaces;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        private IPrizeRequester callingForm;
        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();

            callingForm = caller;
        }
        //TODO : Doesn't run for some reason --- f ixed, doesn't auto generate event
        private void CreatePrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PrizeModel model = new PrizeModel(
                    placeNumberTextBox.Text,
                    placeNameTextBox.Text,
                    prizeAmountTextBox.Text, 
                    prizePercentageTextBox.Text);

                GlobalConfig.Connection.CreatePrize(model);

                callingForm.PrizeComplete(model);
                this.Close();

                //placeNameTextBox.Text = "";
                //placeNumberTextBox.Text = "";
                //prizeAmountTextBox.Text = "0";
                //prizePercentageTextBox.Text = "0";

            }
            else
            {
                MessageBox.Show("Invalid form");
            }

        }

        private bool ValidateForm()
        {
            bool output = true;
            int placeNumber = 0;
            string placeName = placeNameTextBox.Text;
            decimal prizeAmount = 0;
            double prizePercentage = 0;
     
            bool placeNumberValidNumber = int.TryParse(placeNumberTextBox.Text, out placeNumber);
            bool placeNameValid = (placeName != "");
            bool prizeAmountValid = decimal.TryParse(prizeAmountTextBox.Text, out prizeAmount);
            bool prizePercentageValid = double.TryParse(prizePercentageTextBox.Text, out prizePercentage);
            
            if (!placeNumberValidNumber)
            {
                output = false;
            }
            // TODO: check both and give feedback (remove else)
            else if (placeNumber < 1)
            {
                output = false;
            }

            if (!prizeAmountValid || !prizePercentageValid)
            {
                output = false;
            }

            if(prizeAmount <= 0 && prizePercentage <= 0)
            {
                output = false;
            }

            if (prizePercentage < 0 || prizePercentage > 100)
            {
                output = false;
            }

            if (!placeNameValid)
            {
                output = false;
            }

            return output;
        }
    }
}
