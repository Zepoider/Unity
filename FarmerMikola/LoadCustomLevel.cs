using UnityEngine;
using UnityEngine.UI;

public class LoadCustomLevel : MonoBehaviour {

    private Button button;
    public int levelNumber;
    private FieldSceneManager mapManager;
    private LevelMap levelMap;

    void Start ()
    {
        mapManager = Camera.main.GetComponent<FieldSceneManager>();
        button = this.GetComponent<Button>();
        button.onClick.AddListener(ReturnToField);
        levelMap = mapManager.levelMap.GetComponent<LevelMap>();
    }

    public void ReturnToField()
    {
        if (mapManager.changeScene.levels.Count > levelNumber && mapManager.changeScene.levels[levelNumber].progressBar > 0)
        {
            PlayerPrefs.SetInt("current", levelNumber);
            PlayerPrefs.SetInt("location", levelMap.location); 
            PlayerPrefs.SetInt("level", levelMap.level); 
            mapManager.changeScene.GoToField();
        }
        else
        {
            return;
        }
    }
}
