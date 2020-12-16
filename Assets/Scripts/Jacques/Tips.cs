using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    public void Refresh()
    {
        ToolTips.ShowTooltip_Static("Refresh Responses: Cycles through responses you've collected");
    }

    public void Affection()
    {
        ToolTips.ShowTooltip_Static("Affection Meter: How much this character likes you");
    }

    public void Combo()
    {
        ToolTips.ShowTooltip_Static("Flow Meter: Reflects the speed of the conversation");
    }

    public void Time()
    {
        ToolTips.ShowTooltip_Static("How much time you have before the next round");
    }

    public void Ammo()
    {
        ToolTips.ShowTooltip_Static("Ammo Count: You can only click on bubbles whilst you have ammo");
    }

    public void HideTip()
    {
        ToolTips.HideTooltip_Static();
    }
}
