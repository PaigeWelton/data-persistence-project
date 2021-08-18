using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public List<HighScore> scoresList = new List<HighScore>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadNameAndScore();
    }

    [System.Serializable]
    class SaveData
    {
        public List<HighScore> scoresList = new List<HighScore>();
    }

    public void SaveNameAndScore()
    {
        SaveData data = new SaveData();

        data.scoresList = scoresList;

        var json = JsonConvert.SerializeObject(scoresList);
        //string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadNameAndScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            //SaveData deserializedData = JsonConvert.DeserializeObject<SaveData>(json);
            //SaveData data = JsonUtility.FromJson<SaveData>(json);

            scoresList = (List<HighScore>)JsonConvert.DeserializeObject(json, typeof(List<HighScore>));
        }
    }
}
