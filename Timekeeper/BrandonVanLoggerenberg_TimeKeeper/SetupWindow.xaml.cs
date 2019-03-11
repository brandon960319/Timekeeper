using System;
using System.Collections.Generic;
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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;

namespace BrandonVanLoggerenberg_TimeKeeper
{
    /// <summary>
    /// Interaction logic for SetupWindow.xaml
    /// </summary>
    public partial class SetupWindow : Window
    {
        B_XML_Class xmlClass = new B_XML_Class();
        GlobalVariables gvars = new GlobalVariables();
        
        string connstr = String.Empty;

        public SetupWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(tabControl.SelectedIndex == 0)
            {
                btn_Previous.IsEnabled = true;
                tabControl.SelectedIndex = 1;
                rBtn_Offline.IsChecked = true;
                cmb_ServerAuth.SelectedIndex = 0;
                btn_TestConnection.IsEnabled = false;
                btn_Next.IsEnabled = true;
            }
            else if (tabControl.SelectedIndex == 1)
            {
                GlobalVariables.connectionString = connstr;
                tabControl.SelectedIndex = 2;
                btn_Next.IsEnabled = false;
            }
            else if (tabControl.SelectedIndex == 2)
            {
                tabControl.SelectedIndex = 3;
            }
            else if (tabControl.SelectedIndex == 3)
            {
                if (rBtn_Sun.IsChecked == true) {
                    GlobalVariables.calendarStartSunday = true;
                }
                else {
                    GlobalVariables.calendarStartSunday = false;
                }

                if (rBtn_Theme1.IsChecked == true) {
                    GlobalVariables.chosenTheme = 0;
                }
                else {
                    GlobalVariables.chosenTheme = 1;
                }

                GlobalVariables.ranSetup = true;
                xmlClass.UpdateAppSettingsXML(GlobalVariables.chosenTheme, GlobalVariables.calendarStartSunday, GlobalVariables.connectionString, GlobalVariables.ranSetup);

                this.Hide();
                btn_Previous.IsEnabled = true;
            }
        }

        #region Connectivity
        private void rBtn_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton).Tag.ToString() == "1")
            {
                btn_ShowDatabaseCreate.IsEnabled = true;
                txt_ServerName.IsEnabled = true;
                txt_DatabaseName.IsEnabled = true;
                cmb_ServerAuth.IsEnabled = true;
                btn_TestConnection.IsEnabled = true;
                btn_Next.IsEnabled = false;
            }
            else
            {
                btn_ShowDatabaseCreate.IsEnabled = false;
                txt_ServerName.IsEnabled = false;
                txt_DatabaseName.IsEnabled = false;
                cmb_ServerAuth.IsEnabled = false;
                btn_TestConnection.IsEnabled = false;
                btn_Next.IsEnabled = true;
            }
        }

        private void cmb_ServerAuth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == 0)
            {
                txt_ServerUsername.IsEnabled = false;
                txt_ServerPassword.IsEnabled = false;
            }
            else
            {
                txt_ServerUsername.IsEnabled = true;
                txt_ServerPassword.IsEnabled = true;
            }
        }
        
        private void btn_TestConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmb_ServerAuth.SelectedIndex == 0)//windows auth
                {
                    connstr = "Data Source=" + txt_ServerName.Text + ";Initial Catalog="+ txt_DatabaseName.Text + ";Integrated Security=SSPI";
                }
                else//sql server auth
                {
                    connstr = "Data Source=" + txt_ServerName.Text + ";Initial Catalog=" + txt_DatabaseName.Text + ";Persist Security Info=True;User Id=" + txt_ServerUsername.Text + ";Password=" + txt_ServerPassword.Text;
                }

                using (var connection = new SqlConnection(connstr))
                {
                    connection.Open();
                    MessageBox.Show("Valid");
                    btn_Next.IsEnabled = true;
                }
            }
            catch
            {
                MessageBox.Show("Invalid");
                btn_Next.IsEnabled = false;
            }
        }
        #endregion

        private void Button_Click_CreateUser(object sender, RoutedEventArgs e)
        {
            B_Users testUser = xmlClass.getUserFromXML(textBox_username.Text,false);
            if (string.IsNullOrEmpty(testUser.username))
            {
                //we can create the user in the XML (for offline user)
                xmlClass.insertUserXML(textBox_username.Text, textBox_password.Text, false);
                testUser = xmlClass.getUserFromXML(textBox_username.Text, false);                
            }

            if (rBtn_Online.IsChecked == true)
            {
                //we Insert the user into the database if the setup is set to SQL Database
                SqlConnection scon = new SqlConnection(connstr);
                try
                {
                    string usernameExists = String.Empty;
                    scon.Open();
                    SqlCommand scom = new SqlCommand(@"Select Username from Users where Username = @Val_Username", scon);
                    scom.Parameters.AddWithValue("@Val_Username", textBox_username.Text);
                    SqlDataReader sread = scom.ExecuteReader();
                    while (sread.Read())
                    {
                        usernameExists = sread["Username"].ToString();
                    }
                    sread.Close();

                    if (string.IsNullOrEmpty(usernameExists))
                    {
                        SqlCommand scom1 = new SqlCommand(@"Insert Into Users (Username,Password) values(@Val_Username,@Val_Password)", scon);
                        scom1.Parameters.AddWithValue("@Val_Username", textBox_username.Text);
                        scom1.Parameters.AddWithValue("@Val_Password", textBox_password.Text);
                        scom1.CommandType = System.Data.CommandType.Text;
                        scom1.ExecuteNonQuery();
                        MessageBox.Show("User Successfully Inserted");
                        btn_Next.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Username already exists, Please choose a different username");
                    }
                }
                catch (Exception ex) { }
                finally
                {
                    scon.Close();
                }
            }
            else {
                btn_Next.IsEnabled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetAppSettings();
        }

        public void GetAppSettings()
        {
            if (string.IsNullOrEmpty(GlobalVariables.connectionString))
            {
                GlobalVariables.connectionString = "";
            }
            GlobalVariables.ranSetup = false;
            xmlClass.CreateAppSettingsXML(GlobalVariables.chosenTheme,GlobalVariables.calendarStartSunday, GlobalVariables.connectionString , GlobalVariables.ranSetup);
            xmlClass.PopulateAppSettings();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mResult = MessageBox.Show("Are you sure that you want to close the setup?", "Closing TimeKeeper", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
            if (mResult == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void btn_ShowDatabaseCreate_Click(object sender, RoutedEventArgs e)
        {
            DatabasePreview dbp = new DatabasePreview();
            dbp.ShowDialog();
        }
    }
}
