using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanWeFixItService;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1/valuations")]
    public class ValuationsController : ControllerBase
    {
        private readonly IDatabaseService _database;

        public ValuationsController(IDatabaseService database)
        {
            _database = database;
        }

        // GET valuation
        public async Task<ActionResult<IEnumerable<MarketValuation>>> Get()
        {

            var marketData = await _database.MarketData();
            var tatalMarketValue = marketData.Where(m => m.Active).Sum(m => m.DataValue);
            var result = new List<MarketValuation>
            {
                new MarketValuation
                {
                    Name = "DataValueTotal",
                    Total = tatalMarketValue
                }
            };
            return Ok(result);
        }
    }
}