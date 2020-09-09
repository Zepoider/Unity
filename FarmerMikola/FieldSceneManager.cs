using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class FieldSceneManager : MonoBehaviour
{
    public GameObject plane;
    public Canvas canvas;
    public Text optimal;
    public Text current;
    public TouchCatcher touchCatcher;
    public GameObject confirmWindow;
    public ChangeScene changeScene;
    public GenerateLevel genLevel;
    public GameObject optionPanel;
    public Button GoToMap;
    public Button nextLevel;
    public GameObject levelMap;

    void Awake()
    {
        changeScene = GameObject.FindGameObjectWithTag("ChangeScene").GetComponent<ChangeScene>();
        changeScene.fieldSceneManager = this;

        plane = GameObject.FindGameObjectWithTag("Ground");
        optionPanel = Instantiate(Resources.Load("Prefabs/OptionPanel"), Vector3.zero, Quaternion.identity) as GameObject;
        optionPanel.GetComponent<Options>().Create(canvas);
        genLevel = GetComponent<GenerateLevel>();
        if (changeScene.randomLevel)
        {
            GoToMap.gameObject.SetActive(false);
        }
    }

    public void FileToPrefs()
    {
        AddPrefs(Path.Combine(Application.streamingAssetsPath, "test.json"));
    }
    public void SaveFile()
    {
        Level level = new Level();
        level.levelMap = changeScene.levels[genLevel.levelNumber].levelMap;
        level.optimalSwipeNumber = changeScene.levels[genLevel.levelNumber].optimalSwipeNumber;
        level.progressBar = changeScene.levels[genLevel.levelNumber].progressBar;
        level.xAxis = changeScene.levels[genLevel.levelNumber].xAxis;
        level.yAxis = changeScene.levels[genLevel.levelNumber].yAxis;

        string json = JsonUtility.ToJson(level);

        string path = Path.Combine(Application.streamingAssetsPath, "Story0.json");

        using (StreamWriter stream = new StreamWriter(path, true))
        {
            stream.WriteLine(json);
        }
    }

    public void SaveToPrefs()
    {

        changeScene.levels[genLevel.levelNumber].progressBar = ProgressLevel();

        string levelstring = "";

        for (int i = 0; i < changeScene.levels.Count; i++)
        {
            levelstring += JsonUtility.ToJson(changeScene.levels[i]) + System.Environment.NewLine;
        }

        PlayerPrefs.SetString("levels", levelstring);
    }

    int ProgressLevel()
    {
        if (touchCatcher.swipeCount <= changeScene.levels[genLevel.levelNumber].optimalSwipeNumber)
        {
            return 3;
        }
        else if (touchCatcher.swipeCount <= changeScene.levels[genLevel.levelNumber].optimalSwipeNumber + 2)
        {
            if (changeScene.levels[genLevel.levelNumber].progressBar < 2)
            {
                return 2;
            }
        }
        else
        {
            if (changeScene.levels[genLevel.levelNumber].progressBar <= 1)
            {
                return 1;
            }
        }

        return changeScene.levels[genLevel.levelNumber].progressBar;
    }

    public void AddPrefs(string path)
    {

        string levelstring = "";

        using (StreamReader stream = new StreamReader(path))
        {
            while (stream.Peek() > -1)
            {
                levelstring += stream.ReadLine() + System.Environment.NewLine;
            }

            PlayerPrefs.SetString("levels", levelstring);
        }

        PlayerPrefs.Save();
    }
    public void ConvertFile()
    {
        using (StreamReader stream = new StreamReader(Path.Combine(Application.streamingAssetsPath, "Story0.json")))
        {
            while (stream.Peek() > -1)
            {
                string levelString = stream.ReadLine();
                levelString = levelString.Substring(0, levelString.Length - 1);

                using (StreamWriter streamwriite = new StreamWriter(Path.Combine(Application.streamingAssetsPath, "test1.json"), true))
                {
                    streamwriite.WriteLine(levelString);
                }
            }
        }
    }

    public void OpenOptions()
    {
        optionPanel.SetActive(true);
        optionPanel.GetComponent<Options>().LoadOptions();
    }

    public void GoToProgressMap()
    {
        levelMap.SetActive(true);
    }
}

[System.Serializable]
public class Level
{
    public List<int> levelMap = new List<int>();
    public int optimalSwipeNumber;
    public int progressBar;
    public int xAxis;
    public int yAxis;
}