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

	public GameObject GenerateOutput(GameObject newBubble)
	{
		activeSpeechBubbles.Add(newBubble);
		newBubble.name = (bubbleCount - 1).ToString();
		SuperTextMesh bubbleText = newBubble.GetComponentInChildren<SuperTextMesh>();		

		bubbleText.text = Grammar.Parse("#output#");
		MoveBubble(newBubble);

		return newBubble;
	}

	public void MoveBubble(GameObject newBubble) 
    {
		for (int i = 0; i < activeSpeechBubbles.IndexOf(newBubble); i++)
		{
			var activeBubbleTransform = activeSpeechBubbles[i].transform.localPosition;
			activeSpeechBubbles[i].GetComponent<RectTransform>().LeanMove(new Vector3(activeBubbleTransform.x, activeBubbleTransform.y + 100, 0), 1);
		}
	}

	public void DeleteChatBoxes()
    {
		for(int i = 0; i < activeSpeechBubbles.Count; i++)
        {
			activeSpeechBubbles.Clear();
			bubbleCount = 0;
        }
		
		foreach(GameObject bubble in GameObject.FindGameObjectsWithTag("SpeechBubble"))
        {
			Destroy(bubble);
        }
    }
}
