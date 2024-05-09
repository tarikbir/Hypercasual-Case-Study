namespace HypercasualPrototype
{
    public interface IDataService
    {
        bool SaveData<T>(string path, T data);
        (bool, T) LoadData<T>(string path);
    }
}
