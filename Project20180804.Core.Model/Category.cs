using System;

namespace Project20180804.Core.Model
{
    public class Category:EntityBase
    {
        public string Title { get; set; }
        public Guid AttachmentID { get; set; }
        public virtual Attachment Attachment { get; set; }
    }
}
