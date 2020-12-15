using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Character : MonoBehaviour
{
    public enum Topic { 
        Hiking, Fracking, Cats, Dogs, Baking, Reading, TV, Coffee, Tea, Gardening
    };
    
    [Range(0.0f, 100.0f)]
    public float affection = 0;
    [Range(0.0f, 1.0f)]
    private float flow = 0.1f;
    public bool active = false;
    public List<int> sentiments = new List<int>();
    private float timeToSpeak = 0.0f;
    [Range(0.1f, 1.0f)]
    public float speechRate = 0.5f;
    private int speechSinceSubjectChange = 0;

    void Update() {
        if (active) {
            flow = GameplayManager.Instance.flow;
            topic = GameplayManager.Instance.topic;

            timeToSpeak -= flow * Time.deltaTime;
            if (timeToSpeak < 0f) {
                Speak();
                timeToSpeak = speechRate + Random.Range(-speechRate * .66f, speechRate * 1.33f);
            }
        }
    }

    public void Initialize() {
        active = true;
    }

    public void Denitialize() {
        active = false;
        timeToSpeak = 0f;
    }

    public void ReceiveMessage(Message msg) {
        int difference;
        if (msg.messageType == Message.MessageType.Topic) {
            ChangeTopic();
        } else if (msg.messageType == Message.MessageType.SmallTalk) {
            AddAffection(affection * 1.2f);
        } else if (msg.messageType == Message.MessageType.Feeling) {
            difference = (int)Mathf.Abs(sentiments[(int)msg.topic] - msg.feeling);
            AddAffection(3 - difference);
        } else if (msg.messageType == Message.MessageType.FeelingTopic) {
            ChangeTopic();
            difference = (int)Mathf.Abs(sentiments[(int)msg.topic] - msg.feeling);
            AddAffection((3 - difference) * 2f);
        }
    }

    public void AddAffection(float val) {
        affection += val * flow;
        if (affection < 0) affection = 0;
        GameplayManager.Instance.AffectionChange(affection);
    }

    [Button]
    public void Test() {
        ReceiveMessage(new Message());
    }

    private void Speak() {
        Message.MessageType messageType;
        Character.Topic topic = this.topic;
        int feeling;

        int changeSubject = Random.Range(0, speechSinceSubjectChange);
        if (changeSubject > 2) {
            //change subject
            if (Random.Range(0, 1) > 0) {
                messageType = Message.MessageType.Topic;
            } else {
                messageType = Message.MessageType.FeelingTopic;
            }
            topic = (Character.Topic)Random.Range(0, 10);
        } else {
            //don't change subject
            if (Random.Range(0, 1) > 0) {
                messageType = Message.MessageType.Feeling;
            } else {
                messageType = Message.MessageType.SmallTalk;
            }
            speechSinceSubjectChange++;
        }

        feeling = sentiments[topic];
    }

    public void ReceiveFlow(float flow) {
        this.flow = flow;
    }

    private void ChangeTopic() {
        speechSinceSubjectChange = 0;
    }
}
