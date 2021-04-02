using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Counselor.Model
{
    public class WorkInfo:BaseId
    {
        [SugarColumn(ColumnDataType = "varchar(30)")]
        public string Name { get; set; }
        
        public DateTime Time { get; set; }
        public string Place { get; set; }
        public string Reason { get; set; }
        [SugarColumn(ColumnDataType = "varchar(50)")]
        public string Licence { get; set; }
        public int CounselorId { get; set; }


        //类型,不映射到数据库
        [SugarColumn(IsIgnore = true)]
        public CounselorInfo CounselorInfo { get; set; }
    }
}
