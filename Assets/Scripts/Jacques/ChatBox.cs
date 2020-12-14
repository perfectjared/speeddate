﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityTracery;

public class ChatBox : MonoBehaviour
{
	public TextAsset GrammarFile;
	public SuperTextMesh message;
	public TraceryGrammar Grammar;
	public RectTransform chatWindow;
	public GameObject speechBubble;

	public int bubbleCount = 0;
	public bool fadeText;

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

    private void Update()
    {
        for(int i = 0; i < activeSpeechBubbles.Count; i++)
        {
			if (activeSpeechBubbles[i].transform.localPosition.y > 84 && fadeText)
            {
				var speechBubbleRawImage = activeSpeechBubbles[i].GetComponent<RawImage>();
			}
        }
    }

    // Instatiates a speech bubble prefab
    public void SpawnChatBox()
	{
		bubbleCount++;
		var newBubble = Instantiate(speechBubble, chatWindow);

		newBubble.LeanRotate(new Vector3(0, 0, 0), 0.5f).setEase(curve);
		newBubble.LeanAlpha(1, 0.3f);
		newBubble.LeanScale(new Vector3(1, 1, 0), 0.3f);


		message = newBubble.GetComponentInChildren<SuperTextMesh>();
		fadeText = true;
		GenerateOutput(newBubble);
		MoveBubble(newBubble);
	}

	// Fills the instatiated speech bubbles text component with teh desired string from the JSON grammar file
	public GameObject GenerateOutput(GameObject newBubble)
	{
		activeSpeechBubbles.Add(newBubble); // Adds the instantiated bubble to the active bubbles list.
		newBubble.name = (bubbleCount - 1).ToString();
		message.text = Grammar.Parse("#greetings# #descriptions#,  My name is <c=black><b>#name#</b></c>. #thinking# <c=red>#topics#</c>, #question#");
		return newBubble;
	}

	public void MoveBubble(GameObject newBubble) 
    {
		for (int i = 0; i < activeSpeechBubbles.IndexOf(newBubble); i++)
		{
			var activeBubbleTransform = activeSpeechBubbles[i].transform.localPosition;
			activeSpeechBubbles[i].GetComponent<RectTransform>().LeanMove(new Vector3(activeBubbleTransform.x, activeBubbleTransform.y + 100, 0), 0.5f);
			message.color = Color.Lerp(Color.clear, Color.black, 0.5f);
		}
	}

	public void FadeBoxes()
    {
		message.GetComponent<RectTransform>().LeanAlphaText(0, 0.5f);
		fadeText = false;
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
