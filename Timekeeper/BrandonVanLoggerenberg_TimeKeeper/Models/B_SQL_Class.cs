using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandonVanLoggerenberg_TimeKeeper
{
    class B_SQL_Class
    {
        public B_SQL_Class()
        {

        }

        #region User
        public B_Users Get_User(string Username, string Password)
        {
            B_Users returnUser = new B_Users();
            SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
            try
            {
                string usernameExists = String.Empty;
                scon.Open();
                SqlCommand scom = new SqlCommand(@"Select Username from Users where Username = @Val_Username and Password = @Val_Password", scon);
                scom.Parameters.AddWithValue("@Val_Username", Username);
                scom.Parameters.AddWithValue("@Val_Password", Password);
                SqlDataReader sread = scom.ExecuteReader();
                while (sread.Read())
                {
                    returnUser.username = sread["Username"].ToString();
                }
                sread.Close();
            }
            catch (Exception ex) { }
            finally
            {
                scon.Close();
            }
            return returnUser;
        }

        public List<B_Users> Get_AllUsers()
        {
            List<B_Users> returnUsers = new List<B_Users>();
            SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
            try
            {
                string usernameExists = String.Empty;
                scon.Open();
                SqlCommand scom = new SqlCommand(@"Select Username from Users", scon);
                SqlDataReader sread = scom.ExecuteReader();
                while (sread.Read())
                {
                    B_Users returnUser = new B_Users();
                    returnUser.username = sread["Username"].ToString();
                    returnUsers.Add(returnUser);
                }
                sread.Close();
            }
            catch (Exception ex) { }
            finally
            {
                scon.Close();
            }
            return returnUsers;
        }

        public bool Insert_User(string Username, string Password)
        {
            bool sReturn = false;
            SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
            try
            {
                scon.Open();
                SqlCommand scom = new SqlCommand(@"select Username from Users where Username = @Val_Username", scon);
                scom.Parameters.AddWithValue("@Val_Username", Username);
                SqlDataReader sdr = scom.ExecuteReader();
                string usr = string.Empty;
                while (sdr.Read())
                {
                    usr = sdr["Username"].ToString();
                }
                sdr.Close();

                if (!string.IsNullOrEmpty(usr))
                {
                    sReturn = false;
                }
                else
                {
                    SqlCommand scom1 = new SqlCommand(@"insert into Users ( Username, Password) values ( @Val_Username, @Val_Password)", scon);
                    scom1.Parameters.AddWithValue("@Val_Username", Username);
                    scom1.Parameters.AddWithValue("@Val_Password", Password);
                    scom1.ExecuteScalar();
                    sReturn = true;
                }
            }
            catch (Exception ex)
            {
                sReturn = false;
            }
            finally
            {
                scon.Close();
            }
            return sReturn;
        }

        public bool Update_User(string UsernameOLD, string UsernameNEW)
        {
            bool sReturn = false;
            SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
            try
            {
                string usernameExists = String.Empty;
                scon.Open();
                SqlCommand scom = new SqlCommand(@"update Users set Username = @Val_UsernameNEW where Username = @Val_UsernameOLD", scon);
                scom.Parameters.AddWithValue("@Val_UsernameOLD", UsernameOLD);
                scom.Parameters.AddWithValue("@Val_UsernameNEW", UsernameNEW);
                scom.ExecuteScalar();
                sReturn = true;
            }
            catch (Exception ex)
            {
                sReturn = false;
            }
            finally
            {
                scon.Close();
            }
            return sReturn;
        }

        public bool Update_Password(string Username, string Password)
        {
            bool sReturn = false;
            SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
            try
            {
                string usernameExists = String.Empty;
                scon.Open();
                SqlCommand scom = new SqlCommand(@"update Users set Password = @Val_Password where Username = @Val_Username", scon);
                scom.Parameters.AddWithValue("@Val_Username", Username);
                scom.Parameters.AddWithValue("@Val_Password", Password);
                scom.ExecuteScalar();
                sReturn = true;
            }
            catch (Exception ex)
            {
                sReturn = false;
            }
            finally
            {
                scon.Close();
            }
            return sReturn;
        }

        public bool Delete_User(string Username)
        {
            bool sReturn = false;
            SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
            try
            {
                string usernameExists = String.Empty;
                scon.Open();
                SqlCommand scom = new SqlCommand(@"delete from Users where Username = @Val_Username", scon);
                scom.Parameters.AddWithValue("@Val_Username", Username);
                scom.ExecuteScalar();
                sReturn = true;
            }
            catch (Exception ex)
            {
                sReturn = false;
            }
            finally
            {
                scon.Close();
            }
            return sReturn;
        }
        #endregion

        #region Appointments
        public List<User_Appointments> GetDailyAppointmentsForUser(string username, DateTime chosenDate, bool isDay)
        {
            List<User_Appointments> uApp = new List<User_Appointments>();
            SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
            try
            {
                string sQuery = @"Select a.Appointment_ID ," +
                                " a.Appointment_Date ," +
                                " a.Appointment_Time ," +
                                " a.Company_IDs," +
                                " a.Appointment_Header," +
                                " a.Appointment_Description," +
                                " a.Expected_Duration from Appointments as a " +
                                " left join AppointmentsUsers as au on a.Appointment_ID = au.Appointment_ID" +
                                " where au.Username = @Val_Username  and " +
                                " a.Appointment_Date = @Val_Appointment_Date" +
                                " and au.Accepted = 1";

                scon.Open();
                SqlCommand scom = new SqlCommand(sQuery, scon);
                scom.Parameters.AddWithValue("@Val_Username", username);
                scom.Parameters.AddWithValue("@Val_Appointment_Date", chosenDate.ToString("yyyy-MM-dd"));
                scom.CommandType = System.Data.CommandType.Text;
                SqlDataReader sread = scom.ExecuteReader();
                while (sread.Read())
                {
                    User_Appointments returnAppoint = new User_Appointments();
                    returnAppoint.Appointment_ID = Convert.ToInt32(sread["Appointment_ID"].ToString());
                    returnAppoint.Appointment_DateTime = Convert.ToDateTime(Convert.ToDateTime(sread["Appointment_Date"]).ToString("yyyy-MM-dd") + " " + sread["Appointment_Time"].ToString());
                    returnAppoint.Company_IDs = sread["Company_IDs"].ToString().Split(",".ToCharArray()).ToList();
                    returnAppoint.Appointment_Header = sread["Appointment_Header"].ToString();
                    returnAppoint.Appointment_Description = sread["Appointment_Description"].ToString();
                    returnAppoint.Expected_Duration = new TimeSpan(0, Convert.ToInt16(sread["Expected_Duration"].ToString()), 0);
                    uApp.Add(returnAppoint);
                }
                sread.Close();

                foreach (User_Appointments uap in uApp)
                {
                    SqlCommand scom1 = new SqlCommand(@"Select Username from AppointmentsUsers where Appointment_ID = @Val_Appointment_ID ", scon);
                    scom1.CommandType = System.Data.CommandType.Text;
                    scom1.Parameters.AddWithValue("@Val_Appointment_ID", uap.Appointment_ID);
                    SqlDataReader sread1 = scom1.ExecuteReader();
                    List<string> attendees = new List<string>();
                    while (sread1.Read())
                    {
                        string testString = sread1["Username"].ToString();
                        attendees.Add(testString);
                    }
                    uap.usernames = attendees;
                    sread1.Close();
                }
            }
            catch (Exception ex) { }
            finally
            {
                scon.Close();
            }
            return uApp;
        }

        public bool Insert_Appointments(User_Appointments appointments)
        {
            bool sReturn = false;
            List<User_Appointments> uApp = new List<User_Appointments>();
            SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
            try
            {
                int appointmentID = 0;
                DateTime insertionDate = DateTime.Now;
                scon.Open();
                SqlCommand scom = new SqlCommand(@"insert into Appointments (Appointment_Date, Appointment_Time, Usernames, Company_IDs, Appointment_Header, Appointment_Description, Expected_Duration) values (@Val_Appointment_Date, @Val_Appointment_Time, @Val_Usernames, @Val_Company_IDs, @Val_Appointment_Header, @Val_Appointment_Description, @Val_Expected_Duration)", scon);
                scom.Parameters.AddWithValue("@Val_Appointment_Date", insertionDate.ToString("yyyy-MM-dd"));
                scom.Parameters.AddWithValue("@Val_Appointment_Time", insertionDate.ToString("HH:mm"));
                scom.Parameters.AddWithValue("@Val_Usernames", "");//insert users into AppointmentUsers once this completes
                scom.Parameters.AddWithValue("@Val_Company_IDs", "");
                scom.Parameters.AddWithValue("@Val_Appointment_Header", appointments.Appointment_Header);
                scom.Parameters.AddWithValue("@Val_Appointment_Description", appointments.Appointment_Description);
                scom.Parameters.AddWithValue("@Val_Expected_Duration", appointments.Expected_Duration.TotalMinutes);
                scom.CommandType = System.Data.CommandType.Text;
                scom.ExecuteNonQuery();

                SqlCommand scom1 = new SqlCommand(@"select Appointment_ID from Appointments where Appointment_Date = @Val_Appointment_Date and Appointment_Time = @Val_Appointment_Time", scon);
                scom1.Parameters.AddWithValue("@Val_Appointment_Date", insertionDate.ToString("yyyy-MM-dd"));
                scom1.Parameters.AddWithValue("@Val_Appointment_Time", insertionDate.ToString("HH:mm"));
                scom1.CommandType = System.Data.CommandType.Text;
                appointmentID = Convert.ToInt32(scom1.ExecuteScalar());

                SqlCommand scom2 = new SqlCommand(@"update Appointments set Appointment_Date = @Val_Appointment_Date, Appointment_Time = @Val_Appointment_Time where Appointment_ID = @Val_Appointment_ID", scon);
                scom2.CommandType = System.Data.CommandType.Text;
                scom2.Parameters.AddWithValue("@Val_Appointment_Date", appointments.Appointment_DateTime.ToString("MM/dd/yyyy"));
                scom2.Parameters.AddWithValue("@Val_Appointment_Time", appointments.Appointment_DateTime.ToString("HH:mm"));
                scom2.Parameters.AddWithValue("@Val_Appointment_ID", appointmentID);
                scom2.ExecuteNonQuery();

                foreach (string insertionUser in appointments.usernames)
                {
                    SqlCommand scom3 = new SqlCommand(@"insert into AppointmentsUsers values (@Val_Appointment_ID, @Val_Username, @Val_Accepted)", scon);
                    scom3.Parameters.AddWithValue("@Val_Appointment_ID", appointmentID);
                    scom3.Parameters.AddWithValue("@Val_Username", insertionUser);
                    if (GlobalVariables.currentUser == insertionUser)
                    {
                        scom3.Parameters.AddWithValue("@Val_Accepted", true);
                    }
                    else
                    {
                        scom3.Parameters.AddWithValue("@Val_Accepted", false);
                    }
                    scom3.CommandType = System.Data.CommandType.Text;
                    scom3.ExecuteNonQuery();
                }



                sReturn = true;
            }
            catch (Exception ex) { }
            finally
            {
                scon.Close();
            }
            return sReturn;
        }

        public bool Update_Appointments(User_Appointments appointmentOld, User_Appointments appointmentNew)
        {
            bool sReturn = false;
            SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
            try
            {
                scon.Open();
                SqlCommand scom = new SqlCommand(@"update Appointments set Appointment_Date = @Val_Appointment_Date, " +
                                                                           " Appointment_Time = @Val_Appointment_Time, " +
                                                                           " Appointment_Header = @Val_Appointment_Header," +
                                                                           " Appointment_Description = @Val_Appointment_Description," +
                                                                           " Expected_Duration = @Val_Expected_Duration " +
                                                                           " where Appointment_ID = @Val_Appointment_ID", scon);
                scom.Parameters.AddWithValue("@Val_Appointment_Date", appointmentNew.Appointment_DateTime.ToString("MM/dd/yyyy"));
                scom.Parameters.AddWithValue("@Val_Appointment_Time", appointmentNew.Appointment_DateTime.ToString("HH:mm"));
                scom.Parameters.AddWithValue("@Val_Appointment_Header", appointmentNew.Appointment_Header);
                scom.Parameters.AddWithValue("@Val_Appointment_Description", appointmentNew.Appointment_Description);
                scom.Parameters.AddWithValue("@Val_Expected_Duration", appointmentNew.Expected_Duration.TotalMinutes);
                scom.Parameters.AddWithValue("@Val_Appointment_ID", appointmentOld.Appointment_ID);
                scom.CommandType = System.Data.CommandType.Text;
                scom.ExecuteNonQuery();

                SqlCommand scom1 = new SqlCommand(@"delete from AppointmentsUsers where Appointment_ID = @Val_Appointment_ID", scon);
                scom1.Parameters.AddWithValue("@Val_Appointment_ID", appointmentOld.Appointment_ID);
                scom1.CommandType = System.Data.CommandType.Text;
                scom1.ExecuteNonQuery();

                foreach (string insertionUser in appointmentNew.usernames)
                {
                    SqlCommand scom2 = new SqlCommand(@"insert into AppointmentsUsers values (@Val_Appointment_ID, @Val_Username, @Val_Accepted)", scon);
                    scom2.Parameters.AddWithValue("@Val_Appointment_ID", appointmentOld.Appointment_ID);
                    scom2.Parameters.AddWithValue("@Val_Username", insertionUser);
                    if (GlobalVariables.currentUser == insertionUser)
                    {
                        scom2.Parameters.AddWithValue("@Val_Accepted", true);
                    }
                    else
                    {
                        scom2.Parameters.AddWithValue("@Val_Accepted", false);
                    }
                    scom2.CommandType = System.Data.CommandType.Text;
                    scom2.ExecuteNonQuery();
                }

                sReturn = true;
            }
            catch (Exception ex) { }
            finally
            {
                scon.Close();
            }
            return sReturn;
        }

        public bool Delete_Appointments(string Username)
        {

            return true;
        }

        public int Get_UnacceptedAppointmentsCount(string Username)
        {
            int sReturn = 0;
            SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
            try
            {
                scon.Open();
                SqlCommand scom1 = new SqlCommand(@"Select Count(Appointment_ID) from AppointmentsUsers where Username = @Val_Username and  Accepted = 0", scon);
                scom1.CommandType = System.Data.CommandType.Text;
                scom1.Parameters.AddWithValue("@Val_Username", Username);
                sReturn = Convert.ToInt32(scom1.ExecuteScalar().ToString());
            }
            catch (Exception ex) { }
            finally
            {
                scon.Close();
            }
            return sReturn;
        }

        public List<User_Appointments> Get_UnacceptedAppointments(string Username)
        {
            List<User_Appointments> uApp = new List<User_Appointments>();
            SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
            try
            {
                string sQuery = @"Select a.Appointment_ID ," +
                                " a.Appointment_Date ," +
                                " a.Appointment_Time ," +
                                " a.Company_IDs," +
                                " a.Appointment_Header," +
                                " a.Appointment_Description," +
                                " a.Expected_Duration from Appointments as a " +
                                " left join AppointmentsUsers as au on a.Appointment_ID = au.Appointment_ID" +
                                " where au.Username = @Val_Username  and " +
                                " au.Accepted = 0";

                scon.Open();
                SqlCommand scom = new SqlCommand(sQuery, scon);
                scom.Parameters.AddWithValue("@Val_Username", Username);
                scom.CommandType = System.Data.CommandType.Text;
                SqlDataReader sread = scom.ExecuteReader();
                while (sread.Read())
                {
                    User_Appointments returnAppoint = new User_Appointments();
                    returnAppoint.Appointment_ID = Convert.ToInt32(sread["Appointment_ID"].ToString());
                    returnAppoint.Appointment_DateTime = Convert.ToDateTime(Convert.ToDateTime(sread["Appointment_Date"]).ToString("yyyy-MM-dd") + " " + sread["Appointment_Time"].ToString());
                    returnAppoint.Company_IDs = sread["Company_IDs"].ToString().Split(",".ToCharArray()).ToList();
                    returnAppoint.Appointment_Header = sread["Appointment_Header"].ToString();
                    returnAppoint.Appointment_Description = sread["Appointment_Description"].ToString();
                    returnAppoint.Expected_Duration = new TimeSpan(0, Convert.ToInt16(sread["Expected_Duration"].ToString()), 0);
                    uApp.Add(returnAppoint);
                }
                sread.Close();

                foreach (User_Appointments uap in uApp)
                {
                    SqlCommand scom1 = new SqlCommand(@"Select Username from AppointmentsUsers where Appointment_ID = @Val_Appointment_ID ", scon);
                    scom1.CommandType = System.Data.CommandType.Text;
                    scom1.Parameters.AddWithValue("@Val_Appointment_ID", uap.Appointment_ID);
                    SqlDataReader sread1 = scom1.ExecuteReader();
                    List<string> attendees = new List<string>();
                    while (sread1.Read())
                    {
                        string testString = sread1["Username"].ToString();
                        attendees.Add(testString);
                    }
                    uap.usernames = attendees;
                    sread1.Close();
                }
            }
            catch (Exception ex) { }
            finally
            {
                scon.Close();
            }
            return uApp;
        }

        public bool Accept_Appointment(string Username, List<string> Appointment_IDs)
        {
            bool sReturn = false;
            if (Appointment_IDs.Count > 0)
            {
                SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
                try
                {
                    scon.Open();
                    SqlCommand scom = new SqlCommand(@"update AppointmentsUsers set Accepted = 1 where Username = @Val_Username and Appointment_ID in (" + string.Join(",", Appointment_IDs) + ")", scon);
                    scom.Parameters.AddWithValue("@Val_Username", Username);
                    scom.ExecuteScalar();
                    sReturn = true;
                }
                catch (Exception ex)
                {
                    sReturn = false;
                }
                finally
                {
                    scon.Close();
                }
            }
            else
            {
                sReturn = true;
            }
            return sReturn;
        }

        public bool Reject_Appointment(string Username, List<string> Appointment_IDs)
        {
            bool sReturn = false;
            if (Appointment_IDs.Count > 0)
            {
                SqlConnection scon = new SqlConnection(GlobalVariables.connectionString);
                try
                {
                    scon.Open();
                    SqlCommand scom = new SqlCommand(@"delete from AppointmentsUsers where Username = @Val_Username and Appointment_ID in (" + string.Join(",", Appointment_IDs) + ")", scon);
                    scom.Parameters.AddWithValue("@Val_Username", Username);
                    scom.ExecuteScalar();
                    sReturn = true;
                }
                catch (Exception ex)
                {
                    sReturn = false;
                }
                finally
                {
                    scon.Close();
                }
            }
            else
            {
                sReturn = true;
            }
            return sReturn;
        }
        #endregion
    }
}
