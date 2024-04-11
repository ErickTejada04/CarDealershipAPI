using System.ComponentModel.DataAnnotations;

namespace DealershipAPI.Entities
{
    public class Image
    {
        [Key]
        public string ImageID { get; set; }
        public string CarID { get; set; }
        public CarEntity Car { get; set; }
        public string ImageURL { get; set; }
        public int Main { get; set; }
    }
}
