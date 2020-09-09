using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseActiveWindow : MonoBehaviour
{
    private GameObject activeWindow;
    void Start()
    {
        activeWindow = this.transform.parent.gameObject;
    }

    public void Close()
    {
        activeWindow.SetActive(false);
    }
}
