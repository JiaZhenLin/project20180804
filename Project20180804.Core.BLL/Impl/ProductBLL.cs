using Microsoft.EntityFrameworkCore;
using Project20180804.Core.DAL.Repositorys;
using Project20180804.Core.DTO.ProductDtos;
using Project20180804.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project20180804.Core.BLL.Impl
{
    public class ProductBLL : IProductBLL
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Attachment> _attachmentRepository;
        private readonly IRepository<ProductXAttachment> _productXAttachmentRepository;

        public ProductBLL(IRepository<Product> productRepository, IRepository<Attachment> attachmentRepository, IRepository<Category> categoryRepository, IRepository<ProductXAttachment> productXAttachmentRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _attachmentRepository = attachmentRepository;
            _productXAttachmentRepository = productXAttachmentRepository;
        }

        public async Task<(bool IsSuccessed, string ErrorMsg, IList<ProductDTO> Data)> GetProductsAsync()
        {
            bool isSuccessed = true;

            string errorMsg = string.Empty;

            var products = await _productRepository.Table.Include(x => x.ProductXAttachments).Include(x => x.Category).ToListAsync();
            var attachmentIds = products.SelectMany(x => x.ProductXAttachments).Select(x => x.AttachmentID).ToList();
            var attachment = await _attachmentRepository.Table.Where(x => attachmentIds.Contains(x.ID)).ToListAsync();
            var productDtos = new List<ProductDTO>();

            products.ForEach(product =>
            {
                var productDto = new ProductDTO()
                {
                    ID = product.ID,
                    CategoryID = product.CategoryID,
                    CategoryTitle = product.Category.Title,
                    Title = product.Title,
                    Description = product.Description
                };

                productDto.AttachmentUrls = product.ProductXAttachments.Select(x => x.Attachment.Url).ToList();

                productDtos.Add(productDto);
            });

            return (isSuccessed, errorMsg, productDtos);
        }

        public async Task<(bool IsSuccessed, string ErrorMsg, IList<ProductDTO> Data)> GetProductsBByCategoryIdAsync(Guid categoryId)
        {
            bool isSuccessed = true;

            string errorMsg = string.Empty;

            var products = await _productRepository.Table.Include(x => x.ProductXAttachments).Include(x => x.Category).Where(x => x.CategoryID == categoryId).ToListAsync();
            var attachmentIds = products.SelectMany(x => x.ProductXAttachments).Select(x => x.AttachmentID).ToList();
            var attachment = await _attachmentRepository.Table.Where(x => attachmentIds.Contains(x.ID)).ToListAsync();
            var productDtos = new List<ProductDTO>();

            products.ForEach(product =>
            {
                var productDto = new ProductDTO()
                {
                    ID = product.ID,
                    CategoryID = product.CategoryID,
                    CategoryTitle = product.Category.Title,
                    Title = product.Title,
                    Description = product.Description
                };
                productDto.PosterUrl = product.ProductXAttachments.Where(x => x.IsPoster).Select(x => x.Attachment.Url).SingleOrDefault();
                productDto.AttachmentUrls = product.ProductXAttachments.Select(x => x.Attachment.Url).ToList();

                productDtos.Add(productDto);
            });

            return (isSuccessed, errorMsg, productDtos);
        }

        public async Task<(bool IsSuccessed, string ErrorMsg, ProductDTO Data)> GetProductByIdAsync(Guid productId)
        {
            bool isSuccessed = true;

            string errorMsg = string.Empty;

            var product = await _productRepository.Table.Include(x => x.ProductXAttachments).Include(x => x.Category).Where(x => x.ID == productId).SingleOrDefaultAsync();
            var attachmentIds = product.ProductXAttachments.Select(x => x.AttachmentID).ToList();
            var attachment = await _attachmentRepository.Table.Where(x => attachmentIds.Contains(x.ID)).ToListAsync();
            var productDtos = new List<ProductDTO>();

            var productDto = new ProductDTO()
            {
                ID = product.ID,
                CategoryID = product.CategoryID,
                CategoryTitle = product.Category.Title,
                Title = product.Title,
                Description = product.Description
            };
            productDto.PosterUrl = product.ProductXAttachments.Where(x => x.IsPoster).Select(x => x.Attachment.Url).SingleOrDefault();
            productDto.AttachmentUrls = product.ProductXAttachments.Select(x => x.Attachment.Url).ToList();

            productDtos.Add(productDto);

            return (isSuccessed, errorMsg, productDto);
        }
        public async Task<(bool IsSuccessed, string ErrorMsg)> AddProductAsync(string title, string description, Guid categoryId, IList<string> attachmentUrls)
        {
            bool isSuccessed = false;

            string errorMsg = string.Empty;

            var product = await _productRepository.Table.SingleOrDefaultAsync(x => x.Title == title);

            if (product != null)
            {
                errorMsg = "数据已存在";

                return (isSuccessed, errorMsg);
            }

            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
            {
                errorMsg = "类别不存在";

                return (isSuccessed, errorMsg);
            }

            product = new Product()
            {
                Title = title,
                Description = description,
                CategoryID = categoryId,
            };

            var attachments = new List<Attachment>();

            var productXAttachments = new List<ProductXAttachment>();

            for (int i = 0; i < attachmentUrls.Count; i++)
            {
                var attachmentUrl = attachmentUrls[i];

                var attachment = new Attachment()
                {
                    Url = attachmentUrl
                };

                attachments.Add(attachment);

                productXAttachments.Add(new ProductXAttachment()
                {
                    AttachmentID = attachment.ID,
                    ProductID = product.ID,
                    IsPoster = i == 0
                });
            }

            await _productRepository.InsertAsync(product);

            await _attachmentRepository.InsertAsync(attachments);

            await _productXAttachmentRepository.InsertAsync(productXAttachments);

            isSuccessed = true;

            return (isSuccessed, errorMsg);
        }

        public async Task<(bool IsSuccessed, string ErrorMsg)> UpdateProductAsync(Guid productId, string title, string description, Guid categoryId, IList<string> attachmentUrls)
        {
            bool isSuccessed = false;

            string errorMsg = string.Empty;

            var product = await _productRepository.Table.SingleOrDefaultAsync(x => x.Title == title);

            if (product != null && product.ID != productId)
            {
                errorMsg = "数据已存在";

                return (isSuccessed, errorMsg);
            }

            product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                errorMsg = "数据不存在";

                return (isSuccessed, errorMsg);
            }

            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
            {
                errorMsg = "类别不存在";

                return (isSuccessed, errorMsg);
            }

            var productXAttachments = await _productXAttachmentRepository.Table.Include(x => x.Attachment).Where(x => x.ProductID == productId).ToListAsync();

            var attachments = productXAttachments.Select(x => x.Attachment).ToList();

            await _productXAttachmentRepository.DeleteAsync(productXAttachments);

            await _attachmentRepository.DeleteAsync(attachments);

            product.Title = title;

            product.Description = description;

            product.CategoryID = categoryId;

            attachments = new List<Attachment>();

            productXAttachments = new List<ProductXAttachment>();

            for (int i = 0; i < attachmentUrls.Count; i++)
            {
                var attachmentUrl = attachmentUrls[i];

                var attachment = new Attachment()
                {
                    Url = attachmentUrl
                };

                attachments.Add(attachment);

                productXAttachments.Add(new ProductXAttachment()
                {
                    AttachmentID = attachment.ID,
                    ProductID = product.ID,
                    IsPoster = i == 0
                });
            }

            await _productRepository.UpdateAsync(product);

            await _attachmentRepository.InsertAsync(attachments);

            await _productXAttachmentRepository.InsertAsync(productXAttachments);

            isSuccessed = true;

            return (isSuccessed, errorMsg);

        }

        public async Task<(bool IsSuccessed, string ErrorMsg)> DeleteProductAsync(Guid productId)
        {
            bool isSuccessed = false;

            string errorMsg = string.Empty;

            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                errorMsg = "数据不存在";

                return (isSuccessed, errorMsg);
            }

            var productXAttachments = await _productXAttachmentRepository.Table.Include(x => x.Attachment).Where(x => x.ProductID == productId).ToListAsync();

            var attachments = productXAttachments.Select(x => x.Attachment).ToList();

            await _productXAttachmentRepository.DeleteAsync(productXAttachments);

            await _attachmentRepository.DeleteAsync(attachments);

            await _productRepository.DeleteAsync(product);

            isSuccessed = true;

            return (isSuccessed, errorMsg);
        }
    }
}
