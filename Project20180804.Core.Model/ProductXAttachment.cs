using System;

namespace Project20180804.Core.Model
{
    public class ProductXAttachment : EntityBase
    {
        public Guid ProductID { get; set; }
        public Guid AttachmentID { get; set; }
        public bool IsPoster { get; set; }
        public virtual Attachment Attachment { get; set; }
        public virtual Product Product { get; set; }
    }
}
