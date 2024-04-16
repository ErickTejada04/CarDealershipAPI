using System.ComponentModel.DataAnnotations;

namespace DealershipAPI.Entities
{
    public class CarEntity
    {
        [Key]
        public string CarID { get; set; }
        public string ModelID { get; set; }
        public ModelEntity Model { get; set; }

        public int Year { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string Condition { get; set; }
        public int Mileage { get; set; }
        public string Transmission { get; set; }

        public string Traction { get; set; }
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
        public int Doors { get; set; }

        public string Status { get; set; } 
    }
}
