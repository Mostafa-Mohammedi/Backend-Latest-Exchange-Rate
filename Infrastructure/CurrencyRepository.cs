using Entities;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace Infrastructure
{
    public class CurrencyRepository : IRepository<Update>
    {
        
        private readonly ApplicationDbContext _context;


        public CurrencyRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public UpdateViewModel GetUpdate(string baseName)
        //*
        //we are getting the uptdate currency and the equal exchange rate for the different 
        //accessing the database
        //Select the necessary information to the user
        //*/
        {
            var data = _context.Updates
            .Where(e =>
        e!.Base!.Equals(baseName) &&
        e.DateUpdate!.Value.Date == DateTime.Now.Date
        )
            .Include(e => e.RatesUpdates)
            .AsNoTracking()
            .Select(update => new UpdateViewModel
            {
                Timestamp = update.Timestamp,
                DateUpdate = String.Format("{0:dd/MM/yyyy}", update.DateUpdate),
                Base = update.Base,
                RatesUpdates = update.RatesUpdates.Select(rate => new RatesUpdateViewModel
                {
                    Amount = rate.Amount,
                    Currency = rate.Currency
                }).ToList()
            })
            .FirstOrDefault();
            return data;
        }

        public UpdateViewModel SaveData(Update instance)
        {
            /*
             Here we are returning the Updated currency information from the GetUpdate method 
             */

            _context.Updates.Add(instance);
            _context.SaveChanges();
            return GetUpdate(instance.Base);
        }
    }

}
