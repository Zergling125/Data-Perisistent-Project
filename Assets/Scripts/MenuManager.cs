using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public TMP_Text bestScore;
    public TMP_InputField enterName;
    public string name;
    public string nextName;
    public int score = 0;

    public static MenuManager Instance;

    private void Start()
    {
        if (name == null)
        {
            name = "Name";
        }
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
        bestScore.SetText("Best Score : " + name + " : " + score);
    }

    public void EnterName()
    {
        if (name == "Name")
        {
            name = enterName.text;
        }
        else
        {
            nextName = enterName.text;
        }
        
        bestScore.SetText("Best Score : " + name + " : " + score);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        SaveData();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        
    }

    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int score;
    }

    public void SaveData()
    {
        PlayerData data = new PlayerData();

        data.name = name;
        data.score = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            name = data.name;
            score = data.score;
        }
    }
}