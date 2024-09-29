using Application.DTOs;
using Application.IRepositories;
using Domain.Entities;
using Grpc.Core;

namespace GrpcService.Services
{
    public class CategoryGrpcService: CategoryService.CategoryServiceBase
    {
        private readonly ICategoryRepository _CategoryRepository;
        public CategoryGrpcService(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        public override async Task<CategoryList> GetAllCategories(Empty request, ServerCallContext context)
        {
            CategoryList response = new CategoryList();
            var categoryList = await _CategoryRepository.getAll();
            if (categoryList != null)
            {
                foreach (var category in categoryList)
                {
                    response.Categories.Add(new CategoryItem
                    {
                        Name = category.Name,
                        Id = category.Id,
                    });
                }
                return response;
            }
            return null;
        }

        public override async Task<CategoryItem> GetCategoryById(GetCategoryByIdRequest request, ServerCallContext context)
        {
            CategoryItem response = new CategoryItem();
            var category = await _CategoryRepository.get(request.Id);
            if (category != null)
            {
                response.Id = category.Id;
                response.Name = category.Name;
                return response;
            }
            else
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"No Categories with {response.Id}"));
            }
        }

        public override async Task<CategoryItem> AddCategory(addCategory request, ServerCallContext context)
        {
            CategoryDto category = new CategoryDto();
            CategoryItem response = new CategoryItem();
            if(request != null && !string.IsNullOrEmpty(request.Name))
            {
                category.name = request.Name;
                var result = await _CategoryRepository.Add(category);
                if (result != null)
                {
                    response.Name = result.Name;
                    response.Id = result.Id;
                    return response;
                }
            }
            throw new RpcException(new Status(StatusCode.InvalidArgument, $"Data is Invalid"));
        }






    }
}
