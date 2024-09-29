using Client.IServices;
using Client.ViewModels.Category;
using Grpc.Net.Client;
using GrpcService;

namespace Client.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly GrpcChannel channel; 
        private readonly CategoryService.CategoryServiceClient categoryServiceClient;
        public CategoriesService()
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            categoryServiceClient = new CategoryService.CategoryServiceClient(channel);
        }
        public async Task<List<CategoryVM>> GetAllCategories()
        {
            List<CategoryVM> categoryVMs = new List<CategoryVM>();
            var categories = await categoryServiceClient.GetAllCategoriesAsync(new Empty());
            if (categories.Categories.Count > 0)
            {

                foreach (var category in categories.Categories)
                {
                    CategoryVM categoryVM = new CategoryVM()
                    {
                        Name = category.Name,
                        Id = category.Id,
                    };
                    categoryVMs.Add(categoryVM);
                }
            }
            return categoryVMs;

        }

        public async Task<CategoryVM> GetCategoryById(int Id)
        {
            CategoryVM categoryVM = new CategoryVM();
            var getCategoryByIdRequest = new GetCategoryByIdRequest { Id = Id };
            var categoryResponse = await categoryServiceClient.GetCategoryByIdAsync(getCategoryByIdRequest);
            if (categoryResponse != null)
            {
                categoryVM = new CategoryVM()
                {
                    Name = categoryResponse.Name,
                    Id = categoryResponse.Id,
                };
            }

            return categoryVM;
        }

        public async Task<CategoryVM> AddCategory(AddCategory model)
        {
            CategoryVM categoryVM = new CategoryVM();
            var addCategoryByIdRequest = new addCategory { Name = model.Name };
            var categoryResponse = await categoryServiceClient.AddCategoryAsync(addCategoryByIdRequest);
            if (categoryResponse != null)
            {
                categoryVM = new CategoryVM()
                {
                    Name = categoryResponse.Name,
                    Id = categoryResponse.Id,
                };
            }

            return categoryVM;
        }

    }
}
