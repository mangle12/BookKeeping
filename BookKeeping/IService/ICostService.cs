using BookKeeping.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookKeeping.IService
{
    public interface ICostService
    {
        Task<List<StatementAccounts>> GetMonthData(string date);

        Task<List<Categorys>> GetCategorys();
    }
}
