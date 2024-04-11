namespace DealershipAPI.Entities
{
    public class CarEntity
    {
        public Guid CarID { get; set; }
        public Guid ModelID { get; set; }
        public ModelEntity Model { get; set; }

        public int Year { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public string Condition { get; set; }
        public int Mileage { get; set; }
        public string Transmission { get; set; }

        public string Traction { get; set; }
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
        public int Doors { get; set; }
    }
}
