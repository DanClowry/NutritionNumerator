using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NutritionNumerator.Models.DataStore
{
    [Table("Food")]
    public class FoodDS
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int FdcId { get; set; }
        public string Name { get; set; }
        public float EnergykCal { get; set; }
        public float Protein { get; set; }
        public float Carbohydrates { get; set; }
        public float Fat { get; set; }

        [ForeignKey(typeof(Day))]
        public int DayId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Day Day { get; set; }
    }
}
