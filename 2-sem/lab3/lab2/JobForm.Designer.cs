namespace lab2
{
    partial class JobForm
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
            this.hiringDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.jobButtonOK = new System.Windows.Forms.Button();
            this.companyTextBox = new System.Windows.Forms.TextBox();
            this.hiringDateLabel = new System.Windows.Forms.Label();
            this.companyLabel = new System.Windows.Forms.Label();
            this.positionTextBox = new System.Windows.Forms.TextBox();
            this.positionLabel = new System.Windows.Forms.Label();
            this.jobButtonCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // hiringDateTimePicker
            // 
            this.hiringDateTimePicker.Location = new System.Drawing.Point(410, 81);
            this.hiringDateTimePicker.Name = "hiringDateTimePicker";
            this.hiringDateTimePicker.Size = new System.Drawing.Size(144, 20);
            this.hiringDateTimePicker.TabIndex = 11;
            this.hiringDateTimePicker.Value = new System.DateTime(2021, 1, 3, 0, 0, 0, 0);
            // 
            // jobButtonOK
            // 
            this.jobButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.jobButtonOK.Location = new System.Drawing.Point(459, 135);
            this.jobButtonOK.Name = "jobButtonOK";
            this.jobButtonOK.Size = new System.Drawing.Size(95, 40);
            this.jobButtonOK.TabIndex = 6;
            this.jobButtonOK.Text = "OK";
            this.jobButtonOK.UseVisualStyleBackColor = true;
            this.jobButtonOK.Click += new System.EventHandler(this.jobButtonOK_Click);
            // 
            // companyTextBox
            // 
            this.companyTextBox.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.companyTextBox.Location = new System.Drawing.Point(30, 80);
            this.companyTextBox.Name = "companyTextBox";
            this.companyTextBox.Size = new System.Drawing.Size(144, 25);
            this.companyTextBox.TabIndex = 7;
            // 
            // hiringDateLabel
            // 
            this.hiringDateLabel.AutoSize = true;
            this.hiringDateLabel.Location = new System.Drawing.Point(407, 60);
            this.hiringDateLabel.Name = "hiringDateLabel";
            this.hiringDateLabel.Size = new System.Drawing.Size(68, 13);
            this.hiringDateLabel.TabIndex = 10;
            this.hiringDateLabel.Text = "Дата найма";
            // 
            // companyLabel
            // 
            this.companyLabel.AutoSize = true;
            this.companyLabel.Location = new System.Drawing.Point(27, 60);
            this.companyLabel.Name = "companyLabel";
            this.companyLabel.Size = new System.Drawing.Size(58, 13);
            this.companyLabel.TabIndex = 6;
            this.companyLabel.Text = "Компания";
            // 
            // positionTextBox
            // 
            this.positionTextBox.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.positionTextBox.Location = new System.Drawing.Point(211, 80);
            this.positionTextBox.Name = "positionTextBox";
            this.positionTextBox.Size = new System.Drawing.Size(144, 25);
            this.positionTextBox.TabIndex = 9;
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Location = new System.Drawing.Point(208, 60);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(65, 13);
            this.positionLabel.TabIndex = 8;
            this.positionLabel.Text = "Должность";
            // 
            // jobButtonCancel
            // 
            this.jobButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.jobButtonCancel.Location = new System.Drawing.Point(340, 135);
            this.jobButtonCancel.Name = "jobButtonCancel";
            this.jobButtonCancel.Size = new System.Drawing.Size(104, 40);
            this.jobButtonCancel.TabIndex = 8;
            this.jobButtonCancel.Text = "Отмена";
            this.jobButtonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.jobButtonCancel);
            this.groupBox3.Controls.Add(this.hiringDateTimePicker);
            this.groupBox3.Controls.Add(this.jobButtonOK);
            this.groupBox3.Controls.Add(this.companyTextBox);
            this.groupBox3.Controls.Add(this.hiringDateLabel);
            this.groupBox3.Controls.Add(this.companyLabel);
            this.groupBox3.Controls.Add(this.positionTextBox);
            this.groupBox3.Controls.Add(this.positionLabel);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(594, 203);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Место работы";
            // 
            // JobForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 203);
            this.Controls.Add(this.groupBox3);
            this.Name = "JobForm";
            this.Text = "JobForm";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker hiringDateTimePicker;
        private System.Windows.Forms.Button jobButtonOK;
        private System.Windows.Forms.TextBox companyTextBox;
        private System.Windows.Forms.Label hiringDateLabel;
        private System.Windows.Forms.Label companyLabel;
        private System.Windows.Forms.TextBox positionTextBox;
        private System.Windows.Forms.Label positionLabel;
        private System.Windows.Forms.Button jobButtonCancel;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}