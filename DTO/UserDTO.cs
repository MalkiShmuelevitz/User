using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record UserDTO(string UserName, string? FirstName, string? LastName, string Password);
    //public record LoginUserDTO(string UserName, string Password);
    public record GetUserDTO(int Id,string UserName, string? FirstName, string? LastName);
}
