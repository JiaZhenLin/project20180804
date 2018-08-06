using System;
using System.Collections.Generic;
using System.Text;

namespace Project20180804.Core.Model
{
    public class Product : EntityBase
    {
        public Guid CategoryID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual IList<ProductXAttachment> ProductXAttachments { get; set; }
        public virtual Category Category { get; set; }
    }
}
