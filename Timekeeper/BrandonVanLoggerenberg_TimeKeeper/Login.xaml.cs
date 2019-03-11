using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace BrandonVanLoggerenberg_TimeKeeper
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        B_XML_Class xmlClass = new B_XML_Class();
        GlobalVariables gvars = new GlobalVariables();
        B_SQL_Class bSQL = new B_SQL_Class();

        public Login()
        {
            InitializeComponent();
        }

        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(GlobalVariables.connectionString))
            {
                B_Users usr = bSQL.Get_User(textBox_username.Text, textBox_password.Text);
                if (!string.IsNullOrEmpty(usr.username))
                {
                    
                    GlobalVariables.currentUser = usr.username;
                    int unacceptedCount = bSQL.Get_UnacceptedAppointmentsCount(GlobalVariables.currentUser);
                    if (unacceptedCount > 0)
                    {
                        this.Hide();
                        Appointment_Acceptance aacc = new Appointment_Acceptance();
                        aacc.ShowDialog();
                        MainWindow calendar = new MainWindow();
                        calendar.Show();
                    }
                    else
                    {
                        this.Hide();
                        MainWindow calendar = new MainWindow();
                        calendar.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Username and Password combination. Please try again.", "Incorrect Username/Password combination", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                B_Users testUser = xmlClass.getUserFromXML(textBox_username.Text, true);
                if (!string.IsNullOrEmpty(testUser.username))
                {
                    testUser.password = textBox_password.Text;
                    if (xmlClass.Login(testUser))
                    {
                        MainWindow calendar = new MainWindow();
                        GlobalVariables.currentUser = testUser.username;
                        calendar.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Username or password incorrect", "Error logging in", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Username or password incorrect", "Error logging in", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                }
            }
        }

        private void textBox_username_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBox_username.Text = textBox_username.Text.TrimStart(" ".ToCharArray());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                xmlClass.PopulateAppSettings();
            }
            catch (Exception ex)
            {
                //pass
            }

            if (string.IsNullOrEmpty(GlobalVariables.connectionString)) { GlobalVariables.connectionString = ""; }
            xmlClass.CreateAppSettingsXML(GlobalVariables.chosenTheme, GlobalVariables.calendarStartSunday, GlobalVariables.connectionString, GlobalVariables.ranSetup);


            if (GlobalVariables.ranSetup == false)
            {
                this.Hide();
                SetupWindow sw = new SetupWindow();
                sw.ShowDialog();
                this.Show();
            }

            MainGrid.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][0] as Brush;
            label_username.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush;
            label_password.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush;
            GridLogin.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][11] as Brush;
            expander_MoreLess.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][11] as Brush;
            expander_MoreLess.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush;
            label_Register.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][12] as Brush;
            label_ForgotPassword.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][12] as Brush;

            if (!string.IsNullOrEmpty(GlobalVariables.connectionString))//Connects to SQL DB
            {

            }
            else//XML Local Files
            {
                bool? toCreateUser = xmlClass.createUsersXML(new List<B_Users>() { });

                if (toCreateUser == null)
                {
                    label_ForgotPassword.IsEnabled = false;
                    MessageBoxResult chosen = System.Windows.MessageBox.Show("There are no users set up in the system. Do you want to create a user?", "No users in the system", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (chosen == MessageBoxResult.Yes)
                    {
                        User_CreateUpdate createUser = new User_CreateUpdate("Create");
                        createUser.Show();
                        this.Hide();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    label_ForgotPassword.IsEnabled = true;
                }
            }


        }

        private void expander_Expanded(object sender, RoutedEventArgs e)
        {
            (sender as Expander).Header = "Less";
            this.Height = 275;
        }

        private void expander_Collapsed(object sender, RoutedEventArgs e)
        {
            (sender as Expander).Header = "More";
            this.Height = 200;

        }

        private void label_Generic_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Label).Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][13] as Brush;
        }

        private void label_Generic_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Label).Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][12] as Brush;
        }

        private void label_Generic_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Label).Content.ToString() == "Register a User")//register user
            {
                User_CreateUpdate userCreate = new User_CreateUpdate("Create");
                userCreate.whereFrom = "Login";
                userCreate.Show();
                this.Hide();
            }
            else//forgot password
            {
                User_CreateUpdate userCreate = new User_CreateUpdate("Change Password");
                userCreate.whereFrom = "Login";
                userCreate.Show();
                this.Hide();
            }

        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
