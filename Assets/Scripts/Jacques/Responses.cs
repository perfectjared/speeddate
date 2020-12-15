using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityTracery;

public class Responses : MonoBehaviour
{
    public RectTransform response_top, response_middle, response_bottom;
    public Texture[] bubbleColors; //TODO: compliment bubble, talk to Jake.
    public Vector3 topPos, middlePos, bottomPos;

    private Vector3 startPos = new Vector3(0,-250,0);

    // Start is called before the first frame update
    void Start()
    {
        InitialiseResponses();
    }

    // Hides the responses and resets thier rotation
    public void InitialiseResponses()
    {
        response_bottom.localPosition = startPos;        
        response_middle.localPosition = startPos;
        response_top.localPosition = startPos;

        response_top.rotation = Quaternion.identity;
        response_middle.rotation = Quaternion.identity;
    }

    // Gives the responses a random image and displayes it on screen
    public void ShowResponses()
    {
        //NOTE: I have no idea how we're gonna make the response bubbles be based on the message type

        var topImage = response_top.GetComponent<RawImage>();
        var middleImage = response_middle.GetComponent<RawImage>();
        var bottomImage = response_bottom.GetComponent<RawImage>();
        
        // Moves and rotates the top bubble
        response_top.LeanMoveLocal(topPos, 0.7f).setEaseSpring();
        response_top.LeanRotateZ(-32, 1f).setEaseOutElastic();
        // Moves and rotates the middle bubble
        response_middle.LeanMoveLocal(middlePos, 0.6f).setEaseSpring();
        response_middle.LeanRotateZ(-16, 1f).setEaseOutElastic();
        // Moves the bottom bubble
        response_bottom.LeanMoveLocal(bottomPos, 0.5f).setEaseSpring();

        // Randomly assigns a texture
        topImage.texture = bubbleColors[Random.Range(0, 3)];
        middleImage.texture = bubbleColors[Random.Range(0, 3)];
        bottomImage.texture = bubbleColors[Random.Range(0, 3)];
    }
}
