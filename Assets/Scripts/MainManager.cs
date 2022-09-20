using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    private int score;
    [SerializeField] private Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        SaveScore(); // ABSTRACTION
        SceneManager.LoadScene(0);
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
    }

    private void SaveScore()
    {
        string path = Application.persistentDataPath + "/saveData.json";
        SaveData dataToSave = new SaveData();
        if (File.Exists(path))
        {
            string jsonRead = File.ReadAllText(path);
            SaveData dataSaved = JsonUtility.FromJson<SaveData>(jsonRead);
            if (dataSaved != null)
            {
                if(dataSaved.highScore < score) {
                    dataToSave.highScore = score;
                } else {
                    dataToSave.highScore = dataSaved.highScore;
                }
            }
            else
            {
                dataToSave.highScore = 0;
            }
        }
        else
        {
            dataToSave.highScore = score;
        }

        string json = JsonUtility.ToJson(dataToSave);

        File.WriteAllText(Application.persistentDataPath + "/saveData.json", json);
    }
}
