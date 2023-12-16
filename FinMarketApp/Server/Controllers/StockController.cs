using FinMarketApp.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinMarketApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FinMarketApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public StockController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // Endpoint to get all stocks (this returns a csv file): https://www.alphavantage.co/query?function=LISTING_STATUS&apikey=demo


        // ================================ Get Stocks ================================
        [HttpGet]
        public ActionResult<List<Stocks>> GetStocks()
        {
            try
            {
                string wwwrootPath = Path.Combine(_hostingEnvironment.ContentRootPath, "..", "Client", "wwwroot");
                string filePath = Path.Combine(wwwrootPath, "listing_status.csv");


              /*  Debug.WriteLine($"*******************************************");
                Debug.WriteLine($"Reading stocks from {filePath}");

                var lines = System.IO.File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    Debug.WriteLine($"CSV Line: {line}");
                }
*/

                var stocks = System.IO.File.ReadAllLines(filePath)
                    .Skip(1) // Skip the header line
                    .Select(line => StocksFromCsv(line))
                    .Where(stock => stock != null) // Filter out any null entries (invalid lines)
                    .ToList();

                return stocks;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading stocks from CSV: {ex.Message}");
                return BadRequest("Failed to read stocks from CSV. See server logs for details.");
            }
        }

        private Stocks StocksFromCsv(string line)
        {
            var values = line.Split(',');

            return new Stocks
            {
                Symbol = values[0],
                Name = values[1],
                Exchange = values[2],
                AssetType = values[3],
                IPODate = values[4],
                DelistingDate = values[5],
                Status = values[6]
            };
        }
    }
}
