using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class HoursLogDTO
    {
        public int Id { get; set; }

        public int Hours { get; set; }

        //DTO
        public int UserID { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
