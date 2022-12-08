using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lab9.DataContext;
using lab9.ViewModels;


namespace lab9.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticlesContext _articlesContext;

        public ArticleController(IArticlesContext articlesContext)
        {
            this._articlesContext = articlesContext;
        }

        // GET: Article
        public ActionResult Index()
        {
            _articlesContext.getArticles().ForEach(article => article.PrintCategories());
            return View(_articlesContext.getArticles());
        }

        // GET: Article/Details/5
        public ActionResult Details(int id)
        {
            _articlesContext.getArticles().ForEach(article => article.PrintCategories());
            return View(_articlesContext.getArticle(id));
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            try
            {
                if (ModelState.IsValid)
                    _articlesContext.AddArticle(article);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Article/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_articlesContext.getArticle(id));
        }

        // POST: Article/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Article article)
        {
            try
            {
                if (ModelState.IsValid)
                    _articlesContext.UpdateArticle(article);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Article/Delete/5
        public ActionResult Delete(int id)
        {
            _articlesContext.getArticles().ForEach(article => article.PrintCategories());
            return View(_articlesContext.getArticle(id));
        }

        // POST: Article/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Article article)
        {
            try
            {
                if (ModelState.IsValid)
                    _articlesContext.RemoveArticle(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}