using UnityEngine;
using UnityEngine.UI;

public class ProgressMapManager : MonoBehaviour {

    public ChangeScene changeScene;
    public int screenNumber;
    public int storyNumber;
    public int startCount;
    public int endCount;
    public GameObject progressField;
    public LoadCustomLevel [] buttons;
    public Canvas canvas;
    public GameObject optionPanel;

        void Awake ()
    {
        changeScene = GameObject.FindGameObjectWithTag("ChangeScene").GetComponent<ChangeScene>();
        changeScene.mapManager = this;

        optionPanel = Instantiate(Resources.Load("Prefabs/OptionPanel"), Vector3.zero, Quaternion.identity) as GameObject;
        optionPanel.GetComponent<Options>().Create(canvas);

        if (PlayerPrefs.GetInt("current") != 0)
        {
            storyNumber = PlayerPrefs.GetInt("current") / 60;
        }

        screenNumber = (PlayerPrefs.GetInt("current") % 60) / 20;
        startCount = storyNumber * 60 + screenNumber * 20;
        endCount = startCount + 19;
    }

    void Start()
    {
        buttons = progressField.transform.GetComponentsInChildren<LoadCustomLevel>();
        FeelButton();
    }

    public void OpenOptions()
    {
        optionPanel.SetActive(true);
        optionPanel.GetComponent<Options>().LoadOptions();
    }

    public void FeelButton()
    {
        int buttonNumber = 0;

        for (int i = startCount; i <= endCount; i++)
        {
            buttons[buttonNumber].levelNumber = i;

            if (changeScene.levels.Count > i)
            {
                buttons[buttonNumber].transform.GetChild(0).GetComponent<Image>().fillAmount = changeScene.levels[i].progressBar * 0.3333f;
            }
            
            if (i == PlayerPrefs.GetInt("current"))
            {
                buttons[buttonNumber].gameObject.GetComponentInChildren<Text>().fontSize = 20;
                buttons[buttonNumber].gameObject.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
            }else
            {
                buttons[buttonNumber].gameObject.GetComponentInChildren<Text>().fontSize = 14;
                buttons[buttonNumber].gameObject.GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
            }

            buttons[buttonNumber].gameObject.GetComponentInChildren<Text>().text = i.ToString();
            buttonNumber++;
        }
    }

    public void LevelChoise(int level)
    {
        storyNumber = level;
        startCount = storyNumber * 60;
        endCount = startCount + 19;
        FeelButton();
    }

    public void ReturnToField()
    {
        changeScene.GoToField();
    }
}
