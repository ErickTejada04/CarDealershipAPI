using System.ComponentModel.DataAnnotations;

namespace DealershipAPI.Entities
{
    public class UserEntity
    {
        [Key]
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public int Active { get; set; }
    }
}
