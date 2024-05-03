using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MeteoApp.Services
{
    public class CityService
    {
        private static CityService instance;
        private SQLiteAsyncConnection db;

        private CityService()
        {
            Init().Wait(); // Inizializza il database in modo sincrono durante la creazione dell'istanza
        }

        public static CityService Instance
        {
            get
            {
                if (instance == null)
                    instance = new CityService();
                return instance;
            }
        }

        private async Task Init()
        {
            try
            {
                if (db != null)
                    return;

                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
                db = new SQLiteAsyncConnection(databasePath);
                await db.CreateTableAsync<Entry>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante l'inizializzazione del database: " + ex.Message);
            }
        }

        public async Task AddCity(string cityName, double longitude, double latitude)
        {
            try
            {
                var city = new Entry
                {
                    Name = cityName,
                    Longitude = longitude,
                    Latitude = latitude
                };

                await db.InsertAsync(city);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante l'aggiunta della città: " + ex.Message);
            }
        }

        public async Task RemoveCity(int id)
        {
            try
            {
                await db.DeleteAsync<Entry>(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante la rimozione della città: " + ex.Message);
            }
        }

        public async Task<IEnumerable<Entry>> GetCities()
        {
            try
            {
                return await db.Table<Entry>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante il recupero delle città: " + ex.Message);
                return null;
            }
        }

        public async Task<Entry> GetCity(int id)
        {
            try
            {
                return await db.Table<Entry>().FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante il recupero della città: " + ex.Message);
                return null;
            }
        }
    }
}
