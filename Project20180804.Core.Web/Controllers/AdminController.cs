using Microsoft.AspNetCore.Mvc;
using Project20180804.Core.BLL;
using Project20180804.Core.Web.Models.Admin;
using System;
using System.Threading.Tasks;

namespace Project20180804.Core.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryBLL _categoryBLL;
        private readonly IProductBLL _productBLL;
        private readonly IVideoBLL _videoBLL;

        public AdminController(ICategoryBLL categoryBLL, IProductBLL productBLL, IVideoBLL videoBLL)
        {
            _categoryBLL = categoryBLL;
            _productBLL = productBLL;
            _videoBLL = videoBLL;
        }

        public IActionResult Category()
        {
            return View();
        }

        public async Task<IActionResult> Video()
        {
            var videoUrl = await _videoBLL.GetVideoUrlAsync();

            ViewBag.VideoUrl = videoUrl;

            return View();
        }

        public IActionResult Product()
        {
            return View();
        }

        public async Task<IActionResult> GetAllCategorysAsync()
        {
            var (IsSuccessed, ErrorMsg, Data) = await _categoryBLL.GetCategorysAsync();

            return Json(new
            {
                Code = Convert.ToInt32(!IsSuccessed),
                Msg = ErrorMsg,
                Data,
                Data.Count
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync(AddCategoryBindingModel model)
        {
            var (IsSuccessed, ErrorMsg) = await _categoryBLL.AddCategoryAsync(model.Title, model.AttachmentUrl);

            return Json(new
            {
                IsSuccessed,
                ErrorMsg
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategoryAsync(UpdateCategoryBindingModel model)
        {
            var (IsSuccessed, ErrorMsg) = await _categoryBLL.UpdateCategoryAsync(model.ID, model.Title, model.AttachmentUrl);

            return Json(new
            {
                IsSuccessed,
                ErrorMsg
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategoryAsync(DeleteCategoryBindingModel model)
        {
            var (IsSuccessed, ErrorMsg) = await _categoryBLL.DeleteCategoryAsync(model.ID);

            return Json(new
            {
                IsSuccessed,
                ErrorMsg
            });
        }

        public async Task<IActionResult> GetAllProductsAsync()
        {
            var (IsSuccessed, ErrorMsg, Data) = await _productBLL.GetProductsAsync();

            return Json(new
            {
                Code = Convert.ToInt32(!IsSuccessed),
                Msg = ErrorMsg,
                Data,
                Data.Count
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync(AddProductBindingModel model)
        {
            var (IsSuccessed, ErrorMsg) = await _productBLL.AddProductAsync(model.Title, model.Description, model.CategoryID, model.AttachmentUrls);

            return Json(new
            {
                IsSuccessed,
                ErrorMsg
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductAsync(UpdateProductBindingModel model)
        {
            var (IsSuccessed, ErrorMsg) = await _productBLL.UpdateProductAsync(model.ID, model.Title, model.Description, model.CategoryID, model.AttachmentUrls);

            return Json(new
            {
                IsSuccessed,
                ErrorMsg
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductAsync(DeleteProductBindingModel model)
        {
            var (IsSuccessed, ErrorMsg) = await _productBLL.DeleteProductAsync(model.ID);

            return Json(new
            {
                IsSuccessed,
                ErrorMsg
            });
        }

        public async Task<IActionResult> AddVideoAsync(string url)
        {
            await _videoBLL.AddVideoAsync(url);

            return Ok();
        }
    }
}