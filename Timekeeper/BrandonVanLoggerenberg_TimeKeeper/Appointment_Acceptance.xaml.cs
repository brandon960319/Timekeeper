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
    /// Interaction logic for Appointment_Acceptance.xaml
    /// </summary>
    public partial class Appointment_Acceptance : Window
    {
        List<string> approvedList = new List<string>();
        List<string> rejectedList = new List<string>();
        GlobalVariables gvars = new GlobalVariables();
        B_SQL_Class bSQL = new B_SQL_Class();
        List<User_Appointments> theAppointments = new List<User_Appointments>();

        public Appointment_Acceptance()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            button_Accept.Visibility = Visibility.Hidden;
            button_Reject.Visibility = Visibility.Hidden;
            button_Finish.Visibility = Visibility.Hidden;

            LoadWindow();
        }

        void LoadWindow()
        {
            MainGrid.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][0] as Brush;
            AppointmentsHeader.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][3] as Brush;
            AppointmentsHeaderLabel.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush;
            AppointmentsContainer.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][5] as Brush;

            AppointmentsHeader_Details.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][10] as Brush;
            AppointmentsHeader_DetailsLabel.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush;
            AppointmentsHeader_DetailsLabel.Content = "Please Select a Time";
            Stackpanel_TimeLine.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][5] as Brush;
            GridAppointmentDetails.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][5] as Brush;
            stackpanel_TimeLineBorder.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][10] as Brush;
            Stackpanel_Attendees.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][5] as Brush;
            stackpanel_AttendeesBorder.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][10] as Brush;
            label_AppointmentHeader.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush;
            label_AppointmentDescription.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush;
            label_Attendees.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush;

            theAppointments = bSQL.Get_UnacceptedAppointments(GlobalVariables.currentUser);
            Stackpanel_TimeLine.Children.Clear();
            foreach (User_Appointments tempAppointments in theAppointments)
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
                CheckBox theCheckbox = new CheckBox()
                {
                    Height = 15,
                    Width = 20
                };
                theCheckbox.Checked += new RoutedEventHandler(this.CheckboxChecked);
                theCheckbox.Unchecked += new RoutedEventHandler(this.CheckboxUnchecked);
                theStackPanel.Children.Add(theCheckbox);
                Label theLabel = new Label()
                {
                    Height = 27,
                    Width = 300,
                    Tag = tempAppointments.Appointment_ID,
                    Content = tempAppointments.Appointment_DateTime.ToString("yyyy/MM/dd HH:mm") + " - " + tempAppointments.Appointment_Header,
                    Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush
                };
                theStackPanel.Children.Add(theLabel);
                Label theLabelstatus = new Label()
                {
                    Height = 27,
                    Width = 100,
                    Tag = tempAppointments.Appointment_ID,
                    Content = "UnAccepted",
                    Foreground = Brushes.Yellow //gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush
                };
                theStackPanel.Children.Add(theLabelstatus);
                theStackPanel.MouseEnter += new MouseEventHandler(this.hoverOver_ColorChange_Appointments);
                theStackPanel.MouseLeave += new MouseEventHandler(this.hoverLeave_ColorChange_Appointments);
                theStackPanel.MouseLeftButtonUp += new MouseButtonEventHandler(this.mouseLeftButtonUp_Appointments);

                Stackpanel_TimeLine.Children.Add(theBorder);
                #endregion
            }
        }
        
        Brush originalColor;
        void hoverOver_ColorChange_Appointments(object sender, MouseEventArgs e)
        {
            originalColor = (sender as StackPanel).Background;
            (sender as StackPanel).Background = gvars.ThemeColors[GlobalVariables.chosenTheme][6] as Brush;
        }

        void hoverLeave_ColorChange_Appointments(object sender, MouseEventArgs e)
        {
            (sender as StackPanel).Background = originalColor;
        }

        void mouseLeftButtonUp_Appointments(object sender, MouseButtonEventArgs e)
        {
            /*
            bool? checkedOrNot;
            checkedOrNot = ((sender as StackPanel).Children[0] as CheckBox).IsChecked;
            if (checkedOrNot == true)
            {
                ((sender as StackPanel).Children[0] as CheckBox).IsChecked = false;
            }
            else if (checkedOrNot == false)
            {
                ((sender as StackPanel).Children[0] as CheckBox).IsChecked = true;
            }
            else
            {
                ((sender as StackPanel).Children[0] as CheckBox).IsChecked = true;
            }
            */

            AppointmentsHeader_DetailsLabel.Content = ((sender as StackPanel).Children[1] as Label).Content.ToString().Split("-".ToCharArray()).ToList()[0].ToString();
            List<string> appointmentAttendeesList = new List<string>();
            List<User_Appointments> chosenAppoint = theAppointments.Where(appoinntItem => appoinntItem.Appointment_ID.ToString() == ((sender as StackPanel).Children[2] as Label).Tag.ToString()).ToList();
            if (chosenAppoint.Count != 0)
            {
                textBox_AppointmentHeader.Text = chosenAppoint[0].Appointment_Header;
                textBox_AppointmentDescription.Text = chosenAppoint[0].Appointment_Description;
                appointmentAttendeesList = chosenAppoint[0].usernames;
            }
            else
            {
                textBox_AppointmentHeader.Text = "";
                textBox_AppointmentDescription.Text = "";
            }

            Stackpanel_Attendees.Children.Clear();
            List<B_Users> allUsers = new List<B_Users>();
             allUsers = bSQL.Get_AllUsers();
            if (allUsers.Count != 0)
            {
                foreach (B_Users tempUser in allUsers)
                {
                    Border theBorder = new Border()
                    {
                        BorderThickness = new Thickness(1, 1, 1, 1),
                        CornerRadius = new CornerRadius(5, 5, 5, 5),
                        Width = 180,
                        Height = 30,
                        IsEnabled = false,
                        Margin = new Thickness(-10, 0, 0, 0),
                        Background = gvars.ThemeColors[GlobalVariables.chosenTheme][10] as Brush
                    };
                    StackPanel theStackpanel = new StackPanel()
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Height = 30,
                        Orientation = Orientation.Horizontal
                    };
                    theBorder.Child = theStackpanel;
                    CheckBox theCheckbox = new CheckBox()
                    {
                        Height = 15,
                        Width = 20
                    };
                    theStackpanel.Children.Add(theCheckbox);
                    Label theLabel = new Label()
                    {
                        Height = 27,
                        Width = 150,
                        Content = tempUser.username,
                        Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush
                    };
                    theStackpanel.Children.Add(theLabel);
                    if (tempUser.username == GlobalVariables.currentUser)
                    {
                        theCheckbox.IsChecked = true;
                    }
                    else if (appointmentAttendeesList.Count > 0)
                    {
                        if (appointmentAttendeesList.Contains(tempUser.username))
                        {
                            theCheckbox.IsChecked = true;
                        }
                    }
                    Stackpanel_Attendees.Children.Add(theBorder);
                }
            }
        }

        #region checkbox

        void CheckboxChecked(object sender, RoutedEventArgs e)
        {
           Button_Check();
        }

        private void CheckboxUnchecked(object sender, RoutedEventArgs e)
        {
            Button_Check();
        }

        #endregion

        #region Accept Reject

        private void button_Accept_Click(object sender, RoutedEventArgs e)
        {
            foreach (Border bd in Stackpanel_TimeLine.Children)
            {
                if (((bd.Child as StackPanel).Children[0] as CheckBox).IsChecked == true)
                {
                    ((bd.Child as StackPanel).Children[2] as Label).Content = "Accepted";
                    ((bd.Child as StackPanel).Children[2] as Label).Foreground = Brushes.Green;
                }
            }

            Button_Check();
            Finish_Check();
        }

        private void button_Accept_All_Click(object sender, RoutedEventArgs e)
        {
            foreach (Border bd in Stackpanel_TimeLine.Children)
            {
                ((bd.Child as StackPanel).Children[2] as Label).Content = "Accepted";
                ((bd.Child as StackPanel).Children[2] as Label).Foreground = Brushes.Green;
            }
            button_Accept.Visibility = Visibility.Hidden;
            button_Reject.Visibility = Visibility.Hidden;
            Finish_Check();
            Button_Check();
        }

        private void button_Reject_Click(object sender, RoutedEventArgs e)
        {
            foreach (Border bd in Stackpanel_TimeLine.Children)
            {
                if (((bd.Child as StackPanel).Children[0] as CheckBox).IsChecked == true)
                {
                    ((bd.Child as StackPanel).Children[2] as Label).Content = "Rejected";
                    ((bd.Child as StackPanel).Children[2] as Label).Foreground = Brushes.Red;
                }
            }

            Button_Check();
            Finish_Check();
        }

        private void button_Reject_All_Click(object sender, RoutedEventArgs e)
        {
            foreach (Border bd in Stackpanel_TimeLine.Children)
            {
                ((bd.Child as StackPanel).Children[2] as Label).Content = "Rejected";
                ((bd.Child as StackPanel).Children[2] as Label).Foreground = Brushes.Red;
            }
            button_Accept.Visibility = Visibility.Hidden;
            button_Reject.Visibility = Visibility.Hidden;
            Finish_Check();
            Button_Check();
        }

        #endregion

        #region Checks

        private void Button_Check()
        {
            int checkCounter = 0;
            foreach (Border bd in Stackpanel_TimeLine.Children)
            {
                if (((bd.Child as StackPanel).Children[0] as CheckBox).IsChecked == true)
                {
                    checkCounter++;
                }
            }

            if (checkCounter > 0)
            {
                button_Accept.Visibility = Visibility.Visible;
                button_Reject.Visibility = Visibility.Visible;
            }
            else
            {
                button_Accept.Visibility = Visibility.Hidden;
                button_Reject.Visibility = Visibility.Hidden;
            }
        }

        private void Finish_Check()
        {
            approvedList.Clear();
            rejectedList.Clear();
            bool sGoodToGo = true;
            foreach (Border bd in Stackpanel_TimeLine.Children)
            {
                if (((bd.Child as StackPanel).Children[2] as Label).Content.ToString() == "UnAccepted")
                {
                    if (sGoodToGo)
                    {
                        sGoodToGo = false;
                    }
                }
                else if (((bd.Child as StackPanel).Children[2] as Label).Content.ToString() == "Accepted")
                {
                    approvedList.Add(((bd.Child as StackPanel).Children[1] as Label).Tag.ToString());
                }
                else
                {
                    rejectedList.Add(((bd.Child as StackPanel).Children[1] as Label).Tag.ToString());
                }
            }

            if (sGoodToGo)
                button_Finish.Visibility = Visibility.Visible;
            else
                button_Finish.Visibility = Visibility.Hidden;
        }

        #endregion 

        #region Finish

        private void button_Finish_Click(object sender, RoutedEventArgs e)
        {
            bool sFailure = false;
            if (!bSQL.Accept_Appointment(GlobalVariables.currentUser, approvedList))
            {
                MessageBox.Show("Failed to Accept the Appointments");
                sFailure = true;
            }
            if (!bSQL.Reject_Appointment(GlobalVariables.currentUser, rejectedList))
            {
                MessageBox.Show("Failed to Reject the Appointments");
                if (!sFailure) { sFailure = true; }
            }
            if (!sFailure)
            {
                MessageBox.Show("Appointments Successfully Updated");
                this.Hide();
            }
        }

        #endregion

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to cancel? \r\nAll UnAccepted appointments will not display in TimeKeeper until Accepted", "Cancel Accepting Appointments", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Hide();
            }            
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
