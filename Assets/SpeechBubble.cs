using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    private Message message;
    public void Click() {
        if (Crosshair.Instance.ammo > 0) {
            message = GetComponent<Message>();
            if (message) {
                GameplayManager.Instance.ShootMessage(message);
                GetComponent<Button>().interactable = false;
            }
        }

        this.gameObject.GetComponent<RectTransform>().LeanScale(new Vector3(0.2f, 0.2f, 0), 1f);
        this.gameObject.GetComponent<RectTransform>().LeanAlpha(0, 1f);
        this.gameObject.GetComponent<RectTransform>().LeanMoveLocal(new Vector3(350, -195), 1f);
        this.GetComponentInChildren<SuperTextMesh>().enabled = false;
    }
}
