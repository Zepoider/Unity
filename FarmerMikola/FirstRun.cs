using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class FirstRun : MonoBehaviour {

    public ChangeScene changeScene;
    public Button start;
    public Button options;
    public Button random;
    public GameObject optionPanel;
    public Canvas canvas;

	void Awake()
    {
        if (GameObject.FindGameObjectWithTag("ChangeScene"))
        {
            changeScene = GameObject.FindGameObjectWithTag("ChangeScene").GetComponent<ChangeScene>();
        }

        if (!PlayerPrefs.HasKey("startgame"))
        {
            PlayerPrefs.SetInt("startgame", 0);
        }
        

        if (!changeScene)
        {
            GameObject change = Instantiate(Resources.Load("Prefabs/ChangeScene"), Vector3.zero, Quaternion.identity) as GameObject;
            changeScene = change.GetComponent<ChangeScene>();

            if (PlayerPrefs.HasKey("ads"))
            {
                changeScene.removeAdds = PlayerPrefs.GetInt("ads");
            }
            else
            {
                PlayerPrefs.SetInt("ads", 0);
            }
        }
        
        changeScene.startManager = this;

        optionPanel = Instantiate(Resources.Load("Prefabs/OptionPanel"), Vector3.zero, Quaternion.identity) as GameObject;
        Options optionScript = optionPanel.GetComponent<Options>();

        if (PlayerPrefs.HasKey("language"))
        {
            optionScript.language.value = PlayerPrefs.GetInt("language");
        }
        else
        {
            PlayerPrefs.SetInt("language", LanguageChoise());
        }

        if (PlayerPrefs.HasKey("music"))
        {
            optionScript.music.value = PlayerPrefs.GetFloat("music");
        }
        else
        {
            PlayerPrefs.SetFloat("music", 0.5f);
        }

        optionScript.Create(canvas);

        start.onClick.AddListener(() => changeScene.GoToLore());
        random.onClick.AddListener(() => changeScene.GoToField(true));
    }

    public void OpenOptions()
    {
        optionPanel.SetActive(true);
        optionPanel.GetComponent<Options>().LoadOptions();
    }

    int LanguageChoise()
    {
        CultureInfo lang = CultureInfo.CurrentUICulture;

        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            return 1;
        }
        else if (Application.systemLanguage == SystemLanguage.Ukrainian)
        {
            return 2;
        }

        return 0;
    }
}
