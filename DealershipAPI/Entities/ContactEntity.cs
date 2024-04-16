using System.ComponentModel.DataAnnotations;

namespace DealershipAPI.Entities
{
    public class ContactEntity
    {
        [Key]
        public string ContactID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
