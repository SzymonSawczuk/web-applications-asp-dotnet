using lab14.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab14.Data
{
    public class MemoryRepository : IRepository
    {

        private readonly Dictionary<int, Article> articles;
        private readonly lab14.Data.MyDbContext _context;

        public MemoryRepository(lab14.Data.MyDbContext context)
        {
            _context = context;
            articles = _context.Article.ToDictionary(elem => elem.Id, elem => elem);
        }

        public Article this[int id] => articles.ContainsKey(id)?articles[id]:null;


        public IEnumerable<Article> Articles => articles.Values;

        public async Task<Article> AddArticleAsync(Article article)
        {
            int key = 0;
            if (article.Id == 0)
            {
                key = articles.Count;
                while (articles.ContainsKey(key)) key++;
            }
            articles[key] = article;
            await _context.Article.AddAsync(article);
            await _context.SaveChangesAsync();
            return article;

        }

        public void DeleteArticle(int id)
        {
            var articleUpdate = _context.Article.FirstOrDefault(item => item.Id == id);

            if (articleUpdate == null)
            {
                return;
            }

            _context.Article.Remove(articles[id]);
            articles.Remove(id);
            _context.SaveChanges();
        }

        public Article UpdateArticle(Article article)
        {
            var articleUpdate = _context.Article.FirstOrDefault(item => item.Id == article.Id);

            if (articleUpdate == null)
            {
                return null;
            }

            int key = articles.First(elem => elem.Key == article.Id).Key;
            articles[key] = article;

            articleUpdate.Name = article.Name;
            articleUpdate.Price = article.Price;
            articleUpdate.CategoryId = article.CategoryId;

            _context.SaveChanges();
            return article;
        }
        
    }
}
