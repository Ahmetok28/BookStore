using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string SurName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string RefreshToken { get; set; } = "";
        public DateTime? RefreshTokenExpireDate { get; set; }
    }
}
