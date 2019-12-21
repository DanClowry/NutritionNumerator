using NutritionNumerator.Models.DataStore;
using SQLite;
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
        }

        public Task<List<Day>> GetDaysAsync()
        {
            return database.Table<Day>().ToListAsync();
        }

        public Task<Day> GetDayAsync(int id)
        {
            return database.Table<Day>().Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Day> GetDayAsync(DateTime date)
        {
            var days = await GetDaysAsync();
            return days.Where(d => DateTime.Equals(d.Date.Date, date)).FirstOrDefault();
        }

        public Task<int> SaveDayAsync(Day day)
        {
            if (day.Id != 0)
            {
                return database.UpdateAsync(day);
            }
            else
            {
                return database.InsertAsync(day);
            }
        }

        public Task<int> DeleteDayAsync(Day day)
        {
            return database.DeleteAsync(day);
        }
    }
}
