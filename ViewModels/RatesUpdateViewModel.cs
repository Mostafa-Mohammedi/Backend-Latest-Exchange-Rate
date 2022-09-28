using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    
    //Is in charge of representing the parameters that we are going to display for the user
    // are going to show the currency and equal amount

    public partial class RatesUpdateViewModel
    {
        public string? Currency { get; set; }
        public decimal? Amount { get; set; }

    }
}
