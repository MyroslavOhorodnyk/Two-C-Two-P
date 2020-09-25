using System;
using Two_C_Two_P.Core.Interfaces;

namespace Two_C_Two_P.Core.Entities
{
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }

        public byte Status { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }
    }
}
