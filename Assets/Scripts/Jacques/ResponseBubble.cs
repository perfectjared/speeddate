using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ResponseBubble : MonoBehaviour
{
    public Message message;
    [SerializeField]
    private Texture[] bubbleColors;

    private Text newMessage;
    private RectTransform rectTransform;
    private RawImage rawImage;
    private Vector3 scaleTo = new Vector3(1.1f, 1.1f, 0);
    private Vector3 scaleFrom = new Vector3(1f, 1f, 0);

    private void OnGUI()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        newMessage = GetComponentInChildren<Text>();
        message = DeckManager.Instance.drawOne();
        newMessage.text = message.sentence;
        SetMessage(message);
    }

    public void Click()
    {
        Responses.Instance.BubbleClicked(this.gameObject);
        GameplayManager.Instance.ReceiveMessage(message);
        DeckManager.Instance.AddToDiscard(message);
        Destroy(this.gameObject);
    }
    public void OnHover()
    {
        rectTransform.LeanScale(scaleTo, 0.2f);
    }

    public void OffHover()
    {
        rectTransform.LeanScale(scaleFrom, 0.2f);
    }
    public void SetMessage(Message msg)
    {
        message = msg;
        switch (message.messageType)
        {
            case Message.MessageType.Topic:
                rawImage.texture = bubbleColors[1];
                break;
            case Message.MessageType.Feeling:
                rawImage.texture = bubbleColors[2];
                break;
            case Message.MessageType.SmallTalk:
                rawImage.texture = bubbleColors[3];
                break;
            case Message.MessageType.FeelingTopic:
                rawImage.texture = bubbleColors[0];
                break;
            default:
                Debug.Log("No message type");
                break;
        }
    }

}
