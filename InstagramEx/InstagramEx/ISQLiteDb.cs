using SQLite;

namespace InstagramEx
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection(string DataBaseName);
    }
}
