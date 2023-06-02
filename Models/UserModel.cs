using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace server.Models
{
    public class UserAuthParams
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [JsonIgnore]
        public string UserPassword { get; set; }
        
        public string UserMail { get; set; } = null;
        public string UserAvatar { get; set; } = null;
    }
}
