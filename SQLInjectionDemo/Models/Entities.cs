using System.ComponentModel.DataAnnotations;

namespace SQLInjectionDemo.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        [Required][MaxLength(20)] public string RoleName { get; set; } = string.Empty;
        public ICollection<User> Users { get; set; } = new List<User>();
    }

    public class User
    {
        public int UserID { get; set; }
        [Required]
        public int RoleID { get; set; }
        [MaxLength(50)]
        public string? FullName { get; set; }
        [MaxLength(150)]
        public string? EmailAdd { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public Role? Role { get; set; }
    }

}