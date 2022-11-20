using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Solutions_Task.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Namber { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
