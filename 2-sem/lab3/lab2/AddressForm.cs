using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class AddressForm : Form
    {
        public Form1 ParentForm;
        public AddressForm()
        {
            InitializeComponent();
        }
        public AddressForm(Form1 parentForm)
        {
            InitializeComponent();
            ParentForm = parentForm;
        }

        private Color warningColor = Color.FromArgb(255, 50, 50);
        private Color blackColor = Color.Black;
        private bool isValid = false;

        private void city_Validating()
        {
            if (cityTextBox.Text == "")
            {
                cityLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        private void street_Validating()
        {
            if (streetTextBox.Text == "")
            {
                streetLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        private void housingNumber_Validation()
        {
            if (houseRadioButton.Checked && houseTextBox.Text == "")
            {
                houseNumberLabel.ForeColor = warningColor;
                isValid = false;
            }
            else if (flatRadioButton.Checked && flatTextBox.Text == "")
            {
                flatNumberLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        private void index_Validating()
        {
            string indexReg = "^[0-9]{6}$";
            if (!Regex.IsMatch(houseIndexTextBox.Text, indexReg))
            {
                houseIndexLabel.ForeColor = warningColor;
                isValid = false;
            }
        }

        public Address studentAddress { get; set; } = null;

        private void buttonOK_Click(object sender, EventArgs e)
        {
            isValid = true;
            city_Validating();
            street_Validating();
            housingNumber_Validation();
            index_Validating();
            if (isValid)
            {
                ParentForm.addressInfo_Button.ForeColor = blackColor;
                string city = cityTextBox.Text;
                string street = streetTextBox.Text;
                HouseType houseType = houseRadioButton.Checked ? HouseType.house : HouseType.flat;
                string housingNumber = houseRadioButton.Checked ? houseTextBox.Text : flatTextBox.Text;
                string index = houseIndexTextBox.Text;
                studentAddress = new Address(city, street, houseType, housingNumber, index);
            }
            else
            {
                ParentForm.addressInfo_Button.ForeColor = warningColor;
            }
        }
    }
}
