using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public Vector3 originalPosition;
    public bool cellVacant = true;
    Vector3 startDragPos;
    GameManager gameManager;
    [SerializeField]
    PuzzleCell currentCell;
    [SerializeField]
    PuzzleCell returnCell;
    void Awake()
    {
        originalPosition = transform.position;
        gameManager = Camera.main.GetComponent<GameManager>();
        currentCell = this;
        returnCell = this;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (returnCell.originalPosition == transform.position)
        {
            returnCell.cellVacant = true;
        }
        startDragPos = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, 4f);
        

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.pressEventCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 4f));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, originalPosition.z);

        foreach (var puzzle in gameManager.puzzleElement)
        {

            currentCell = puzzle.GetComponent<PuzzleCell>();

            if (Vector3.Distance(currentCell.originalPosition, transform.position) < 0.5f)
            {
                if (currentCell.cellVacant)
                {
                    transform.position = currentCell.originalPosition;
                    currentCell.cellVacant = false;
                    returnCell = puzzle.GetComponent<PuzzleCell>();
                    break;
                }
                else
                {
                    transform.position = startDragPos;

                    if (returnCell.originalPosition == transform.position)
                    {
                        returnCell.cellVacant = false;
                        break;
                    }
                }
            }
        }
    }
}
