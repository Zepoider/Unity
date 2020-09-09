using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public List<Level> levels;
    public bool randomLevel;
    public int levelFromMap;
    public GameObject options;
    public int removeAdds;
    public ProgressMapManager mapManager;
    public FirstRun startManager;
    public FieldSceneManager fieldSceneManager;
    public LoreSceneManager loreManager;
    public bool firstRun;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (!PlayerPrefs.HasKey("current"))
        {
            PlayerPrefs.SetInt("current", 0);
        }

        string loadPath = Path.Combine(Application.streamingAssetsPath, "test.json");


        if (Application.platform == RuntimePlatform.Android && !PlayerPrefs.HasKey("levels"))
        {
            StartCoroutine(AndroidLoad(loadPath));
        }
        else if (!PlayerPrefs.HasKey("levels"))
        {
            StartCoroutine(AndroidLoad(loadPath));
        }

        //PlayerPrefs.SetInt("startgame", 0);
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("ads", 0);
    }

    public void GoToField(bool random = false)
    {
        if (random)
        {
            randomLevel = true;
            levelFromMap = Random.Range(0, levels.Count);

            if (!firstRun)
            {
                LoadFile(PlayerPrefs.GetString("levels"));
                firstRun = true;
            }

            SceneManager.LoadScene("Field");
        }

        SceneManager.LoadScene("Field");
    }

    public void GoToLore(bool x = false)
    {
        if (!firstRun)
        {
            LoadFile(PlayerPrefs.GetString("levels"));
            firstRun = true;
        }
        if (PlayerPrefs.GetInt("startgame") == 0)
        {
            PlayerPrefs.SetInt("startgame", 1);
            SceneManager.LoadScene("Lore");
        }
        else
        {
            SceneManager.LoadScene("Field");
        }
    }

    public void GoToProgressMap()
    {
        SceneManager.LoadScene("ProgressMap");
    }

    public void GoToMain()
    {
        randomLevel = false;

        if (SceneManager.GetActiveScene().name != "Start")
        {
            SceneManager.LoadScene("Start");
        }
        else
        {
            options.GetComponent<Options>().Close();
        }

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadFile(string prefs)
    {
        using (StringReader reader = new StringReader(prefs))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                levels.Add(JsonUtility.FromJson<Level>(line));
            }
        }
    }

    public IEnumerator AndroidLoad(string path)
    {
        UnityWebRequest request = UnityWebRequest.Get(path);

        string levelstring = "";

        request.SendWebRequest();

        while (!request.isDone)
        {
            yield return null;
        }

        if (!request.isNetworkError && (request.responseCode == 0 || request.responseCode == (long)System.Net.HttpStatusCode.OK))
        {

            using (StringReader reader = new StringReader(request.downloadHandler.text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    levelstring += line + System.Environment.NewLine;
                }
                PlayerPrefs.SetString("levels", levelstring);
            }
        }

        request.Dispose();
    }
}
