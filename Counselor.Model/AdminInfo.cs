using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Counselor.Model
{
    public class AdminInfo:BaseId
    {
        [SugarColumn(ColumnDataType = "varchar(30)")]
        public string Name { get; set; }
        [SugarColumn(ColumnDataType = "varchar(2)")]
        public string Sex { get; set; }
        [SugarColumn(ColumnDataType = "varchar(20)")]
        public string TelNumber { get; set; }
        public string Address { get; set; }
        public string Units { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(50)")]
        public string AdminName { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(64)")]
        public string AdminPwd { get; set; }
    }
}
