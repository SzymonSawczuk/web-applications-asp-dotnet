using System;
using System.Collections.Generic;
using lab9.ViewModels;

namespace lab9.DataContext
{
	public interface IArticlesContext
	{

        void AddArticle(Article newArticle);
        List<Article> getArticles();
        Article getArticle(int id);
		void UpdateArticle(Article updatedArticle);
        void RemoveArticle(int id);
    }
}

