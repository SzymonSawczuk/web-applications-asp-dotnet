using lab14.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab14.Data
{
    public interface IRepository
    {
        IEnumerable<Article> Articles { get; }
        Article this[int id] { get; }
        Task<Article> AddArticleAsync(Article article);
        Article UpdateArticle(Article article);
        void DeleteArticle(int id);

        public IEnumerable<Article> getNext(int id, int n, int categoryId);

    }
}
