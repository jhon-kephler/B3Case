using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Case.Domain.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public string Status { get; private set; }
        public DateTime Date { get; private set; }
    }
}
