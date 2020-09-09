using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerDownHandler {

    public GameObject obstacle;
    public GameObject Darkness;
    public int type;
    GameObject player;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (player.GetComponent<Move>().createObstacles && type == 2)
        {
            type = 1;
            player.GetComponent<Move>().createObstacles = false;
        }
    }

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
}
