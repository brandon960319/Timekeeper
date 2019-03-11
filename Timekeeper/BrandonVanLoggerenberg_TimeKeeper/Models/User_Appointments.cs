using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandonVanLoggerenberg_TimeKeeper
{
    class User_Appointments
    {
        public int Appointment_ID { get; set; }
        public List<string> usernames { get; set; }// link to user        
        public List<string> Company_IDs { get; set; } // link to company

        public string Appointment_Header { get; set; }
        public string Appointment_Description { get; set; }
        public DateTime Appointment_DateTime { get; set; }
        public TimeSpan Expected_Duration { get; set; }
    }
}
