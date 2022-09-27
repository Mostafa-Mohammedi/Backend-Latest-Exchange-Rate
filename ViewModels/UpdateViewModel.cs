namespace ViewModels
{
    public partial class UpdateViewModel
    {
        // is in chage of displaying the date for for rates update time and currency
        //output a list of updated currency
        public UpdateViewModel()
        {
            RatesUpdates = new HashSet<RatesUpdateViewModel>();


        }

        public string? DateUpdate { get; set; }
        public string? Base { get; set; }
        public string? Timestamp { get; set; }

        public virtual ICollection<RatesUpdateViewModel> RatesUpdates { get; set; }

    }
}