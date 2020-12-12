using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum Topic { 
        Hiking, Fracking, Cats, Dogs, Baking, Reading, TV, Coffee, Tea, Gardening
    };
    
    public float affection = 0;
    private float flow = 0;
    [SerializeField]
    private bool active = false;
    public List<int> sentiments = new List<int>();
    private float timeToSpeak = 5.0f;
    [Range(1.0f, 5.0f)]
    public float speechRate = 2.5f;

    void Update() {
        if (active) {
            timeToSpeak -= flow * Time.deltaTime;
            if (timeToSpeak < 0f) {
                Speak();
                timeToSpeak = speechRate * 10f;
            }
        }
    }

    public void Initialize() {
        active = true;
    }

    public void Denitialize() {
        active = false;
    }

    public bool RelevanceCheck(Message msg) {
        //check if relevant
        return true;
    }

    public void ReceiveMessage(Message msg) {
        if (RelevanceCheck(msg)) {

        }
        //change affection based on message received
        //respond based on message received
    }

    private void Speak() {
        //duh
    }

    public void ReceiveFlow(float flow) {
        this.flow = flow;
    }
}
