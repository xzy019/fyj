using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counselor.Model.DTO
{
    public class NoticeDTO
    {
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Category { get; set; }
        public string Path { get; set; }
        public int AdminName { get; set; }
    }
}
