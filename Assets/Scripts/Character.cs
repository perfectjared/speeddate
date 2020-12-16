using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityTracery;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public enum Topic { 
        None, Hiking, Fracking, Cats, Dogs, Baking, Reading, TV, Coffee, Tea, Gardening
    };
    
    public List<Texture> images;
    private RawImage image;

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
    public AudioManager audioManager;

    private void Start() {
        Grammar = new TraceryGrammar(GrammarFile.text);
        image = GetComponent<RawImage>();
        audioManager = FindObjectOfType<AudioManager>();
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
        //GetComponent<RectTransform>().localPosition = new Vector3(-400, -30, 0);
        timeToSpeak = .1f;
    }

    public void ReceiveMessage(Message msg) {
        int difference;
        if (msg.messageType == Message.MessageType.Topic) {
            ChangeTopic();
        } else if (msg.messageType == Message.MessageType.SmallTalk) {
            AddAffection(2.5f);
            React(0);
        } else if (msg.messageType == Message.MessageType.Feeling) {
            difference = (int)Mathf.Abs(sentiments[(int)this.topic] - msg.feeling);
            AddAffection(3 - difference);
            React(difference);
        } else if (msg.messageType == Message.MessageType.FeelingTopic) {
            ChangeTopic();
            difference = (int)Mathf.Abs(sentiments[(int)this.topic] - msg.feeling);
            AddAffection((3 - difference) * 2f);
            React(difference);
        }
    }

    public void AddAffection(float val) {
        if (val > 0) GameplayManager.Instance.ChangeFlow();
        else GameplayManager.Instance.ChangeFlow(true);
        
        affection += val;
        if (affection < 0) affection = 0;
        GameplayManager.Instance.AffectionChange(affection);
        if (affection >= 100) SceneManager.LoadScene("youdidit", LoadSceneMode.Single);
    }

    private void React(int value) {
        int listAt = 0;
        Debug.Log(value);
        switch (value) {
            case 0:
            case 1:
            listAt = 1;
            audioManager.Play("Good");
            break;
            case 2:
            case 3:
            audioManager.Play("Eh");
            break;
            case 4:
            case 5:
            case 6:
            listAt = 2;
            audioManager.Play("Bad");
            break;
        }
        if (listAt != 0) {
            image.texture = images[listAt];
        }
        StartCoroutine(ReturnFace());
    }

    private IEnumerator ReturnFace() {
        var waitTime = 2f;
        var img = image.texture;
        bool interrupted = false;
        while (waitTime > 0) {
            waitTime -= Time.deltaTime;
            if (image.texture != img) {
                break;
                interrupted = true;
            }
            yield return new WaitForEndOfFrame();
        }
        if (!interrupted) {
            Debug.Log("test");
            image.texture = images[0];
        }
        yield return new WaitForEndOfFrame();
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
