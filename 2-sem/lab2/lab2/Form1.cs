using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

namespace lab2
{
    public partial class Form1 : Form
    {   // создать несколько форм
        public Form1()
        {
            InitializeComponent();
            addedListBox.DataSource = students;
            addedListBox.DisplayMember = "FullName";
            addedListBox.ValueMember = "Id";
        }

        private void commonTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || (Keys)e.KeyChar == Keys.Back);
        }

        private void digitsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || (Keys)e.KeyChar == Keys.Back);
        }

        private void floatTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || (Keys)e.KeyChar == Keys.Back || e.KeyChar == ',');
        }
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
            if(DateTime.Now.Year - birthdayYear != (Convert.ToInt32(ageTextBox.Text) + 1) && DateTime.Now.Year - birthdayYear != Convert.ToInt32(ageTextBox.Text))
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
            if(groupTextBox.Text == "")
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
            if(specComboBox.Text == "")
            {
                specLabel.ForeColor = warningColor;
                isValid = false;
            }
        }
        private void grade_Validating()
        {
            if(gradeTextBox.Text != "")
            {
                float grade = float.Parse(gradeTextBox.Text);
                if(grade < 1 || grade > 10)
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
        //private void city_Validating()
        //{
        //    if (cityTextBox.Text == "")
        //    {
        //        cityLabel.ForeColor = warningColor;
        //        isValid = false;
        //    }
        //}
        //private void street_Validating()
        //{
        //    if (streetTextBox.Text == "")
        //    {
        //        streetLabel.ForeColor = warningColor;
        //        isValid = false;
        //    }
        //}
        //private void housingNumber_Validation()
        //{
        //    if (houseRadioButton.Checked && houseTextBox.Text == "")
        //    {
        //        houseNumberLabel.ForeColor = warningColor;
        //        isValid = false;
        //    }
        //    else if(flatRadioButton.Checked && flatTextBox.Text == "") 
        //    {
        //        flatNumberLabel.ForeColor = warningColor;
        //        isValid = false;
        //    }
        //}
        //private void index_Validating()
        //{
        //    string indexReg = "[0-9]{6}";
        //    if (!Regex.IsMatch(houseIndexTextBox.Text, indexReg))
        //    {
        //        houseIndexLabel.ForeColor = warningColor;
        //        isValid = false;
        //    }
        //}
        //private void company_Validating()
        //{
        //    if (companyTextBox.Text == "")
        //    {
        //        companyLabel.ForeColor = warningColor;
        //        isValid = false;
        //    }
        //}
        //private void position_Validating()
        //{
        //    if (positionTextBox.Text == "")
        //    {
        //        positionLabel.ForeColor = warningColor;
        //        isValid = false;
        //    }
        //}
        //private void hiringDate_Validating()
        //{
        //    int hiringYear = hiringDateTimePicker.Value.Year;
        //    if(hiringYear < birthdayDateTimePicker.Value.Year || hiringYear > DateTime.Now.Year)
        //    {
        //        hiringDateLabel.ForeColor = warningColor;
        //        isValid = false;
        //    }
        //}
        private Color warningColor = Color.FromArgb(255, 50, 50);
        private Color blackColor = Color.Black;
        private bool isValid = false;
        private bool hasWork = false;
        //private List<Student> students = new List<Student>();
        //private BindingSource bindingStudents = new BindingSource();
        private BindingList<Student> students = new BindingList<Student>();
        private List<Student> deserializedStudents = new List<Student>();
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
            //city_Validating();
            //street_Validating();
            //housingNumber_Validation();
            //index_Validating();
            //if (hasWork)
            //{
            //    company_Validating();
            //    position_Validating();
            //    hiringDate_Validating();
            //}
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
                //string city = cityTextBox.Text;
                //string street = streetTextBox.Text;
                //HouseType houseType = houseRadioButton.Checked ? HouseType.house : HouseType.flat;
                //string housingNumber = houseRadioButton.Checked ? houseTextBox.Text : flatTextBox.Text;
                //string index = houseIndexTextBox.Text;
                //Address addr = new Address(city, street, houseType, housingNumber, index);
                Address addr = studentAddr;
                Job job = studentJob;
                //if (hasWork)
                //{
                //    string company = companyTextBox.Text;
                //    string position = positionTextBox.Text;
                //    string hiringDate = hiringDateTimePicker.Value.Date.ToShortDateString();
                //    job = new Job(company, position, hiringDate);
                //}
                if(hasWork && job.Company == null)
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
                foreach (var group in panel1.Controls)
                {
                    if(group is GroupBox)
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
        #region
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
            if(keyData == Keys.Delete && addedListBox.Focused)
            {
                //Guid selectedItemId = (addedListBox.SelectedItem as Student).Guid;
                for (int i = 0; i < students.Count; i++)
                {
                    if(students[i].Guid == (addedListBox.SelectedItem as Student).Guid)
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

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            deserializedStudents = XmlSerializationWrapper.Deserialize<List<Student>>("dstudents.xml", FileMode.OpenOrCreate);
            foreach (var student in deserializedStudents)
            {
                TreeNode studentNode = new TreeNode(student.FullName);

                TreeNode personalInfo = new TreeNode("Личная информация");
                personalInfo.Nodes.Add("Фамилия: " + student.Surname);
                personalInfo.Nodes.Add("Имя: " + student.Name);
                personalInfo.Nodes.Add("Отчество: " + student.Patronymic);
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

                treeView1.Nodes.Add(studentNode);
            }
        }

        private void addressInfo_Button_Click(object sender, EventArgs e)
        {
            AddressForm addrForm = new AddressForm(this);
            addrForm.ShowDialog();

            if (addrForm.DialogResult == DialogResult.OK)
            {
                studentAddr = addrForm.studentAddress;
                    // Display a message box indicating that the OK button was clicked.
                    //MessageBox.Show(studentAddr == null ? "null" : studentAddr.City);
                // Optional: Call the Dispose method when you are finished with the dialog box.
                addrForm.Dispose();
            }
            else
            {
                // Display a message box indicating that the Cancel button was clicked.
                //MessageBox.Show("Отмена");
                // Optional: Call the Dispose method when you are finished with the dialog box.
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
