using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDTO
    {
        // always have validation annotations on Data Transfer objects, not on model entities (they must remain pure entities, without dependencies)
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Password must be at least 6 characters long, and must contain 1 uppercase, 1 lowercase, 1 number and 1 non-alphanumeric")]
        public string Password { get; set; }
    }
}