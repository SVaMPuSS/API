using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class User
    {
        [JsonIgnore]
        public int id { get; set; }
        [Required]
        public string login { get; set; }
        [Required]
        public string password { get; set; }
        [JsonIgnore]
        public int? token { get; set; }
    }
}
