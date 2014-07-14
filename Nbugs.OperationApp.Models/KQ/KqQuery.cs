using System;
using System.ComponentModel.DataAnnotations;
using Nbugs.OperationApp.Models.System;

namespace Nbugs.OperationApp.Models.KQ
{
    public class KqQuery : Query
    {
        public string mobile { get; set; }

        public string machine_id { get; set; }

        public string ori_card_id { get; set; }

        public string school_id { get; set; }
        [DataType(DataType.DateTime,ErrorMessage="请确认输入的是时间!")]
        public DateTime? datetime1 { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "请确认输入的是时间!")]
        public DateTime? datetime2 { get; set; }
    }
}