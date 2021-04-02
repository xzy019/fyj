using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Counselor.Model
{
    public class RoadInfo:BaseId
    {
        public string Location { get; set; }
        public DateTime ReportTime { get; set; }
        [SugarColumn(ColumnDataType = "varchar(20)")]
        public string State { get; set; }
        public int CounselorId { get; set; }

        //类型,不映射到数据库
        [SugarColumn(IsIgnore = true)]
        public CounselorInfo CounselorInfo { get; set; }
    }
}
