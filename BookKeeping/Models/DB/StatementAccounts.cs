using System;
using System.Collections.Generic;

namespace BookKeeping.Models.DB
{
    public partial class StatementAccounts
    {
        public Guid Uid { get; set; }
        public int? Id { get; set; }
        public int? Money { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public DateTime? Date { get; set; }
        public string PayType { get; set; }
        public string Remarks { get; set; }
    }
}