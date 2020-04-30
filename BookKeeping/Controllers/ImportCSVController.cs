using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookKeeping.IService;
using BookKeeping.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookKeeping.Controllers
{
    public class ImportCSVController : Controller
    {
        private readonly IImportCSVService _importCSVService;        

        public ImportCSVController(IImportCSVService importCSVService)
        {
            _importCSVService = importCSVService;                    
        }

        public IActionResult Index()
        {
            return View();
        }

        // This action handles the form POST and the upload
        [HttpPost]
        public async Task<IActionResult> OpenCSV(IFormFile file)
        {            
            bool isSuccess = await _importCSVService.InsertDataFromCSV(file);

            if (isSuccess)
            {                
                return RedirectToAction("Index");
            }
            else
            { 
                return StatusCode(500);
            }            
        }
    }
}