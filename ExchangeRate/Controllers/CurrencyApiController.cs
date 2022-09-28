using Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using ViewModels;

namespace ExchangeRate.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CurrencyApiController : ControllerBase
    {
        private readonly string apiKey = "cQFofRVIlT4O9koGQ78A5X5dKzuNlx8A";
        private readonly IRepository<Update> _datarepository;

        public CurrencyApiController(IRepository<Update> repo)
        {
            _datarepository = repo;
        }


        [HttpGet(Name = "GetCurrency")]
        public IActionResult GetCurrency(string baseName = "USD")
        {

            UpdateViewModel? update = null;
            var dataFromDb = _datarepository.GetUpdate(baseName);

            if (dataFromDb == null)
            {
                /*
                 * Taken from API documentation:
                 * https://apilayer.com/marketplace/fixer-api?e=Sign+In&l=Success#details-tab
                 */
                var client = new RestClient($"https://api.apilayer.com/fixer/latest?&base={baseName}");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("apikey", apiKey);

                IRestResponse response = client.Execute(request);

                //taken from the documentation 
                // https://www.newtonsoft.com/json/help/html/deserializeobject.htm 
                // deserialize the json object from the fixer API
                dynamic parseJson = JsonConvert.DeserializeObject(response.Content);

                var newUpdate = new Update();

                newUpdate.Timestamp = parseJson.timestamp;
                newUpdate.Base = parseJson.@base;

                // loop through the json object and get the name and the currency value from the fixer.io and add them to the Update list
                // save tthe data to database
                var rates = parseJson.rates;
                foreach (var item in rates)
                {
                    var key = item.Name;
                    var value = item.First.Value;

                    RatesUpdate rate = new RatesUpdate();
                    rate.Amount = (decimal)value;
                    rate.Currency = key;
                    newUpdate.RatesUpdates.Add(rate);
                }

                update = _datarepository.SaveData(newUpdate);
            }

            else
            {
                update = dataFromDb;
            }



            return Ok(update);
        }
    }
}
