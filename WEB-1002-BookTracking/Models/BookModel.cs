using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WEB_1002_BookTracking.Models
{
	public class TrackingContext: DbContext
    {
        public TrackingContext(DbContextOptions<TrackingContext> options)
			: base(options)
		{

		}

		public DbSet<BookModel>? BookModels { get; set; }
		public DbSet<CategoryModel>? CategoryModels { get; set; }
		public DbSet<CategoryTypeModel>? CategoryTypeModels { get; set; }
	}

    public class BookModel
    {
		[Key]
		public int BookModelId { get; set; }
		[Required]
		public string? Title { get; set; }
		[Required]
		public CategoryModel? CategoryModel { get; set; }
		[Required]
		[EmailAddress]
		public string? Author { get; set; }
	}
}

