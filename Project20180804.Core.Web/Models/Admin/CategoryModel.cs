using System;

namespace Project20180804.Core.Web.Models.Admin
{
    public class AddCategoryBindingModel
    {
        public string Title { get; set; }
        public string AttachmentUrl { get; set; }
    }

    public class UpdateCategoryBindingModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string AttachmentUrl { get; set; }
    }

    public class DeleteCategoryBindingModel
    {
        public Guid ID { get; set; }
    }
}
