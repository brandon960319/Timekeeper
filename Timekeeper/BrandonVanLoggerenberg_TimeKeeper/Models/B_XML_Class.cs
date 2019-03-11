using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Globalization;


namespace BrandonVanLoggerenberg_TimeKeeper
{
    class B_XML_Class
    {
        public B_XML_Class()
        {
            // createUsersXML(new List<B_Users>());
        }

        #region Users
        public bool? createUsersXML(List<B_Users> Users)
        {
            bool? returnType = false;
            XElement[] userContents = new XElement[Users.Count];

            int tempUserCount = 0;
            foreach (B_Users tempUser in Users)
            {
                userContents[tempUserCount] = new XElement
                        ("UserInfo",
                            new XAttribute("username", tempUser.username.ToString()),
                            new XAttribute("password", tempUser.password.ToString())
                        );
                tempUserCount++;
            }

            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML"))//creates directory
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\XML");
                }

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml"))//File exists
                {
                    if (XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml").Descendants("Users").Descendants("UserInfo").Count() == 0)
                    {
                        XDocument doc = new XDocument
                        (
                            new XElement
                            ("Users_XML_Document",
                                new XElement
                                ("Users",
                                    userContents
                                )
                            )
                        );
                        doc.Save("Users.xml");
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                        File.Move(AppDomain.CurrentDomain.BaseDirectory + @"\Users.xml", AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Users.xml");

                        if (Users.Count == 0)
                        {
                            return null;
                        }
                    }
                    else//the file exists so we can append to the file
                    {
                        foreach (var tempUsr in Users)
                        {
                            insertUserXML(tempUsr.username, tempUsr.password, false);
                        }
                    }
                }
                else//file doesnt exist so we create it
                {
                    XDocument doc = new XDocument
                        (
                            new XElement
                            ("Users_XML_Document",
                                new XElement
                                ("Users",
                                    userContents
                                )
                            )
                        );
                    doc.Save("Users.xml");
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                    File.Move(AppDomain.CurrentDomain.BaseDirectory + @"\Users.xml", AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Users.xml");

                    if (Users.Count == 0)
                    {
                        return null;
                    }
                }
                returnType = true;
            }
            catch (Exception ex)
            {
                returnType = false;
            }
            return returnType;
        }

        public B_Users getUserFromXML(string username, bool errorchecking)
        {

            B_Users returnUser = new B_Users() { username = "", password = "" };

            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML"))//creates directory
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\XML");
                }

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml"))//checks if file exists
                {
                    if (0 == XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml").Descendants("Users").Descendants("UserInfo").Count())
                    {
                        if (errorchecking)
                        {
                            System.Windows.MessageBox.Show("There are no users set up in the system. Please create a user first", "No Users in the system", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                        }
                    }
                    else
                    {
                        if (XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml").Descendants("UserInfo").Where(tempUser => tempUser.ToString().Remove(0, 20).Split(@"""".ToCharArray())[0] == username).ToList().Count() > 0)//more than 1
                        {
                            returnUser.username = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml").Descendants("UserInfo")
                            .Where(
                            tempUser => tempUser.ToString().Remove(0, 20).Split(@"""".ToCharArray())[0]
                            == username
                            )
                            .ToList()[0].ToString().Remove(0, 20).Split(@"""".ToCharArray())[0];
                        }
                    }
                }
                else
                {
                    if (errorchecking)
                    {
                        System.Windows.MessageBox.Show("There are no users set up in the system. Please create a user first", "No Users in the system", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                    }
                }

            }
            catch (Exception ex)
            {
                //silent
            }
            return returnUser;
        }

        public List<B_Users> getAllUsersFromXML()
        {
            List<B_Users> returnUsers = new List<B_Users>();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml"))
            {
                List<XElement> usersElement = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml").Descendants("UserInfo").ToList();

                if (usersElement.Count > 0)
                {
                    for (int userKeep = 0; userKeep < usersElement.Count; userKeep++)
                    {
                        returnUsers.Add(new B_Users()
                        {
                            username = usersElement[userKeep].ToString().Remove(0, 20).Split(@"""".ToCharArray())[0]
                        });
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("There are no users set up in the system. Please create a user first", "No Users in the system", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("There are no users set up in the system. Please create a user first", "No Users in the system", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }

            return returnUsers;
        }

        public bool insertUserXML(string username, string password, bool errorchecking)
        {
            bool returnType = false;

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML"))//creates directory
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\XML");
            }

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml"))//exists
            {
                B_Users tempUsers = getUserFromXML(username, false);
                if (tempUsers.username == "")
                {
                    XDocument doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                    doc.Descendants("Users").ToList()[0]
                     .Add(
                         new XElement
                            ("UserInfo",
                                new XAttribute("username", username.ToString()),
                                new XAttribute("password", password.ToString())
                            )
                         );
                    doc.Save("Users.xml");
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                    File.Move(AppDomain.CurrentDomain.BaseDirectory + @"\Users.xml", AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Users.xml");
                    returnType = true;
                }
                else//exists
                {
                    if (errorchecking)
                    {
                        System.Windows.MessageBox.Show("Username already exists, the user wasn't created. Please choose another username", "User Already exists", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                    }
                }

            }
            else//create the file and the user
            {
                createUsersXML(new List<B_Users>() { new B_Users() { username = username, password = password } });
                returnType = true;
            }

            return returnType;
        }

        public bool updateUserXML(string usernameOLD, string usernameNEW)
        {
            bool returnType = false;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml"))//exists
            {
                B_Users tempUsers = getUserFromXML(usernameNEW, false);
                if (tempUsers.username == "")
                {
                    XDocument doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                    doc.Descendants("UserInfo").Where(theUsername => theUsername.ToString().Remove(0, 20).Split(@"""".ToCharArray())[0] == usernameOLD).ToList()[0]
                       .Attribute("username").Value = usernameNEW;

                    doc.Save("Users.xml");
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                    File.Move(AppDomain.CurrentDomain.BaseDirectory + @"\Users.xml", AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Users.xml");
                    returnType = true;
                }
                else//New Username Exist
                {
                    System.Windows.MessageBox.Show("Username already exists, the user wasn't created. Please choose another username", "User Already exists", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                }

            }
            else
            {
                System.Windows.MessageBox.Show("There are no users set up in the system. Please create a user first", "No Users in the system", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }

            return returnType;
        }

        public bool updateUserPasswordXML(string username, string password)
        {
            bool returnType = false;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml"))//exists
            {
                    XDocument doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                    doc.Descendants("UserInfo").Where(theUsername => theUsername.ToString().Remove(0, 20).Split(@"""".ToCharArray())[0] == username).ToList()[0]
                       .Attribute("password").Value = password;

                    doc.Save("Users.xml");
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                    File.Move(AppDomain.CurrentDomain.BaseDirectory + @"\Users.xml", AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Users.xml");
                    returnType = true;
                System.Windows.MessageBox.Show("Password Successfully updated.", "Password updated", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }
            else
            {
                System.Windows.MessageBox.Show("The password could not be updated.", "Password not updated", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }

            return returnType;
        }

        public bool deleteUserFromXML(string username)
        {
            bool returnType = false;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml"))//exists
            {
                XDocument doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                doc.Descendants("UserInfo").Where(theUsername => theUsername.ToString().Remove(0, 20).Split(@"""".ToCharArray())[0] == username).ToList()[0]
                   .Remove();

                doc.Save("Users.xml");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                File.Move(AppDomain.CurrentDomain.BaseDirectory + @"\Users.xml", AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Users.xml");
                returnType = true;
                System.Windows.MessageBox.Show("User Successfully Deleted.", "User Deleted", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }
            else
            {
                System.Windows.MessageBox.Show("The user could not be deleted.", "User not deleted", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }

            return returnType;
        }
        #endregion

        #region Login
        public bool Login(B_Users userDetails)
        {
            bool returnType = false;
            if (XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Users.xml").Descendants("UserInfo").Where(theUsername => theUsername.ToString()
            .Remove(0, 20).Split(@"""".ToCharArray())[0] == userDetails.username).ToList()[0].Attribute("password").Value == userDetails.password)
            {
                returnType = true;
            }
            return returnType;
        }
        #endregion

        #region Appointments
        public bool? CreateAppointmentsXML(List<User_Appointments> appointments)
        {
            bool? returnType = false;
            XElement[] userAppointments = new XElement[appointments.Count];

            int tempAppointmentCount = 0;
            foreach (User_Appointments tempAppointment in appointments)
            {
                userAppointments[tempAppointmentCount] =
                    new XElement("Appointment",
                      new XAttribute("Appointment_ID", tempAppointment.Appointment_ID.ToString()),
                      new XAttribute("Appointment_DateTime", tempAppointment.Appointment_DateTime.ToString("MM/dd/yyyy HH:mm")),
                      new XAttribute("Usernames", tempAppointment.usernames[0].ToString()),
                      new XAttribute("Company_IDs", tempAppointment.Company_IDs[0]),
                      new XAttribute("Appointment_Header", tempAppointment.Appointment_Header),
                      new XAttribute("Appointment_Description", tempAppointment.Appointment_Description),
                      new XAttribute("Expected_Duration", tempAppointment.Expected_Duration.TotalMinutes.ToString())
                   );
                tempAppointmentCount++;
            }

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML"))//creates directory
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\XML");
            }

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml"))//File exists
            {
                if (XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml").Descendants("Appointments").Descendants("Appointment").Count() == 0)
                {
                    XDocument doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml");
                    doc.Descendants("Appointments").ToList()[0].Add(
                       userAppointments
                    );
                    doc.Save("Appointments.xml");
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml");
                    File.Move(AppDomain.CurrentDomain.BaseDirectory + @"\Appointments.xml", AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml");
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Appointments.xml");
                    returnType = true;
                }
                else//the file exists so we can append to the file
                {
                    foreach (var tempUsr in appointments)
                    {
                        InsertAppointmentsXML(tempUsr, false);
                    }
                }
            }
            else//file doesnt exist so we create it
            {
                XDocument doc = new XDocument(
                new XElement("Appointments_XML_Document",
                    new XElement("Appointments",
                        userAppointments
                )));
                doc.Save("Appointments.xml");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml");
                File.Move(AppDomain.CurrentDomain.BaseDirectory + @"\Appointments.xml", AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Appointments.xml");
            }
            return returnType;
        }

        public bool InsertAppointmentsXML(User_Appointments appointments, bool errorChecking)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML"))//creates directory
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\XML");
            }
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml"))//File exists
            {
                XDocument doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml");
                List<XElement> listOfAppointments = doc.Descendants("Appointments").Descendants("Appointment").ToList();
                if (listOfAppointments.Count != 0)
                {

                }
                listOfAppointments.Add(
                     new XElement("Appointment",
                          new XAttribute("Appointment_ID", appointments.Appointment_ID.ToString()),
                          new XAttribute("Appointment_DateTime", appointments.Appointment_DateTime.ToString()),//"dd/MM/yyyy HH:mm"
                          new XAttribute("Usernames", string.Join(",", appointments.usernames)),
                          new XAttribute("Company_IDs", string.Join(",", appointments.Company_IDs)),
                          new XAttribute("Appointment_Header", appointments.Appointment_Header),
                          new XAttribute("Appointment_Description", appointments.Appointment_Description),
                          new XAttribute("Expected_Duration", appointments.Expected_Duration.TotalMinutes.ToString())
                       )
                     );
                doc.Descendants("Appointments").Elements("Appointment").Remove();
                doc.Descendants("Appointments").ToList()[0].Add(listOfAppointments);

                doc.Save("Appointments.xml");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml");
                File.Move(AppDomain.CurrentDomain.BaseDirectory + @"\Appointments.xml", AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Appointments.xml");
            }
            else
            {
                CreateAppointmentsXML(new List<User_Appointments> () {  appointments });
            }
            return true;
        }

        public bool UpdateAppointmentsXML(User_Appointments appointmentOld, User_Appointments appointmentNew, bool errorChecking)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML"))//creates directory
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\XML");
            }
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml"))//File exists
            {
                XDocument doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml");
                List<XElement> listOfAppointments = doc.Descendants("Appointments").ToList()[0].Elements("Appointment").ToList();
                XElement theUpdate = listOfAppointments
                .Where(tempSearch => tempSearch.Attribute("Usernames").Value.ToString()            == string.Join(",", appointmentOld.usernames))
                .Where(tempSearch => tempSearch.Attribute("Appointment_DateTime").Value.ToString() == appointmentOld.Appointment_DateTime.ToString(""))
                .ToList()[0];
                listOfAppointments.Remove(theUpdate);
                theUpdate.Attribute("Usernames").Value = string.Join(",", appointmentNew.usernames);
                theUpdate.Attribute("Appointment_Header").Value = appointmentNew.Appointment_Header;
                theUpdate.Attribute("Appointment_Description").Value = appointmentNew.Appointment_Description;
                listOfAppointments.Add(theUpdate);

                doc.Descendants("Appointments").Elements("Appointment").Remove();
                doc.Descendants("Appointments").ToList()[0].Add(listOfAppointments);


                /*
                
                listOfAppointments.Add(
                     new XElement("Appointment",
                          new XAttribute("Appointment_ID", appointments.Appointment_ID.ToString()),
                          new XAttribute("Appointment_DateTime", appointments.Appointment_DateTime.ToString()),//"dd/MM/yyyy HH:mm"
                          new XAttribute("Usernames", string.Join(",", appointments.usernames)),
                          new XAttribute("Company_IDs", string.Join(",", appointments.Company_IDs)),
                          new XAttribute("Appointment_Header", appointments.Appointment_Header),
                          new XAttribute("Appointment_Description", appointments.Appointment_Description),
                          new XAttribute("Expected_Duration", appointments.Expected_Duration.TotalMinutes.ToString())
                       )
                     );
                */

                //doc.Descendants("Appointments").ToList()[0].Add(listOfAppointments);

                doc.Save("Appointments.xml");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml");
                File.Move(AppDomain.CurrentDomain.BaseDirectory + @"\Appointments.xml", AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Appointments.xml");
            }
            else
            {
                CreateAppointmentsXML(new List<User_Appointments>() { appointmentNew });
            }
            return true;
        }

        public List<User_Appointments> GetDailyAppointmentsForUser(string username, DateTime chosenDate)
        {
            List<User_Appointments> appointments = new List<User_Appointments>();

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML"))//creates directory
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\XML");
            }
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml"))//File exists
            {
                foreach (XElement tempElement in XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\Appointments.xml")
                .Descendants("Appointments").ToList()[0].Descendants("Appointment")
                .ToList())
                {
                    //DateTime test = tempElement.Attributes("Appointment_DateTime").ToList()[0].Value.ToString()

                    if ((Convert.ToDateTime(tempElement.Attributes("Appointment_DateTime").ToList()[0].Value.ToString()).Date == chosenDate) &&
                        (tempElement.Attributes("Usernames").ToList()[0].Value.ToString().Split(",".ToCharArray()).ToList().Contains(username) == true))
                    {
                        appointments.Add(
                            new User_Appointments()
                            {
                                Appointment_ID = Convert.ToInt32(tempElement.Attributes("Appointment_ID").ToList()[0].Value.ToString()),
                                Appointment_DateTime = Convert.ToDateTime(tempElement.Attributes("Appointment_DateTime").ToList()[0].Value.ToString()),
                                usernames = tempElement.Attributes("Usernames").ToList()[0].Value.ToString().Split(",".ToCharArray()).ToList(),
                                Company_IDs = tempElement.Attributes("Company_IDs").ToList()[0].Value.ToString().Split(",".ToCharArray()).ToList(),

                                Appointment_Header = tempElement.Attributes("Appointment_Header").ToList()[0].Value.ToString(),
                                Appointment_Description = tempElement.Attributes("Appointment_Description").ToList()[0].Value.ToString(),
                                Expected_Duration = new TimeSpan(0, Convert.ToInt16(tempElement.Attributes("Expected_Duration").ToList()[0].Value.ToString()), 0)
                            }
                        );
                    }
                }
            }
            else
            {
                CreateAppointmentsXML( appointments);
            }
            return appointments;
        }
        #endregion

        #region App Settings
        public bool CreateAppSettingsXML(int chosenTheme, bool SundayFirstDay, string ConnectionString, bool RanSetup)
        {
            bool returnType = false;
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML"))//creates directory
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\XML");
            }
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\XML\App_Config.xml"))//File exists
            {
                if(XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\App_Config.xml").Descendants("ApplicationSettings").Count() != 0)
                {
                    returnType = true;
                }                
            }
            else//doesnt exist so we create it
            {
                XDocument doc = new XDocument(
                    new XElement("ApplicationSettings",
                    new XAttribute("ConnectionString", ConnectionString),
                    new XAttribute("SundayFirstDay", SundayFirstDay),
                    new XAttribute("ChosenTheme", chosenTheme),
                    new XAttribute("RanSetup",RanSetup)
                 ));
                doc.Save("App_Config.xml");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\XML\App_Config.xml");
                File.Move(AppDomain.CurrentDomain.BaseDirectory + @"\App_Config.xml", AppDomain.CurrentDomain.BaseDirectory + @"\XML\App_Config.xml");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\App_Config.xml");
                returnType = true;//created
            }
            return returnType;
        }

        public void UpdateAppSettingsXML(int chosenTheme, bool SundayFirstDay, string ConnectionString, bool RanSetup)
        {
            XDocument doc = new XDocument(
              new XElement("ApplicationSettings",
                        new XAttribute("ConnectionString", ConnectionString),
                        new XAttribute("SundayFirstDay", SundayFirstDay),
                        new XAttribute("ChosenTheme", chosenTheme),
                        new XAttribute("RanSetup", RanSetup)
              ));

            doc.Save("App_Config.xml");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\XML\App_Config.xml");
            File.Move(AppDomain.CurrentDomain.BaseDirectory + @"\App_Config.xml", AppDomain.CurrentDomain.BaseDirectory + @"\XML\App_Config.xml");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\App_Config.xml");

        }

        public void PopulateAppSettings()
        {
            XDocument xdo = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\XML\App_Config.xml");

            GlobalVariables.connectionString = xdo.Descendants("ApplicationSettings").Attributes("ConnectionString").ToList()[0].ToString().Replace(@"ConnectionString=""","").TrimEnd(@"""".ToCharArray());
            
            GlobalVariables.chosenTheme = Convert.ToInt16(xdo.Descendants("ApplicationSettings").Attributes("ChosenTheme").ToList()[0].ToString().Split("=".ToCharArray())[1].Trim(@"""".ToCharArray()));
            if (xdo.Descendants("ApplicationSettings").Attributes("SundayFirstDay").ToList()[0].ToString().Split("=".ToCharArray())[1].Trim(@"""".ToCharArray()) == "false")
            {
                GlobalVariables.calendarStartSunday = false;
            }
            else
            {
                GlobalVariables.calendarStartSunday = true;
            }

            if (xdo.Descendants("ApplicationSettings").Attributes("RanSetup").ToList()[0].ToString().Split("=".ToCharArray())[1].Trim(@"""".ToCharArray()) == "false")
            {
                GlobalVariables.ranSetup = false;
            }
            else
            {
                GlobalVariables.ranSetup = true;
            }

        }
        #endregion
    }
}
