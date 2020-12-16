using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    private Message message;
    public void Click() {
        message = GetComponent<Message>();
        if (message) {
            GameplayManager.Instance.ShootMessage(message);
            GetComponent<Button>().interactable = false;
        }
    }
}
