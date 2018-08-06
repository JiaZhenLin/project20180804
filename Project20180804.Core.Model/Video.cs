using System;

namespace Project20180804.Core.Model
{
    public class Video : EntityBase
    {
        public Guid AttachmentID { get; set; }
        public virtual Attachment Attachment
        {
            get; set;
        }
    }
}
