using System;

namespace Two_C_Two_P.Core.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }

        byte Status { get; set; }

        string CreatedBy { get; set; }

        string ModifiedBy { get; set; }

        DateTimeOffset CreatedDate { get; set; }

        DateTimeOffset ModifiedDate { get; set; }
    }
}
