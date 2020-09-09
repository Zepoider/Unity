using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMap : MonoBehaviour
{
    public GameObject locationPanel;
    public GameObject levelPanel;
    public GameObject gamePanel;
    public int startLevel;
    public int endLevel;
    public int location = 1;
    public int level = 1;
    private Button previousButtonLocation;
    private Button previousButtonLevel;
   
    void Start()
    {
        if (!PlayerPrefs.HasKey("location"))
        {
            location = 1;
            PlayerPrefs.SetInt("location", location);
        }else
        {
            location = PlayerPrefs.GetInt("location");
        }

        if (!PlayerPrefs.HasKey("level"))
        {
            level = 1;
            PlayerPrefs.SetInt("level", level);
        }
        else
        {
            level = PlayerPrefs.GetInt("level");
        }

        previousButtonLocation = locationPanel.transform.GetChild(location - 1).GetComponent<Button>();
        previousButtonLocation.GetComponent<Image>().color = Color.white;

        previousButtonLevel = levelPanel.transform.GetChild(level - 1).GetComponent<Button>();
        previousButtonLevel.GetComponent<Image>().color = Color.white;
    }

    public void SetLocation(Button button)
    {
        location = int.Parse(button.name);

        if (previousButtonLocation != null)
        {
            previousButtonLocation.GetComponent<Image>().color = Color.red;
        }

        button.GetComponent<Image>().color = Color.white;
        previousButtonLocation = button;
    }

    public void SetLevel(Button button)
    {
        level = int.Parse(button.name);

        if (previousButtonLevel != null)
        {
            previousButtonLevel.GetComponent<Image>().color = Color.blue;
        }

        button.GetComponent<Image>().color = Color.white;
        previousButtonLevel = button;

    }
}
