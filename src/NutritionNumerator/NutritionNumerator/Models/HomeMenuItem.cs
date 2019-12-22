﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NutritionNumerator.Models
{
    public enum MenuItemType
    {
        Summary,
        Browse,
        History,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
