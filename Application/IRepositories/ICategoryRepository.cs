using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> getAll();
        Task<Category> get(int id);
        Task<Category> Add(CategoryDto category);
        Task<Category> Update(CategoryDto category);
        Task<string> Delete(int id);
        Task<bool> checkIsExistById(int id);

    }
}
