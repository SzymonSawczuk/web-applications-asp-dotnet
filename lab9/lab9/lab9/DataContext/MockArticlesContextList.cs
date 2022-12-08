using System;
using System.Collections.Generic;
using lab9.ViewModels;
using System.Linq;

namespace lab9.DataContext
{
	public class MockArticlesContextList: IArticlesContext
	{
		private List<Article> articles = new List<Article>();

        public void AddArticle(Article newArticle)
        {

            int nextIndex = (from article in articles
                             orderby article.Id descending
                             select article.Id).FirstOrDefault() + 1;

            //int nextIndex = this.articles.Max(article => article.Id) + 1;
            newArticle.Id = nextIndex;

            this.articles.Add(newArticle);
        }

        public Article getArticle(int id)
        {
            return (from article in articles
                   where article.Id == id
                   select article).SingleOrDefault();

            //return this.articles.FirstOrDefault(article => article.Id == id);
        }

        public List<Article> getArticles()
        {
            return this.articles;
        }

        public void RemoveArticle(int id)
        {
            //int indexToRemove = this.articles.FindIndex(article => article.Id == id);

            Article artcileToRemove = (from article in articles
                             where article.Id == id
                             select article).SingleOrDefault();


            if (artcileToRemove != null)
                this.articles.Remove(artcileToRemove);
        }   

        public void UpdateArticle(Article updatedArticle)
        {
            this.articles = (from article in articles
                            select article.Id == updatedArticle.Id ? updatedArticle : article).ToList();

        }
    }
}

