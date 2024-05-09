using System;
using System.IO;
using UnityEngine;

namespace HypercasualPrototype
{
    public class DataService : IDataService
    {
        public bool SaveData<T>(string path, T data)
        {
            string fullPath = Application.persistentDataPath + path;

            try
            {
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                
                using FileStream stream = File.Create(fullPath);
                stream.Close(); // To counteract "file in use" issue
                //Encryption could be added here. I didn't have enough time.
                File.WriteAllText(fullPath, JsonUtility.ToJson(data));
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError("Save data failed: " + e.Message);
                return false;
            }
        }

        public (bool, T) LoadData<T>(string path)
        {
            string fullPath = Application.persistentDataPath + path;

            if (!File.Exists(fullPath))
            {
                Debug.Log($"Save data at {fullPath} does not exist!");
                return (false, default);
            }

            try
            {
                T data = JsonUtility.FromJson<T>(File.ReadAllText(fullPath));
                return (true, data);
            }
            catch (Exception e)
            {
                Debug.LogError("Load data failed: " + e.Message);
                throw e;
            }
        }
    }
}
