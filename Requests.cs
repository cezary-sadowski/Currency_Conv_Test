using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Currency_Conv_Test
{
    public class Requests
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
