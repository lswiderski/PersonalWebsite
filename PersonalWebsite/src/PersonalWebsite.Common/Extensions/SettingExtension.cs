using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace PersonalWebsite.Common.Extensions
{
    public static class SettingExtension
    {
        public static int FromSetting(this int value, string setting)
        {
            var result = int.TryParse(setting, out value);

            if (result)
            {
                return value;
            }
            else
            {
                return 0;
            }
        }

        public static decimal FromSetting(this decimal value, string setting)
        {
            var result = decimal.TryParse(setting, out value);

            if (result)
            {
                return value;
            }
            else
            {
                return 0;
            }
        }

        public static bool FromSetting(this bool value, string setting)
        {
            var result = bool.TryParse(setting, out value);

            if (result)
            {
                return value;
            }
            else
            {
                return false;
            }
        }

        public static string FromSetting(this string value, string setting)
        {
            var result = string.IsNullOrEmpty(setting);

            if (result)
            {
                return setting;
            }
            else
            {
                return "";
            }
        }

    }
}
