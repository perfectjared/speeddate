using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseBubble : MonoBehaviour
{

    private RectTransform rectTransform;
    private Vector3 scaleTo = new Vector3(1.1f, 1.1f, 0);
    private Vector3 scaleFrom = new Vector3(1f, 1f, 0);

    private void OnGUI()
    {
        rectTransform = this.GetComponent<RectTransform>();
    }

    public void Click()
    {
        Responses.Instance.BubbleClicked(this.name);
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

}
