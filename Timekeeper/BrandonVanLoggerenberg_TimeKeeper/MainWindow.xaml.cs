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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Xml.Linq;
using System.Threading;
using System.Configuration;

namespace BrandonVanLoggerenberg_TimeKeeper
{

    public partial class MainWindow : Window
    {


        bool enableDayPreview = false;
        private DateTime currentDay = DateTime.Now;
        int shaveX = 75;//navigation button sizes div 2
        int shaveY = 100;
        int DaysOfWeekHeight = 25;
        B_XML_Class xmlClass = new B_XML_Class();
        B_SQL_Class bSQL = new B_SQL_Class();
        GlobalVariables gvars = new GlobalVariables();
        private bool windowLoaded;


        public MainWindow()
        {
            InitializeComponent();
        }

        public bool LoadCalendar()
        {
            try
            {
                xmlClass.PopulateAppSettings();
            }
            catch (Exception)
            {
                //pass
            }
            
            MainGrid.Children.Clear();

            MainGrid.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][0] as Brush;

            #region MainGrid
            Grid calendar = new Grid()
            {
                Name = "calendar",
                Width = MainGrid.Width - shaveX,
                Height = MainGrid.Height - shaveY,
                Margin = new Thickness(shaveX / 2, shaveY / 2, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            MainGrid.Children.Add(calendar);
            #endregion

            #region File and UserWelcome
            Grid UserWelcome = new Grid()
            {
                Name = "UserWelcome",
                Width = MainGrid.Width,
                Height = 30,
                Margin = new Thickness(0, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush
            };
            UserWelcome.Children.Add(
                new Label()
                {
                    Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush,
                    Content = "Welcome ",
                    Margin = new Thickness(MainGrid.Width - 200, 0, 0, 0)  //Thickness(MainGrid.Width/2, (shaveY / 2) - 30, 0, 0)
                });
            UserWelcome.Children.Add(
                new Label()
                {
                    Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][12] as Brush,
                    Content = GlobalVariables.currentUser,
                    Margin = new Thickness(MainGrid.Width - 140, 0, 0, 0)
                }
                );


            Grid MenuGrid = new Grid()
            {
                Height = 21,
                Width = 35,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 0, 0, 0),
                Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush
            };

            Menu theMenu = new Menu() { Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush };
            MenuItem FileMenu = new MenuItem()     { Header = "File", Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush, Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush };
            MenuItem MenuItem1 = new MenuItem()       { Header = "Users", Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush, Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush };
            MenuItem themeChange = new MenuItem()     { Header = "Theme", Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush, Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush };
            MenuItem theme1Selection = new MenuItem()    { Header = "Dark", Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush, Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush };
            MenuItem theme2Selection = new MenuItem()    { Header = "Light", Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush, Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush };
            MenuItem MenuCalendar = new MenuItem()    { Header = "Calendar", Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush, Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush };
            MenuItem MenuCalendar_DayOfWeekStarts = 
                                      new MenuItem() { Header = "First Day Of Week", Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush, Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush };
            MenuItem MenuCalendar_DayOfWeekStarts_Monday =
                                      new MenuItem() { Header = "Monday", Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush, Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush };
            MenuItem MenuCalendar_DayOfWeekStarts_Sunday =
                                      new MenuItem() { Header = "Sunday", Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush, Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush };
            MenuItem MenuItem2 = new MenuItem()       { Header = "Log Out", Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush, Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush };
            MenuItem MenuItem3 = new MenuItem()       { Header = "Close Timekeeper", Background = gvars.ThemeColors[GlobalVariables.chosenTheme][1] as Brush, Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][2] as Brush };

            MenuItem1.Click += MenuItem1_Click;
            MenuItem2.Click += MenuItem2_Click;
            MenuItem3.Click += MenuItem3_Click;
            theme1Selection.Click += Theme1Selection_Click;
            theme2Selection.Click += Theme2Selection_Click;
            MenuCalendar_DayOfWeekStarts_Monday.Click += MenuCalendar_DayOfWeekStarts_Monday_Click;
            MenuCalendar_DayOfWeekStarts_Sunday.Click += MenuCalendar_DayOfWeekStarts_Sunday_Click;

