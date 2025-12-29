using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.DTO
{
    public record CategoryDTO (string Name, string Description);

    public record UpdateCategoryDTO(int Id, string Name, string Description);

}
