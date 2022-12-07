using System;
using System.Collections.Generic;
using lab9.ViewModels;
using System.Linq;

namespace lab9.DataContext
{
	public class MockArticlesContextDict: IArticlesContext
	{
		Dictionary<int, Article> articles = new Dictionary<int, Article>();

        public void AddArticle(Article newArticle)
        {
            int nextIndex = (from article in articles
                             orderby article.Key descending
                             select article.Value.Id).FirstOrDefault() + 1;

            newArticle.Id = nextIndex;

            this.articles.Add(nextIndex, newArticle);
        }

        public Article getArticle(int id)
        {
            return (from article in articles
                    where article.Value.Id == id
                    select article.Value).SingleOrDefault();
        }

        public List<Article> getArticles()
        {
            return this.articles.Values.ToList();
        }

        public void RemoveArticle(int id)
        {
            //int indexToRemove = this.articles.FindIndex(article => article.Id == id);

            int idToRemove = -1;

            try
            {
                idToRemove = (from article in articles
                                     where article.Value.Id == id
                                     select article.Value.Id).Single();
            }
            catch (Exception ex)
            {
                idToRemove = -1;
            }

            if (idToRemove != -1)
                this.articles.Remove(idToRemove);
        }

        public void UpdateArticle(Article updatedArticle)
        {
            this.articles = (from article in articles
                             select article.Value.Id == updatedArticle.Id ? updatedArticle : article.Value).ToDictionary(article => article.Id);

        }
    }
}

