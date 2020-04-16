using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie_rekrutacyjne.DB.Entities
{
    public class Client : Entity
    {
        public ICollection<Order> Order { get; set; }
    }
}
