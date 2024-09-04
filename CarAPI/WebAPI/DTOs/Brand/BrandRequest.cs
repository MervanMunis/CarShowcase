﻿using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Brand
{
    public class BrandRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Brand name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
