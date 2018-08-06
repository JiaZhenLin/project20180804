using Microsoft.EntityFrameworkCore;
using Project20180804.Core.DAL.Repositorys;
using Project20180804.Core.DTO.CategoryDtos;
using Project20180804.Core.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project20180804.Core.BLL.Impl
{
    public class CategoryBLL : ICategoryBLL
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Attachment> _attachmentRepository;
        private readonly IRepository<Product> _productRepository;

        public CategoryBLL(IRepository<Category> categoryRepository, IRepository<Attachment> attachmentRepository, IRepository<Product> productRepository)
        {
            _categoryRepository = categoryRepository;
            _attachmentRepository = attachmentRepository;
            _productRepository = productRepository;
        }

        public async Task<(bool isSuccessed, string ErrorMsg, IList<CategoryDTO> Data)> GetCategorysAsync()
        {
            bool isSuccessed = true;

            string errorMsg = string.Empty;

            var sql = _categoryRepository.Table.ToList();

            var categorys = await _categoryRepository.Table.Include(x => x.Attachment).Select(x => new CategoryDTO() { ID = x.ID, Title = x.Title, AttachmentUrl = x.Attachment.Url }).ToListAsync();

            return (isSuccessed, errorMsg, categorys);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _categoryRepository.Table.Include(x => x.Attachment).Where(x=>x.ID == categoryId).Select(x => new CategoryDTO() { ID = x.ID, Title = x.Title, AttachmentUrl = x.Attachment.Url }).SingleOrDefaultAsync();

            return category;
        }

        public async Task<(bool IsSuccessed, string ErrorMsg)> AddCategoryAsync(string title, string attachmentUrl)
        {
            bool isSuccessed = false;

            string errorMsg = string.Empty;

            var category = await _categoryRepository.Table.SingleOrDefaultAsync(x => x.Title == title);

            if (category != null)
            {
                errorMsg = "标题已存在";

                return (isSuccessed, errorMsg);
            }

            var attachment = new Attachment()
            {
                Url = attachmentUrl
            };

            category = new Category()
            {
                Title = title,
                AttachmentID = attachment.ID
            };

            await _attachmentRepository.InsertAsync(attachment);
            await _categoryRepository.InsertAsync(category);

            isSuccessed = true;

            return (isSuccessed, errorMsg);
        }

        public async Task<(bool IsSuccessed, string ErrorMsg)> UpdateCategoryAsync(Guid id, string title, string attachmentUrl)
        {
            bool isSuccessed = false;

            string errorMsg = string.Empty;

            var category = await _categoryRepository.Table.SingleOrDefaultAsync(x => x.Title == title);

            if (category != null && category.ID != id)
            {
                errorMsg = "标题已存在";

                return (isSuccessed, errorMsg);
            }

            category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                errorMsg = "数据不存在";

                return (isSuccessed, errorMsg);
            }



            var attachment = new Attachment()
            {
                Url = attachmentUrl
            };

            category.Title = title;
            category.AttachmentID = attachment.ID;

            await _attachmentRepository.InsertAsync(attachment);
            await _categoryRepository.UpdateAsync(category);

            isSuccessed = true;

            return (isSuccessed, errorMsg);
        }

        public async Task<(bool IsSuccessed, string ErrorMsg)> DeleteCategoryAsync(Guid id)
        {
            bool isSuccessed = false;

            string errorMsg = string.Empty;

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                errorMsg = "数据不存在";

                return (isSuccessed, errorMsg);
            }

            var productCount = await _productRepository.Table.Where(x => x.CategoryID == category.ID).CountAsync();

            if (productCount > 0)
            {
                errorMsg = "已被产品使用，不可删除";

                return (isSuccessed, errorMsg);
            }

            await _categoryRepository.DeleteAsync(category);

            isSuccessed = true;

            return (isSuccessed, errorMsg);
        }
    }
}
