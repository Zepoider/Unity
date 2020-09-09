using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyObstacles : MonoBehaviour , IPointerDownHandler {

    public GameObject player;
    public Cell cell;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (player.GetComponent<Move>().destroyObstacles)
        {
            cell.type = 2;
            player.GetComponent<Move>().destroyObstacles = false;
            Destroy(gameObject);
        }
    }

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	
}
