using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    PlayerData playerData;

    private void Awake()
    {
        if(Instance == null)
        {
            Destroy(gameObject);
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
        File.WriteAllText(Application.dataPath + "/savefile.json", json);
    }

    public void LoadPlayer()
    {
        string path = Application.dataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
    }
    [System.Serializable]
    class PlayerData
    {
        public string Name;
        public int Score;
    }

}
