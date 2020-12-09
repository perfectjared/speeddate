using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_UI : MonoBehaviour
{
    private SuperTextMesh stm;
    public int time = 99;
    
    void Start() {
        stm = GetComponent<SuperTextMesh>();
    }
    public void setTime(int time) {
        this.time = time;
        stm.text = time + "";
        stm.Rebuild();
    }
}
