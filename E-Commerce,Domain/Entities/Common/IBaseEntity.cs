using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities.Common
{
    public interface IBaseEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
