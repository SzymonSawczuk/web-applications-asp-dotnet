using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using lab12.Models;
using Microsoft.AspNetCore.Http;

namespace lab12.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name of product")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
        [Display(Name = "Price")]
        [Range(0, 1_000_000_000, ErrorMessage = "Price should be in range of 0 to 1 000 000 000PLN")]
        [DisplayFormat(DataFormatString = "{0:F2} PLN")]
        public float Price { get; set; }

        [Display(Name = "Picture")]
        [NotMapped]
        public IFormFile Picture { get; set; } 

        public string? FilePath { get; set; } = null;

        [Required]
        public int CategoryId { get; set; }


        [Display(Name = "Category")]
        public Category Category { get; set; }

        public Article()
        {
             
        }

        public Article(int id, string name, float price, IFormFile picture, string? filePath, int categoryId,  Category? category)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Picture = picture;
            this.FilePath = filePath;
            this.CategoryId = categoryId;
            this.Category = category;
        }

    }
}
