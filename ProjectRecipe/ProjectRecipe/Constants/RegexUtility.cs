using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Constants
{
    public static class RegexUtility
    {
        public static string ValidEmailAddress = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        public static string ValidContactNumber =/* @"^\d{10}$"*/@"^(9)\d{9}$";
        public static string ValidMobileNumber = @"^(\+639)\d{9}$";
        public static string ValidName = @"^([a-zA-Z]+?)([-\s'][a-zA-Z]+)*?$";
        public static string ValidUsername = @"^(?=.{6,30}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$";
    }
}
