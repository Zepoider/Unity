using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TouchCatcher : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler

{

    public GameObject player;
    private Move move;
    public int swipeCount;
    private ChangeScene changeScene;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Field")
        {
            move = player.GetComponent<Move>();
        }
        else if (SceneManager.GetActiveScene().name == "Lore")
        {
            changeScene = Camera.main.GetComponent<LoreSceneManager>().changeScene;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "Lore")
        {
            changeScene.GoToField();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "Field")
        {
            if (!move.moved)
            {
                if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
                {

                    if (eventData.delta.x > 0)
                    {
                        move.directionMove = new Vector3(player.transform.position.x + 1, player.transform.position.y, 0);
                        move.directionIndex = 1;
                    }
                    else if (eventData.delta.x < 0)
                    {
                        move.directionMove = new Vector3(player.transform.position.x - 1, player.transform.position.y, 0);
                        move.directionIndex = 2;
                    }
                    else return;
                }
                else
                {
                    if (eventData.delta.y > 0)
                    {
                        move.directionMove = new Vector3(player.transform.position.x, player.transform.position.y + 1, 0);
                        move.directionIndex = 3;
                    }
                    else if (eventData.delta.y < 0)
                    {

                        move.directionMove = new Vector3(player.transform.position.x, player.transform.position.y - 1, 0);
                        move.directionIndex = 4;
                    }
                    else return;
                }

                if (move.CheckMove(move.directionMove))
                {
                    swipeCount++;
                    Camera.main.GetComponent<FieldSceneManager>().current.text = swipeCount.ToString();
                    move.moved = true;
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
