using System;
using System.Collections.Generic;

namespace Project20180804.Core.Web.Models.Admin
{
    public class AddProductBindingModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CategoryID { get; set; }
        public IList<string> AttachmentUrls { get; set; }
    }

    public class UpdateProductBindingModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CategoryID { get; set; }
        public IList<string> AttachmentUrls { get; set; }
    }

    public class DeleteProductBindingModel
    {
        public Guid ID { get; set; }
    }
}
