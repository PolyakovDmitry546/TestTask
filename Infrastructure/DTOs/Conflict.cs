using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs
{
    public class Conflict
    {
        public string BrigadeCode { get; set; }
        public string[] DevicesSerials { get; set; }
    }
}
