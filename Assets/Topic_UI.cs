using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hiking, Fracking, Cats, Dogs, Baking, Reading, TV, Coffee, Tea, Gardening

public class Topic_UI : MonoBehaviour
{
    public string topic = "";
    public string defaultTopic = "~";
    private SuperTextMesh stm;
    void Start()
    {
        stm = GetComponent<SuperTextMesh>();   
        topic = defaultTopic;
        ChangeText();
    }

    public void ReceiveTopic(string topic) {
        this.topic = topic;
        ChangeText();
    }

    public void Reset() {
        topic = defaultTopic;
        ChangeText();
    }

    private void ChangeText() {
        stm.text = topic;
        stm.Rebuild();
    }
}
