﻿namespace RandomValuesNppPlugin
{
    partial class AboutForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lnkInfo = new System.Windows.Forms.LinkLabel();
            this.lnkGithub = new System.Windows.Forms.LinkLabel();
            this.picEasterEgg = new System.Windows.Forms.PictureBox();
            this.lblDisclaimer = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.btnDonate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picEasterEgg)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(62, 197);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(119, 25);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblTitle.Location = new System.Drawing.Point(8, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(228, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Random Values v";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lnkInfo
            // 
            this.lnkInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkInfo.LinkArea = new System.Windows.Forms.LinkArea(70, 13);
            this.lnkInfo.Location = new System.Drawing.Point(8, 49);
            this.lnkInfo.Name = "lnkInfo";
            this.lnkInfo.Size = new System.Drawing.Size(228, 47);
            this.lnkInfo.TabIndex = 2;
            this.lnkInfo.TabStop = true;
            this.lnkInfo.Tag = "1";
            this.lnkInfo.Text = "Generate random values for passwords, guids and testing purposes,\r\nby Bas de Reuv" +
    "er";
            this.lnkInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkInfo.UseCompatibleTextRendering = true;
            this.lnkInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkClicked);
            // 
            // lnkGithub
            // 
            this.lnkGithub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkGithub.Location = new System.Drawing.Point(0, 138);
            this.lnkGithub.Name = "lnkGithub";
            this.lnkGithub.Size = new System.Drawing.Size(244, 13);
            this.lnkGithub.TabIndex = 4;
            this.lnkGithub.TabStop = true;
            this.lnkGithub.Tag = "2";
            this.lnkGithub.Text = "https://github.com/BdR76/RandomValuesNPP/";
            this.lnkGithub.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkClicked);
            // 
            // picEasterEgg
            // 
            this.picEasterEgg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picEasterEgg.Location = new System.Drawing.Point(206, 191);
            this.picEasterEgg.Name = "picEasterEgg";
            this.picEasterEgg.Size = new System.Drawing.Size(32, 32);
            this.picEasterEgg.TabIndex = 7;
            this.picEasterEgg.TabStop = false;
            this.picEasterEgg.Tag = "0";
            this.picEasterEgg.Click += new System.EventHandler(this.picEasterEgg_Click);
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDisclaimer.Location = new System.Drawing.Point(8, 96);
            this.lblDisclaimer.Name = "lblDisclaimer";
            this.lblDisclaimer.Size = new System.Drawing.Size(228, 32);
            this.lblDisclaimer.TabIndex = 2;
            this.lblDisclaimer.Text = "This software is free-to-use and it is provided as-is without warranty of any kin" +
    "d.";
            this.lblDisclaimer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblLine
            // 
            this.lblLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine.Location = new System.Drawing.Point(4, 38);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(236, 2);
            this.lblLine.TabIndex = 1;
            this.lblLine.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnDonate
            // 
            this.btnDonate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDonate.Location = new System.Drawing.Point(62, 165);
            this.btnDonate.Name = "btnDonate";
            this.btnDonate.Size = new System.Drawing.Size(119, 25);
            this.btnDonate.TabIndex = 8;
            this.btnDonate.Text = "Donate";
            this.btnDonate.UseVisualStyleBackColor = true;
            this.btnDonate.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(244, 229);
            this.Controls.Add(this.btnDonate);
            this.Controls.Add(this.picEasterEgg);
            this.Controls.Add(this.lnkGithub);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblDisclaimer);
            this.Controls.Add(this.lnkInfo);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.picEasterEgg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.LinkLabel lnkInfo;
        private System.Windows.Forms.LinkLabel lnkGithub;
        private System.Windows.Forms.PictureBox picEasterEgg;
        private System.Windows.Forms.Label lblDisclaimer;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Button btnDonate;
    }
}