using System;
using System.ComponentModel.DataAnnotations;

namespace WEB_1002_BookTracking.Models
{
    public class CategoryTypeModel
    {
        [Key]
        public int CategoryTypeModelId { get; set; }
        [Required]
        public string? Name { get; set; }

        public CategoryTypeModel()
        {
            Name = "";            
        }
    }
}

