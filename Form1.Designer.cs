namespace Teardown_SGE
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openSavegameButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.generalSettings = new System.Windows.Forms.Button();
            this.playerSettings = new System.Windows.Forms.Button();
            this.toolSettings = new System.Windows.Forms.Button();
            this.levelSettings = new System.Windows.Forms.Button();
            this.challengeSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Teko SemiBold", 36F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 404);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(503, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "Teardown Save Game Editor";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "savegame.bin";
            // 
            // openSavegameButton
            // 
            this.openSavegameButton.BackColor = System.Drawing.Color.Black;
            this.openSavegameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openSavegameButton.Font = new System.Drawing.Font("Teko SemiBold", 15F);
            this.openSavegameButton.ForeColor = System.Drawing.Color.White;
            this.openSavegameButton.Location = new System.Drawing.Point(12, 12);
            this.openSavegameButton.Name = "openSavegameButton";
            this.openSavegameButton.Size = new System.Drawing.Size(256, 64);
            this.openSavegameButton.TabIndex = 1;
            this.openSavegameButton.Text = "Load Savegame File";
            this.openSavegameButton.UseVisualStyleBackColor = false;
            this.openSavegameButton.Click += new System.EventHandler(this.openSavegameButton_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Teko SemiBold", 15F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(256, 64);
            this.button1.TabIndex = 2;
            this.button1.Text = "Save Savegame File";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.Enabled = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Teko SemiBold", 15F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(12, 222);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(256, 64);
            this.button2.TabIndex = 3;
            this.button2.Text = "Save Savegame File Manually";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Black;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Teko SemiBold", 15F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(12, 82);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(256, 64);
            this.button3.TabIndex = 4;
            this.button3.Text = "Load Savegame File Manually";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(274, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(466, 274);
            this.panel1.TabIndex = 5;
            // 
            // generalSettings
            // 
            this.generalSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.generalSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.generalSettings.ForeColor = System.Drawing.Color.White;
            this.generalSettings.Location = new System.Drawing.Point(274, 292);
            this.generalSettings.Name = "generalSettings";
            this.generalSettings.Size = new System.Drawing.Size(75, 23);
            this.generalSettings.TabIndex = 0;
            this.generalSettings.Text = "General";
            this.generalSettings.UseVisualStyleBackColor = false;
            // 
            // playerSettings
            // 
            this.playerSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.playerSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.playerSettings.ForeColor = System.Drawing.Color.White;
            this.playerSettings.Location = new System.Drawing.Point(355, 292);
            this.playerSettings.Name = "playerSettings";
            this.playerSettings.Size = new System.Drawing.Size(75, 23);
            this.playerSettings.TabIndex = 6;
            this.playerSettings.Text = "Player";
            this.playerSettings.UseVisualStyleBackColor = false;
            // 
            // toolSettings
            // 
            this.toolSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.toolSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.toolSettings.ForeColor = System.Drawing.Color.White;
            this.toolSettings.Location = new System.Drawing.Point(436, 292);
            this.toolSettings.Name = "toolSettings";
            this.toolSettings.Size = new System.Drawing.Size(75, 23);
            this.toolSettings.TabIndex = 7;
            this.toolSettings.Text = "Tools";
            this.toolSettings.UseVisualStyleBackColor = false;
            // 
            // levelSettings
            // 
            this.levelSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.levelSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.levelSettings.ForeColor = System.Drawing.Color.White;
            this.levelSettings.Location = new System.Drawing.Point(517, 292);
            this.levelSettings.Name = "levelSettings";
            this.levelSettings.Size = new System.Drawing.Size(75, 23);
            this.levelSettings.TabIndex = 8;
            this.levelSettings.Text = "Levels";
            this.levelSettings.UseVisualStyleBackColor = false;
            // 
            // challengeSettings
            // 
            this.challengeSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.challengeSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.challengeSettings.ForeColor = System.Drawing.Color.White;
            this.challengeSettings.Location = new System.Drawing.Point(598, 292);
            this.challengeSettings.Name = "challengeSettings";
            this.challengeSettings.Size = new System.Drawing.Size(142, 23);
            this.challengeSettings.TabIndex = 9;
            this.challengeSettings.Text = "Challenges";
            this.challengeSettings.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(752, 473);
            this.Controls.Add(this.challengeSettings);
            this.Controls.Add(this.levelSettings);
            this.Controls.Add(this.toolSettings);
            this.Controls.Add(this.playerSettings);
            this.Controls.Add(this.generalSettings);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.openSavegameButton);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Teardown Save Game Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button openSavegameButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button generalSettings;
        private System.Windows.Forms.Button playerSettings;
        private System.Windows.Forms.Button toolSettings;
        private System.Windows.Forms.Button levelSettings;
        private System.Windows.Forms.Button challengeSettings;
    }
}

