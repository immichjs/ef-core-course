using AppConsoleEF.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsoleEF.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public FreightType FreightType { get; set; }
        public OrderStatus Status { get; set; }
        public string Observation { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
