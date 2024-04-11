using System.ComponentModel.DataAnnotations;

namespace DealershipAPI.Entities
{
    public class BodyEntity
    {
        [Key]
        public string BodyID { get; set; }
        public string Body { get; set; }

    }
}
