using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab13.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name of category")]
        public string Name { get; set; }

        public Category()
        {

        }

        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

    }
}
