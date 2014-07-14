using System;

namespace Nbugs.OperationApp.Models.SMS
{
    public class SmsSubmit
    {
        public int id { get; set; }

        public string sender_name { get; set; }

        public string msg_content { get; set; }

        public DateTime create_time { get; set; }

        public DateTime send_time { get; set; }

        public string msg_type { get; set; }

        public int submited_mobile_count { get; set; }
    }
}