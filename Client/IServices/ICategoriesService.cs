using Client.ViewModels.Category;

namespace Client.IServices
{
    public interface ICategoriesService
    {
        public Task<List<CategoryVM>> GetAllCategories();
        public Task<CategoryVM> GetCategoryById(int Id);
        Task<CategoryVM> AddCategory(AddCategory model);
    }
}
