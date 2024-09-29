using Application.DTOs;
using Application.IRepositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GrpcContext _rpcContext;
        public CategoryRepository(GrpcContext rpcContext)
        {
            _rpcContext = rpcContext;
        }
        public async Task<Category> Add(CategoryDto category)
        {
            if(category != null && !string.IsNullOrEmpty(category.name))
            {
                Category newCategory = new Category()
                {
                    Name = category.name,
                };
                await _rpcContext.AddAsync(newCategory);
                await _rpcContext.SaveChangesAsync();
                return await Task.FromResult(new Category() {Id = newCategory.Id,Name=newCategory.Name});
            }
            else
            {
                return null;
            }
        }

        public async Task<string> Delete(int id)
        {
            var category = await get(id);
            if (category == null)
            {
                return $"there is no category with ID : {id}";
            }
            _rpcContext.Categories.Remove(category);
            await _rpcContext.SaveChangesAsync();
            return $"Category with ID : {id} deleted";
        }
        public async Task<bool> checkIsExistById(int id)
        {
            var isExist = await _rpcContext.Categories.AnyAsync(c => c.Id == id);
            return isExist;
        }
        public async Task<Category> get(int id)
        {
            var category = await _rpcContext.Categories.Where(a=>a.Id == id).FirstOrDefaultAsync();
            return category;
        }

        public async Task<List<Category>> getAll()
        {
            var categories = await _rpcContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> Update(CategoryDto category)
        {
            throw new NotImplementedException();
        }
    }
}
