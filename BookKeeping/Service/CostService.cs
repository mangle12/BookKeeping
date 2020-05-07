using BookKeeping.IService;
using BookKeeping.Models.DB;
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

        public List<StatementAccounts> getMonthData(string date)
        {
            List<StatementAccounts> list = null;

            string[] months = date.Split('-');
            DateTime startDate = DateTime.Parse(date);
            DateTime endDate = int.Parse(months[1]) < 12 ? DateTime.Parse(months[0] + "-" + (int.Parse(months[1]) + 1).ToString()) : DateTime.Parse((int.Parse(months[0]) + 1).ToString() + "-1");

            list = _dbContext.StatementAccounts.Where(x => x.Date < endDate && x.Date >= startDate).OrderBy(x => x.Date).ToList();

            return list;
        }

        public List<Categorys> getCategorys()
        {
            List<Categorys> list = _dbContext.Categorys.OrderBy(x => x.Sort).ToList();

            return list;
        }
    }
}
