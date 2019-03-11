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

namespace BrandonVanLoggerenberg_TimeKeeper
{
    /// <summary>
    /// Interaction logic for User_CreateUpdate.xaml
    /// </summary>
    public partial class User_CreateUpdate : Window
    {
        GlobalVariables gvars = new GlobalVariables();
        B_Users buser_Old = new B_Users();
        B_Users buser_New = new B_Users();
        B_XML_Class xmlClass = new B_XML_Class();
        B_SQL_Class bSQL = new B_SQL_Class();
        public string whereFrom;

        public User_CreateUpdate()
        {
            InitializeComponent();
        }

        public User_CreateUpdate(string toDo)
        {
            InitializeComponent();
            LoadPage(toDo);
        }

        public User_CreateUpdate(string toDo, string originalUser)
        {
            InitializeComponent();
            buser_Old.username = originalUser;
            textBox_username.Text = buser_Old.username;
            if (toDo == "Change Password")
            {
                textBox_username.IsEnabled = false;
            }
            LoadPage(toDo);
        }

        private void LoadPage(string toDo)
        {
            if (toDo == "Create")
            {
                button_Create.Content = "Create";
                this.Title = "Timekeeper User Create";
                label_Header.Content = "User Create";
            }
            else if (toDo == "Update")
            {
                button_Create.Content = "Save";
                this.Title = "Timekeeper User Update";
                label_Header.Content = "User Update";
            }
            else//password change
            {
                button_Create.Content = "Save";
                this.Title = "Timekeeper Password Update";
                label_Header.Content = "Password Update";    
            }
            MainGrid.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][0] as Brush;
            UserGrid.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][11] as Brush;
            HeaderGrid.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][3] as Brush;
            label_Header.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][4] as Brush;
            
            label_username.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush;
            label_password.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush;
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            bool returnType = false;
            textBox_username.Text = textBox_username.Text.TrimEnd();
            if (this.Title == "Timekeeper User Create")//Create user
            {
                #region insert user
                //insert users
                if (!string.IsNullOrEmpty(GlobalVariables.connectionString))
                {
                    bool sCompleted = bSQL.Insert_User(textBox_username.Text,textBox_password.Text);
                    if (!sCompleted)
                    {
                        MessageBox.Show("Could not insert the user, please enter a different Username/Password combination");
                    }
                    else
                    {
                        MessageBox.Show("Successfully inserted the user!");
                        CloseTheWindow();
                    }
                }
                else
                {
                    returnType = xmlClass.insertUserXML(textBox_username.Text, textBox_password.Text, true);
                    if (!returnType)
                    {
                        MessageBox.Show("Could not insert the user, please enter a different Username/Password combination");
                        textBox_username.Text = "";
                        textBox_password.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Successfully inserted the user!");
                        CloseTheWindow();
                    }
                }
                #endregion
            }
            else if (this.Title == "Timekeeper User Update")//Update user
            {
                #region Update User
                //update user
                if (!string.IsNullOrEmpty(GlobalVariables.connectionString))
                {
                    returnType = bSQL.Update_User(buser_Old.username, textBox_username.Text);
                    bool sCompleted = bSQL.Update_Password(textBox_username.Text, textBox_password.Text);
                    if ((!sCompleted) || (!returnType))
                    {
                        MessageBox.Show("Could not update the user.");
                    }
                    else
                    {
                        MessageBox.Show("Successfully updated the user!");
                        CloseTheWindow();
                    }
                    
                }
                else
                {
                    returnType = xmlClass.updateUserXML(buser_Old.username, textBox_username.Text);
                    bool sCompleted = xmlClass.updateUserPasswordXML(textBox_username.Text, textBox_password.Text);
                    if ((!sCompleted) || (!returnType))
                    {
                        MessageBox.Show("Could not update the user.");
                    }
                    else
                    {
                        MessageBox.Show("Successfully updated the user!");
                        CloseTheWindow();
                    }
                }

                #endregion

            }
            else//update password
            {
                //update password
                if (!string.IsNullOrEmpty(GlobalVariables.connectionString))
                {
                    returnType = bSQL.Update_Password(textBox_username.Text, textBox_password.Text);
                    if (!returnType)
                    {
                        MessageBox.Show("Could not update the password.");
                    }
                    else
                    {
                        MessageBox.Show("Successfully updated the password!");
                        CloseTheWindow();
                    }
                }
                else
                {
                    returnType = xmlClass.updateUserPasswordXML(textBox_username.Text, textBox_password.Text);
                    if ((!returnType))
                    {
                        MessageBox.Show("Could not update the password.");
                    }
                    else
                    {
                        MessageBox.Show("Successfully updated the password!");
                        CloseTheWindow();
                    }
                }
            }
        }

        private void textBox_username_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBox_username.Text = textBox_username.Text.TrimStart(" ".ToCharArray());
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            CloseTheWindow();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CloseTheWindow();
        }

        private void CloseTheWindow()
        {
            if (whereFrom == "User Management")
            {
                User_Management um = new User_Management();
                um.Show();
            }
            else
            {
                Login login = new Login();
                login.Show();
            }
            this.Hide();
        }
    }
}
