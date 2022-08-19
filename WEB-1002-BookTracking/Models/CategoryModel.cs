using System;
using System.ComponentModel.DataAnnotations;

namespace WEB_1002_BookTracking.Models
{
    public class CategoryModel
    {
		[Key]
		public int CategoryModelId { get; set; }
		[Required]
		public CategoryTypeModel? CategoryTypeModel { get; set; }
		[Required]
		public string? Description { get; set; }

		public CategoryModel()
		{
			Description = "";
			CategoryTypeModel = new CategoryTypeModel();
		}
	}
}

