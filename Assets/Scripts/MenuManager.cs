using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText; 
    // Start is called before the first frame update
    void Start()
    {
        GetHighScore();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OpenSettings()
    {
        
    }
    [System.Serializable]
    class SaveData
    {
        public int highScore;
    }
    private void GetHighScore()
    {
        string path = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(path))
        {
            string jsonRead = File.ReadAllText(path);
            SaveData dataSaved = JsonUtility.FromJson<SaveData>(jsonRead);
            if (dataSaved != null)
            {
                highScoreText.text = "High score: " + dataSaved.highScore;
            }
            else
            {
                highScoreText.text = "High score: 0";
            }
        }
        else
        {
                highScoreText.text = "High score: 0";            
        }
    }
}
