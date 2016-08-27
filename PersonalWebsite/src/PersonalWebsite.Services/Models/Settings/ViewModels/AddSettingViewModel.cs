using PersonalWebsite.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public class AddSettingViewModel
    {
        [Required]
        [Display(Name = "Setting Name")]
        public string Name { get; set; }
        
        [Required]
        public SettingDataType Type { get; set; }

        public string Value { get; set; }
    }
}
