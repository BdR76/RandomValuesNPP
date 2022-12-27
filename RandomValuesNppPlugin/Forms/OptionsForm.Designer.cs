
namespace RandomValuesNppPlugin
{
    partial class OptionsForm
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
            this.labelWidth = new System.Windows.Forms.Label();
            this.textWidth = new System.Windows.Forms.TextBox();
            this.labelCase = new System.Windows.Forms.Label();
            this.comboboxCase = new System.Windows.Forms.ComboBox();
            this.checkMixMask = new System.Windows.Forms.CheckBox();
            this.checkPwSafe = new System.Windows.Forms.CheckBox();
            this.labelEmptyPerc = new System.Windows.Forms.Label();
            this.textEmptyPerc = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(12, 20);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(79, 13);
            this.labelWidth.TabIndex = 0;
            this.labelWidth.Text = "Maximum width";
            // 
            // textWidth
            // 
            this.textWidth.Location = new System.Drawing.Point(128, 16);
            this.textWidth.Name = "textWidth";
            this.textWidth.Size = new System.Drawing.Size(66, 20);
            this.textWidth.TabIndex = 2;
            // 
            // labelCase
            // 
            this.labelCase.AutoSize = true;
            this.labelCase.Location = new System.Drawing.Point(12, 48);
            this.labelCase.Name = "labelCase";
            this.labelCase.Size = new System.Drawing.Size(112, 13);
            this.labelCase.TabIndex = 1;
            this.labelCase.Text = "Uppercase/lowercase";
            // 
            // comboboxCase
            // 
            this.comboboxCase.BackColor = System.Drawing.SystemColors.Window;
            this.comboboxCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxCase.FormattingEnabled = true;
            this.comboboxCase.Items.AddRange(new object[] {
            "(None)",
            "Lower",
            "Upper",
            "Mixed",
            "InitCap"});
            this.comboboxCase.Location = new System.Drawing.Point(128, 44);
            this.comboboxCase.Name = "comboboxCase";
            this.comboboxCase.Size = new System.Drawing.Size(94, 21);
            this.comboboxCase.TabIndex = 3;
            // 
            // checkMixMask
            // 
            this.checkMixMask.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkMixMask.Location = new System.Drawing.Point(12, 72);
            this.checkMixMask.Name = "checkMixMask";
            this.checkMixMask.Size = new System.Drawing.Size(130, 17);
            this.checkMixMask.TabIndex = 4;
            this.checkMixMask.Text = "Mix mask";
            this.checkMixMask.UseVisualStyleBackColor = true;
            // 
            // checkPwSafe
            // 
            this.checkPwSafe.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkPwSafe.Location = new System.Drawing.Point(12, 100);
            this.checkPwSafe.Name = "checkPwSafe";
            this.checkPwSafe.Size = new System.Drawing.Size(130, 17);
            this.checkPwSafe.TabIndex = 4;
            this.checkPwSafe.Text = "Password safe chars.";
            this.checkPwSafe.UseVisualStyleBackColor = true;
            // 
            // labelEmptyPerc
            // 
            this.labelEmptyPerc.AutoSize = true;
            this.labelEmptyPerc.Location = new System.Drawing.Point(12, 132);
            this.labelEmptyPerc.Name = "labelEmptyPerc";
            this.labelEmptyPerc.Size = new System.Drawing.Size(93, 13);
            this.labelEmptyPerc.TabIndex = 0;
            this.labelEmptyPerc.Text = "Empty percentage";
            // 
            // textEmptyPerc
            // 
            this.textEmptyPerc.Location = new System.Drawing.Point(128, 128);
            this.textEmptyPerc.Name = "textEmptyPerc";
            this.textEmptyPerc.Size = new System.Drawing.Size(66, 20);
            this.textEmptyPerc.TabIndex = 2;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(152, 164);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(71, 164);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 199);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.checkPwSafe);
            this.Controls.Add(this.checkMixMask);
            this.Controls.Add(this.comboboxCase);
            this.Controls.Add(this.textEmptyPerc);
            this.Controls.Add(this.textWidth);
            this.Controls.Add(this.labelEmptyPerc);
            this.Controls.Add(this.labelCase);
            this.Controls.Add(this.labelWidth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelCase;
        private System.Windows.Forms.TextBox textWidth;
        private System.Windows.Forms.ComboBox comboboxCase;
        private System.Windows.Forms.CheckBox checkMixMask;
        private System.Windows.Forms.Label labelEmptyPerc;
        private System.Windows.Forms.TextBox textEmptyPerc;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkPwSafe;
    }
}