using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DealershipAPI.Entities
{
    public class BodyEntity
    {
        [Key]
        public string BodyID { get; set; }
        public string Body { get; set; }
        public int Status { get; set; }

    }
}
