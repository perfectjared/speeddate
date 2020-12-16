using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
	public SuperTextMesh message;
	public RectTransform chatWindow;
	public GameObject speechBubble;
	public GameObject playerSpeechBubble;
	public Message bubbleMessage;

	public int bubbleCount = 0;
	public bool fadeText;

	public AnimationCurve curve;

	[SerializeField]
	private List<GameObject> activeSpeechBubbles;

	[SerializeField]
	private List<GameObject> activeResponseBubbles;
	
    private void Start()
	{
		chatWindow = this.GetComponent<RectTransform>();
		bubbleMessage = new Message();
	}

    private void Update()
    {
		// Deletes speech bubble if they above the chat window
        for(int i = 0; i < activeSpeechBubbles.Count; i++)
        {
			activeSpeechBubbles[i].name = i.ToString();
			if (activeSpeechBubbles[i].transform.localPosition.y > 84)
            {
				var speechBubbleText = activeSpeechBubbles[i].GetComponentInChildren<SuperTextMesh>();
				speechBubbleText.enabled = false;
				Destroy(activeSpeechBubbles[i]);
				activeSpeechBubbles.RemoveAt(i);
				
			}
        }
    }

    public void ReceiveMessage(Message msg, bool character = true) {
		bubbleMessage.messageType = msg.messageType;
		bubbleMessage.feeling = msg.feeling;
		bubbleMessage.topic = msg.topic;
		bubbleMessage.sentence = msg.sentence;
		SpawnSpeechBubble(character);
	}
	
	// Instatiates a speech bubble prefab
    public void SpawnSpeechBubble(bool character)
	{
		GameObject newBubble;
		if (!character) {
			newBubble = Instantiate(playerSpeechBubble, chatWindow);
		} else {
			newBubble = Instantiate(speechBubble, chatWindow);
		}

		newBubble.LeanRotate(new Vector3(0, 0, 0), 1f).setEaseOutElastic();
		newBubble.GetComponent<RectTransform>().LeanAlpha(0.8f, 0.5f);
		newBubble.LeanScale(new Vector3(1, 1, 0), 0.3f);

		var tempMessage = newBubble.AddComponent<Message>();
		tempMessage.messageType = bubbleMessage.messageType;
		tempMessage.feeling = bubbleMessage.feeling;
		tempMessage.topic = bubbleMessage.topic;
		tempMessage.sentence = bubbleMessage.sentence;

		message = newBubble.GetComponentInChildren<SuperTextMesh>();
		fadeText = true;
		GenerateOutput(newBubble);
		MoveBubble(newBubble);
	}

	// Fills the instatiated speech bubbles text component with teh desired string from the JSON grammar file
	public GameObject GenerateOutput(GameObject newBubble)
	{
		activeSpeechBubbles.Add(newBubble); // Adds the instantiated bubble to the active bubbles list.
		//newBubble.name = (bubbleCount - 1).ToString();
		//message.text = Grammar.Parse("#output#");
		message.text = bubbleMessage.sentence;
		return newBubble;
	}

	// Moves the older chat messages upwards
	public void MoveBubble(GameObject newBubble) 
    {
		for (int i = 0; i < activeSpeechBubbles.IndexOf(newBubble); i++)
		{
			var activeBubbleTransform = activeSpeechBubbles[i].transform.localPosition;
			activeSpeechBubbles[i].GetComponent<RectTransform>().LeanMove(new Vector3(activeBubbleTransform.x, activeBubbleTransform.y + 110, 0), 0.5f).setEaseSpring();
		}
	}

	//Deletes all chat boxes and clears the activeSpeechBubble List
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
