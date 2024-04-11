using System.ComponentModel.DataAnnotations;

namespace DealershipAPI.Entities
{
    public class ModelEntity
    {
        [Key]
        public string ModelID { get; set; }
        public string ModelName { get ; set;}

        public string BodyID { get; set; }
        public BodyEntity Body { get; set; }

        public string BrandID { get; set; }
        public BrandEntity Brand { get; set; }


    }
}
