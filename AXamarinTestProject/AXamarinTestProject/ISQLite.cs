
namespace AXamarinTestProject
    {
    public interface ISQLite //получение пути к базе на разных устройствах
        {
            string GetDatabasePath(string filename);
        }
    }
