using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    public enum MessageType {
        Topic, SmallTalk, Feeling, FeelingTopic
    }

    public MessageType messageType;
    public Character.Topic topic;
    public float feeling;
    private string sentence;

        void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Message(MessageType messageType, Character.Topic topic, float feeling) {
        this.messageType = messageType;
        this.topic = topic;
        this.feeling = feeling;
        GenerateSentence();
    }

    private string GenerateSentence() {
        this.sentence = "";
    }
}
