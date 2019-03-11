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
    public partial class Calendar_Appointments : Window
    {
        public DateTime dt { get; set; }
        B_XML_Class xmlClass = new B_XML_Class();
        B_SQL_Class bSQL = new B_SQL_Class();
        List<User_Appointments> dailyAppointments = new List<User_Appointments>();
        User_Appointments appointmentUpdates;
        User_Appointments appointmentOld;
        GlobalVariables gvars = new GlobalVariables();
        bool saveEnable = false;

        public Calendar_Appointments()
        {
            InitializeComponent();
        }

        public Calendar_Appointments(string _currentUser, DateTime dateTime)
        {
            InitializeComponent();
            dt = dateTime;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            appointmentUpdates  = new User_Appointments();
            appointmentUpdates.Appointment_DateTime = new DateTime();
            appointmentUpdates.Appointment_Description = "";
            appointmentUpdates.Appointment_Header = "";
            appointmentUpdates.Appointment_ID = 0;
            appointmentUpdates.Company_IDs = new List<string>();
            appointmentUpdates.Expected_Duration = new TimeSpan(0,60,0);
            appointmentUpdates.usernames = new List<string>();

            appointmentOld = new User_Appointments();
            appointmentOld.Appointment_DateTime = new DateTime();
            appointmentOld.Appointment_Description = "";
            appointmentOld.Appointment_Header = "";
            appointmentOld.Appointment_ID = 0;
            appointmentOld.Company_IDs = new List<string>();
            appointmentOld.Expected_Duration = new TimeSpan(0, 60, 0);
            appointmentOld.usernames = new List<string>();

            LoadTimes();
        }

        private void LoadTimes()
        {
            try
            {
                xmlClass.PopulateAppSettings();
            }
            catch (Exception ex)
            {
                //pass
            }

            button_Cancel.IsEnabled = true;
            button_Save.IsEnabled = saveEnable;

            if (!string.IsNullOrEmpty(GlobalVariables.connectionString))
            {
              dailyAppointments = bSQL.GetDailyAppointmentsForUser(GlobalVariables.currentUser, dt,false);
            }
            else
            {
              dailyAppointments = xmlClass.GetDailyAppointmentsForUser(GlobalVariables.currentUser, dt);
            }

            /*
            List<string> times = new List<string> {
                "00:00" , "00:30" , "01:00" , "01:30" , "02:00" , "02:30" ,
                "03:00" , "03:30" , "04:00" , "04:30" , "05:00" , "05:30" ,
                "06:00" , "06:30" , "07:00" , "07:30" , "08:00" , "08:30" ,
                "09:00" , "09:30" , "10:00" , "10:30" , "11:00" , "11:30" ,
                "12:00" , "12:30" , "13:00" , "13:30" , "14:00" , "14:30" ,
                "15:00" , "15:30" , "16:00" , "16:30" , "17:00" , "17:30" ,
                "18:00" , "18:30" , "19:00" , "19:30" , "20:00" , "20:30" ,
                "21:00" , "21:30" , "22:00" , "22:30" , "23:00" , "23:30"
            };
            */

            List<string> times = new List<string> {
                "00:00" ,  "01:00"  , "02:00" ,
                "03:00" ,  "04:00"  , "05:00" ,
                "06:00" ,  "07:00"  , "08:00" ,
                "09:00" ,  "10:00"  , "11:00" ,
                "12:00" ,  "13:00"  , "14:00" ,
                "15:00" ,  "16:00"  , "17:00" ,
                "18:00" ,  "19:00"  , "20:00" ,
                "21:00" ,  "22:00"  , "23:00"
                };
            bool oddEven = true;

            MainGrid.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][0] as Brush;
            AppointmentsHeader.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][3] as Brush;
            AppointmentsHeaderLabel.Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush;
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

            AppointmentsHeaderLabel.Content = dt.ToString("dd MMMM yyyy");

            for (int i = 0; i < 24; i++)
            {
                Border theBorder = new Border() {
                    BorderThickness = new Thickness(1, 1, 1, 1),
                    CornerRadius = new CornerRadius(5, 5, 5, 5),
                    Width = 350,
                    Height = 30,
                    Background = gvars.ThemeColors[GlobalVariables.chosenTheme][10] as Brush
                };
                StackPanel theStackPanel = new StackPanel() {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Height = 30,
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 0, 0, 0),
                };
                theBorder.Child = theStackPanel;

                theStackPanel.MouseEnter += new MouseEventHandler(this.hoverOver_ColorChange_Appointments);
                theStackPanel.MouseLeave += new MouseEventHandler(this.hoverLeave_ColorChange_Appointments);
                theStackPanel.MouseLeftButtonUp += new MouseButtonEventHandler(this.mouseLeftButtonUp_DisplayAppointment);

                if (oddEven)
                {
                  //  theStackPanel.Background = Brushes.LightGray;
                    oddEven = false;
                }
                else
                {
                    oddEven = true;
                }

                Label theTimeLabel = new Label() {
                    Height = 25,
                    Width = 40,
                    Content = times[i],
                    Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush
                };
                theStackPanel.Children.Add(theTimeLabel);

                List<User_Appointments> chosenAppoint = dailyAppointments.Where(appoinntItem => appoinntItem.Appointment_DateTime.ToString("HH:mm") == times[i]).ToList();
                Label theAppointmentLabel = new Label() {
                    Height = 25,
                    Width = 300,
                    Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush
                };

                if (chosenAppoint.Count != 0)
                {
                    theAppointmentLabel.Content = chosenAppoint[0].Appointment_Header;
                    
                }
                else
                {
                    theAppointmentLabel.Content = "";
                }
                theStackPanel.Children.Add(theAppointmentLabel);
                Stackpanel_TimeLine.Children.Add(theBorder);
            }
        }

        Brush originalColor;

        #region dynamic mouse events
        void hoverOver_ColorChange_Appointments(object sender, MouseEventArgs e)
        {
            originalColor = (sender as StackPanel).Background;
            (sender as StackPanel).Background = gvars.ThemeColors[GlobalVariables.chosenTheme][6] as Brush;
        }

        void hoverLeave_ColorChange_Appointments(object sender, MouseEventArgs e)
        {
            (sender as StackPanel).Background = originalColor;
        }

        void mouseLeftButtonUp_DisplayAppointment(object sender, MouseButtonEventArgs e)
        {
            AppointmentsHeader_DetailsLabel.Content = ((sender as StackPanel).Children[0] as Label).Content.ToString();
            List<string> appointmentAttendeesList = new List<string>();
            List <User_Appointments> chosenAppoint = dailyAppointments.Where(appoinntItem => appoinntItem.Appointment_DateTime.ToString("HH:mm") == ((sender as StackPanel).Children[0] as Label).Content.ToString()).ToList();
            if (chosenAppoint.Count != 0)
            {
                textBox_AppointmentHeader.Text          = chosenAppoint[0].Appointment_Header;
                textBox_AppointmentDescription.Text     = chosenAppoint[0].Appointment_Description;
                appointmentUpdates.Appointment_DateTime = chosenAppoint[0].Appointment_DateTime;
                appointmentOld                          = chosenAppoint[0];
                appointmentAttendeesList                = chosenAppoint[0].usernames;
            }
            else
            {
                textBox_AppointmentHeader.Text = "";
                textBox_AppointmentDescription.Text = "";
            }

            Stackpanel_Attendees.Children.Clear();
            List<B_Users> allUsers = new List<B_Users>();

            if (!string.IsNullOrEmpty(GlobalVariables.connectionString))
            {
                allUsers = bSQL.Get_AllUsers();
                //no exceptions. there should be users in the system if you got this far. you are a user after all
            }
            else
            {
                allUsers = xmlClass.getAllUsersFromXML();
                //no exceptions. there should be users in the system if you got this far. you are a user after all
            }

            if (allUsers.Count != 0)
            {
                foreach (B_Users tempUser in allUsers)
                {
                    Border theBorder = new Border() {
                        BorderThickness = new Thickness(1, 1, 1, 1),
                        CornerRadius = new CornerRadius(5, 5, 5, 5),
                        Width = 180,
                        Height = 30,
                        Margin = new Thickness(-10, 0, 0, 0),
                        Background = gvars.ThemeColors[GlobalVariables.chosenTheme][10] as Brush
                    };
                    StackPanel theStackpanel = new StackPanel() {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Height = 30, Orientation = Orientation.Horizontal
                    };
                    theBorder.Child = theStackpanel;
                    CheckBox theCheckbox = new CheckBox() {
                        Height = 15,
                        Width = 20
                    };
                    theStackpanel.Children.Add(theCheckbox);
                    Label theLabel = new Label() {
                        Height = 27,
                        Width = 150 ,
                        Content =tempUser.username,
                        Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush
                    };
                    theStackpanel.Children.Add(theLabel);
                    if (tempUser.username == GlobalVariables.currentUser)
                    {
                        theBorder.IsEnabled = false;
                        theCheckbox.IsChecked = true;
                    }
                    else if (appointmentAttendeesList.Contains(tempUser.username))
                    {
                        theCheckbox.IsChecked = true;
                    }
                    else
                    {
                        theStackpanel.MouseEnter += new MouseEventHandler(this.hoverOver_ColorChange_Attendees);
                        theStackpanel.MouseLeave += new MouseEventHandler(this.hoverLeave_ColorChange_Attendees);
                        theStackpanel.MouseLeftButtonUp += new MouseButtonEventHandler(this.mouseLeftButtonUp_CheckUser);
                    }
                    Stackpanel_Attendees.Children.Add(theBorder);
                }                
            }            
        }

        void hoverOver_ColorChange_Attendees(object sender, MouseEventArgs e)
        {
            originalColor = ((sender as StackPanel).Parent as Border).Background;
            ((sender as StackPanel).Parent as Border).Background = gvars.ThemeColors[GlobalVariables.chosenTheme][6] as Brush;
        }

        void hoverLeave_ColorChange_Attendees(object sender, MouseEventArgs e)
        {
            ((sender as StackPanel).Parent as Border).Background = gvars.ThemeColors[GlobalVariables.chosenTheme][10] as Brush;
        }

        void mouseLeftButtonUp_CheckUser(object sender, MouseButtonEventArgs e)
        {
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
            appointmentUpdates.usernames.Clear();
            foreach (Border tempBorder in Stackpanel_Attendees.Children)
            {
                if (((tempBorder.Child as StackPanel).Children[0] as CheckBox).IsChecked == true)
                {
                    appointmentUpdates.usernames.Add(((tempBorder.Child as StackPanel).Children[1] as Label).Content.ToString());
                }                    
            }            
        }
        #endregion

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            bool sReturn = false;
            appointmentUpdates.Appointment_DateTime = Convert.ToDateTime((AppointmentsHeader.Children[0] as Label).Content.ToString() + " " + (AppointmentsHeader_Details.Children[0] as Label).Content.ToString());
            appointmentUpdates.usernames.Clear();
            foreach (Border tempBorder in Stackpanel_Attendees.Children)
            {
                if (((tempBorder.Child as StackPanel).Children[0] as CheckBox).IsChecked == true)
                {
                    appointmentUpdates.usernames.Add(((tempBorder.Child as StackPanel).Children[1] as Label).Content.ToString());
                }
            }
            if (appointmentOld.usernames.Count != 0)//update
            {
                if (!string.IsNullOrEmpty(GlobalVariables.connectionString))
                {
                    sReturn = bSQL.Update_Appointments(appointmentOld, appointmentUpdates);
                    if (sReturn)
                    {
                        MessageBox.Show("Appointment successfully updated");
                    }
                    else
                    {
                        MessageBox.Show("Appointment update failed");
                    }
                }
                else
                {
                    sReturn = xmlClass.UpdateAppointmentsXML(appointmentOld, appointmentUpdates, false);
                    if (sReturn)
                    {
                        MessageBox.Show("Appointment successfully updated");
                    }
                    else
                    {
                        MessageBox.Show("Appointment update failed");
                    }
                }
                MainWindow mw = new MainWindow();                
                mw.Show();
                mw.LoadCalendar();
                this.Hide();
            }
            else//insert
            {
                if (!string.IsNullOrEmpty(GlobalVariables.connectionString))
                {
                    sReturn = bSQL.Insert_Appointments(appointmentUpdates);
                    if (sReturn)
                    {
                        MessageBox.Show("Appointment successfully created");
                    }
                    else
                    {
                        MessageBox.Show("Appointment creation failed");
                    }
                }
                else
                {
                    sReturn = xmlClass.InsertAppointmentsXML(appointmentUpdates, false);
                    if (sReturn)
                    {
                        MessageBox.Show("Appointment successfully created");
                    }
                    else
                    {
                        MessageBox.Show("Appointment creation failed");
                    }
                }
                MainWindow mw = new MainWindow();                
                mw.Show();
                mw.LoadCalendar();
                this.Hide();
            }
        }

        private void button_Cancel_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }
        
        private void textBox_AppointmentHeader_TextChanged(object sender, TextChangedEventArgs e)
        {
            appointmentUpdates.Appointment_Header = textBox_AppointmentHeader.Text;
            if (!string.IsNullOrEmpty(textBox_AppointmentDescription.Text) && !string.IsNullOrEmpty(textBox_AppointmentHeader.Text) && (Stackpanel_Attendees.Children.Count!=0))
            {
                saveEnable = true;
                button_Save.IsEnabled = saveEnable;
            }
        }

        private void textBox_AppointmentDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            appointmentUpdates.Appointment_Description = textBox_AppointmentDescription.Text;
            if (!string.IsNullOrEmpty(textBox_AppointmentDescription.Text) && !string.IsNullOrEmpty(textBox_AppointmentHeader.Text) && (Stackpanel_Attendees.Children.Count != 0))
            {
                saveEnable = true;
                button_Save.IsEnabled = saveEnable;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }
}
