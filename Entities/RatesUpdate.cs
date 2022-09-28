using Microsoft.VisualBasic;

namespace Entities
//Automatically created with the database first approach
//Is in charge of showing the equivalent rate to the selected currency

{
    public partial class RatesUpdate
    {
        public int IdRate { get; set; }
        public string? Currency { get; set; }
        public decimal? Amount { get; set; }
        public int IdUpdate { get; set; }

        public virtual Update IdUpdateNavigation { get; set; } = null!;                           

    }
}