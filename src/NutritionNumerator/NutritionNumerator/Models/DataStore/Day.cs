using SQLite;
using System;

namespace NutritionNumerator.Models.DataStore
{
    public class Day
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public DateTime Date { get; set; }
        public float EnergykCal { get; set; }
        public float Protein { get; set; }
        public float Carbohydrates { get; set; }
        public float Fat { get; set; }
    }
}
