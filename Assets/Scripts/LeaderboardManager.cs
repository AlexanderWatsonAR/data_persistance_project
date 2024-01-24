using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] GameObject ScoreEntryRow;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerManager.Instance == null)
            return;

        List<PlayerData> leaderboard = PlayerManager.Instance.LoadLeaderboard();

        if (leaderboard == null)
            return;

        foreach(PlayerData data in leaderboard)
        {
            if (data == null)
                continue;

            AddEntry(data);
        }

    }

    private void AddEntry(PlayerData data)
    {
        GameObject entry = Instantiate(ScoreEntryRow, ScoreEntryRow.transform.parent);
        entry.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = data.Name;
        entry.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = data.Score.ToString();
    }

}
