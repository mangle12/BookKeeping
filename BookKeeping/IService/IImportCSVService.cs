using BookKeeping.Models.DB;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookKeeping.IService
{
    public interface IImportCSVService
    {
        Task<bool> InsertDataFromCSV(IFormFile file);
    }
}
