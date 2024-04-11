using System.ComponentModel.DataAnnotations;

namespace DealershipAPI.Entities
{
    public class BrandEntity
    {
        [Key]
        public string BrandID { get; set; }
        public string Brand { get; set; }
    }
}
