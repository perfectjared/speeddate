using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityTracery;

public class Responses : Singleton<Responses>
{
    public GameObject top, middle, bottom;
    public Vector3 topPos, middlePos, bottomPos;
    public RectTransform spawnPos;
    private Vector3 startPos = new Vector3(270,-200,0);


    // Hides the responses and resets thier rotation
    public void InitialiseResponses()
    {
        if (Crosshair.Instance.ammo > 0) {
            GameObject newTop;
            GameObject newMiddle;
            GameObject newBottom;

            if (GameObject.Find("Top"))
            {
                Destroy(GameObject.Find("Top"));
                DeckManager.Instance.DiscardMessage(GameObject.Find("Top").GetComponent<ResponseBubble>().message);
            }

            if (GameObject.Find("Middle"))
            {
                Destroy(GameObject.Find("Middle"));
                DeckManager.Instance.DiscardMessage(GameObject.Find("Middle").GetComponent<ResponseBubble>().message);
            }

            if (GameObject.Find("Bottom"))
            {
                Destroy(GameObject.Find("Bottom"));
                DeckManager.Instance.DiscardMessage(GameObject.Find("Bottom").GetComponent<ResponseBubble>().message);
            }

            newTop = Instantiate(top, spawnPos.position, Quaternion.identity, this.transform);
            newTop.name = "Top";
            DisplayResponse(newTop);

            newMiddle = Instantiate(middle, spawnPos.position, Quaternion.identity, this.transform);
            newMiddle.name = "Middle";
            DisplayResponse(newMiddle);

            newBottom = Instantiate(bottom, spawnPos.position, Quaternion.identity, this.transform);
            newBottom.name = "Bottom";
            DisplayResponse(newBottom);

            GameplayManager.Instance.ChangeFlow(true);
        }
    }

    public void Replace(GameObject responseBubble)
    {      
        if (responseBubble.name == "Top")
        {
            responseBubble = Instantiate(top, spawnPos.position, Quaternion.identity, this.transform);
            responseBubble.name = "Top";
            DisplayResponse(responseBubble);
        }

        if(responseBubble.name == "Middle")
        {
            responseBubble = Instantiate(middle, spawnPos.position, Quaternion.identity, this.transform);
            responseBubble.name = "Middle";
            DisplayResponse(responseBubble);
        }

        if (responseBubble.name == "Bottom")
        {
            responseBubble = Instantiate(bottom, spawnPos.position, Quaternion.identity, this.transform);
            responseBubble.name = "Bottom";
            DisplayResponse(responseBubble);
        }
    }

    public void BubbleClicked(GameObject clickedBubble) 
    {
       
    }

    public void DisplayResponse(GameObject responseBubble)
    {
        //NOTE: I have no idea how we're gonna make the response bubbles be based on the message type     

        if (responseBubble.name == "Top")
        {
            responseBubble.LeanMoveLocal(topPos, 0.7f).setEaseSpring();
            responseBubble.LeanRotateZ(-32, 1f).setEaseOutElastic();
        }

        if (responseBubble.name == "Middle")
        {
            responseBubble.LeanMoveLocal(middlePos, 0.7f).setEaseSpring();
            responseBubble.LeanRotateZ(-16, 1f).setEaseOutElastic();
        }

        if (responseBubble.name == "Bottom")
        {
            responseBubble.LeanMoveLocal(bottomPos, 0.5f).setEaseSpring();
        }              
    }
}
