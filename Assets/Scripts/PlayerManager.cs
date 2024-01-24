using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    PlayerData playerData;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SavePlayerName(string name)
    {
        playerData ??= new PlayerData();
        playerData.Name = name;
    }

    public void SavePlayerScore(int score)
    {
        playerData ??= new PlayerData();
        playerData.Score = score;
    }

    public void SavePlayer()
    {
        if (playerData == null)
            return;
        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/leaderboard.json";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, json);
            return;
        }

        using (StreamWriter writer = File.AppendText(path))
        {
            writer.WriteLine();
            writer.Write(json);
        }
    }

    public List<PlayerData> LoadLeaderboard()
    {
        string path = Application.persistentDataPath + "/leaderboard.json";

        if (!File.Exists(path))
            return null;

        List<PlayerData> leaderboard = new List<PlayerData>();

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                leaderboard.Add(JsonUtility.FromJson<PlayerData>(line));
            }
        }

        return leaderboard;
    }

    public PlayerData GetBestScore()
    {
        List<PlayerData> leaderboard = LoadLeaderboard();

        if(leaderboard == null)
            return null;

        return leaderboard.OrderByDescending(x => x.Score).FirstOrDefault();

    }

}

[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Score;
}
