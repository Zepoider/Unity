using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class GameManager : MonoBehaviour
{
    public GameObject[] puzzleElement = new GameObject[36];
    public Sprite[] puzzleSprite = new Sprite[2];
    public GameObject contour;
    private int imageCount;
    void Start()
    {
        NewPuzzle(false);
        imageCount = 0;
        contour.SetActive(true);

        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3676671", false);
        }
    }

    public void NewPuzzle(bool change)
    {
        if (change)
        {
            imageCount++;
            if (imageCount >= puzzleSprite.Length)
            {
                imageCount = 0;
            }

            contour.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().sprite = puzzleSprite[imageCount];

            if (Advertisement.IsReady())
            {
                Advertisement.Show("video");
            }
        }

        foreach (GameObject element in puzzleElement)
        {
            if (change)
            {
                SpriteRenderer newSprite = element.gameObject.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
                newSprite.sprite = puzzleSprite[imageCount];
                element.GetComponent<PuzzleCell>().cellVacant = true;
            }

            element.transform.position = new Vector3(Random.Range(-2.2f, 2.2f), Random.Range(-1.2f, -4f));

        }
    }
}
