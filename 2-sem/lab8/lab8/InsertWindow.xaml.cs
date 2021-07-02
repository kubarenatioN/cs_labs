using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab8
{
    /// <summary>
    /// Логика взаимодействия для InsertWindow.xaml
    /// </summary>
    public partial class InsertWindow : Window, INotifyPropertyChanged
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["univerConnection"].ConnectionString;
        DataSet studentsDataSet;
        public InsertWindow(DataSet ds)
        {
            InitializeComponent();
            studentsDataSet = ds;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InsertDataClick(object sender, RoutedEventArgs e)
        {

            string query = "addStudent";

            // Создание подключения
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlTransaction tran = connection.BeginTransaction();

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = tran;

                try
                {
                    command.Parameters.Add(new SqlParameter("@name", name.Text));
                    command.Parameters.Add(new SqlParameter("@surname", surname.Text));
                    command.Parameters.Add(new SqlParameter("@fathername", fathername.Text));
                    command.Parameters.Add("@birthday", SqlDbType.Date).Value = birthday.Text == "" ? null : birthday.Text;
                    command.Parameters.Add(new SqlParameter("@gender", gender.Text));
                    command.Parameters.Add(new SqlParameter("@photo", Encoding.ASCII.GetBytes(photo.Text)));

                    SqlParameter parameter = command.Parameters.Add("@id", SqlDbType.Int, 0, "Id");
                    parameter.Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    adapter.Update(studentsDataSet);
                    studentsDataSet.AcceptChanges();

                    tran.Commit();
                    Console.WriteLine("Success");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    tran.Rollback();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    tran.Rollback();
                }
            }
        }

    }
}
