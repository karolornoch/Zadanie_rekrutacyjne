using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie_rekrutacyjne.DB.Entities
{
    public class Order : Entity
    {
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}
