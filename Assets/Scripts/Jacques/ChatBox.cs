using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityTracery;

public class ChatBox : MonoBehaviour
{
	public TextAsset GrammarFile;
	public SuperTextMesh TextOutput;
	public TraceryGrammar Grammar;
	public RectTransform chatWindow;
	public GameObject speechBubble;

	public int bubbleCount = 0;

	public AnimationCurve curve;

	[SerializeField]
	private List<GameObject> activeSpeechBubbles;
	
	public struct SpeechBubble
    {
		SuperTextMesh text;
		RectTransform position;
		RawImage img;		
    }

    private void Start()
	{
		Debug.Log("GrammarFile text: " + GrammarFile.text);
		Grammar = new TraceryGrammar(GrammarFile.text);
		chatWindow = this.GetComponent<RectTransform>();
	}

	public void SpawnChatBox()
	{
		bubbleCount++;
		var newBubble = Instantiate(speechBubble, chatWindow);
		TextOutput = newBubble.GetComponent<SuperTextMesh>();
		GenerateOutput(newBubble);
	}

	public GameObject GenerateOutput(GameObject currentBubble)
	{
		activeSpeechBubbles.Add(currentBubble);
		currentBubble.name = bubbleCount.ToString();


		GameObject lastBubble; // The previously spawned chat bubble
		RectTransform currentPosition = currentBubble.GetComponent<RectTransform>();

		for (int i = 0; i < activeSpeechBubbles.Count; i++)
		{
			if(i != 0)
            {
				lastBubble = activeSpeechBubbles[i - 1];
				currentBubble.LeanMoveY(lastBubble.transform.localPosition.y + 100, 1f);
				MoveBubble(lastBubble.transform, currentPosition, curve);
			}
		}

		var stringToParse = Grammar.Parse("output");
		

		return currentBubble;
	}

	public void MoveBubble(Transform lastBubbleTransform, RectTransform speechBubble, AnimationCurve motion) 
    {
		speechBubble.LeanMoveY(lastBubbleTransform.localPosition.y + 100, 1f).setEase(motion);
	}

	public void DeleteChatBoxes()
    {
		for(int i = 0; i < activeSpeechBubbles.Count; i++)
        {
			activeSpeechBubbles.Clear();
        }
		
		foreach(GameObject bubble in GameObject.FindGameObjectsWithTag("SpeechBubble"))
        {
			Destroy(bubble);
        }
    }
}
