using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace lab9.ViewModels
{
	public enum Categories
	{
		Books,
		Electronics,
		SportEquipment,
		Clothes
	}


    public class Article
	{

		public int Id { get; set; }

		[Required]
		[Display(Name="Name of product")]
		public string Name { get; set; }

		[Required]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} must be a Number.")]
		[Display(Name = "Price in PLN")]
        [Range(0, 1_000_000_000, ErrorMessage = "Price should be in range of 0 to 1 000 000 000PLN")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public float Price { get; set; }

		[DataType(DataType.DateTime)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [DisplayFormat]
		public List<Categories> CategoriesList { get; set; }

		public String CategoriesPrint;

		public void PrintCategories()
		{
			this.CategoriesPrint = String.Join(", ", this.CategoriesList);
		}

		public Article(int id, string name, float price, DateTime expirationDate, List<Categories> categories)
		{
			this.Id = id;
			this.Name = name;
			this.Price = price;
			this.ExpirationDate = expirationDate;
			this.CategoriesList = categories;
		}

		public Article()
		{
            this.Id = 0;
            this.Name = "test";
            this.Price = 12;
            this.ExpirationDate = new DateTime(29,12,12);
            this.CategoriesList = new List<Categories>() { Categories.Books};
        }

    

    }

}

