using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutController : MonoBehaviour
{
    public List<Sprite> images;
    public Canvas main;
    public Image image;
    public Button nextBtn;
    public Button doneBtn;

    private int index = 0;
    public void StartTut()
    {
        doneBtn.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(true);
        gameObject.SetActive(true);
        index = 0;
        image.sprite = images[0];
    }

    public void NextAction()
    {
        index++;
        image.sprite = images[index];

        if (index == images.Count-1)
        {
            doneBtn.gameObject.SetActive(true);
            nextBtn.gameObject.SetActive(false);
        }
    }

    public void Done()
    {
        gameObject.SetActive(false);
        main.gameObject.SetActive(true);
    }
}
