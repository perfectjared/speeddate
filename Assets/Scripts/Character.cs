using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityTracery;

public class Character : MonoBehaviour
{
    public enum Topic { 
        None, Hiking, Fracking, Cats, Dogs, Baking, Reading, TV, Coffee, Tea, Gardening
    };
    
    public List<Texture> images;

    [Range(0.0f, 100.0f)]
    public float affection = 0;
    [Range(0.0f, 1.0f)]
    private float flow = 0.1f;
    public bool active = false;
    public List<int> sentiments = new List<int>();
    private float timeToSpeak = 0.1f;
    [Range(0.1f, 1.0f)]
    public float speechRate = 0.5f;
    private int speechSinceSubjectChange = 0;
    private Character.Topic topic;
    public TextAsset GrammarFile;
    public TraceryGrammar Grammar;

    private void Start() {
        Grammar = new TraceryGrammar(GrammarFile.text);
    }
    
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
        timeToSpeak = .1f;
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
        if (val > 0) GameplayManager.Instance.ChangeFlow();
        else GameplayManager.Instance.ChangeFlow(true);
        
        affection += val;
        if (affection < 0) affection = 0;
        GameplayManager.Instance.AffectionChange(affection);
    }

    [Button]
    private void Speak() {
        Message.MessageType messageType;
        int feeling;
        Character.Topic topic;

        int changeSubject = Random.Range(0, speechSinceSubjectChange);
        if (changeSubject > 2 || this.topic == Topic.None) {
            //change subject
            if (Random.Range(0, 2) > 0) {
                messageType = Message.MessageType.Topic;
            } else {
                messageType = Message.MessageType.FeelingTopic;
            }
            ChangeTopic();
        } else {
            //don't change subject
            if (Random.Range(0, 4) > 0) {
                messageType = Message.MessageType.Feeling;
            } else {
                messageType = Message.MessageType.SmallTalk;
            }
            topic = this.topic;
            speechSinceSubjectChange++;
        }

        feeling = sentiments[(int)this.topic];
        Message message = new Message(messageType, this.topic, feeling);
        GameplayManager.Instance.CharacterSpeak(GenerateSentence(message));
    }

    private Message GenerateSentence(Message msg) {
        string sentence = "";
        switch(msg.messageType) {
            case Message.MessageType.Topic:
            sentence = Grammar.Parse("#topic#");
            break;
            case Message.MessageType.Feeling:
            sentence = Grammar.Parse("#feeling#");
            break;
            case Message.MessageType.FeelingTopic:
            sentence = Grammar.Parse("#feelingTopic#");
            break;
            case Message.MessageType.SmallTalk:
            sentence = Grammar.Parse("#smallTalk#");
            break;
        }
        //& is feeling, $ is topic. topic is red, feeling is green <c=green></c>
        sentence = sentence.Replace("$", "<c=red>" + msg.topic.ToString().ToUpper() + "</c>");
        
        string feeling = "";
        switch((int)msg.feeling) {
            case -3:
            feeling = "hate";
            break;
            case -2:
            feeling = "dislike";
            break;
            case -1:
            feeling = "am disinterested in";
            break;
            case 0:
            feeling = "am neutral toward";
            break;
            case 1:
            feeling = "am interested in";
            break;
            case 2: 
            feeling = "like";
            break;
            case 3:
            feeling = "love";
            break;
        }
        sentence = sentence.Replace("&", "<c=#23911d>" + feeling.ToUpper() + "</c>");

        msg.sentence = sentence;
        return msg;
    }

    private void ChangeTopic() {
        Character.Topic oldTopic = this.topic;
        while (this.topic == oldTopic) this.topic = (Character.Topic)Random.Range(1, 10);
        speechSinceSubjectChange = 0;
        //Debug.Log("topic change");
    }
}
