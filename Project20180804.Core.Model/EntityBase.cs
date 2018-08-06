using System;

namespace Project20180804.Core.Model
{
    public abstract class EntityBase
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
