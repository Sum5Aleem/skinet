using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
