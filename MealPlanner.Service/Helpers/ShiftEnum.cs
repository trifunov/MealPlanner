using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MealPlanner.Service.Helpers
{
    public enum ShiftEnum
    {
        [Description("Прва смена")]
        First = 0,

        [Description("Втора смена")]
        Second = 1,

        [Description("Трета смена")]
        Third = 2,

        [Description("Администрација")]
        Administration = 3,
    }
}
