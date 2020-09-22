using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
