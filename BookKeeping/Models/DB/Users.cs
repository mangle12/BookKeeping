using System;
using System.Collections.Generic;

namespace BookKeeping.Models.DB
{
    public partial class Users
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDay { get; set; }
    }
}