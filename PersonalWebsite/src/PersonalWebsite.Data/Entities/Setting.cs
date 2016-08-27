using PersonalWebsite.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Data.Entities
{
    public class Setting
    {
        public int SettingId { get;  set; }

        public string Name { get; set; }

        public SettingDataType Type { get; set; }

        public string Value { get; set; }
    }
}
