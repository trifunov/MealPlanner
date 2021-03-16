using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MealPlanner.Service.Helpers
{
    public static class PlanHelper
    {
        public static List<int> ShiftsToList(string shifts)
        {
            var result = new List<int>();
            foreach(var item in shifts.Split(','))
            {
                result.Add(Int32.Parse(item));
            }

            return result;
        }

        public static List<string> GetShiftNames(List<int> shifts)
        {
            var result = new List<string>();
            foreach (var item in shifts)
            {
                result.Add(((ShiftEnum)item).GetDescription());
            }

            return result;
        }

        public static string GetShiftName(int shift)
        {
            return ((ShiftEnum)shift).GetDescription();
        }

        public static string GetDescription(this Enum value)
        {
            var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var descriptionAttribute = enumMember == null
                    ? default(DescriptionAttribute)
                    : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            return descriptionAttribute == null
                        ? value.ToString()
                        : descriptionAttribute.Description;
        }
    }
}
