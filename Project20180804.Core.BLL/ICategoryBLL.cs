using Project20180804.Core.DTO.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project20180804.Core.BLL
{
    public interface ICategoryBLL
    {
        Task<(bool isSuccessed, string ErrorMsg, IList<CategoryDTO> Data)> GetCategorysAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(Guid categoryId);
        Task<(bool IsSuccessed, string ErrorMsg)> AddCategoryAsync(string title, string attachmentUrl);
        Task<(bool IsSuccessed, string ErrorMsg)> UpdateCategoryAsync(Guid id, string title, string attachmentUrl);
        Task<(bool IsSuccessed, string ErrorMsg)> DeleteCategoryAsync(Guid id);
    }
}
