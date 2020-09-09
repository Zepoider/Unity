using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    public Button close;
    public Button terms;
    public Button ads;
    public Button mainScreen;
    public Slider music;
    public Slider sfx;
    public Dropdown language;

    private AudioSource audioMusic;
    private ChangeScene changeScene;

	public void Create (Canvas canvas)
    {
        changeScene = GameObject.FindGameObjectWithTag("ChangeScene").GetComponent<ChangeScene>();
        changeScene.options = this.gameObject;
        

        this.transform.SetParent(canvas.transform, false);

        LoadOptions();

        audioMusic = changeScene.GetComponent<AudioSource>();
        audioMusic.volume = music.value;

        this.gameObject.SetActive(false);

        music.onValueChanged.AddListener(delegate { audioMusic.volume = music.value; });

    }

    public void Close()
    {
        PlayerPrefs.SetFloat("music", music.value);
        PlayerPrefs.SetFloat("sfx", sfx.value);
        PlayerPrefs.SetInt("language", language.value);
        this.gameObject.SetActive(false);
    }

    public void LoadOptions()
    {
        if (PlayerPrefs.GetInt("ads") == 1)
        {
            ads.interactable = false;
        }

        language.value = PlayerPrefs.GetInt("language");

        //if (changeScene.firstRun && !changeScene.firstRun.firstRun)
        //{
        //    changeScene.GetComponent<Localization>().ApplyLocalization(language.value);
        //}
        
        music.value = PlayerPrefs.GetFloat("music");
        sfx.value = PlayerPrefs.GetFloat("sfx");
        language.onValueChanged.AddListener(delegate { changeScene.GetComponent<Localization>().ApplyLocalization(language.value); });
        mainScreen.onClick.AddListener(delegate { changeScene.GoToMain(); });
        ads.onClick.AddListener(delegate { changeScene.GetComponent<Purchase>().BuyProductID("noads"); });

        changeScene.GetComponent<Localization>().ApplyLocalization(language.value);
    }

}
