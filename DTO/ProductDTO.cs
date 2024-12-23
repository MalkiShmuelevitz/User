using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{

    public record ProductDTO(string? CategoryCategoryName,string ProductName, string? Description, int Price, string? Picture);
}
