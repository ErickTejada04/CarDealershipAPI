namespace DealershipAPI.Utilities
{
    public class CarParameters
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Body { get; set; }
        public int? Year { get; set; }
        public decimal? Price { get; set; }
        public string? Condition { get; set; }
        public int? Mileage { get; set; }
        public string? Transmission { get; set; }
        public string? Keyword { get; set; }
        public string? SortBy { get; set; } = "MostRecent";
        public string? Status { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }


    }
}
