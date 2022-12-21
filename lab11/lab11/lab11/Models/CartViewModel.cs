using lab10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab11.Models
{
    public class CartViewModel
    {

        public Article Article { get; set; }
        public int Amount { get; set; }

        public CartViewModel()
        {
                
        }

        public CartViewModel(Article article, int amount)
        {
            this.Article = article;
            this.Amount = amount;
        }

    }
}
