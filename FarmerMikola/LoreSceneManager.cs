using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoreSceneManager : MonoBehaviour
{
    public ChangeScene changeScene;
    public Canvas canvas;
    public Image loreCanvas;

    void Awake()
    {
        changeScene = GameObject.FindGameObjectWithTag("ChangeScene").GetComponent<ChangeScene>();
        changeScene.loreManager = this;
    }
}
