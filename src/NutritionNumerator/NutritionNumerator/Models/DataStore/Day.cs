using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NutritionNumerator.Models.DataStore
{
    public class Day
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public DateTime Date { get; set; }
        public float EnergykCal
        {
            get
            {
                return Foods.Sum(f => f.EnergykCal);
            }
        }
        public float Protein
        {
            get
            {
                return Foods.Sum(f => f.Protein);
            }
        }
        public float Carbohydrates
        {
            get
            {
                return Foods.Sum(f => f.Carbohydrates);
            }
        }
        public float Fat
        {
            get
            {
                return Foods.Sum(f => f.Fat);
            }
        }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<FoodDS> Foods { get; set; }
    }
}
