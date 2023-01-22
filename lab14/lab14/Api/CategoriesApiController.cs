using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab14.Data;
using lab14.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace lab14.Api
{
    [EnableCors]
    [Route("api/category")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class CategoriesApiController : ControllerBase
    {
        private IRepositoryCategory repository;

        public CategoriesApiController(IRepositoryCategory repo) => repository = repo;

        [HttpGet]
        public IEnumerable<Category> Get() => repository.Categories;

        [HttpGet("{id}")]
        public Category Get(int id) => repository[id];

        [HttpPost]
        public async Task<Category> Post([FromBody] Category res) =>
            await repository.AddCategoryAsync(new Category {Id = 0,  Name = res.Name });

        [HttpPut]
        public Category Put([FromBody] Category res) =>
             repository.UpdateCategory(res);

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody] JsonPatchDocument<Category> patch)
        {
            Category res = Get(id);
            if (res == null) return NotFound();

            patch.ApplyTo(res);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteCategory(id);
    }
}
