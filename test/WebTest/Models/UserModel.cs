using System.ComponentModel.DataAnnotations;

namespace WebTest.Models {
    public class UserModel {
        public string email { get; set; }
        [Required] public string username { get; set; }
        [Required] public string password { get; set; }
        public string phoneNumber { get; set; }
    }
}
