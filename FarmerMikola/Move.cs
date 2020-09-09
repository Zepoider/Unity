using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Move : MonoBehaviour

{
    public Cell cell;
    public bool moved;
    public Vector3 directionMove;
    public int directionIndex;
    public float speed;
    public InputField inputField;
    public bool destroyObstacles;
    public bool createObstacles;
    GenerateLevel generateLevel;

    void Start()
    {
        generateLevel = Camera.main.GetComponent<GenerateLevel>();
    }

    void Update()
    {
        if (moved)
        {
            MovePlayer(directionMove, directionIndex);
        }
    }
    void MovePlayer(Vector3 move, int direction)
    {

        if (transform.position != cell.transform.position)
        {
            if (CheckMove(move))
            {
                transform.position = Vector3.MoveTowards
                    (transform.position, new Vector3(cell.transform.position.x, cell.transform.position.y, -0.5f), speed * Time.deltaTime);

                moved = true;

                if (transform.position == new Vector3(cell.transform.position.x, cell.transform.position.y, -0.5f))
                {
                    Actions();
                    if (direction == 1)
                    {
                        directionMove = new Vector3(transform.position.x + 1, transform.position.y, 0);
                    }
                    else if (direction == 2)
                    {
                        directionMove = new Vector3(transform.position.x - 1, transform.position.y, 0);
                    }
                    else if (direction == 3)
                    {
                        directionMove = new Vector3(transform.position.x, transform.position.y + 1, 0);
                    }
                    else if (direction == 4)
                    {
                        directionMove = new Vector3(transform.position.x, transform.position.y - 1, 0);
                    }
                    CheckMove(directionMove);
                }

            }
        }
    }


    public bool CheckMove(Vector3 direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction - transform.position, out hit, 10f, 5))
        {
            if (hit.transform.tag == "Cell")
            {
                if (hit.transform.GetComponent<Cell>().type != 1)
                {
                    cell = hit.transform.GetComponent<Cell>();
                    return true;
                }
            }
        }

        moved = false;
        return false;
    }

    public void DestroyObstacles()
    {
        if (!destroyObstacles)
        {
            destroyObstacles = true;
        }
        else
        {
            destroyObstacles = false;
        }
    }

    public void CreateObstacle()
    {
        if (!createObstacles)
        {
            createObstacles = true;
        }
        else
        {
            createObstacles = false;
        }
    }

    void Actions()
    {
        if (cell.Darkness != null)
        {
            DestroyImmediate(cell.Darkness);
            generateLevel.fogCount--;
            if (generateLevel.fogCount == 0)
            {
                generateLevel.fieldManager.touchCatcher.gameObject.SetActive(false);
                generateLevel.fieldManager.SaveToPrefs();
                generateLevel.fieldManager.confirmWindow.SetActive(true);
            }
        }
        cell.type = 2;
    }

    public void ChangeSpeed()
    {
        if (inputField.text != null)
        {
            speed = int.Parse(inputField.text);
        }
    }
}
