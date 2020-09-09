using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveProgressField : MonoBehaviour
{
    private int directionIndex;
    public bool moved;
    public bool changeDirection;
    public GameObject anchorRight;
    public GameObject anchorLeft;
    private Vector3 originalPosition;
    private Vector3 direction;
    public ProgressMapManager mapManager;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (moved)
        {
            MoveField();
        }
    }

    public void MoveField()
    {
        if (directionIndex == 1)
        {
            transform.position = Vector3.Lerp(transform.position, direction, 0.5f);

            if (transform.position.x >= 11 || changeDirection)
            {

                mapManager.FeelButton();

                if (!changeDirection)
                {
                    transform.position = anchorLeft.transform.position;
                    direction = originalPosition;
                }

                changeDirection = true;

                if (transform.position.x > 0.05f)
                {
                    moved = false;
                    changeDirection = false;
                    transform.position = originalPosition;
                }
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, direction, 0.5f);

            if (transform.position.x <= -11 || changeDirection)
            {

                mapManager.FeelButton();

                if (!changeDirection)
                {
                    transform.position = anchorRight.transform.position;
                    direction = originalPosition;
                }

                changeDirection = true;

                if (transform.position.x > 0 && transform.position.x < 0.1f)
                {
                    moved = false;
                    changeDirection = false;
                    transform.position = originalPosition;
                }
            }
        }
    }

    public void SwipeRight()
    {
        if (mapManager.startCount < (60*mapManager.storyNumber+40))
        {
            mapManager.startCount += 20;
            mapManager.endCount += 20; 

            if (!moved)
            {
                moved = true;

                if (!changeDirection)
                {
                    direction = anchorRight.transform.position;
                }

                directionIndex = 1;
            }
        }
    }

    public void SwipeLeft()
    {
        if (mapManager.startCount > (60 * mapManager.storyNumber))
        {
            mapManager.startCount -= 20;
            mapManager.endCount -= 20;

            if (!moved)
            {
                moved = true;

                if (!changeDirection)
                {
                    direction = anchorLeft.transform.position;
                }

                directionIndex = 2;
            }
        }
    }
}
