using System;

namespace Nbugs.OperationApp.Models.KQ
{
    public class KQ
    {
        public int kq_id { get; set; }

        //public int appCode { get; set; }
        public string to_mobile { get; set; }

        public DateTime? att_time { get; set; }

        public DateTime? recv_time { get; set; }

        public string ori_card_id { get; set; }

        public string jj_card_id { get; set; }

        public string machine_id { get; set; }

        //public string school_id { get; set; }
        public string school_name { get; set; }

        //public string cls_id { get; set; }
        public string cls_name { get; set; }

        //public string jyb_user_id { get; set; }
        public string user_name { get; set; }

        public string sms_id { get; set; }

        public string jj_type { get; set; }

        public string in_out { get; set; }
    }
}