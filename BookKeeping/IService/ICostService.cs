using BookKeeping.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookKeeping.IService
{
    public interface ICostService
    {
        List<StatementAccounts> getMonthData(string date);

        List<Categorys> getCategorys();
    }
}