            FileMenu.Items.Add(MenuItem1);
            FileMenu.Items.Add(themeChange);
            FileMenu.Items.Add(MenuCalendar);
            FileMenu.Items.Add(new Separator());
            FileMenu.Items.Add(MenuItem2);
            FileMenu.Items.Add(MenuItem3);

            MenuCalendar.Items.Add(MenuCalendar_DayOfWeekStarts);
            MenuCalendar_DayOfWeekStarts.Items.Add(MenuCalendar_DayOfWeekStarts_Monday);
            MenuCalendar_DayOfWeekStarts.Items.Add(MenuCalendar_DayOfWeekStarts_Sunday);
            themeChange.Items.Add(theme1Selection);
            themeChange.Items.Add(theme2Selection);

            theMenu.Items.Add(FileMenu);     
            MenuGrid.Children.Add(theMenu);
            UserWelcome.Children.Add(MenuGrid);

            MainGrid.Children.Add(UserWelcome);
            #endregion

            #region Header
            Grid calendarHeader = new Grid()
            {
                Background = gvars.ThemeColors[GlobalVariables.chosenTheme][3] as Brush,
                Name = "calendarHeader",
                Width = MainGrid.Width,
                Height = 30,
                Margin = new Thickness(0, (shaveY / 2) - 30, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
            };
            calendarHeader.Children.Add(
                new Label()
                {
                    Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][4] as Brush,
                    Content = currentDay.ToString("MMMM") + " " + currentDay.Year.ToString(),
                    Margin = new Thickness((MainGrid.Width / 2) - 50, 0, 0, 0)  //Thickness(MainGrid.Width/2, (shaveY / 2) - 30, 0, 0)
                }
                );
            MainGrid.Children.Add(calendarHeader);
            #endregion

            #region NavigationButtons
            Button changeMonth_Back = new Button()
            {
                Height = calendar.Height,
                Width = shaveX / 2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Content = "<",
                Margin = new Thickness(0, shaveY / 2, 0, 0),
                Background = gvars.ThemeColors[GlobalVariables.chosenTheme][9] as Brush,
                Foreground = Brushes.Black
            };
            changeMonth_Back.Click += new RoutedEventHandler(this.MonthChange);
            MainGrid.Children.Add(changeMonth_Back);

            Button changeMonth_Forward = new Button()
            {
                Height = calendar.Height,
                Width = shaveX / 2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Content = ">",
                Margin = new Thickness(changeMonth_Back.Width + calendar.Width, shaveY / 2, 0, 0),
                Background = gvars.ThemeColors[GlobalVariables.chosenTheme][9] as Brush,
                Foreground = Brushes.Black
            };
            changeMonth_Forward.Click += new RoutedEventHandler(this.MonthChange);
            MainGrid.Children.Add(changeMonth_Forward);

            #endregion

            int extraColumns_Front = 0;
            int extraColumns_End = 0;
            bool dayFound = false;
            DateTime beforeSearch = currentDay.AddDays(-(Convert.ToDouble(currentDay.Day) - 1));

            while (dayFound == false)
            {
                if (GlobalVariables.calendarStartSunday)//start week on sunday
                {
                    if (beforeSearch.DayOfWeek == DayOfWeek.Sunday)
                    {
                        dayFound = true;
                    }
                    else
                    {
                        extraColumns_Front += 1;
                        beforeSearch = beforeSearch.AddDays(-1);
                    }
                }
                else//start week on monday
                {
                    if (beforeSearch.DayOfWeek == DayOfWeek.Monday)
                    {
                        dayFound = true;
                    }
                    else
                    {
                        extraColumns_Front += 1;
                        beforeSearch = beforeSearch.AddDays(-1);
                    }
                }
            }

            int TotalColumns = extraColumns_Front + DateTime.DaysInMonth(currentDay.Year, currentDay.Month);
            extraColumns_End = 7 - (TotalColumns % 7);
            if (extraColumns_End >= 7)
            {
                extraColumns_End = 0;
            }
            TotalColumns += extraColumns_End;
            int rows = TotalColumns / 7;
            int columns = 7;

