using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DealershipAPI.Entities
{
    public class ModelEntity
    {
        [Key]
        [JsonIgnore]
        public string ModelID { get; set; }
        public string Model { get ; set;}
        public BodyEntity Body { get; set; }
        public BrandEntity Brand { get; set; }
    }
}
