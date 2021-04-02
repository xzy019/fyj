using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Counselor.Model
{
    public class NoticeInfo:BaseId
    {
        public string Title { get; set; }
        public DateTime Time { get; set; }
        [SugarColumn(ColumnDataType = "varchar(20)")]
        public string Category { get; set; }
        public string Path { get; set; }
        public int AdminId { get; set; }
        //类型,不映射到数据库
        [SugarColumn(IsIgnore = true)]
        public AdminInfo AdminInfo { get; set; }
    }
}
