using NutritionNumerator.Models.DataStore;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionNumerator.Services
{
    public class SqliteDataStore : IDataStore
    {
        private readonly SQLiteAsyncConnection database;

        public SqliteDataStore(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
            database.CreateTableAsync<Day>().Wait();
            database.CreateTableAsync<FoodDS>().Wait();
        }

        public Task<List<Day>> GetDaysAsync()
        {
            return database.GetAllWithChildrenAsync<Day>();
        }

        public Task<Day> GetDayAsync(int id)
        {
            return database.GetWithChildrenAsync<Day>(id, recursive: true);
        }

        public async Task<Day> GetDayAsync(DateTime date)
        {
            var days = await GetDaysAsync();
            return days.Where(d => DateTime.Equals(d.Date.Date, date)).FirstOrDefault();
        }

        public Task SaveDayAsync(Day day)
        {
            return database.InsertOrReplaceWithChildrenAsync(day, recursive: true);
        }

        public Task DeleteDayAsync(Day day)
        {
            return database.DeleteAsync(day, true);
        }
    }
}
