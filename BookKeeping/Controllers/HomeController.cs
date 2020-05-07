using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookKeeping.Models;
using BookKeeping.IService;
using BookKeeping.Models.DB;
using Microsoft.AspNetCore.Http;
using BookKeeping.Tools;

namespace BookKeeping.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICostService _costService;        

        public HomeController(ILogger<HomeController> logger, ICostService costService)
        {
            _logger = logger;
            _costService = costService;            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //當月所有花費
        [HttpPost]
        public IActionResult getMonthData(string date)
        {
            List<StatementAccounts> monthData =  _costService.getMonthData(date);

            HttpContext.Session.SetObjectAsJson("monthData", monthData);

            int? totalCost = getTotalCost();

            return Json(new { datalist = monthData, totalCost = totalCost });
        }

        //記帳分類
        public IActionResult getCategorys()
        {            
            List<Categorys> list = new List<Categorys>();

            if (HttpContext.Session.GetObjectFromJson<List<Categorys>>("categoryList") == null)
            {
                list = _costService.getCategorys();
                HttpContext.Session.SetObjectAsJson("categoryList", list);
            }
            else
            {
                list = HttpContext.Session.GetObjectFromJson<List<Categorys>>("categoryList");
            }                        

            return Ok(list);
        }

        //圓餅圖
        public IActionResult getPieChartData()
        {
            List<PieChart> list = new List<PieChart>();
            List<Categorys> categoryList = HttpContext.Session.GetObjectFromJson<List<Categorys>>("categoryList");

            foreach (Categorys item in categoryList) {
                PieChart pc = new PieChart
                {
                    Label = item.CategoryName,
                    data = Convert.ToDouble(getCategoryCost(item.CategoryName)),
                    color = item.Color
                };

                list.Add(pc);
            }

            return Ok(list);
        }

        //總花費
        private int? getTotalCost()
        {
            List<StatementAccounts> monthData = HttpContext.Session.GetObjectFromJson<List<StatementAccounts>>("monthData");

            return monthData.Sum(x => x.Money);
        }

        //類別花費
        private int? getCategoryCost(string name)
        {
            List<StatementAccounts> monthData = HttpContext.Session.GetObjectFromJson<List<StatementAccounts>>("monthData");

            return monthData.Where(x => x.Category == name).Sum(y => y.Money);
        }
    }
}
