using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver_License_DataAccess_Layer
{
    public static class clsEventLogHandler
    {
        /// <summary>
        /// This Function Will Take Care of Exeptions Handeling on SystemLogging
        /// </summary>
        /// <param name="Source"> Your Project Or Application Name</param>
        /// <param name="Message"> You Log Message</param>
        /// <param name="Type"> Your Log Type</param>
        public static void ExeptionsEventLog(string Message, EventLogEntryType Type, string Source = "DLVDApp")
        {
            if (!EventLog.SourceExists(Source))
            {
                EventLog.CreateEventSource(Source, "Application");
            }

            EventLog.WriteEntry(Source, Message, Type);
        }
    }
}
