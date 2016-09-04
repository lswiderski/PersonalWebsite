﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Services.Models
{
    public interface ISettingModel
    {
        void AddSetting(AddSettingViewModel model);

        void EditSetting(EditSettingViewModel model);

        SettingViewModel GetSetting(int id);

        List<SettingViewModel> GetSettings();
    }
}