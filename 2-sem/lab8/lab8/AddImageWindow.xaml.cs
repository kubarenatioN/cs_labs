using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для AddImageWindow.xaml
    /// </summary>
    public partial class AddImageWindow : Window
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["univerConnection"].ConnectionString;
        private DataSet studentsDataSet;
        private string strName;
        private string imageName;
        public AddImageWindow(DataSet ds)
        {
            InitializeComponent();

            studentsDataSet = ds;
        }

        private void ChoseImageClick(object sender, RoutedEventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                fldlg.ShowDialog();
                {
                    strName = fldlg.SafeFileName;
                    imageName = fldlg.FileName;
                    ImageSourceConverter isc = new ImageSourceConverter();
                    imagePreview.SetValue(Image.SourceProperty, isc.ConvertFromString(imageName));
                }
                fldlg = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(idTextBox.Text, out int id))
            {
                InsertImageData(id);
            }
        }

        private void InsertImageData(int id)
        {
            try
            {
                if (imageName != "")
                {
                    //Initialize a file stream to read the image file
                    FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);

                    //Initialize a byte array with size of stream
                    byte[] imgByteArr = new byte[fs.Length];

                    //Read data from the file stream and put into the byte array
                    fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));

                    //Close a file stream
                    fs.Close();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = $"update Students set Photo = @img where Id = @id";

                        SqlCommand command = new SqlCommand(query, connection);

                        //Pass byte array into database
                        command.Parameters.Add(new SqlParameter("@img", imgByteArr));
                        command.Parameters.Add(new SqlParameter("@id", id));

                        int result = command.ExecuteNonQuery();
                        if (result == 1)
                        {
                            MessageBox.Show("Image added successfully.");
                            //BindImageList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
