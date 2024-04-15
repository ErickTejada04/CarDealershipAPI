using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DealershipAPI.Entities
{
    public class BrandEntity
    {
        [Key]
        [JsonIgnore]
        public string BrandID { get; set; }
        public string Brand { get; set; }
    }
}
