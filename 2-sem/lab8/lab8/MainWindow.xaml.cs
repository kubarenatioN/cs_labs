using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Configuration;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;

namespace lab8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        
        private SqlDataAdapter adapter;
        private DataSet studentsDataSet;
        private SqlCommandBuilder commandBuilder;
        private string connectionString = ConfigurationManager.ConnectionStrings["univerConnection"].ConnectionString;
        private DataTable studentsDataTable;
        public DataTable StudentsDataTable
        {
            get { return studentsDataTable; }
            set
            {
                if (studentsDataTable == value) return;
                studentsDataTable = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(StudentsDataTable)));
            }
        }
        private DataTable studProgressDataTable;
        public DataTable StudProgressDataTable
        {
            get { return studProgressDataTable; }
            set
            {
                if (studProgressDataTable == value) return;
                studProgressDataTable = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(StudProgressDataTable)));
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            //dataGrid.DataContext = StudProgressDataTable;
            studentsDataSet = new DataSet();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MergeTablesClick(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Создаем адаптер
                adapter = new SqlDataAdapter(GetPageData, connection);

                DataSet progressStudDataSet = new DataSet();

                // Fill dataSet with data
                adapter.Fill(progressStudDataSet);
                DataTable st = progressStudDataSet.Tables[0];
                //DataTable pt = studentsDataSet.Tables[1];

                //DataSet progressDataSet = new DataSet();
                //adapter2.Fill(progressDataSet, "Progress");
                //DataTable pt = progressDataSet.Tables[0];
                //Console.WriteLine(pt.Columns[1]);
                //var Join = (from s in st.AsEnumerable()
                //            from p in pt.AsEnumerable()
                //            where s.Field<int>(0) == p.Field<int>(0)
                //            select new {
                //                Name = s.Field<string>("Name"),
                //                Surname = s.Field<string>("Surname"),
                //                Birthday = s.Field<DateTime?>("Birthday"),
                //                Gender = s.Field<string>("Gender"),
                //                Course = p.Field<string>(1),
                //                Group = p.Field<string>(2)
                //            }).ToList();
                //studProgress.ItemsSource = Join;

                SqlCommand command = new SqlCommand("select Name, Surname, Fathername, Birthday, Gender, Course, [Group], AvgGrade" +
                    " from Students inner join Progress on Students.Id = Progress.StudentId", connection);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                //studProgress.ItemsSource = dt.DefaultView;
                StudProgressDataTable = dt;
            }
            
        }
        public ImageSource BytesToImage(byte[] imageData)
        {
            if (imageData == null) return null;
            using (var ms = new MemoryStream(imageData))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();

                ImageSource imgSrc = image as ImageSource;

                return imgSrc;
            }
        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Создаем адаптер
                adapter = new SqlDataAdapter(GetPageData, connection);

                if (studentsDataSet.Tables.Count > 0) studentsDataSet.Tables[0].Rows.Clear();
                // Fill dataSet with data
                adapter.Fill(studentsDataSet);

                var Query = (from row in studentsDataSet.Tables[0].AsEnumerable()
                             select new
                             {
                                 Id = row.Field<int>("Id"),
                                 Name = row.Field<string>("Name"),
                                 Surname = row.Field<string>("Surname"),
                                 Fathername = row.Field<string>("Fathername"),
                                 Birthday = row.Field<DateTime?>("Birthday"),
                                 Gender = row.Field<string>("Gender"),
                                 Photo = LoadImage(row.Field<byte[]>("Photo")) as BitmapImage

                             }).ToList();

                //foreach(var p in studentsDataSet.Tables[0].Columns["Photo"].)
                //{

                //}

                // get data from dataset
                StudentsDataTable = studentsDataSet.Tables[0];
                //dataGrid.ItemsSource = Query;
            }
        }
        private int curPage = 0;
        private int pageSize = 5;
        private string pageParameter = "Id";
        public string GetPageData
        {
            get
            {
                return $"select * from Students ORDER BY {pageParameter} OFFSET (({curPage}) * {pageSize}) ROWS FETCH NEXT {pageSize} ROWS ONLY";
            }
        }

        private void LoadDataClick(object sender, RoutedEventArgs e)
        {
            LoadData();
            dataGrid.Columns[0].IsReadOnly = true;
        }
        private void PrevDataGridPage(object sender, RoutedEventArgs e)
        {
            if (studentsDataSet.Tables.Count == 0) return;
            if (curPage == 0) return;
            curPage--;

            LoadData();
        }
        private void NextDataGridPage(object sender, RoutedEventArgs e)
        {
            if (studentsDataSet.Tables.Count == 0) return;
            if (studentsDataSet.Tables[0].Rows.Count < pageSize) return;
            curPage++;

            LoadData();
        }
        private void SelectDataClick(object sender, RoutedEventArgs e)
        {
            string query = "select * from Students";

            // Создание подключения
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = query;
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetValue(i) != DBNull.Value)
                            sb.AppendFormat("{0}-", Convert.ToString(reader.GetValue(i)));
                    }
                    Console.WriteLine(sb);
                }
                reader.Close();
            }
        }

        private void OpenInsertWindow(object sender, RoutedEventArgs e)
        {
            InsertWindow insertWindow = new InsertWindow(studentsDataSet);
            insertWindow.ShowDialog();
        }
        private void AddImageClick(object sender, RoutedEventArgs e)
        {
            AddImageWindow addImageWindow = new AddImageWindow(studentsDataSet);
            addImageWindow.ShowDialog();
        }
        
        private void InsertDataClick(object sender, RoutedEventArgs e)
        {
            if (studentsDataSet.Tables.Count == 0) return;
            DataRow row = studentsDataSet.Tables[0].NewRow();
            studentsDataSet.Tables[0].Rows.Add(row);
        }

        private void SaveDataClick(object sender, RoutedEventArgs e)
        {
            if (studentsDataSet.Tables.Count == 0) return;

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                adapter = new SqlDataAdapter(GetPageData, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("addStudent", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 15, "Name"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@surname", SqlDbType.NVarChar, 15, "Surname"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@fathername", SqlDbType.NVarChar, 20, "Fathername"));
                adapter.InsertCommand.Parameters.Add("@birthday", SqlDbType.DateTime).SourceColumn = "Birthday";
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@gender", SqlDbType.Char, 1, "Gender"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@photo", SqlDbType.VarBinary, 8000, "Photo"));

                SqlParameter param = adapter.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 0, "Id");
                param.Direction = ParameterDirection.Output;

                adapter.Update(studentsDataSet);
                studentsDataSet.AcceptChanges();
            }
        }

        private void AgeDescSortClick(object sender, RoutedEventArgs e)
        {
            if (studentsDataSet.Tables.Count == 0) return;

            pageParameter = "Birthday desc, Id";

            LoadData();
        }
        private void AgeAscSortClick(object sender, RoutedEventArgs e)
        {
            if (studentsDataSet.Tables.Count == 0) return;

            pageParameter = "Birthday, Id";

            LoadData();
        }
        private void NameSortClick(object sender, RoutedEventArgs e)
        {
            if (studentsDataSet.Tables.Count == 0) return;

            pageParameter = "Name";

            LoadData();
        }
        private void GenderSortClick(object sender, RoutedEventArgs e)
        {
            if (studentsDataSet.Tables.Count == 0) return;

            pageParameter = "Gender";

            LoadData();
        }

        private void DeleteRowClick(object sender, RoutedEventArgs e)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                while (dataGrid.SelectedItems.Count > 0)
                {
                    DataRowView dataRow = (DataRowView)dataGrid.SelectedItem; //dataRow holds the selection
                    int id = (int)dataRow.Row.ItemArray[0];
                    studentsDataSet.Tables[0].Rows.Remove(dataRow.Row);

                    string query = $"delete from Students where Id='{id}'";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    //studentsDataSet.Tables[0].Rows.Remove((dataGrid.SelectedItem as DataRowView).Row);

                }
            }
        }
        
    }
}
