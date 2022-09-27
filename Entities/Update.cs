using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public partial class Update
    {
        // The Update class is in charge of updating the currency



        public Update()
        {
            RatesUpdates = new HashSet<RatesUpdate>();                                           

            DateUpdate = DateTime.Now;                                                          


        }

        public int IdUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string? Base { get; set; }
        public string? Timestamp { get; set; }

        public virtual ICollection<RatesUpdate> RatesUpdates { get; set; }                     



    }
}
