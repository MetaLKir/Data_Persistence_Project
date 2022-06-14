using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataTransfer : MonoBehaviour
{
    public static DataTransfer instanceDataTransfer;
    public int score;
    public string playerNameDefault = "no record";
    public string playerNameMaxScore = "no record";
    public string playerNameCurrent;

    private void Awake()
    {
        // !!! SINGLETON pattern !!!
        // DataTransfer object creates on loading Menu scene.
        // Since we save instance of DataTransfer, backing to menu causes multiple DataTransfers exist.
        // This if-check destroys DataTransfer if one already exist and ends function in this case.
        if (instanceDataTransfer != null)
        {
            Destroy(gameObject);
            return;
        }

        instanceDataTransfer = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string playerNameSaved;
        public int highestScoreSaved;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.playerNameSaved = playerNameCurrent;
        data.highestScoreSaved = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerNameMaxScore = data.playerNameSaved;
            score = data.highestScoreSaved;
        }
    }
}