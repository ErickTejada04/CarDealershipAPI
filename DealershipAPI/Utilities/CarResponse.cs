namespace DealershipAPI.Utilities
{
    public class CarResponse
    {
        public string CarID { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public string BodyName { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string Condition { get; set; }
        public int Mileage { get; set; }
        public string Transmission { get; set; }
        public string Traction { get; set; }
        public string Description { get; set; }
        public int Doors { get; set; }
        public DateTime CreationDate { get; set; }
        public List<ImageResponse> Images { get; set; }
        public string Status { get; set; }
    }
}
