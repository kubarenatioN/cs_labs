using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Bind addedListBox with student collection -> synchronization
            addedListBox.DataSource = students;
            addedListBox.DisplayMember = "FullName";
            addedListBox.ValueMember = "Id";
            savingStudents.CollectionChanged += (sender, e) => toolStripStatusLabel2.Text = savingStudents.Count.ToString();
            SetTimer();
        }

        internal void commonTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || (Keys)e.KeyChar == Keys.Back);
        }

        internal void digitsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || (Keys)e.KeyChar == Keys.Back);
        }

        private void floatTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || (Keys)e.KeyChar == Keys.Back || e.KeyChar == ',');
        }

        #region Validatings

        private void age_Validating()
        {
            if (!(int.TryParse(ageTextBox.Text, out int age) && age > 15 && age < 100))
            {
                ageLabel.ForeColor = warningColor;
                birthdayLabel.ForeColor = warningColor;
                isValid = false;
            }
            else
            {
                birthday_Validating();
            }
        }
        private void surname_Validating()
        {
            if (surnameTextBox.Text == "")
            {
                surnameLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        private void name_Validating()
        {
            if (nameTextBox.Text == "")
            {
                nameLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        private void patronymic_Validating()
        {
            if (patronymicTextBox.Text == "")
            {
                patroLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        private void birthday_Validating()
        {
            int birthdayYear = birthdayDateTimePicker.Value.Year;
            if (DateTime.Now.Year - birthdayYear != (Convert.ToInt32(ageTextBox.Text) + 1) && DateTime.Now.Year - birthdayYear != Convert.ToInt32(ageTextBox.Text))
            {
                birthdayLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        private void course_Validating()
        {
            if (courseComboBox.Text == "")
            {
                courseLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        private void group_Validating()
        {
            if (groupTextBox.Text == "")
            {
                groupLabel.ForeColor = warningColor;
                isValid = false;
            }
            else
            {
                short group = Convert.ToInt16(groupTextBox.Text);
                if (group < 1 || group > 13)
                {
                    groupLabel.ForeColor = warningColor;
                    isValid = false;
                }
            }
        }
        private void spec_Validating()
        {
            if (specComboBox.Text == "")
            {
                specLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        private void grade_Validating()
        {
            if (gradeTextBox.Text != "")
            {
                float grade = float.Parse(gradeTextBox.Text);
                if (grade < 1 || grade > 10)
                {
                    gradeLabel.ForeColor = warningColor;
                    isValid = false;
                }
            }
            else
            {
                gradeLabel.ForeColor = warningColor;
                isValid = false;
            }
        }

#endregion

        private Color warningColor = Color.FromArgb(255, 50, 50);
        private Color blackColor = Color.Black;
        private bool isValid = false;
        private bool hasWork = false;
        //private List<Student> students = new List<Student>();
        //private BindingSource bindingStudents = new BindingSource();
        private BindingList<Student> students = new BindingList<Student>();

        // From deserialization
        private List<Student> deserializedStudents = new List<Student>();

        // From searches + sortings to implement save option
        private ObservableCollection<Student> savingStudents = new ObservableCollection<Student>();

        private Address studentAddr = null;
        private Job studentJob = new Job();
        private void addButton_Click(object sender, EventArgs e)
        {
            isValid = true;
            surname_Validating();
            name_Validating();
            patronymic_Validating();
            age_Validating();
            //birthday_Validating(); It is inside age_Validating()
            course_Validating();
            group_Validating();
            spec_Validating();
            grade_Validating();
            if (studentAddr == null) addressInfo_Button.ForeColor = warningColor;
            if (studentJob.Company == null) jobButton.ForeColor = warningColor;
            if (isValid && studentAddr != null)
            {
                string surname = surnameTextBox.Text;
                string name = nameTextBox.Text;
                string patron = patronymicTextBox.Text;
                string age = ageTextBox.Text;
                Gender gender = manRadioButton.Checked ? Gender.мужской : Gender.женский;
                string birthday = birthdayDateTimePicker.Value.Date.ToShortDateString();
                string course = courseComboBox.Text;
                string group = groupTextBox.Text;
                string spec = specComboBox.Text;
                string grade = gradeTextBox.Text;
                Address addr = studentAddr;
                Job job = studentJob;
                if (hasWork && job.Company == null)
                {
                    return;
                }
                Student newStudent = new Student(Guid.NewGuid(), surname, name, patron, age, gender, birthday, course, group, spec, grade, addr, job);
                students.Add(newStudent);//Adding to binding collection + to added list
                studentJob = new Job();
                studentAddr = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (students.Count > 0)
            {
                XmlSerializationWrapper.Serialize(students, "students.xml", FileMode.Create);
                students.Clear();
                toolStripStatusLabel4.Text = "Сериализация";
                foreach (var group in panel1.Controls)
                {
                    if (group is GroupBox)
                    {
                        foreach (var component in (group as GroupBox).Controls)
                        {
                            if (component is TextBox)
                            {
                                (component as TextBox).Clear();
                            }
                        }
                    }
                }
            }
        }

        private void houseType_Changed(object sender, EventArgs e)
        {
            if (houseRadioButton.Checked)
            {
                houseTextBox.Enabled = true;
                flatTextBox.Enabled = false;
            }
            else
            {
                houseTextBox.Enabled = false;
                flatTextBox.Enabled = true;
            }
        }

        //FIELD VALIDATION
        #region FIELD VALIDATION
        //private void groupTextBox_Validating(object sender, CancelEventArgs e)
        //{
        //    if (short.TryParse(groupTextBox.Text, out short res) && res > 0 && res < 20)
        //    {
        //        e.Cancel = false;
        //    }
        //    else
        //    {
        //        groupTextBox.BackColor = Color.FromArgb(255, 128, 128);
        //        e.Cancel = true;
        //    }
        //}

        //private void groupTextBox_Validated(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Validated");
        //}
        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Delete && addedListBox.Focused)
            {
                //Guid selectedItemId = (addedListBox.SelectedItem as Student).Guid;
                for (int i = 0; i < students.Count; i++)
                {
                    if (students[i].Guid == (addedListBox.SelectedItem as Student).Guid)
                    {
                        students.Remove(students[i]);
                    }
                }
                return true;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }

        #region Field Style Refresh

        private void surnameTextBox_Enter(object sender, EventArgs e)
        {
            surnameLabel.ForeColor = blackColor;
        }
        private void nameTextBox_Enter(object sender, EventArgs e)
        {
            nameLabel.ForeColor = blackColor;
        }
        private void patronymicTextBox_Enter(object sender, EventArgs e)
        {
            patroLabel.ForeColor = blackColor;
        }
        private void ageTextBox_Enter(object sender, EventArgs e)
        {
            ageLabel.ForeColor = blackColor;
        }
        private void birthdayDateTimePicker_Enter(object sender, EventArgs e)
        {
            birthdayLabel.ForeColor = blackColor;
        }
        private void courseComboBox_Enter(object sender, EventArgs e)
        {
            courseLabel.ForeColor = blackColor;
        }
        private void groupTextBox_Enter(object sender, EventArgs e)
        {
            groupLabel.ForeColor = blackColor;
        }
        private void specComboBox_Enter(object sender, EventArgs e)
        {
            specLabel.ForeColor = blackColor;
        }
        private void gradeTextBox_Enter(object sender, EventArgs e)
        {
            gradeLabel.ForeColor = blackColor;
        }

        private void cityTextBox_Enter(object sender, EventArgs e)
        {
            cityLabel.ForeColor = blackColor;
        }

        private void streetTextBox_Enter(object sender, EventArgs e)
        {
            streetLabel.ForeColor = blackColor;
        }

        private void houseTextBox_Enter(object sender, EventArgs e)
        {
            houseNumberLabel.ForeColor = blackColor;
            flatNumberLabel.ForeColor = blackColor;
        }

        private void flatTextBox_Enter(object sender, EventArgs e)
        {
            flatNumberLabel.ForeColor = blackColor;
            houseNumberLabel.ForeColor = blackColor;
        }

        private void houseIndexTextBox_Enter(object sender, EventArgs e)
        {
            houseIndexLabel.ForeColor = blackColor;
        }

        private void companyTextBox_Enter(object sender, EventArgs e)
        {
            companyLabel.ForeColor = blackColor;
        }

        private void positionTextBox_Enter(object sender, EventArgs e)
        {
            positionLabel.ForeColor = blackColor;
        }

        private void hiringDateTimePicker_Enter(object sender, EventArgs e)
        {
            hiringDateLabel.ForeColor = blackColor;
        }

        #endregion

        private void hasWorkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (hasWorkCheckBox.Checked)
            {
                hasWork = true;
                jobButton.Enabled = true;
            }
            else
            {
                hasWork = false;
                jobButton.Enabled = false;
            }
        }

        private void CreateNode(Student student, TreeView Tree)
        {
            TreeNode studentNode = new TreeNode(student.FullName);

            TreeNode personalInfo = new TreeNode("Личная информация");
            personalInfo.Nodes.Add("surname", "Фамилия: " + student.Surname);
            personalInfo.Nodes.Add("name", "Имя: " + student.Name);
            personalInfo.Nodes.Add("patron", "Отчество: " + student.Patronymic);
            personalInfo.Nodes.Add("Возраст: " + student.Age);
            personalInfo.Nodes.Add("Пол: " + student.Gender.ToString());
            personalInfo.Nodes.Add("Дата рождения: " + student.Birthday);
            studentNode.Nodes.Add(personalInfo);

            TreeNode educationInfo = new TreeNode("Образование");
            educationInfo.Nodes.Add("Курс: " + student.Course);
            educationInfo.Nodes.Add("Группа: " + student.Group);
            educationInfo.Nodes.Add("Специальность: " + student.Specialization);
            educationInfo.Nodes.Add("Средний балл: " + student.Grade);
            studentNode.Nodes.Add(educationInfo);

            TreeNode addressInfo = new TreeNode("Адрес");
            addressInfo.Nodes.Add("Город: " + student.Address.City);
            addressInfo.Nodes.Add("Улица: " + student.Address.Street);
            addressInfo.Nodes.Add("Тип жилища: " + student.Address.HouseType);
            addressInfo.Nodes.Add("Номер жилища: " + student.Address.HouseNumber);
            addressInfo.Nodes.Add("Индекс: " + student.Address.Index);
            studentNode.Nodes.Add(addressInfo);

            if (student.Job.Company != null)
            {
                TreeNode jobInfo = new TreeNode("Работа");
                jobInfo.Nodes.Add("Компания: " + student.Job.Company);
                jobInfo.Nodes.Add("Должность: " + student.Job.Position);
                jobInfo.Nodes.Add("Дата найма: " + student.Job.HiringDate);
                studentNode.Nodes.Add(jobInfo);
            }

            Tree.Nodes.Add(studentNode);
        }

        //private Student CreateCollectionFromTree(TreeView tree)
        //{
        //    List<Student> studentsFromTree = new List<Student>();
        //    foreach (TreeNode student in tree.Nodes)
        //    {
        //        TreeNode personalInfo = student.Nodes[0];
        //        string surname = personalInfo.Nodes[0].Text;
        //        string name = nameTextBox.Text;
        //        string patron = patronymicTextBox.Text;
        //        string age = ageTextBox.Text;
        //        Gender gender = manRadioButton.Checked ? Gender.мужской : Gender.женский;
        //        string birthday = birthdayDateTimePicker.Value.Date.ToShortDateString();
        //        string course = courseComboBox.Text;
        //        string group = groupTextBox.Text;
        //        string spec = specComboBox.Text;
        //        string grade = gradeTextBox.Text;
        //        string city = cityTextBox.Text;
        //        string street = streetTextBox.Text;
        //        HouseType houseType = houseRadioButton.Checked ? HouseType.house : HouseType.flat;
        //        string housingNumber = houseRadioButton.Checked ? houseTextBox.Text : flatTextBox.Text;
        //        string index = houseIndexTextBox.Text;
        //        Address addr = new Address(city, street, houseType, housingNumber, index);
        //        Job job = new Job();
        //        if (hasWork)
        //        {
        //            string company = companyTextBox.Text;
        //            string position = positionTextBox.Text;
        //            string hiringDate = hiringDateTimePicker.Value.Date.ToShortDateString();
        //            job = new Job(company, position, hiringDate);
        //        }
        //        Student newStudent = new Student(Guid.NewGuid(), surname, name, patron, age, gender, birthday, course, group, spec, grade, addr, job);
        //        studentsFromTree.Add(newStudent);//Adding to binding collection + to added list
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            deserializedStudents = XmlSerializationWrapper.Deserialize<List<Student>>("dstudents.xml", FileMode.OpenOrCreate);
            foreach (var student in deserializedStudents)
            {
                CreateNode(student, treeView1);
            }
            toolStripStatusLabel4.Text = "Десериализация";
        }

        private void addressInfo_Button_Click(object sender, EventArgs e)
        {
            AddressForm addrForm = new AddressForm(this);
            addrForm.ShowDialog();

            if (addrForm.DialogResult == DialogResult.OK)
            {
                studentAddr = addrForm.studentAddress;
                addrForm.Dispose();
            }
            else
            {
                addrForm.Dispose();
            }
        }

        private void jobButton_Click(object sender, EventArgs e)
        {
            JobForm jobForm = new JobForm(this);
            jobForm.ShowDialog();

            if (jobForm.DialogResult == DialogResult.OK)
            {
                studentJob = jobForm.job;
                jobForm.Dispose();
            }
            else
            {
                jobForm.Dispose();
            }
        }

        #region SEARCH

        enum searchType
        {
            FIO,
            Spec,
            Course,
            Grade
        }
        List<Student> searchResults = new List<Student>();
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
            {
                (sender as ToolStripMenuItem).Checked = false;
                queryTextBox.TextChanged -= FIOQueryTextBox_TextChanged;
                queryTextBox.TextChanged -= specQueryTextBox_TextChanged;
                queryTextBox.TextChanged -= courseQueryTextBox_TextChanged;
                queryTextBox.TextChanged -= gradeQueryTextBox_TextChanged;
                searchResults.Clear();
                treeView2.Nodes.Clear();
                queryTextBox.Clear();
            }
            else
            {
                foreach (ToolStripMenuItem item in поискToolStripMenuItem.DropDown.Items)
                {
                    item.Checked = false;
                }
                if (deserializedStudents.Count > 0)
                {
                    (sender as ToolStripMenuItem).Checked = true;
                    searchOptions();
                    toolStripStatusLabel4.Text = "Поиск";
                }
            }
        }
        private void searchOptions()
        {
            int type = -1;
            for (int i = 0; i < поискToolStripMenuItem.DropDown.Items.Count; i++)
            {
                if ((поискToolStripMenuItem.DropDown.Items[i] as ToolStripMenuItem).Checked)
                {
                    type = i;
                    break;
                }
            }
            queryTextBox.TextChanged -= FIOQueryTextBox_TextChanged;
            queryTextBox.TextChanged -= specQueryTextBox_TextChanged;
            queryTextBox.TextChanged -= courseQueryTextBox_TextChanged;
            queryTextBox.TextChanged -= gradeQueryTextBox_TextChanged;
            switch (type)
            {
                case (int)searchType.FIO:
                    queryTextBox.TextChanged += FIOQueryTextBox_TextChanged;
                    break;
                case (int)searchType.Spec:
                    queryTextBox.TextChanged += specQueryTextBox_TextChanged;
                    break;
                case (int)searchType.Course:
                    queryTextBox.TextChanged += courseQueryTextBox_TextChanged;
                    break;
                case (int)searchType.Grade:
                    queryTextBox.TextChanged += gradeQueryTextBox_TextChanged;
                    break;
                default:
                    break;
            }
        }

        [NameValidation]
        [Required(ErrorMessage = "Обязательно к заполнению", AllowEmptyStrings = true)]
        [RegularExpression(@"^([А-Яа-я]|\s)+$", ErrorMessage = "Неверный формат")]
        public string FIOQuery { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению", AllowEmptyStrings = true)]
        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Неверный формат")]
        public string SpecQuery { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению", AllowEmptyStrings = true)]
        [StringLength(1, ErrorMessage = "Превышено значение")]
        [Range(1, 4, ErrorMessage = "Выход за диапазон")]
        public string CourseQuery { get; set; }


        [Required(ErrorMessage = "Обязательно к заполнению")]
        [StringLength(2, ErrorMessage = "Превышено значение")]
        [Range(1, 10, ErrorMessage = "Выход за диапазон")]
        public string GradeQuery { get; set; }

        private void FIOQueryTextBox_TextChanged(object sender, EventArgs e)
        {
            //var validationResult = new List<ValidationResult>();
            //var validationContext = new ValidationContext(this)
            //{
            //    MemberName = "FIOQuery"
            //};
            //if (Validator.TryValidateProperty(queryTextBox.Text, validationContext, validationResult))
            //{
            //    FIOQuery = queryTextBox.Text;
            //    errorLabel.Text = string.Empty;
            //}
            //else
            //{
            //    string errMessage = "";
            //    foreach (var err in validationResult)
            //    {
            //        errMessage += err.ErrorMessage;
            //        errMessage += '\n';
            //    }
            //    errorLabel.Text = errMessage;
            //    return;
            //}
            if (deserializedStudents.Count > 0)
            {
                //Validating query text
                if (Validations.TryValidate(queryTextBox.Text, new ValidationContext(this), "FIOQuery", errorLabel)) return;
                else FIOQuery = Validations.validResult;

                if (поВозрастаниюToolStripMenuItem.Checked || поУбываниюToolStripMenuItem.Checked) courseSort(курсToolStripMenuItem1, new EventArgs());
                searchResults.Clear();
                treeView2.Nodes.Clear();
                int numberOfSpaces = queryTextBox.Text.ToCharArray().Count(x => x == ' ');
                int numberOfChars = FIOQuery.Length;
                if (numberOfSpaces == 0 && queryTextBox.Text != "")
                {
                    foreach (Student student in deserializedStudents)
                    {
                        string surname = student.Surname;
                        string name = student.Name;
                        string patron = student.Patronymic;
                        if (Regex.IsMatch(FIOQuery, surname.SubstringOrFull(numberOfChars), RegexOptions.IgnoreCase) ||
                           Regex.IsMatch(FIOQuery, name.SubstringOrFull(numberOfChars), RegexOptions.IgnoreCase) ||
                           Regex.IsMatch(FIOQuery, patron.SubstringOrFull(numberOfChars), RegexOptions.IgnoreCase))
                        {
                            searchResults.Add(student);
                            CreateNode(student, treeView2);
                        }
                    }
                }
                else if (numberOfSpaces == 1)
                {
                    foreach (Student student in deserializedStudents)
                    {
                        string surname = student.Surname;
                        string name = student.Name;
                        if (Regex.IsMatch(FIOQuery, (surname + ' ' + name).SubstringOrFull(numberOfChars), RegexOptions.IgnoreCase) ||
                            Regex.IsMatch(FIOQuery, (name + ' ' + surname).SubstringOrFull(numberOfChars), RegexOptions.IgnoreCase))
                        {
                            searchResults.Add(student);
                            CreateNode(student, treeView2);
                        }
                    }
                }
                else if (numberOfSpaces == 2)
                {
                    foreach (Student student in deserializedStudents)
                    {
                        string surname = student.Surname;
                        string name = student.Name;
                        string patron = student.Patronymic;
                        if (Regex.IsMatch(FIOQuery, (surname + ' ' + name + ' ' + patron).SubstringOrFull(numberOfChars), RegexOptions.IgnoreCase) ||
                            Regex.IsMatch(FIOQuery, (name + ' ' + patron + ' ' + surname).SubstringOrFull(numberOfChars), RegexOptions.IgnoreCase))
                        {
                            searchResults.Add(student);
                            CreateNode(student, treeView2);
                        }
                    }
                }
                savingStudents.Clear();
                foreach (Student s in searchResults)
                {
                    savingStudents.Add(s);
                }
            }
        }
        private void specQueryTextBox_TextChanged(object sender, EventArgs e)
        {
            if(deserializedStudents.Count > 0)
            {
                if (Validations.TryValidate(queryTextBox.Text, new ValidationContext(this), "SpecQuery", errorLabel)) return;
                else SpecQuery = Validations.validResult;

                var query = deserializedStudents.Where(s => s.Specialization.SubstringOrFull(SpecQuery.Length) == SpecQuery.ToUpper());
                searchResults.Clear();
                treeView2.Nodes.Clear();
                foreach (var student in query)
                {
                    searchResults.Add(student);
                    CreateNode(student, treeView2);
                }
                savingStudents.Clear();
                foreach (Student s in searchResults)
                {
                    savingStudents.Add(s);
                }
            }
        }
        private void courseQueryTextBox_TextChanged(object sender, EventArgs e)
        {
            if (deserializedStudents.Count > 0)
            {
                if (Validations.TryValidate(queryTextBox.Text, new ValidationContext(this), "CourseQuery", errorLabel)) return;
                else CourseQuery = Validations.validResult;

                var query = deserializedStudents.Where(student => student.Course == CourseQuery);
                searchResults.Clear();
                savingStudents.Clear();
                treeView2.Nodes.Clear();
                foreach (var student in query)
                {
                    searchResults.Add(student);
                    CreateNode(student, treeView2);
                }
                savingStudents.Clear();
                foreach (Student s in searchResults)
                {
                    savingStudents.Add(s);
                }
            }
        }
        private void gradeQueryTextBox_TextChanged(object sender, EventArgs e)
        {
            if (deserializedStudents.Count > 0)
            {
                if (Validations.TryValidate(queryTextBox.Text, new ValidationContext(this), "GradeQuery", errorLabel)) return;
                else GradeQuery = Validations.validResult;

                // take all students with specific marks
                var query = deserializedStudents.Where(student =>
                Math.Round(double.Parse(student.Grade)) == double.Parse(GradeQuery));

                searchResults.Clear();
                savingStudents.Clear();
                treeView2.Nodes.Clear();
                foreach (var student in query)
                {
                    searchResults.Add(student);
                    CreateNode(student, treeView2);
                }
                savingStudents.Clear();
                foreach (Student s in searchResults)
                {
                    savingStudents.Add(s);
                }
            }
        }

        #endregion

        #region Sortings

        private void sortItem_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
            {
                (sender as ToolStripMenuItem).Checked = false;
            }
            else
            {
                foreach (ToolStripMenuItem type in сортировкаToolStripMenuItem.DropDown.Items)
                {
                    foreach (ToolStripMenuItem item in type.DropDown.Items)
                    {
                        item.Checked = false;
                    }
                }
                if (deserializedStudents.Count > 0)
                {
                    (sender as ToolStripMenuItem).Checked = true;
                    toolStripStatusLabel4.Text = "Сортировка";
                }
            }
        }

        private void courseSort(object sender, EventArgs e)
        {
            if(deserializedStudents.Count > 0 && (поУбываниюToolStripMenuItem.Checked || поВозрастаниюToolStripMenuItem.Checked))
            {
                // if before sorting, search was used and we have search results in treeView2
                if(searchResults.Count > 0)
                {
                    // Clear treeView2 and display sorted search results
                    treeView2.Nodes.Clear();
                    if (поУбываниюToolStripMenuItem.Checked) searchResults.Sort(Sortings.CourseByDescending);
                    else searchResults.Sort(Sortings.CourseByAscending);
                    foreach (Student s in searchResults)
                    {
                        CreateNode(s, treeView2);
                    }
                    savingStudents.Clear();
                    foreach (Student s in searchResults)
                    {
                        savingStudents.Add(s);
                    }
                }
                // if before sorting we dont use search -> searchResults is empty
                // we use Linq to XML
                else
                {
                    treeView2.Nodes.Clear();
                    XDocument xmlDoc = XDocument.Load("dstudents.xml");

                    var query = xmlDoc.Element("ArrayOfStudent").Elements("Student")
                        .Select(student => new Student
                        {
                            Guid = Guid.Parse(student.Element("Guid").Value),
                            Name = student.Element("Name").Value,
                            Surname = student.Element("Surname").Value,
                            Patronymic = student.Element("Patronymic").Value,
                            Age = student.Element("Age").Value,
                            Gender = (Gender)Enum.Parse(typeof(Gender), student.Element("Gender").Value),
                            Birthday = student.Element("Birthday").Value,
                            Course = student.Element("Course").Value,
                            Group = student.Element("Group").Value,
                            Specialization = student.Element("Specialization").Value,
                            Grade = student.Element("Grade").Value,
                            //Address = new Address
                            //{
                            //    City = (string)student.Element("Address").Element("City").Value,
                            //    Street = student.Element("Address").Element("Street").Value,
                            //    HouseType = (HouseType)Enum.Parse(typeof(HouseType), student.Element("Address").Element("HouseType").Value),
                            //    HouseNumber = student.Element("Address").Element("HouseNumber").Value,
                            //    Index = student.Element("Address").Element("Index").Value
                            //}
                            Address = student.Elements("Address").Select(addr => new Address
                            {
                                City = addr.Element("City").Value,
                                Street = addr.Element("Street").Value,
                                HouseType = (HouseType)Enum.Parse(typeof(HouseType), addr.Element("HouseType").Value),
                                HouseNumber = addr.Element("HouseNumber").Value,
                                Index = addr.Element("Index").Value
                            }).First(),
                            Job = student.Element("Job").IsEmpty ? new Job() : student.Elements("Job").Select(job => new Job
                            {
                                Company = job.Element("Company").Value,
                                Position = job.Element("Position").Value,
                                HiringDate = job.Element("HiringDate").Value
                            }).SingleOrDefault()
                        });

                    if (поУбываниюToolStripMenuItem.Checked)
                    {
                        query = query.OrderByDescending(student => student.Course);
                    }
                    else if(поВозрастаниюToolStripMenuItem.Checked)
                    {
                        query = query.OrderBy(student => student.Course);
                    }

                    savingStudents.Clear();
                    foreach (Student s in query)
                    {
                        savingStudents.Add(s);
                        CreateNode(s, treeView2);
                    }
                }
            }
        }

        private void hiringSort(object sender, EventArgs e)
        {
            if(deserializedStudents.Count > 0 && (поУбываниюToolStripMenuItem1.Checked || поВозрастаниюToolStripMenuItem1.Checked))
            {
                // Clear treeView2 and display sorted search results
                if (searchResults.Count > 0)
                {
                    // Clear treeView2 and display sorted search results
                    treeView2.Nodes.Clear();

                    //Delete all student, who dont work from searchResults collection
                    searchResults.RemoveAll(s => s.Job.Company == null);

                    if (поУбываниюToolStripMenuItem1.Checked) searchResults.Sort(Sortings.HiringTimeByDescending);
                    else searchResults.Sort(Sortings.HiringTimeByAscending);
                    foreach (Student s in searchResults)
                    {
                        CreateNode(s, treeView2);
                    }
                    savingStudents.Clear();
                    foreach (Student s in searchResults)
                    {
                        savingStudents.Add(s);
                    }
                }
                else
                {
                    treeView2.Nodes.Clear();
                    XDocument xmlDoc = XDocument.Load("dstudents.xml");

                    var query = xmlDoc.Element("ArrayOfStudent").Elements("Student")
                        .Select(student => new Student
                        {
                            Guid = Guid.Parse(student.Element("Guid").Value),
                            Name = student.Element("Name").Value,
                            Surname = student.Element("Surname").Value,
                            Patronymic = student.Element("Patronymic").Value,
                            Age = student.Element("Age").Value,
                            Gender = (Gender)Enum.Parse(typeof(Gender), student.Element("Gender").Value),
                            Birthday = student.Element("Birthday").Value,
                            Course = student.Element("Course").Value,
                            Group = student.Element("Group").Value,
                            Specialization = student.Element("Specialization").Value,
                            Grade = student.Element("Grade").Value,
                            //Address = new Address
                            //{
                            //    City = (string)student.Element("Address").Element("City").Value,
                            //    Street = student.Element("Address").Element("Street").Value,
                            //    HouseType = (HouseType)Enum.Parse(typeof(HouseType), student.Element("Address").Element("HouseType").Value),
                            //    HouseNumber = student.Element("Address").Element("HouseNumber").Value,
                            //    Index = student.Element("Address").Element("Index").Value
                            //}
                            Address = student.Elements("Address").Select(addr => new Address
                            {
                                City = addr.Element("City").Value,
                                Street = addr.Element("Street").Value,
                                HouseType = (HouseType)Enum.Parse(typeof(HouseType), addr.Element("HouseType").Value),
                                HouseNumber = addr.Element("HouseNumber").Value,
                                Index = addr.Element("Index").Value
                            }).First(),
                            Job = student.Element("Job").IsEmpty ? new Job() : student.Elements("Job").Select(job => new Job
                            {
                                Company = job.Element("Company").Value,
                                Position = job.Element("Position").Value,
                                HiringDate = job.Element("HiringDate").Value
                            }).SingleOrDefault()
                        });

                    // Deleting jobless students from query
                    List<Student> queryList = query.ToList();
                    foreach (Student s in queryList.ToArray())
                    {
                        if (s.Job.Company == null) queryList.Remove(s);
                    }

                    // Sort query by HiringDate
                    if (поУбываниюToolStripMenuItem1.Checked)
                    {
                        query = queryList.OrderBy(student => DateTime.Parse(student.Job.HiringDate).Year);
                    }
                    else if (поВозрастаниюToolStripMenuItem1.Checked)
                    {
                        query = queryList.OrderByDescending(student => DateTime.Parse(student.Job.HiringDate).Year);
                    }

                    savingStudents.Clear();
                    foreach (Student s in query)
                    {
                        savingStudents.Add(s);
                        CreateNode(s, treeView2);
                    }
                }
            }
        }

        #endregion

        // Clearing results, query and treeView
        private void clearTreeButton_Click(object sender, EventArgs e)
        {
            treeView2.Nodes.Clear();
            searchResults.Clear();
            savingStudents.Clear();
            queryTextBox.Clear();
            errorLabel.Text = string.Empty;
            foreach (ToolStripMenuItem item in поискToolStripMenuItem.DropDown.Items)
            {
                item.Checked = false;
            }
            foreach (ToolStripMenuItem item in сортировкаToolStripMenuItem.DropDown.Items)
            {
                item.Checked = false;
            }
            toolStripStatusLabel4.Text = "Очистка";
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(treeView2.Nodes.Count > 0)
            {
                XmlSerializationWrapper.Serialize(savingStudents, "saved-students.xml", FileMode.Create);
                toolStripStatusLabel4.Text = "Сохранение";
            }
        }
        
        // date and time
        private void SetTimer()
        {
            TimerCallback tm = new TimerCallback(CheckDateTime);
            // создаем таймер
            System.Threading.Timer timer = new System.Threading.Timer(tm, null, 0, 1000);
        }
        public void CheckDateTime(object o)
        {
            toolStripStatusLabel6.Text = DateTime.Now.ToLocalTime().ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //SetTimer();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developed by: Kubarko Nikita\n11.01.21 — 24.01.21");
        }

        internal Dictionary<string, string> constructedQuery = new Dictionary<string, string>();
        private void конструкторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if((sender as ToolStripMenuItem).Checked)
            {
                using (SearchConstructor SearchConstructorForm = new SearchConstructor(this))
                {
                    SearchConstructorForm.ShowDialog();

                    if (SearchConstructorForm.DialogResult == DialogResult.OK)
                    {
                        searchResults.Clear();
                        savingStudents.Clear();
                        treeView2.Nodes.Clear();
                        foreach (Student s in deserializedStudents)
                        {
                            bool flag = true;
                            foreach (var item in constructedQuery)
                            {
                                Type t = s.GetType();//Get reflecting type
                                string propertyValue = null;
                                if (item.Key == "Birthday") propertyValue = Convert.ToDateTime(t.GetField(item.Key).GetValue(s)).Year.ToString();
                                else if (item.Key == "City") propertyValue = (string)s.Address.GetType().GetField(item.Key).GetValue(s.Address);
                                else propertyValue = (string)t.GetField(item.Key).GetValue(s);

                                if (propertyValue != item.Value) flag = false;
                            }
                            if (flag)
                            {
                                searchResults.Add(s);
                                savingStudents.Add(s);
                                CreateNode(s, treeView2);
                            }
                        }
                    }
                    конструкторToolStripMenuItem.Checked = false;
                }
            }
        }
    }

    // Custom validation attribute
    public class NameValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Regex.IsMatch(value.ToString(), "^[ыъьйщшц]", RegexOptions.IgnoreCase))
            {
                ErrorMessage = "Некорректное имя";
                return false;
            }
            return true;
        }
    }

    // Validation wrapper class
    public static class Validations
    {
        public static List<ValidationResult> validationResult;
        public static ValidationContext validationContext;
        public static string validResult;
        public static bool TryValidate(string query, ValidationContext vCntx, string propName, Label errLabel)
        {
            validationResult = new List<ValidationResult>();
            vCntx.MemberName = propName;
            if (Validator.TryValidateProperty(query, vCntx, validationResult))
            {
                validResult = query;
                errLabel.Text = string.Empty;
                return false;
            }
            else
            {
                string errMessage = "";
                foreach (var err in validationResult)
                {
                    errMessage += err.ErrorMessage;
                    errMessage += '\n';
                }
                errLabel.Text = errMessage;
                return true;
            }
        }
    }

    // Define static class with sort methods
    public static class Sortings
    {
        public static int CourseByAscending(Student s1, Student s2)
        {
            if (int.Parse(s1.Course) > int.Parse(s2.Course))
            {
                return 1;
            }
            else if (int.Parse(s1.Course) == int.Parse(s2.Course))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public static int CourseByDescending(Student s1, Student s2)
        {
            if (int.Parse(s1.Course) > int.Parse(s2.Course))
            {
                return -1;
            }
            else if (int.Parse(s1.Course) == int.Parse(s2.Course))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        public static int HiringTimeByAscending(Student s1, Student s2)
        {
            if(DateTime.Parse(s1.Job.HiringDate).Year > DateTime.Parse(s2.Job.HiringDate).Year)
            {
                return -1;
            }
            else if(DateTime.Parse(s1.Job.HiringDate).Year == DateTime.Parse(s2.Job.HiringDate).Year)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        public static int HiringTimeByDescending(Student s1, Student s2)
        {
            if (DateTime.Parse(s1.Job.HiringDate).Year > DateTime.Parse(s2.Job.HiringDate).Year)
            {
                return 1;
            }
            else if (DateTime.Parse(s1.Job.HiringDate).Year == DateTime.Parse(s2.Job.HiringDate).Year)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }

    public static class StringExtensions
    {
        public static string SubstringOrFull(this string str, int len)
        {
            if(len > str.Length)
            {
                return str;
            }
            else
            {
                return str.Substring(0, len);
            }
        }
    }

    public enum Gender
    {
        мужской,
        женский
    }
    public enum HouseType
    {
        flat,
        house
    }
    public class Address
    {
        public string City;
        public string Street;
        public HouseType HouseType;
        public string HouseNumber;
        public string Index;
        public Address(string city, string street, HouseType houseType, string houseNumber, string index)
        {
            City = city;
            Street = street;
            HouseType = houseType;
            HouseNumber = houseNumber;
            Index = index;
        }
        public Address() { }
    }
    public class Job
    {
        public string Company;
        public string Position;
        public string HiringDate;
        public Job(string company, string pos, string hiringDate)
        {
            Company = company;
            Position = pos;
            HiringDate = hiringDate;
        }
        public Job() { }
    }
    public class Student 
    {
        public Guid Guid;
        public Guid Id
        {
            get
            {
                return Guid;
            }
        }
        public string Name;
        public string Surname;
        public string FullName
        {
            get
            {
                return Surname + " " + Name;
            }
        }
        public string Patronymic;
        public string Age;
        public Gender Gender;
        public string Birthday;
        public string Course;
        public string Group;
        public string Specialization;
        public string Grade;
        public Address Address;
        public Job Job;
        public Student(Guid guid, string surname, string name, string patro, string age, Gender gender, string birthday, string course, string group, string spec, string grade, Address addr, Job job)
        {
            Guid = guid;
            Surname = surname;
            Name = name;
            Patronymic = patro;
            Age = age;
            Gender = gender;
            Birthday = birthday;
            Course = course;
            Group = group;
            Specialization = spec;
            Grade = grade;
            Address = addr;
            Job = job;
        }
        public override string ToString()
        {
            return $"Имя: {Name}" +
                $"\nФамилия: {Surname}" +
                $"\nОтчество: {Patronymic}" +
                $"\nПол: {Gender}" +
                $"\nВозраст: {Age}" +
                $"\nДата рождения: {Birthday}" +
                $"\nКурс: {Course}" +
                $"\nГруппа: {Group}" +
                $"\nСпециальность: {Specialization}" +
                $"\nОценка: {Grade}" +
                $"\nАдрес: {Address}" +
                $"\nМесто работы: {Job}";
            
        }
        public Student() { }
    }
    public static class XmlSerializationWrapper
    {
        public static void Serialize<T>(T obj, string filename, FileMode filemode)
        {
            XmlSerializer Serializer = new XmlSerializer(typeof(T));

            using (FileStream serfs = new FileStream(filename, filemode))
            {
                Serializer.Serialize(serfs, obj);
            }
        }
        public static T Deserialize<T>(string filename, FileMode filemode)
        {
            T deserializedCollection;
            XmlSerializer Deserializer = new XmlSerializer(typeof(T));

            using(FileStream deserfs = new FileStream(filename, filemode))
            {
                deserializedCollection = (T)Deserializer.Deserialize(deserfs);
            }
            return deserializedCollection;
        }
    }
}
