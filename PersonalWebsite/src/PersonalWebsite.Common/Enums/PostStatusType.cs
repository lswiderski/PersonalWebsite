using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Common.Enums
{
    public enum PostStatusType : byte
    {
        DRAFT = 0,

        PUBLISHED = 1,

        TRASHED = 2,

        DELETED = 4
    }
}
