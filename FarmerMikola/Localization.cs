using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    private ChangeScene changeScene;

    void Awake()
    {
        changeScene = gameObject.GetComponent<ChangeScene>();
    }

    public void ApplyLocalization(int language)
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            OptionLoc(language);
            FirstRun firstRun = changeScene.startManager;

            if (language == 0)
            {
                if (PlayerPrefs.GetInt("startgame") == 0)
                {
                    firstRun.start.GetComponentInChildren<Text>().text = "New Game";
                }
                else
                {
                    firstRun.start.GetComponentInChildren<Text>().text = "Continue";
                }
                

                firstRun.options.GetComponentInChildren<Text>().text = "Options";
                firstRun.random.GetComponentInChildren<Text>().text = "Random";
            }
            else if (language == 1)
            {
                if (PlayerPrefs.GetInt("startgame") == 0)
                {
                    firstRun.start.GetComponentInChildren<Text>().text = "Новая Игра";
                }
                else
                {
                    firstRun.start.GetComponentInChildren<Text>().text = "Продолжить";
                }

                firstRun.options.GetComponentInChildren<Text>().text = "Настройки";
                firstRun.random.GetComponentInChildren<Text>().text = "Случайная Игра";
            }
            else if (language == 2)
            {
                if (PlayerPrefs.GetInt("startgame") == 0)
                {
                    firstRun.start.GetComponentInChildren<Text>().text = "Нова Гра";
                }
                else
                {
                    firstRun.start.GetComponentInChildren<Text>().text = "Продовжити";
                }

                firstRun.options.GetComponentInChildren<Text>().text = "Налаштування";
                firstRun.random.GetComponentInChildren<Text>().text = "Випадкова Гра";
            }
        }else
        if (SceneManager.GetActiveScene().name == "Field")
        {
            OptionLoc(language);
            FieldSceneManager fieldManager = changeScene.fieldSceneManager;

            if (language == 0)
            {
                
            }
            else if (language == 1)
            {
                
            }
            else if (language == 2)
            {
                
            }
        }
        else
        if (SceneManager.GetActiveScene().name == "ProgressMap")
        {
            OptionLoc(language);
            ProgressMapManager progressMap = changeScene.mapManager;

            if (language == 0)
            {

            }
            else if (language == 1)
            {

            }
            else if (language == 2)
            {

            }
        }

    }

    private void OptionLoc(int language)
    {
        Options options = changeScene.options.GetComponent<Options>();

        if (language == 0)
        {
            options.close.GetComponentInChildren<Text>().text = "Close";
            options.terms.GetComponentInChildren<Text>().text = "Policy Terms";
            options.ads.GetComponentInChildren<Text>().text = "Remove Ads";
            options.mainScreen.GetComponentInChildren<Text>().text = "Main Screen";
        }
        else if (language == 1)
        {
            options.close.GetComponentInChildren<Text>().text = "Закрыть";
            options.terms.GetComponentInChildren<Text>().text = "Политика Безопасности";
            options.ads.GetComponentInChildren<Text>().text = "Убрать Рекламу";
            options.mainScreen.GetComponentInChildren<Text>().text = "Главный Экран";
        }
        else if (language == 2)
        {
            options.close.GetComponentInChildren<Text>().text = "Закрити";
            options.terms.GetComponentInChildren<Text>().text = "Політика Безпеки";
            options.ads.GetComponentInChildren<Text>().text = "Відключити Рекламу";
            options.mainScreen.GetComponentInChildren<Text>().text = "Головний Екран";
        }
    }

}
