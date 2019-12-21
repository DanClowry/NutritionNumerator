using NutritionNumerator.Models.DataStore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutritionNumerator.Services
{
    public interface IDataStore
    {
        Task<List<Day>> GetDaysAsync();
        Task<Day> GetDayAsync(int id);
        Task<Day> GetDayAsync(DateTime date);
        Task<int> SaveDayAsync(Day day);
        Task<int> DeleteDayAsync(Day day);
    }
}
