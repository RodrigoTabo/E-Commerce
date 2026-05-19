using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared.DTOs.Reviews
{
    public class DeleteReviewDTO
    {
        public int IdReview { get; set; }
        public int IdProducto { get; set; }
    }
}
