﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Message : MonoBehaviour
{
    public enum MessageType {
        Topic, SmallTalk, Feeling, FeelingTopic
    }

    public MessageType messageType;
    public Character.Topic topic;
    public float feeling;
    public string sentence;

    public Message(MessageType messageType = MessageType.Topic, Character.Topic topic = 0, float feeling = 99) {
        if (feeling != 99) { 
        this.messageType = messageType;
        this.topic = topic;
        this.feeling = feeling;
        this.sentence = GenerateSentence();
        } else { randomMessage(); }
    }

    [Button]
    public void Test() {
        new Message();
    }

    public void randomMessage() {
        this.messageType = (MessageType)Random.Range(0,4);
        this.topic = (Character.Topic)Random.Range(0, 10);
        this.feeling = (int)Random.Range(-3, 4);
        this.sentence = "message: " + messageType.ToString() + ", topic: " + topic.ToString() + ", feeling: " + feeling;
    }


    private string GenerateSentence() {
        //TEMP obviously
        return this.sentence = ("message: " + messageType.ToString() + ", topic: " + topic.ToString() + ", feeling: " + feeling);
    }
}
