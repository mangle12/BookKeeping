using BookKeeping.IService;
using BookKeeping.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookKeeping.Service
{
    public class CostService : ICostService
    {
        private BookkeepingContext _dbContext;

        public CostService(BookkeepingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StatementAccounts>> GetMonthData(string date)
        {
            List<StatementAccounts> list = null;

            string[] months = date.Split('-');
            DateTime startDate = DateTime.Parse(date);
            DateTime endDate = int.Parse(months[1]) < 12 ? DateTime.Parse(months[0] + "-" + (int.Parse(months[1]) + 1).ToString()) : DateTime.Parse((int.Parse(months[0]) + 1).ToString() + "-1");

            list = await _dbContext.StatementAccounts.Where(x => x.Date < endDate && x.Date >= startDate).OrderBy(x => x.Date).ToListAsync();

            return list;
        }

        public async Task<List<Categorys>> GetCategorys()
        {
            List<Categorys> list = await _dbContext.Categorys.OrderBy(x => x.Sort).ToListAsync();

            return list;
        }
    }
}
