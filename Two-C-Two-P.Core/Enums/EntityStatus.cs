using System;

namespace Two_C_Two_P.Core.Enums
{
    [Flags]
    public enum EntityStatus : byte
    {
        Undefined = 0,
        Active = 1,
        Inactive = 2,
        Deleted = 4
    }
}
