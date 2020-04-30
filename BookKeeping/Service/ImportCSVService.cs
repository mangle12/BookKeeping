using BookKeeping.IService;
using BookKeeping.Models.DB;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeping.Service
{
    public class ImportCSVService : IImportCSVService
    {
        private BookkeepingContext _dbContext;

        public ImportCSVService(BookkeepingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> InsertDataFromCSV(IFormFile file)
        {
            int index = 0;
            try
            {
                DataTable dt = ConvertCSVtoDataTable(file);

                foreach (DataRow row in dt.Rows)
                {
                    index++;

                    StatementAccounts sa = new StatementAccounts();
                    sa.Uid = Guid.NewGuid();
                    sa.Id = int.Parse(row["Id"].ToString());
                    sa.Money = int.Parse(row["金額"].ToString());
                    sa.Category = row["分類"].ToString();
                    sa.SubCategory = row["子分類"].ToString();

                    string rowDate = row["日期"].ToString();
                    sa.Date = DateTime.Parse(string.Format($"{rowDate.Substring(0, 4)}/{rowDate.Substring(4, 2)}/{rowDate.Substring(6, 2)}"));
                    sa.PayType = row["付款(轉出)"].ToString();
                    sa.Remarks = row["備註"].ToString();

                    _dbContext.StatementAccounts.Add(sa);                    
                }

                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }                       
        }

        public static DataTable ConvertCSVtoDataTable(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(file.OpenReadStream(), Encoding.GetEncoding(950)))
            {
                var aa = sr.ReadLine();
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    if (dt.Rows.Count == 308)
                    {
                        int a = 0;
                    }

                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }


            return dt;
        }
    }
}
