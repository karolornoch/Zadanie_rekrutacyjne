using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie_rekrutacyjne.DB.Entities
{
    public class Product: Entity
    {
        public string Name { get; set; }
        public Decimal NetValue { get; set; }
        public Decimal GrossValue { get; set; }
    }
}
