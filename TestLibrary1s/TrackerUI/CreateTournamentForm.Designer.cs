
namespace TrackerUI
{
    partial class CreateTournamentForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerLabel = new System.Windows.Forms.Label();
            this.tournamentNameLabel = new System.Windows.Forms.Label();
            this.entryFeeBox = new System.Windows.Forms.TextBox();
            this.tournamentNameBox = new System.Windows.Forms.TextBox();
            this.entryFeeLabel = new System.Windows.Forms.Label();
            this.selectTeamLabel = new System.Windows.Forms.Label();
            this.selectTeamBox = new System.Windows.Forms.ComboBox();
            this.addTeamButton = new System.Windows.Forms.Button();
            this.removeTeamButton = new System.Windows.Forms.Button();
            this.removePrizeButton = new System.Windows.Forms.Button();
            this.teamsLabel = new System.Windows.Forms.Label();
            this.prizesLabel = new System.Windows.Forms.Label();
            this.createTournamentButton = new System.Windows.Forms.Button();
            this.teamsListBox = new System.Windows.Forms.ListBox();
            this.prizesListBox = new System.Windows.Forms.ListBox();
            this.createPrizeButton = new System.Windows.Forms.Button();
            this.newTeamLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Yu Gothic UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.headerLabel.Location = new System.Drawing.Point(12, 9);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(334, 50);
            this.headerLabel.TabIndex = 2;
            this.headerLabel.Text = "Create Tournament";
            // 
            // tournamentNameLabel
            // 
            this.tournamentNameLabel.AutoSize = true;
            this.tournamentNameLabel.Font = new System.Drawing.Font("Yu Gothic UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tournamentNameLabel.Location = new System.Drawing.Point(35, 78);
            this.tournamentNameLabel.Name = "tournamentNameLabel";
            this.tournamentNameLabel.Size = new System.Drawing.Size(236, 37);
            this.tournamentNameLabel.TabIndex = 4;
            this.tournamentNameLabel.Text = "Tournament Name";
            // 
            // entryFeeBox
            // 
            this.entryFeeBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.entryFeeBox.Location = new System.Drawing.Point(171, 159);
            this.entryFeeBox.Name = "entryFeeBox";
            this.entryFeeBox.Size = new System.Drawing.Size(100, 29);
            this.entryFeeBox.TabIndex = 9;
            // 
            // tournamentNameBox
            // 
            this.tournamentNameBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tournamentNameBox.Location = new System.Drawing.Point(44, 118);
            this.tournamentNameBox.Name = "tournamentNameBox";
            this.tournamentNameBox.Size = new System.Drawing.Size(227, 29);
            this.tournamentNameBox.TabIndex = 10;
            // 
            // entryFeeLabel
            // 
            this.entryFeeLabel.AutoSize = true;
            this.entryFeeLabel.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.entryFeeLabel.Location = new System.Drawing.Point(35, 152);
            this.entryFeeLabel.Name = "entryFeeLabel";
            this.entryFeeLabel.Size = new System.Drawing.Size(97, 30);
            this.entryFeeLabel.TabIndex = 11;
            this.entryFeeLabel.Text = "Entry Fee";
            // 
            // selectTeamLabel
            // 
            this.selectTeamLabel.AutoSize = true;
            this.selectTeamLabel.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.selectTeamLabel.Location = new System.Drawing.Point(35, 232);
            this.selectTeamLabel.Name = "selectTeamLabel";
            this.selectTeamLabel.Size = new System.Drawing.Size(123, 30);
            this.selectTeamLabel.TabIndex = 12;
            this.selectTeamLabel.Text = "Select Team";
            // 
            // selectTeamBox
            // 
            this.selectTeamBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.selectTeamBox.FormattingEnabled = true;
            this.selectTeamBox.Location = new System.Drawing.Point(44, 283);
            this.selectTeamBox.Name = "selectTeamBox";
            this.selectTeamBox.Size = new System.Drawing.Size(227, 29);
            this.selectTeamBox.TabIndex = 13;
            // 
            // addTeamButton
            // 
            this.addTeamButton.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addTeamButton.Location = new System.Drawing.Point(81, 326);
            this.addTeamButton.Name = "addTeamButton";
            this.addTeamButton.Size = new System.Drawing.Size(140, 41);
            this.addTeamButton.TabIndex = 16;
            this.addTeamButton.Text = "Add Team";
            this.addTeamButton.UseVisualStyleBackColor = true;
            this.addTeamButton.Click += new System.EventHandler(this.addTeamButton_Click);
            // 
            // removeTeamButton
            // 
            this.removeTeamButton.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.removeTeamButton.Location = new System.Drawing.Point(692, 118);
            this.removeTeamButton.Name = "removeTeamButton";
            this.removeTeamButton.Size = new System.Drawing.Size(99, 69);
            this.removeTeamButton.TabIndex = 17;
            this.removeTeamButton.Text = "Remove Selected";
            this.removeTeamButton.UseVisualStyleBackColor = true;
            this.removeTeamButton.Click += new System.EventHandler(this.removeTeamButton_Click);
            // 
            // removePrizeButton
            // 
            this.removePrizeButton.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.removePrizeButton.Location = new System.Drawing.Point(692, 326);
            this.removePrizeButton.Name = "removePrizeButton";
            this.removePrizeButton.Size = new System.Drawing.Size(99, 69);
            this.removePrizeButton.TabIndex = 21;
            this.removePrizeButton.Text = "Remove Selected";
            this.removePrizeButton.UseVisualStyleBackColor = true;
            this.removePrizeButton.Click += new System.EventHandler(this.removePrizeButton_Click);
            // 
            // teamsLabel
            // 
            this.teamsLabel.AutoSize = true;
            this.teamsLabel.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.teamsLabel.Location = new System.Drawing.Point(418, 45);
            this.teamsLabel.Name = "teamsLabel";
            this.teamsLabel.Size = new System.Drawing.Size(156, 30);
            this.teamsLabel.TabIndex = 22;
            this.teamsLabel.Text = "Teams / Players";
            // 
            // prizesLabel
            // 
            this.prizesLabel.AutoSize = true;
            this.prizesLabel.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.prizesLabel.Location = new System.Drawing.Point(418, 250);
            this.prizesLabel.Name = "prizesLabel";
            this.prizesLabel.Size = new System.Drawing.Size(67, 30);
            this.prizesLabel.TabIndex = 23;
            this.prizesLabel.Text = "Prizes";
            // 
            // createTournamentButton
            // 
            this.createTournamentButton.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.createTournamentButton.Location = new System.Drawing.Point(293, 456);
            this.createTournamentButton.Name = "createTournamentButton";
            this.createTournamentButton.Size = new System.Drawing.Size(230, 69);
            this.createTournamentButton.TabIndex = 24;
            this.createTournamentButton.Text = "Create Tournament";
            this.createTournamentButton.UseVisualStyleBackColor = true;
            this.createTournamentButton.Click += new System.EventHandler(this.createTournamentButton_Click);
            // 
            // teamsListBox
            // 
            this.teamsListBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.teamsListBox.FormattingEnabled = true;
            this.teamsListBox.ItemHeight = 21;
            this.teamsListBox.Location = new System.Drawing.Point(418, 78);
            this.teamsListBox.Name = "teamsListBox";
            this.teamsListBox.Size = new System.Drawing.Size(255, 151);
            this.teamsListBox.TabIndex = 25;
            // 
            // prizesListBox
            // 
            this.prizesListBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.prizesListBox.FormattingEnabled = true;
            this.prizesListBox.ItemHeight = 21;
            this.prizesListBox.Location = new System.Drawing.Point(418, 281);
            this.prizesListBox.Name = "prizesListBox";
            this.prizesListBox.Size = new System.Drawing.Size(255, 151);
            this.prizesListBox.TabIndex = 26;
            // 
            // createPrizeButton
            // 
            this.createPrizeButton.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.createPrizeButton.Location = new System.Drawing.Point(81, 385);
            this.createPrizeButton.Name = "createPrizeButton";
            this.createPrizeButton.Size = new System.Drawing.Size(140, 41);
            this.createPrizeButton.TabIndex = 27;
            this.createPrizeButton.Text = "Create Prize";
            this.createPrizeButton.UseVisualStyleBackColor = true;
            this.createPrizeButton.Click += new System.EventHandler(this.createPrizeButton_Click);
            // 
            // newTeamLink
            // 
            this.newTeamLink.AutoSize = true;
            this.newTeamLink.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.newTeamLink.Location = new System.Drawing.Point(185, 242);
            this.newTeamLink.Name = "newTeamLink";
            this.newTeamLink.Size = new System.Drawing.Size(86, 20);
            this.newTeamLink.TabIndex = 28;
            this.newTeamLink.TabStop = true;
            this.newTeamLink.Text = "Create New";
            this.newTeamLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.newTeamLink_LinkClicked);
            // 
            // CreateTournamentForm
            // 
            this.ClientSize = new System.Drawing.Size(853, 537);
            this.Controls.Add(this.newTeamLink);
            this.Controls.Add(this.createPrizeButton);
            this.Controls.Add(this.prizesListBox);
            this.Controls.Add(this.teamsListBox);
            this.Controls.Add(this.createTournamentButton);
            this.Controls.Add(this.prizesLabel);
            this.Controls.Add(this.teamsLabel);
            this.Controls.Add(this.removePrizeButton);
            this.Controls.Add(this.removeTeamButton);
            this.Controls.Add(this.addTeamButton);
            this.Controls.Add(this.selectTeamBox);
            this.Controls.Add(this.selectTeamLabel);
            this.Controls.Add(this.entryFeeLabel);
            this.Controls.Add(this.tournamentNameBox);
            this.Controls.Add(this.entryFeeBox);
            this.Controls.Add(this.tournamentNameLabel);
            this.Controls.Add(this.headerLabel);
            this.Name = "CreateTournamentForm";
            this.Text = "Create Tournament Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label tournamentNameLabel;
        private System.Windows.Forms.TextBox entryFeeBox;
        private System.Windows.Forms.TextBox tournamentNameBox;
        private System.Windows.Forms.Label entryFeeLabel;
        private System.Windows.Forms.Label selectTeamLabel;
        private System.Windows.Forms.ComboBox selectTeamBox;
        private System.Windows.Forms.Button addTeamButton;
        private System.Windows.Forms.Button removeTeamButton;
        private System.Windows.Forms.Button removePrizeButton;
        private System.Windows.Forms.Label teamsLabel;
        private System.Windows.Forms.Label prizesLabel;
        private System.Windows.Forms.Button createTournamentButton;
        private System.Windows.Forms.ListBox teamsListBox;
        private System.Windows.Forms.ListBox prizesListBox;
        private System.Windows.Forms.Button createPrizeButton;
        private System.Windows.Forms.LinkLabel newTeamLink;
    }
}

