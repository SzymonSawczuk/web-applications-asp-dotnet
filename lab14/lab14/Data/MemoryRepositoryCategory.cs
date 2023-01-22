using lab14.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab14.Data
{
    public class MemoryRepositoryCategory : IRepositoryCategory
    {

        private readonly Dictionary<int, Category> categories;
        private readonly lab14.Data.MyDbContext _context;

        public MemoryRepositoryCategory(lab14.Data.MyDbContext context)
        {
            _context = context;
            categories = _context.Category.ToDictionary(elem => elem.Id, elem => elem);
        }

        public Category this[int id] => categories.ContainsKey(id) ? categories[id] : null;


        public IEnumerable<Category> Categories => categories.Values;

        public async Task<Category> AddCategoryAsync(Category category)
        {
            int key = 0;
            if (category.Id == 0)
            {
                key = categories.Count;
                while (categories.ContainsKey(key)) key++;
            }
            categories[key] = category;
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public void DeleteCategory(int id)
        {
            var categorUpdate = _context.Category.FirstOrDefault(item => item.Id == id);

            if (categorUpdate == null)
            {
                return;
            }

            _context.Category.Remove(categories[id]);
            categories.Remove(id);
            _context.SaveChanges();
        }

        public Category UpdateCategory(Category category)
        {
            var categorUpdate = _context.Category.FirstOrDefault(item => item.Id == category.Id);

            if (categorUpdate == null)
            {
                return null;
            }

            int key = categories.First(elem => elem.Key == category.Id).Key;
            categories[key] = category;

            categorUpdate.Name = category.Name;

            _context.SaveChanges();
            return category;
        }
    }
}
