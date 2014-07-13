using System;

namespace Nbugs.OperationApp.Models.KQ
{
    public class KQDayLog
    {
        public string user_id { get; set; }

        public string kq_date { get; set; }

        public DateTime? in_time { get; set; }

        public DateTime? out_time { get; set; }

        public string check_detail { get; set; }

        public string school_id { get; set; }

        public string user_name { get; set; }

        public string cls_or_dept_id { get; set; }

        public string cls_or_dept_name { get; set; }

        public string in_out_flag { get; set; }

        public string kq_position { get; set; }

        public int appCode { get; set; }

        public int tableId { get; set; }
    }
}