using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTips : MonoBehaviour
{
    private static ToolTips instance;

    private Text tooltipText;
    private RectTransform rectTransformBG;
    [SerializeField]
    private Camera uiCam;

    private void Awake()
    {
        instance = this;
        rectTransformBG = transform.Find("BG").GetComponent<RectTransform>();
        tooltipText = transform.Find("tt").GetComponent<Text>();
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector2 localPoint;

        if(Input.mousePosition.x > 1100)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), new Vector2(Input.mousePosition.x - 700, Input.mousePosition.y), uiCam, out localPoint);
        }
        else
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), new Vector2(Input.mousePosition.x + 300, Input.mousePosition.y), uiCam, out localPoint);
        }

        transform.localPosition = localPoint;
    }

    public void ShowToolTip(string tooltipString)
    {
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        float textPadding = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + (textPadding * 2), tooltipText.preferredHeight + (textPadding * 2));
        rectTransformBG.sizeDelta = backgroundSize;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }    

    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.ShowToolTip(tooltipString);
    }

    public static void HideTooltip_Static()
    {
        instance.HideToolTip();
    }
}
