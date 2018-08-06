using System;
using System.Collections.Generic;

namespace Project20180804.Core.DTO.ProductDtos
{
    public class ProductDTO
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryTitle { get; set; }
        public Guid CategoryID { get; set; }
        public string PosterUrl { get; set; }
        public IList<string> AttachmentUrls { get; set; }
    }
}
