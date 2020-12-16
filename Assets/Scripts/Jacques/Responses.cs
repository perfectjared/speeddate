using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityTracery;

public class Responses : Singleton<Responses>
{
    public Texture[] bubbleColors;
    public GameObject top, middle, bottom;
    public Vector3 topPos, middlePos, bottomPos;
    public RectTransform spawnPos;
    private Vector3 startPos = new Vector3(270,-200,0);


    // Hides the responses and resets thier rotation
    public void InitialiseResponses()
    {
  
        Debug.Log("Spawned");
        GameObject newTop = top;
        GameObject newMiddle = middle;
        GameObject newBottom = bottom;

        if (!GameObject.Find("Top"))
        {
            newTop = Instantiate(newTop, spawnPos.position, Quaternion.identity, this.transform);
            newTop.name = "Top";
            newTop.LeanMoveLocal(topPos, 0.7f).setEaseSpring();
            newTop.LeanRotateZ(-32, 1f).setEaseOutElastic();
        }

        if (!GameObject.Find("Middle"))
        {
            newMiddle = Instantiate(newMiddle, spawnPos.position, Quaternion.identity, this.transform);
            newMiddle.name = "Middle";
            newMiddle.LeanMoveLocal(middlePos, 0.7f).setEaseSpring();
            newMiddle.LeanRotateZ(-16, 1f).setEaseOutElastic();
        }

        if (!GameObject.Find("Bottom"))
        {
            newBottom = Instantiate(newBottom, spawnPos.position, Quaternion.identity, this.transform);
            newBottom.name = "Bottom";
            newBottom.LeanMoveLocal(bottomPos, 0.5f).setEaseSpring();
        }
    }

    public void BubbleClicked(string name) 
    {
        Debug.Log(name);        
    }

    // Shows three repsonses
    public void Cycle()
    {
        //NOTE: I have no idea how we're gonna make the response bubbles be based on the message type     

        //var topImage = response_top.GetComponent<RawImage>();
        //var middleImage = response_middle.GetComponent<RawImage>();
        //var bottomImage = response_bottom.GetComponent<RawImage>();

        //// Moves and rotates the top bubble
        //response_top.LeanMoveLocal(topPos, 0.7f).setEaseSpring();
        //response_top.LeanRotateZ(-32, 1f).setEaseOutElastic();
        //// Moves the middle button
        //response_middle.LeanMoveLocal(middlePos, 0.7f).setEaseSpring();
        //response_middle.LeanRotateZ(-16, 1f).setEaseOutElastic();
        //// Moves the bottom bubble
        //response_bottom.LeanMoveLocal(bottomPos, 0.5f).setEaseSpring();

        //// Randomly assigns a texture
        //topImage.texture = bubbleColors[Random.Range(0, 4)];
        //middleImage.texture = bubbleColors[Random.Range(0, 4)];
        //bottomImage.texture = bubbleColors[Random.Range(0, 4)];
    }
}
