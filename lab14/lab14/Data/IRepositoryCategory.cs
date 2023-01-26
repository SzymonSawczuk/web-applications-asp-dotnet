using lab14.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab14.Data
{
    public interface IRepositoryCategory
    {
        IEnumerable<Category> Categories { get; }
        Category this[int id] { get; }
        Task<Category> AddCategoryAsync(Category category);
        Category UpdateCategory(Category category);
        void DeleteCategory(int id);

    }
}
