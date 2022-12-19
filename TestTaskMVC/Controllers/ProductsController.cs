using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTaskMVC.Data.Services;
using TestTaskMVC.Models;

namespace TestTaskMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService service;

        public ProductsController(IProductService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var products = await service.GetAllAsync();
            if(products.Length == 0)
                return View("Product not found!");

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return View(await service.GetById(id));
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync(Product product)
        {
            return View(await service.CreateAsync(product));
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long id, Product product)
        {
            return View(await service.UpdateAsync(id, product));
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            await service.DeleteAsync(id);

            return View("Done");
        }
    }
}
