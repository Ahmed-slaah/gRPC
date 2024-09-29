using Client.IServices;
using Client.ViewModels.Category;
using Grpc.Net.Client;
using GrpcService;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        public CategoryController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var result = await _categoriesService.GetAllCategories();
                return View(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var result = await _categoriesService.GetCategoryById(id);
                return View(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody]AddCategory model)
        {
            try
            {
                var result = await _categoriesService.AddCategory(model);
                return View(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
