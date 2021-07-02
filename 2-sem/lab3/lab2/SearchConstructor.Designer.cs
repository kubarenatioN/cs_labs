namespace lab2
{
    partial class SearchConstructor
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
            this.optionPicker = new System.Windows.Forms.GroupBox();
            this.optionsButton = new System.Windows.Forms.Button();
            this.optionsLabel = new System.Windows.Forms.Label();
            this.optionsComboBox = new System.Windows.Forms.ComboBox();
            this.optionsFiller = new System.Windows.Forms.Panel();
            this.constructorButtonOK = new System.Windows.Forms.Button();
            this.constructorButtonCancel = new System.Windows.Forms.Button();
            this.optionPicker.SuspendLayout();
            this.SuspendLayout();
            // 
            // optionPicker
            // 
            this.optionPicker.Controls.Add(this.optionsButton);
            this.optionPicker.Controls.Add(this.optionsLabel);
            this.optionPicker.Controls.Add(this.optionsComboBox);
            this.optionPicker.Dock = System.Windows.Forms.DockStyle.Top;
            this.optionPicker.Location = new System.Drawing.Point(0, 0);
            this.optionPicker.Name = "optionPicker";
            this.optionPicker.Size = new System.Drawing.Size(325, 86);
            this.optionPicker.TabIndex = 0;
            this.optionPicker.TabStop = false;
            this.optionPicker.Text = "Добавить запрос";
            // 
            // optionsButton
            // 
            this.optionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsButton.Location = new System.Drawing.Point(201, 31);
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.Size = new System.Drawing.Size(118, 40);
            this.optionsButton.TabIndex = 2;
            this.optionsButton.Text = "Добавить";
            this.optionsButton.UseVisualStyleBackColor = true;
            this.optionsButton.Click += new System.EventHandler(this.optionsButton_Click);
            // 
            // optionsLabel
            // 
            this.optionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.optionsLabel.AutoSize = true;
            this.optionsLabel.Location = new System.Drawing.Point(12, 31);
            this.optionsLabel.Name = "optionsLabel";
            this.optionsLabel.Size = new System.Drawing.Size(107, 13);
            this.optionsLabel.TabIndex = 1;
            this.optionsLabel.Text = "Выберите критерий";
            // 
            // optionsComboBox
            // 
            this.optionsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.optionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.optionsComboBox.FormattingEnabled = true;
            this.optionsComboBox.Location = new System.Drawing.Point(11, 50);
            this.optionsComboBox.Name = "optionsComboBox";
            this.optionsComboBox.Size = new System.Drawing.Size(121, 21);
            this.optionsComboBox.TabIndex = 0;
            // 
            // optionsFiller
            // 
            this.optionsFiller.AutoScroll = true;
            this.optionsFiller.Dock = System.Windows.Forms.DockStyle.Top;
            this.optionsFiller.Location = new System.Drawing.Point(0, 86);
            this.optionsFiller.Name = "optionsFiller";
            this.optionsFiller.Size = new System.Drawing.Size(325, 174);
            this.optionsFiller.TabIndex = 3;
            // 
            // constructorButtonOK
            // 
            this.constructorButtonOK.BackColor = System.Drawing.Color.Transparent;
            this.constructorButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.constructorButtonOK.Location = new System.Drawing.Point(201, 284);
            this.constructorButtonOK.Name = "constructorButtonOK";
            this.constructorButtonOK.Size = new System.Drawing.Size(112, 44);
            this.constructorButtonOK.TabIndex = 4;
            this.constructorButtonOK.Text = "OK";
            this.constructorButtonOK.UseVisualStyleBackColor = false;
            this.constructorButtonOK.Click += new System.EventHandler(this.constructorButtonOK_Click);
            // 
            // constructorButtonCancel
            // 
            this.constructorButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.constructorButtonCancel.Location = new System.Drawing.Point(68, 284);
            this.constructorButtonCancel.Name = "constructorButtonCancel";
            this.constructorButtonCancel.Size = new System.Drawing.Size(112, 44);
            this.constructorButtonCancel.TabIndex = 4;
            this.constructorButtonCancel.Text = "Отмена";
            this.constructorButtonCancel.UseVisualStyleBackColor = true;
            // 
            // SearchConstructor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 350);
            this.Controls.Add(this.constructorButtonCancel);
            this.Controls.Add(this.constructorButtonOK);
            this.Controls.Add(this.optionsFiller);
            this.Controls.Add(this.optionPicker);
            this.Name = "SearchConstructor";
            this.Text = "SearchConstructor";
            this.Load += new System.EventHandler(this.SearchConstructor_Load);
            this.optionPicker.ResumeLayout(false);
            this.optionPicker.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox optionPicker;
        private System.Windows.Forms.Button optionsButton;
        private System.Windows.Forms.Label optionsLabel;
        private System.Windows.Forms.ComboBox optionsComboBox;
        private System.Windows.Forms.Panel optionsFiller;
        private System.Windows.Forms.Button constructorButtonOK;
        private System.Windows.Forms.Button constructorButtonCancel;
    }
}