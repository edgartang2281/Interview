using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanWeFixItService;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1/marketdata")]
    public class MarketDataController : ControllerBase
    {
        private readonly IDatabaseService _database;

        public MarketDataController(IDatabaseService database)
        {
            _database = database;
        }

        // GET market data
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> Get()
        {
            var instruments = await _database.Instruments();
            var marketData = await _database.MarketData();
            var result = from m in marketData join i in instruments on m.Sedol equals i.Sedol
                         select new MarketDataDto
                         {
                             Id = m.Id,
                             InstrumentId = i.Id,
                             Active = m.Active,
                             DataValue = m.DataValue
                         };

            return Ok(result);

        }
    }
}