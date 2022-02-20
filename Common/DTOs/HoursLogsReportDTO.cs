using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class HoursLogsReportDTO
    {
        public Dictionary<string, Dictionary<string, int>> Items { get; set; }
    }
}
