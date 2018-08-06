using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Project20180804.Core.BLL;
using Project20180804.Core.Framework;
using System;
using System.Threading.Tasks;

namespace Project20180804.Core.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryBLL _categoryBLL;
        private readonly IProductBLL _productBLL;
        private readonly IVideoBLL _videoBLL;
        private readonly IOptions<AppSettings> _appSetting;

        public HomeController(ICategoryBLL categoryBLL, IProductBLL productBLL, IVideoBLL videoBLL, IOptions<AppSettings> appSetting)
        {
            _categoryBLL = categoryBLL;
            _productBLL = productBLL;
            _videoBLL = videoBLL;
            _appSetting = appSetting;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var (isSuccessed, ErrorMsg, Data) = await _categoryBLL.GetCategorysAsync();

            var video = await _videoBLL.GetVideoUrlAsync();

            ViewBag.Categorys = Data;

            ViewBag.VideoUrl = video;

            return View();
        }

        public async Task<IActionResult> GetProductsByCategoryAsync(Guid categoryId)
        {
            var (IsSuccessed, ErrorMsg, Data) = await _productBLL.GetProductsBByCategoryIdAsync(categoryId);

            var category = await _categoryBLL.GetCategoryByIdAsync(categoryId);

            ViewBag.Products = Data;

            ViewBag.Category = category;

            return View("Product");
        }

        public async Task<IActionResult> GetProductByIdAsync(Guid productId)
        {
            var (IsSuccessed, ErrorMsg, Data) = await _productBLL.GetProductByIdAsync(productId);

            ViewBag.Product = Data;

            return View("ProductDetails");
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
