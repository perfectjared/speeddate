using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Likes { 
    //populate list 
};

public class Character : MonoBehaviour
{
    public float affection = 0;
    private float flow = 0;
    public void Initialize() {
        
    }

    public void ReceiveMessage(GameObject gameObject) {
        //change affection based on message received
        //respond based on message received
    }

    public void ReceiveFlow(float flow) {
        this.flow = flow;
    }
}
