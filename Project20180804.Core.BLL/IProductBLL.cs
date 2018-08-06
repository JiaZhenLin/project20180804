using Project20180804.Core.DTO.ProductDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project20180804.Core.BLL
{
    public interface IProductBLL
    {
        Task<(bool IsSuccessed, string ErrorMsg, IList<ProductDTO> Data)> GetProductsAsync();
        Task<(bool IsSuccessed, string ErrorMsg, IList<ProductDTO> Data)> GetProductsBByCategoryIdAsync(Guid categoryId);
        Task<(bool IsSuccessed, string ErrorMsg, ProductDTO Data)> GetProductByIdAsync(Guid productId);
        Task<(bool IsSuccessed, string ErrorMsg)> AddProductAsync(string title, string description, Guid categoryId, IList<string> attachmentUrls);
        Task<(bool IsSuccessed, string ErrorMsg)> UpdateProductAsync(Guid productId, string title, string description, Guid categoryId, IList<string> attachmentUrls);
        Task<(bool IsSuccessed, string ErrorMsg)> DeleteProductAsync(Guid productId);
    }
}
