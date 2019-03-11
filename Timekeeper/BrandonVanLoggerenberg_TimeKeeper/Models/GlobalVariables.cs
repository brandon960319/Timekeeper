using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BrandonVanLoggerenberg_TimeKeeper
{
    class GlobalVariables
    {
        
        #region GlobalTempSettings

        public static string currentUser { get; set; }
        public static int chosenTheme { get; set; }
        public static bool calendarStartSunday { get; set; }
        public static string connectionString { get; set; }
        public static bool ranSetup { get; set; }

        #endregion

        #region themes

        public List<List<object>> ThemeColors = new List<List<object>>()
        {
            #region DarkTheme
            new List<object>()
             {
                 new SolidColorBrush(new Color() { A=255,R=41,G=41,B=41}),        // 0  //Window Main Panel
                                                                   
                 new SolidColorBrush(new Color() { A=255,R=41,G=41,B=41}),        // 1  //Welcome Panel
                 new SolidColorBrush(Colors.White),                               // 2  //Welcome Panel Text
                                                                   
                 new SolidColorBrush(new Color() { A=255,R=24,G=24,B=24}),        // 3  //Header Panel
                 new SolidColorBrush(Colors.White),                               // 4  //Header Text
                                                                   
                 new SolidColorBrush(new Color() { A=255,R=69,G=69,B=69}),        // 5  //Calendar Day Color 1
                 new SolidColorBrush(new Color() { A=255,R=0,G=170,B=222}),       // 6  //Highlite Panel
                 new SolidColorBrush(Colors.Black),                               // 7  //Highlite Text
                 new SolidColorBrush(Colors.White),                               // 8  //Normal Text
                 new SolidColorBrush(Colors.LightBlue),                           // 9  //Buttons
                 new SolidColorBrush(new Color() { A=255,R=185,G=185,B=185}),     // 10 //Borders

                 new SolidColorBrush(new Color() { A=255,R=86,G=86,B=86}),        // 11 //Reusable Headers
                 new SolidColorBrush(Colors.White),                               // 12 //Hyperlink Normal
                 new SolidColorBrush(new Color() { A=255,R=85,G=26,B=255}),       // 13 //Hyperlink Hover
             },
            #endregion

            #region LightTheme
            new List<object>()
             {
                 new SolidColorBrush(Colors.White),                               // 0  //Window Main Panel
                                                                                      
                 new SolidColorBrush(Colors.LightBlue),                           // 1  //Welcome Panel
                 new SolidColorBrush(Colors.Black),                               // 2  //Welcome Panel Text
                                                                                      
                 new SolidColorBrush(Colors.White),                               // 3  //Header Panel
                 new SolidColorBrush(Colors.Black),                               // 4  //Header Text
                                                                                      
                 new SolidColorBrush(Colors.LightGray),                           // 5  //Calendar Day Color 1
                 new SolidColorBrush(new Color() { A=255,R=0,G=170,B=222}),       // 6  //Highlite Panel
                 new SolidColorBrush(Colors.Black),                               // 7  //Highlite Text
                 new SolidColorBrush(Colors.Black),                               // 8  //Normal Text
                 new SolidColorBrush(Colors.LightBlue),                           // 9  //Buttons
                 new SolidColorBrush(new Color() { A=255,R=185,G=185,B=185}),     // 10 //Borders

                 new SolidColorBrush(new Color() { A=255,R=86,G=86,B=86}),        // 11 //Reusable Headers
                 new SolidColorBrush(Colors.White),                               // 12 //Hyperlink Normal
                 new SolidColorBrush(new Color() { A=255,R=85,G=26,B=255}),       // 13 //Hyperlink Hover
             },
            #endregion
        };

        #endregion
    }
}
