using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counselor.Model.DTO
{
    public class RoadDTO
    {
        public string Location { get; set; }
        public DateTime ReportTime { get; set; }
        public string State { get; set; }
        public int CounselorName { get; set; }
    }
}
