using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class JobForm : Form
    {
        private Form1 ParentForm;
        public JobForm()
        {
            InitializeComponent();
        }
        public JobForm(Form1 f)
        {
            InitializeComponent();
            ParentForm = f;
        }

        private Color blackColor = Color.Black;
        private Color warningColor = Color.FromArgb(255, 50, 50);

        private void company_Validating()
        {
            if (companyTextBox.Text == "")
            {
                companyLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        private void position_Validating()
        {
            if (positionTextBox.Text == "")
            {
                positionLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        private void hiringDate_Validating()
        {
            int hiringYear = hiringDateTimePicker.Value.Year;
            if (hiringYear < ParentForm.birthdayDateTimePicker.Value.Year || hiringYear > DateTime.Now.Year)
            {
                hiringDateLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        public Job job { get; set; } = null;
        bool isValid = false;

        private void jobButtonOK_Click(object sender, EventArgs e)
        {
            isValid = true;
            company_Validating();
            position_Validating();
            hiringDate_Validating();
            if (isValid)
            {
                ParentForm.jobButton.ForeColor = blackColor;
                string company = companyTextBox.Text;
                string position = positionTextBox.Text;
                string hiringDate = hiringDateTimePicker.Value.Date.ToShortDateString();
                job = new Job(company, position, hiringDate);
            }
            else
            {
                ParentForm.jobButton.ForeColor = warningColor;
            }
        }
    }
}
