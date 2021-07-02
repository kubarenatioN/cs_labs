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
    public partial class SearchConstructor : Form
    {
        private Form1 parentForm;
        public SearchConstructor()
        {
            InitializeComponent();
        }
        public SearchConstructor(Form1 parent)
        {
            InitializeComponent();
            parentForm = parent;
        }
        private List<SearchComboBoxItem> Options = new List<SearchComboBoxItem>()
        {
            new SearchComboBoxItem("Фамилия", "Surname", ComboBoxItemType.Letters),
            new SearchComboBoxItem("Имя", "Name", ComboBoxItemType.Letters),
            new SearchComboBoxItem("Год рождения", "Birthday", ComboBoxItemType.Digits),
            new SearchComboBoxItem("Курс", "Course", ComboBoxItemType.Digits),
            new SearchComboBoxItem("Специальность", "Specialization", ComboBoxItemType.Letters),
            new SearchComboBoxItem("Город", "City", ComboBoxItemType.Letters),
        };
        private Dictionary<string, string> searchConstruction = new Dictionary<string, string>();
        private void SearchConstructor_Load(object sender, EventArgs e)
        {
            optionsComboBox.Items.AddRange(Options.ToArray());
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            if (optionsComboBox.SelectedItem == null) return;

            SearchComboBoxItem selectedItem = (optionsComboBox.SelectedItem as SearchComboBoxItem);
            optionsComboBox.Items.Remove(selectedItem);

            Label label1 = new Label()
            {
                Location = new System.Drawing.Point(3, 11),
                Dock = DockStyle.Left,
                Text = "Критерий",
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            Label label2 = new Label()
            {
                Location = new System.Drawing.Point(124, 11),
                Dock = DockStyle.Left,
                Text = "Значение",
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            Label optionLabel = new Label()
            {
                Location = new System.Drawing.Point(3, 30),
                Dock = DockStyle.Left,
                Text = selectedItem.Text,
                Tag = selectedItem.Parameter,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            TextBox textBox = new TextBox()
            {
                Location = new System.Drawing.Point(127, 27),
                Dock = DockStyle.Fill,
                Name = "textBox1",
            };
            if(selectedItem.Type == ComboBoxItemType.Letters)
            {
                textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(parentForm.commonTextBox_KeyPress);
            }
            else textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(parentForm.digitsTextBox_KeyPress);

            TableLayoutPanel table = new TableLayoutPanel()
            {
                ColumnCount = 3,
                Dock = System.Windows.Forms.DockStyle.Top,
                RowCount = 2,
                Size = new System.Drawing.Size(294, 50),
                Padding = new Padding(3, 3, 3, 10),
            };

            Button deleteButton = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "X",
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 0),
                Padding = new Padding(0, 0, 0, 0)
            };
            deleteButton.Click += new EventHandler(returnIntoList);

            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));

            table.Controls.Add(textBox, 1, 1);
            table.Controls.Add(label1, 0, 0);
            table.Controls.Add(optionLabel, 0, 1);
            table.Controls.Add(label2, 1, 0);
            table.Controls.Add(deleteButton, 2, 0);
            table.SetRowSpan(deleteButton, 2);

            optionsFiller.Controls.Add(table);

        }

        private void returnIntoList(object sender, EventArgs e)
        {
            TableLayoutPanel t = (sender as Button).Parent as TableLayoutPanel;
            string tag = (string)(t.GetControlFromPosition(0, 1) as Label).Tag;

            optionsComboBox.Items.Add(Options.Find(item => item.Parameter == tag));
            (sender as Button).Parent.Dispose();
        }

        private void constructorButtonOK_Click(object sender, EventArgs e)
        {
            foreach (TableLayoutPanel table in optionsFiller.Controls)
            {
                string key = (string)(table.GetControlFromPosition(0, 1) as Label).Tag;
                string value = table.GetControlFromPosition(1, 1).Text;
                searchConstruction.Add(key, value);
            }
            parentForm.constructedQuery = searchConstruction;
        }
    }
}