            int dateTileSize_Height = (Convert.ToInt32(calendar.Height) - DaysOfWeekHeight) / rows;
            int dateTileSize_Width = Convert.ToInt32(calendar.Width) / columns;
            int rowXY = 0;

            int ColumnXY = 0;
            int CounterFills;

            #region DaysOfWeek Header
            string[] DaysOfWeek;
            if (GlobalVariables.calendarStartSunday)//starts Sunday
            {
                DaysOfWeek = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            }
            else//starts Monday
            {
                DaysOfWeek = new string[] {  "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" , "Sunday" };
            }

            for (int DayOfWeek = 0; DayOfWeek < 7; DayOfWeek++)
            {
                StackPanel DayOfWeekPanel = new StackPanel()
                {
                    Height = DaysOfWeekHeight,
                    Width = dateTileSize_Width,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(ColumnXY, rowXY, 0, 0),
                    IsEnabled = false,
                    Background = gvars.ThemeColors[GlobalVariables.chosenTheme][11] as Brush
                };
                DayOfWeekPanel.Children.Add(
                    new Label()
                    {
                        Content = DaysOfWeek[DayOfWeek],
                        Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush
                    });
                calendar.Children.Add(DayOfWeekPanel);
                ColumnXY += dateTileSize_Width;

            }
            rowXY += DaysOfWeekHeight;
            ColumnXY = 0;
            #endregion

            #region Before

            for (CounterFills = 1; CounterFills <= extraColumns_Front; CounterFills++)
            {
                StackPanel DayPanel = new StackPanel()
                {
                    Height = dateTileSize_Height,
                    Width = dateTileSize_Width,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(ColumnXY, rowXY, 0, 0),
                    IsEnabled = false
                };

                DayPanel.Children.Add(new Label() { Content = beforeSearch.ToString("dd") });
                beforeSearch = beforeSearch.AddDays(1);
                calendar.Children.Add(DayPanel);
                ColumnXY += dateTileSize_Width;
            }
            #endregion

            DateTime DuringSearch = currentDay.AddDays(-(Convert.ToDouble(currentDay.Day) - 1));

            #region Middle
            for (int counterRows = 1; counterRows <= rows; counterRows++)
            {
                bool escaped = false;
                for (int counterColumns = CounterFills; counterColumns <= columns; counterColumns++)
                {
                    Border DayBorder = new Border()
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1, 1, 1, 1),
                        CornerRadius = new CornerRadius(1, 1, 1, 1)
                    };

                    StackPanel DayPanel = new StackPanel()
                    {
                        Height = dateTileSize_Height - 1,
                        Width = dateTileSize_Width - 1,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(ColumnXY, rowXY, 0, 0),
                        Background = gvars.ThemeColors[GlobalVariables.chosenTheme][5] as Brush,
                    };
                    DayBorder.Child = DayPanel;
                    DayPanel.MouseEnter += new MouseEventHandler(this.hoverOver_ColorChange);
                    DayPanel.MouseLeave += new MouseEventHandler(this.hoverLeave_ColorChange);
                    DayPanel.MouseLeftButtonUp += new MouseButtonEventHandler(this.mouseLeftButtonUp_DisplayDay);
                    DayPanel.Children.Add(
                        new Label()
                        {
                            Content = DuringSearch.ToString("dd"),
                            Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush,
                        });
                    //get user appointments
                    List<User_Appointments> appoints = new List<User_Appointments>();

                    if (!string.IsNullOrEmpty(GlobalVariables.connectionString))
                    {
                         appoints = bSQL.GetDailyAppointmentsForUser(GlobalVariables.currentUser,
                                   Convert.ToDateTime(string.Join(" ", ((DayPanel.Children[0] as Label).Content.ToString() + " " +
                                   ((MainGrid.Children[2] as Grid).Children[0] as Label).Content.ToString()))),
                                   true);                                    
                    }
                    else
                    {
                        appoints = xmlClass.GetDailyAppointmentsForUser(GlobalVariables.currentUser,
                                   Convert.ToDateTime(string.Join(" ", ((DayPanel.Children[0] as Label).Content.ToString() + " " +
                                   ((MainGrid.Children[2] as Grid).Children[0] as Label).Content.ToString()))));
                    }
                    
