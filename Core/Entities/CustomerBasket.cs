using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public string BasketItems { get; set; }
        public DateTime Expire { get; set; }
    }
}
