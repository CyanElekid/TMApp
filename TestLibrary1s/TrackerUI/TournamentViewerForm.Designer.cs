
namespace TrackerUI
{
    partial class TournamentViewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TournamentViewerForm));
            this.tournamentNameLabel = new System.Windows.Forms.Label();
            this.roundLabel = new System.Windows.Forms.Label();
            this.unplayedCheckbox = new System.Windows.Forms.CheckBox();
            this.roundBox = new System.Windows.Forms.ComboBox();
            this.matchupListBox = new System.Windows.Forms.ListBox();
            this.teamOneLabel = new System.Windows.Forms.Label();
            this.score1TextBox = new System.Windows.Forms.TextBox();
            this.score1Label = new System.Windows.Forms.Label();
            this.score2Label = new System.Windows.Forms.Label();
            this.score2TextBox = new System.Windows.Forms.TextBox();
            this.teamTwoLabel = new System.Windows.Forms.Label();
            this.vsLabel = new System.Windows.Forms.Label();
            this.headerLabel = new System.Windows.Forms.Label();
            this.scoreButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tournamentNameLabel
            // 
            this.tournamentNameLabel.AutoSize = true;
            this.tournamentNameLabel.Font = new System.Drawing.Font("Yu Gothic UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tournamentNameLabel.Location = new System.Drawing.Point(244, 9);
            this.tournamentNameLabel.Name = "tournamentNameLabel";
            this.tournamentNameLabel.Size = new System.Drawing.Size(155, 50);
            this.tournamentNameLabel.TabIndex = 2;
            this.tournamentNameLabel.Text = "<none>";
            // 
            // roundLabel
            // 
            this.roundLabel.AutoSize = true;
            this.roundLabel.Font = new System.Drawing.Font("Yu Gothic UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.roundLabel.Location = new System.Drawing.Point(12, 77);
            this.roundLabel.Name = "roundLabel";
            this.roundLabel.Size = new System.Drawing.Size(95, 37);
            this.roundLabel.TabIndex = 3;
            this.roundLabel.Text = "Round";
            // 
            // unplayedCheckbox
            // 
            this.unplayedCheckbox.AutoSize = true;
            this.unplayedCheckbox.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.unplayedCheckbox.Location = new System.Drawing.Point(113, 132);
            this.unplayedCheckbox.Name = "unplayedCheckbox";
            this.unplayedCheckbox.Size = new System.Drawing.Size(193, 36);
            this.unplayedCheckbox.TabIndex = 4;
            this.unplayedCheckbox.Text = "Unplayed Only";
            this.unplayedCheckbox.UseVisualStyleBackColor = true;
            this.unplayedCheckbox.CheckedChanged += new System.EventHandler(this.unplayedCheckbox_CheckedChanged);
            // 
            // roundBox
            // 
            this.roundBox.FormattingEnabled = true;
            this.roundBox.Location = new System.Drawing.Point(113, 86);
            this.roundBox.Name = "roundBox";
            this.roundBox.Size = new System.Drawing.Size(193, 28);
            this.roundBox.TabIndex = 5;
            this.roundBox.SelectedIndexChanged += new System.EventHandler(this.roundBox_SelectedIndexChanged);
            // 
            // matchupListBox
            // 
            this.matchupListBox.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.matchupListBox.FormattingEnabled = true;
            this.matchupListBox.ItemHeight = 30;
            this.matchupListBox.Location = new System.Drawing.Point(12, 214);
            this.matchupListBox.Name = "matchupListBox";
            this.matchupListBox.Size = new System.Drawing.Size(294, 244);
            this.matchupListBox.TabIndex = 6;
            this.matchupListBox.SelectedIndexChanged += new System.EventHandler(this.matchupListBox_SelectedIndexChanged);
            // 
            // teamOneLabel
            // 
            this.teamOneLabel.AutoSize = true;
            this.teamOneLabel.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.teamOneLabel.Location = new System.Drawing.Point(442, 214);
            this.teamOneLabel.Name = "teamOneLabel";
            this.teamOneLabel.Size = new System.Drawing.Size(150, 32);
            this.teamOneLabel.TabIndex = 7;
            this.teamOneLabel.Text = "<Team one>";
            // 
            // score1TextBox
            // 
            this.score1TextBox.Location = new System.Drawing.Point(521, 254);
            this.score1TextBox.Name = "score1TextBox";
            this.score1TextBox.Size = new System.Drawing.Size(100, 27);
            this.score1TextBox.TabIndex = 8;
            // 
            // score1Label
            // 
            this.score1Label.AutoSize = true;
            this.score1Label.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.score1Label.Location = new System.Drawing.Point(442, 249);
            this.score1Label.Name = "score1Label";
            this.score1Label.Size = new System.Drawing.Size(73, 32);
            this.score1Label.TabIndex = 9;
            this.score1Label.Text = "Score";
            // 
            // score2Label
            // 
            this.score2Label.AutoSize = true;
            this.score2Label.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.score2Label.Location = new System.Drawing.Point(442, 390);
            this.score2Label.Name = "score2Label";
            this.score2Label.Size = new System.Drawing.Size(73, 32);
            this.score2Label.TabIndex = 12;
            this.score2Label.Text = "Score";
            // 
            // score2TextBox
            // 
            this.score2TextBox.Location = new System.Drawing.Point(521, 395);
            this.score2TextBox.Name = "score2TextBox";
            this.score2TextBox.Size = new System.Drawing.Size(100, 27);
            this.score2TextBox.TabIndex = 11;
            // 
            // teamTwoLabel
            // 
            this.teamTwoLabel.AutoSize = true;
            this.teamTwoLabel.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.teamTwoLabel.Location = new System.Drawing.Point(442, 355);
            this.teamTwoLabel.Name = "teamTwoLabel";
            this.teamTwoLabel.Size = new System.Drawing.Size(148, 32);
            this.teamTwoLabel.TabIndex = 10;
            this.teamTwoLabel.Text = "<Team two>";
            // 
            // vsLabel
            // 
            this.vsLabel.AutoSize = true;
            this.vsLabel.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.vsLabel.Location = new System.Drawing.Point(497, 302);
            this.vsLabel.Name = "vsLabel";
            this.vsLabel.Size = new System.Drawing.Size(61, 32);
            this.vsLabel.TabIndex = 13;
            this.vsLabel.Text = "-VS-";
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Gadugi", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.headerLabel.Location = new System.Drawing.Point(12, 15);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(233, 44);
            this.headerLabel.TabIndex = 14;
            this.headerLabel.Text = "Tournament";
            // 
            // scoreButton
            // 
            this.scoreButton.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.scoreButton.Location = new System.Drawing.Point(674, 298);
            this.scoreButton.Name = "scoreButton";
            this.scoreButton.Size = new System.Drawing.Size(89, 49);
            this.scoreButton.TabIndex = 15;
            this.scoreButton.Text = "Score";
            this.scoreButton.UseVisualStyleBackColor = true;
            this.scoreButton.Click += new System.EventHandler(this.scoreButton_Click);
            // 
            // TournamentViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(800, 501);
            this.Controls.Add(this.scoreButton);
            this.Controls.Add(this.headerLabel);
            this.Controls.Add(this.vsLabel);
            this.Controls.Add(this.score2Label);
            this.Controls.Add(this.score2TextBox);
            this.Controls.Add(this.teamTwoLabel);
            this.Controls.Add(this.score1Label);
            this.Controls.Add(this.score1TextBox);
            this.Controls.Add(this.teamOneLabel);
            this.Controls.Add(this.matchupListBox);
            this.Controls.Add(this.roundBox);
            this.Controls.Add(this.unplayedCheckbox);
            this.Controls.Add(this.roundLabel);
            this.Controls.Add(this.tournamentNameLabel);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TournamentViewerForm";
            this.Text = "Tournament Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tournamentNameLabel;
        private System.Windows.Forms.Label roundLabel;
        private System.Windows.Forms.CheckBox unplayedCheckbox;
        private System.Windows.Forms.ComboBox roundBox;
        private System.Windows.Forms.ListBox matchupListBox;
        private System.Windows.Forms.Label teamOneLabel;
        private System.Windows.Forms.TextBox score1TextBox;
        private System.Windows.Forms.Label score1Label;
        private System.Windows.Forms.Label score2Label;
        private System.Windows.Forms.TextBox score2TextBox;
        private System.Windows.Forms.Label teamTwoLabel;
        private System.Windows.Forms.Label vsLabel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Button scoreButton;
    }
}

