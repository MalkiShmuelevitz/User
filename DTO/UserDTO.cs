using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record UserDTO(
        [EmailAddress]
        [Required]
        [StringLength(50, ErrorMessage = "username more than length 50")]
        string UserName,
        string? FirstName,
        string? LastName,
        [Required]
        [StringLength(20, ErrorMessage = "password more than length 20")]
        string Password);
    public record GetUserDTO(int Id,string UserName, string? FirstName, string? LastName);
}