                    if (appoints.Count != 0)
                    {
                        foreach (User_Appointments tempAppoint in appoints)
                        {
                            Border DayPreviewBorder =
                                new Border()
                                {
                                    BorderThickness = new Thickness(1, 1, 1, 1),
                                    CornerRadius = new CornerRadius(5, 5, 5, 5),
                                    Background = gvars.ThemeColors[GlobalVariables.chosenTheme][10] as Brush
                                };
                            DayPreviewBorder.Child =
                                new Label()
                                {
                                    Content = tempAppoint.Appointment_DateTime.ToString("HH:mm - ") + tempAppoint.Appointment_Header,
                                    Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush
                                };
                            double theHeight = (DayPreviewBorder.Child as Label).Height;
                            DayPanel.Children.Add(DayPreviewBorder);
                        }
                    }

                    calendar.Children.Add(DayBorder);
                    ColumnXY += dateTileSize_Width;
                    if (DuringSearch.AddDays(1).Month != DuringSearch.Month)
                    {
                        DuringSearch = DuringSearch.AddDays(1);
                        escaped = true;
                        break;
                    }
                    else
                    {
                        DuringSearch = DuringSearch.AddDays(1);
                    }

                }
                if (escaped == false)
                {
                    CounterFills = 1;
                    ColumnXY = 0;
                    rowXY += dateTileSize_Height;
                }
            }
            #endregion

            DateTime AfterSearch = DuringSearch.AddDays(-(Convert.ToDouble(DuringSearch.Day) - 1));

            #region After
            for (int counterColumns = 1; counterColumns <= extraColumns_End; counterColumns++)
            {

                StackPanel DayPanel = new StackPanel()
                {
                    Height = dateTileSize_Height,
                    Width = dateTileSize_Width,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(ColumnXY, rowXY, 0, 0),
                    IsEnabled = false
                };
                DayPanel.Children.Add(new Label() { Content = AfterSearch.ToString("dd") });


                calendar.Children.Add(DayPanel);
                ColumnXY += dateTileSize_Width;
                AfterSearch = AfterSearch.AddDays(1);

            }
            #endregion

            return true;
        }
        
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainGrid.Height = this.ActualHeight;
            MainGrid.Width = this.ActualWidth;
            if (windowLoaded == true)
            {
                LoadCalendar();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Height = this.ActualHeight;
            MainGrid.Width = this.ActualWidth;
            windowLoaded = true;
            LoadCalendar();
        }

        void MonthChange(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton.Content.ToString() == "<")
            {
                currentDay = currentDay.AddMonths(-1);
                LoadCalendar();
            }
            else
            {
                currentDay = currentDay.AddMonths(1);
                LoadCalendar();
            }

        }

        #region Dynamic Event Handlers

        void hoverOver_ColorChange(object sender, MouseEventArgs e)
        {
            StackPanel chosenDay = sender as StackPanel;
            chosenDay.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][6] as Brush;
            (chosenDay.Children[0] as Label).Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][7] as Brush;

            double totalWidth = MainGrid.Width;
            double totalHeight = MainGrid.Height;
            double centerWidth = chosenDay.Margin.Left + (chosenDay.Width / 2);
            double centerHeight = chosenDay.Margin.Top + (chosenDay.Height / 2);
            double toTake_Top = 0;
            double toTake_Left = 0;

            #region DayPreviewCode
            if (enableDayPreview)
            {
                int dateTileSize_Height = 200;
                int dateTileSize_Width = 300;
                //width
                if ((chosenDay.Margin.Left + chosenDay.Width + dateTileSize_Width) > totalWidth)
                {
                    //place it left           
                    toTake_Left = centerWidth - (chosenDay.Width + ((dateTileSize_Width / 4) * 3));
                }
                else
                {
                    //place it right
                    toTake_Left = centerWidth + chosenDay.Width;
                }

                //Height
                if ((centerHeight + (((dateTileSize_Height / 2) / 4) * 3)) > totalHeight)
                {
                    toTake_Top = chosenDay.Margin.Top + (((dateTileSize_Height / 2) / 4) * 3);
                }
                else
                {
                    toTake_Top = chosenDay.Margin.Top;
                }


                StackPanel DayPreview = new StackPanel()
                {
                    Height = dateTileSize_Height,
                    Width = dateTileSize_Width,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(toTake_Left, toTake_Top, 0, 0),
                };

                List<User_Appointments> appoints =
                xmlClass.GetDailyAppointmentsForUser(GlobalVariables.currentUser, Convert.ToDateTime(string.Join(" ", ((chosenDay.Children[0] as Label).Content.ToString() + " " +
                ((MainGrid.Children[2] as Grid).Children[0] as Label).Content.ToString()))));
                if (appoints.Count != 0)
                {
                    foreach (User_Appointments tempAppoint in appoints)
                    {
                        Border DayPreviewBorder = new Border() { BorderThickness = new Thickness(1, 1, 1, 1), CornerRadius = new CornerRadius(5, 5, 5, 5) };
                        DayPreviewBorder.Child = new Label() { Content = tempAppoint.Appointment_DateTime.ToString("HH:mm - ") + tempAppoint.Appointment_Header };
                        DayPreview.Children.Add(DayPreviewBorder);
                    }
                }
                MainGrid.Children.Add(DayPreview);

            }
            #endregion
        }

        void hoverLeave_ColorChange(object sender, MouseEventArgs e)
        {
            StackPanel chosenDay = sender as StackPanel;
            chosenDay.Background = gvars.ThemeColors[GlobalVariables.chosenTheme][5] as Brush;
            (chosenDay.Children[0] as Label).Foreground = gvars.ThemeColors[GlobalVariables.chosenTheme][8] as Brush;

            if (enableDayPreview)
            {
                MainGrid.Children.RemoveAt(MainGrid.Children.Count - 1);
            }
        }

        void mouseLeftButtonUp_DisplayDay(object sender, MouseButtonEventArgs e)
        {
            DateTime chosenDate = Convert.ToDateTime(string.Join(" ", (((sender as StackPanel).Children[0] as Label).Content.ToString() + " " +
                    ((MainGrid.Children[2] as Grid).Children[0] as Label).Content.ToString())));
            Calendar_Appointments calAppoint = new Calendar_Appointments(GlobalVariables.currentUser, chosenDate);

            calAppoint.Show();
            this.Hide();
        }

        //File items
        private void MenuItem3_Click(object sender, RoutedEventArgs e)//close app
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuItem2_Click(object sender, RoutedEventArgs e)//Log out
        {
            Login login = new Login();
            login.Show();
            GlobalVariables.currentUser = "";
            this.Hide();
        }

        private void MenuItem1_Click(object sender, RoutedEventArgs e)//Users
        {
            User_Management userManagement = new User_Management();
            userManagement.Show();

            this.Hide();
        }

        private void Theme2Selection_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.chosenTheme = 1;
            xmlClass.UpdateAppSettingsXML(GlobalVariables.chosenTheme, GlobalVariables.calendarStartSunday, GlobalVariables.connectionString, GlobalVariables.ranSetup);
            LoadCalendar();
        }

        private void Theme1Selection_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.chosenTheme = 0;
            xmlClass.UpdateAppSettingsXML(GlobalVariables.chosenTheme, GlobalVariables.calendarStartSunday, GlobalVariables.connectionString, GlobalVariables.ranSetup);
            LoadCalendar();
        }

        private void MenuCalendar_DayOfWeekStarts_Sunday_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.calendarStartSunday = true;
            xmlClass.UpdateAppSettingsXML(GlobalVariables.chosenTheme, GlobalVariables.calendarStartSunday, GlobalVariables.connectionString, GlobalVariables.ranSetup);
            LoadCalendar();
        }

        private void MenuCalendar_DayOfWeekStarts_Monday_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.calendarStartSunday = false;
            xmlClass.UpdateAppSettingsXML(GlobalVariables.chosenTheme, GlobalVariables.calendarStartSunday, GlobalVariables.connectionString, GlobalVariables.ranSetup);
            LoadCalendar();
        }
        #endregion
        
        #region UpdaterThread
        void checkForUpdates()
        {

        }
        #endregion

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
