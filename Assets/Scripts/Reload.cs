using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    private SuperTextMesh stm;
    public string text = "<c=red><w>RELOAD";

    private void Start()
    {
        stm = GetComponent<SuperTextMesh>();
    }

    public void Display()
    {
        stm.text = text;
        stm.Rebuild();
    }

    public void Stop()
    {
        stm.text = "";
        stm.Rebuild();
    }
}
