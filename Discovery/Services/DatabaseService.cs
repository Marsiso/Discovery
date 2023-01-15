using Discovery.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discovery.Services;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseService(string databasePath)
    {
        _database = new SQLiteAsyncConnection(databasePath);
        _database.CreateTableAsync<PhotoEntity>();
    }

    public Task<int> CreatePhoto(PhotoEntity photo) => _database.InsertAsync(photo);

    public Task<List<PhotoEntity>> GetAllPhotos() => _database.Table<PhotoEntity>().ToListAsync();

    public Task<int> UpdatePhoto(PhotoEntity photo) => _database.UpdateAsync(photo);

    public Task<int> DeletePhoto(PhotoEntity photo) => _database.DeleteAsync(photo);
}
