using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseBubble : MonoBehaviour
{

    public Message message;
   

    private RectTransform rectTransform;
    private Vector3 scaleTo = new Vector3(1.1f, 1.1f, 0);
    private Vector3 scaleFrom = new Vector3(1f, 1f, 0);

    private void OnGUI()
    {
        rectTransform = this.GetComponent<RectTransform>();
    }

    public void Click()
    {
        Responses.Instance.BubbleClicked(this.gameObject);
        //GameplayManager.Instance.RecieveMessage(message);
       
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
                break;
            case Message.MessageType.Feeling:
                break;
            case Message.MessageType.SmallTalk:
                break;
            case Message.MessageType.FeelingTopic:
                break;
            default:
                Debug.Log("No message type");
                break;
        }
    }

}
