using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GenerateLevel : MonoBehaviour
{

    public GameObject cellClosed;
    public int _xAxis;
    public int _yAxis;
    private float _positionX;
    private float _positionY;
    private float _offsetX = 0.5f;
    private float _offsetY = 0.5f;
    public List<GameObject> cellArray;
    public GameObject player;
    private GameObject plane;
    public int levelNumber;
    public int storyNumber;
    public FieldSceneManager fieldManager;
    public int fogCount;

   private void Start()
    {

        if (PlayerPrefs.GetInt("ads") == 0)
        {
            if (Advertisement.isSupported)
            {
                Advertisement.Initialize("3218328", false);
            }
        }

        fieldManager = GetComponent<FieldSceneManager>();

        plane = fieldManager.plane;

        if (!fieldManager.changeScene.randomLevel)
        {
            levelNumber = PlayerPrefs.GetInt("current");
        }else
        {
            levelNumber = fieldManager.changeScene.levelFromMap;
        }

        GenerateField();
        
    }

    public void GenerateForward()
    {

        StopAllCoroutines();

        if (levelNumber < fieldManager.changeScene.levels.Count-1)
        {
            if (!fieldManager.changeScene.randomLevel)
            {
                fieldManager.confirmWindow.SetActive(false);
                levelNumber++;
            }
            else
            {
                fieldManager.confirmWindow.SetActive(false);
                levelNumber = Random.Range(0, fieldManager.changeScene.levels.Count);
            }

            if (PlayerPrefs.GetInt("ads") == 0 && levelNumber % 10 == 0)
            {
                if (Advertisement.isSupported)
                {
                    Advertisement.Show("video");
                }
            }

            fieldManager.nextLevel.gameObject.SetActive(false);
            GenerateField();
        }
    }
    public void GenerateBack()
    {
        if (levelNumber > 0)
        {
            levelNumber--;
            GenerateField();
        }
    }

    public void GenerateCurrent()
    {
        fieldManager.confirmWindow.SetActive(false);
        fieldManager.nextLevel.gameObject.SetActive(false);

        StopAllCoroutines();

        GenerateField();
    }

    private void InstallStartPosition()
    {
        _xAxis = fieldManager.changeScene.levels[levelNumber].xAxis;
        _yAxis = fieldManager.changeScene.levels[levelNumber].yAxis;

        if (_xAxis % 2 != 0)
        {
            _offsetX = 0;
        }
        else
        {
            _offsetX = 0.5f;
        }

        if (_yAxis % 2 != 0)
        {
            _offsetY = 0;
        }
        else
        {
            _offsetY = 0.5f;
        }

        _positionX = -_xAxis / 2 + _offsetX;
        _positionY = -_yAxis / 2 + _offsetY;

        plane.transform.localScale += new Vector3(_yAxis / 10f - 1, 1, _xAxis / 10f - 1);

    }

    private void ClearField()
    {
        if (cellArray.Count > 0)
        {
            foreach (GameObject item in cellArray)
            {
                DestroyImmediate(item);
            }

            cellArray.Clear();
            plane.transform.localScale = new Vector3(1, 1, 1);
            fieldManager.touchCatcher.swipeCount = 0;
            fieldManager.current.text = fieldManager.touchCatcher.swipeCount.ToString();
            fogCount = 0;
        }
    }

    public void GenerateField()
    {
        if (levelNumber < fieldManager.changeScene.levels.Count)
        {
            ClearField();

            InstallStartPosition();

            for (int i = 0; i < _yAxis; i++)
            {
                for (int j = 0; j < _xAxis; j++)
                {
                    GameObject cell = Instantiate(cellClosed, new Vector3((i + _positionY), (j + _positionX), 0), Quaternion.identity);
                    cellArray.Add(cell);
                }
            }

            player.transform.position = new Vector3(cellArray[0].transform.position.x, cellArray[0].transform.position.y, -0.5f);

            for (int i = 0; i < fieldManager.changeScene.levels[levelNumber].levelMap.Count; i++)
            {
                if (fieldManager.changeScene.levels[levelNumber].levelMap[i] == 1)
                {
                    Cell obstacle = cellArray[i].GetComponent<Cell>();
                    obstacle.type = 1;
                    obstacle.gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
            }

            for (int i = 1; i < cellArray.Count; i++)
            {
                Cell cell = cellArray[i].GetComponent<Cell>();

                if (cell.type == 0)
                {
                    GameObject fog = Instantiate(Resources.Load("Prefabs/Darkness") as GameObject, cell.transform.position, Quaternion.identity);
                    cell.Darkness = fog;
                    fogCount++;
                    fog.transform.SetParent(cell.transform);
                }
            }

            fieldManager.optimal.text = fieldManager.changeScene.levels[levelNumber].optimalSwipeNumber.ToString();
            fieldManager.touchCatcher.gameObject.SetActive(true);
        }

        if (!fieldManager.changeScene.randomLevel)
        {
            PlayerPrefs.SetInt("current", levelNumber);
        }

        StartCoroutine(WaitAvoidButton());
    }

    public void AvoidLevel()
    {
        if (fieldManager.changeScene.levels[levelNumber].progressBar == 0 && !fieldManager.changeScene.randomLevel)
        {
            fieldManager.changeScene.levels[levelNumber].progressBar = 1;
        }
        
        fieldManager.nextLevel.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("ads") == 0)
        {
            if (Advertisement.isSupported)
            {
                Advertisement.Show("rewardedVideo");
            }
        }
        GenerateForward();
    }

    private IEnumerator WaitAvoidButton()
    {
        yield return new WaitForSeconds(20f);
        fieldManager.nextLevel.gameObject.SetActive(true);
    }
}