using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialImages : MonoBehaviour
{
    public List<Texture> TutorialSlides; // The list of images
    public GameObject slide;
    public SuperTextMesh pressSpace;
    private RectTransform rectTransform;


    // Disable the crosshair
    private Crosshair crosshair;

    private void Awake()
    {
        foreach(GameObject image in GameObject.FindGameObjectsWithTag("TutorialImage"))
        {
            TutorialSlides.Add(image.GetComponent<RawImage>().texture);
        }
    }

    private void Start()
    {
        pressSpace.gameObject.SetActive(true);
        rectTransform = slide.GetComponent<RectTransform>();

        rectTransform.LeanAlpha(1, 0.5f);
        
        crosshair = FindObjectOfType<Crosshair>();
        crosshair.enabled = false;
    }

    public void FadeIn()
    {
        rectTransform.LeanAlpha(1, 0.5f);
    }

    public void FadeOut()
    {
        rectTransform.LeanAlpha(0, 0.5f);
    }

    private void Update()
    {

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (slide.GetComponent<RawImage>().texture == TutorialSlides[0])
            {
                FadeIn();
                slide.GetComponent<RawImage>().texture = TutorialSlides[1];
            }

            else if (slide.GetComponent<RawImage>().texture == TutorialSlides[1])
            {
                FadeIn();
                slide.GetComponent<RawImage>().texture = TutorialSlides[2];
            }

            else if (slide.GetComponent<RawImage>().texture == TutorialSlides[2])
            {
                FadeIn();
                slide.GetComponent<RawImage>().texture = TutorialSlides[3];
            }

            else if (slide.GetComponent<RawImage>().texture == TutorialSlides[3])
            {
                FadeIn();
                slide.GetComponent<RawImage>().texture = TutorialSlides[4];
            }

            else if (slide.GetComponent<RawImage>().texture == TutorialSlides[4])
            {
                FadeOut();
                pressSpace.gameObject.SetActive(false);
            }
        }

        //for(int i = 0; i > TutorialSlides.Count; i++)
        //{
            
        //}

       
    }
}
