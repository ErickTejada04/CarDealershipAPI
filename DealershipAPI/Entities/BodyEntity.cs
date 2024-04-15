using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DealershipAPI.Entities
{
    public class BodyEntity
    {
        [Key]
        [JsonIgnore]
        public string BodyID { get; set; }
        public string Body { get; set; }

    }
}
