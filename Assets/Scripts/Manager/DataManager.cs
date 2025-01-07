using System;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    GameData gameData;
    public GameData GameData { get { return gameData; } }

#if UNITY_EDITOR
    string path = Path.Combine(Application.dataPath, $"Resources/Data/Save");
#else
    string path = Path.Combine(Application.persistentDataPath, $"Resources/Data/Save");
#endif

    public void NewData()
    {
        gameData = new GameData();
    }

    public void SaveData<T>(int idx = 0)
    {
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText($"{path}_{idx}.txt", json);
    }

    public void LoadData(int idx = 0)
    {
        if (File.Exists($"{path}_{idx}.txt") == false)
        {
            NewData();
            return;
        }

        string json = File.ReadAllText($"{path}_{idx}.txt");
        try
        {
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        catch (Exception ex)
        {
            Debug.Log($"Load data fail : {ex.Message}");
            NewData();
        }
    }

    public bool ExistData(int idx = 0)
    {
        return File.Exists($"{path}_{idx}.txt");
    }
}
