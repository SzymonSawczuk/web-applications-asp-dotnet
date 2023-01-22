using lab14.Data;
using lab14.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab14.Api
{
    [EnableCors]
    [Route("api/article")]
    [ApiController]
    public class ArticleApiController : ControllerBase
    {
        private IRepository repository;

        public ArticleApiController(IRepository repo) => repository = repo;

        [HttpGet]
        public IEnumerable<Article> Get() => repository.Articles;

        [HttpGet("{id}")]
        public Article Get(int id) => repository[id];

        [HttpPost]
        public async Task<Article> PostAsync([FromBody] Article res) =>
            await repository.AddArticleAsync(new Article { Name = res.Name, Price = res.Price, Picture = null, FilePath = null, CategoryId = res.CategoryId, Category = null });

        [HttpPut]
        public Article Put([FromBody] Article res) =>
            repository.UpdateArticle(res);

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody] JsonPatchDocument<Article> patch)
        {
            Article res = Get(id);
            if (res == null) return NotFound();

            patch.ApplyTo(res);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteArticle(id);
    }
}
