using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counselor.Model.DTO
{
    public class WorkDTO
    {
        public string Name { get; set; }

        public DateTime Time { get; set; }
        public string Place { get; set; }
        public string Reason { get; set; }
        public string Licence { get; set; }
        public int CounselorName { get; set; }
    }
}
