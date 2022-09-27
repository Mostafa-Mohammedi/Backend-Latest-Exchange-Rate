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
                 * reading the API from the documentation:
                 * https://apilayer.com/marketplace/fixer-api?e=Sign+In&l=Success#details-tab
                 * 
                 
                 */
                var client = new RestClient($"https://api.apilayer.com/fixer/latest?&base={baseName}");

                client.Timeout = -1;

                var request = new RestRequest(Method.GET);
                request.AddHeader("apikey", apiKey);

                IRestResponse response = client.Execute(request);


                dynamic parseJson = JsonConvert.DeserializeObject(response.Content);

                var newUpdate = new Update();

                newUpdate.Timestamp = parseJson.timestamp;
                newUpdate.Base = parseJson.@base;

                var rates = parseJson.rates;
                
                //delete later
                Type _t = rates.GetType();

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
