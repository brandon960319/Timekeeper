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
    /// Interaction logic for User_Management.xaml
    /// </summary>
    public partial class User_Management : Window
    {
        B_XML_Class xmlClass = new B_XML_Class();
        GlobalVariables gvars = new GlobalVariables();
        B_SQL_Class bSQL = new B_SQL_Class();

        public User_Management()
        {
            InitializeComponent();
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }
        
        private void button_New_Click(object sender, RoutedEventArgs e)
        {
            User_CreateUpdate userCreate = new User_CreateUpdate("Create");
            userCreate.whereFrom = "User Management";
            userCreate.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadWindow();
        }

        void LoadWindow()
        {

            MainGrid.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][0] as Brush;
            UsersHeader.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][3] as Brush;
            UsersHeaderLabel.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush;
            UserContainer.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][5] as Brush;

            List<B_Users> theUsers = new List<B_Users>();

            try
            {
                xmlClass.PopulateAppSettings();

                if (!string.IsNullOrEmpty(GlobalVariables.connectionString))
                {
                   theUsers = bSQL.Get_AllUsers();
                }
                else
                {
                    theUsers = xmlClass.getAllUsersFromXML();
                }
                    
            }
            catch (Exception ex)
            {
                //pass
            }

            Stackpanel_TimeLine.Children.Clear();
            foreach (B_Users tempUser in theUsers)
            {
                #region populate UsersGrid
                Border theBorder = new Border()
                {
                    BorderThickness = new Thickness(1, 1, 1, 1),
                    CornerRadius = new CornerRadius(1, 1, 1, 1),
                    Width = 420,
                    Height = 30,
                    Background = gvars.ThemeColors[GlobalVariables.chosenTheme][10] as Brush
                };
                StackPanel theStackPanel = new StackPanel()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Height = 30,
                    Width = 420,
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 0, 0, 0),
                };
                theBorder.Child = theStackPanel;

                theStackPanel.MouseEnter += new MouseEventHandler(this.hoverOver_ColorChange_Users);
                theStackPanel.MouseLeave += new MouseEventHandler(this.hoverLeave_ColorChange_Users);


                Label theUsersLabel = new Label()
                {
                    Height = 25,
                    Width = 180,
                    Content = tempUser.username,
                    Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush
                };
                theStackPanel.Children.Add(theUsersLabel);

                Label changePasswordUser = new Label()
                {
                    Height = 25,
                    Width = 120,
                    Content = "Change Password",
                    Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][12] as Brush
                };
                changePasswordUser.MouseEnter += new MouseEventHandler(this.hoverOver_ColorChange_Links);
                changePasswordUser.MouseLeave += new MouseEventHandler(this.hoverLeave_ColorChange_Links);
                changePasswordUser.MouseLeftButtonUp += new MouseButtonEventHandler(this.mouseLeftButtonUp_Users);
                theStackPanel.Children.Add(changePasswordUser);

                Label editUser = new Label()
                {
                    Height = 25,
                    Width = 40,
                    Content = "Edit",
                    Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][12] as Brush
                };
                editUser.MouseEnter += new MouseEventHandler(this.hoverOver_ColorChange_Links);
                editUser.MouseLeave += new MouseEventHandler(this.hoverLeave_ColorChange_Links);
                editUser.MouseLeftButtonUp += new MouseButtonEventHandler(this.mouseLeftButtonUp_Users);
                theStackPanel.Children.Add(editUser);

                Label deleteUser = new Label()
                {
                    Height = 25,
                    Width = 50,
                    Content = "Delete",
                    Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][12] as Brush
                };
                deleteUser.MouseEnter += new MouseEventHandler(this.hoverOver_ColorChange_Links);
                deleteUser.MouseLeave += new MouseEventHandler(this.hoverLeave_ColorChange_Links);
                deleteUser.MouseLeftButtonUp += new MouseButtonEventHandler(this.mouseLeftButtonUp_Users);
                theStackPanel.Children.Add(deleteUser);

                Stackpanel_TimeLine.Children.Add(theBorder);
                #endregion
            }
        }

        Brush originalColor;
        void hoverOver_ColorChange_Users(object sender, MouseEventArgs e)
        {
            originalColor = (sender as StackPanel).Background;
            (sender as StackPanel).Background = gvars.ThemeColors[GlobalVariables.chosenTheme][6] as Brush;
        }

        void hoverLeave_ColorChange_Users(object sender, MouseEventArgs e)
        {
            (sender as StackPanel).Background = originalColor;
        }
        
        void hoverOver_ColorChange_Links(object sender, MouseEventArgs e)
        {
            (sender as Label).Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][13] as Brush;
        }

        void hoverLeave_ColorChange_Links(object sender, MouseEventArgs e)
        {
            (sender as Label).Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][12] as Brush;
        }
        
        void mouseLeftButtonUp_Users(object sender, MouseEventArgs e)
        {
            string todo = (sender as Label).Content.ToString();
            string chosenUser = (((sender as Label).Parent as StackPanel).Children[0] as Label).Content.ToString();

            if (todo == "Change Password")
            {
                User_CreateUpdate userCreate = new User_CreateUpdate("Change Password", chosenUser);
                userCreate.whereFrom = "User Management";
                userCreate.Show();
                this.Hide();
            }
            else if (todo == "Edit")
            {
                User_CreateUpdate userCreate = new User_CreateUpdate("Update", chosenUser);
                userCreate.whereFrom = "User Management";
                userCreate.Show();
                this.Hide();
            }
            else//delete
            {
                bool deletedOrNot = false;
                if (!string.IsNullOrEmpty(GlobalVariables.connectionString))
                {
                    deletedOrNot =  bSQL.Delete_User(chosenUser);
                    if (deletedOrNot)
                    {
                        MessageBox.Show("Successfully deleted the user!");
                        LoadWindow();
                    }
                    else
                    {
                        MessageBox.Show("Failed to deleted the user");
                    }
                }
                else
                {
                    deletedOrNot = xmlClass.deleteUserFromXML(chosenUser);
                    if (deletedOrNot)
                    {
                        MessageBox.Show("Successfully deleted the user");
                        LoadWindow();
                    }
                    else
                    {
                        MessageBox.Show("Failed to deleted the user");
                    }
                }
            }
        }
        
    }
}
