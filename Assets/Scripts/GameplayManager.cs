using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

enum State { Start, Play, Transition, Pause, Cutscene, End};
public class GameplayManager : Singleton<GameplayManager>
{
    public ChatBox chatBox;

    [SerializeField]
    private Character character;
    public int characterAt = 0;
    public List<GameObject> characters = new List<GameObject>();

    [SerializeField]
    private int round = 0;

    [Range(0.1f, 0.5f)]
    public float flow = 0.1f;
    [Range(0.01f, 0.1f)]
    public float flowAdd = 0.05f;
    public AudioManager audioManager;

    public Character.Topic topic = Character.Topic.None;

    public _UnityEventFloat affectionChange;
    public _UnityEventFloat flowChange;
    public _UnityEventString topicChange;

    private Timer timer;
    private DeckManager deckManager;


    void Start() {
        timer = GetComponent<Timer>();
        deckManager = GetComponent<DeckManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void StartPlay() {
        NextCharacter();
        Responses.Instance.InitialiseResponses();
    }

    public void EndPlay() {
        Debug.Log("The End");
    }

    void Update() {
        //if (character) AffectionChange(character.affection);
        if (flow > 0.1f) flow -= Time.deltaTime * 0.0125f;
        if (flow < 0.1f) flow = 0.1f;
        FlowChange(flow);
    }

    [Button]
    private void NextCharacter() {
        if (character) 
        {
            character.gameObject.GetComponent<RectTransform>().LeanMoveLocalX(-400, 1f).setEaseInOutSine();
            character.Denitialize();            
        }   
        flow = 0.1f;
        characterAt++;

        if (characterAt > characters.Count) {
            round++;
            characterAt = 1;
        }

            character = characters[characterAt - 1].GetComponent<Character>();
            character.Initialize();
            //Move Char on screen
            audioManager.Play("Whistle");
            switch (characterAt - 1)
            {
                case 0:
                    character.gameObject.GetComponent<RectTransform>().LeanMoveLocalX(-100, 2).setEaseInOutSine();
                    break;
                case 1:
                    character.gameObject.GetComponent<RectTransform>().LeanMoveLocalX(-44, 2).setEaseInOutSine();
                    break;
                case 2:
                    character.gameObject.GetComponent<RectTransform>().LeanMoveLocalX(-100, 2).setEaseInOutSine();
                    break;
            }
            timer.Reset();
            timer.StartTimer();
            ChangeTopic(Character.Topic.None);
            chatBox.DeleteChatBoxes();
            StartCoroutine(WaitForTimer());
    }

    [Button]
    public void RespondRandomly()
    {
        ReceiveMessage(new Message());
        flow = (0.5f > flow + flowAdd) ? flow + flowAdd : 0.5f;
    }

    public void ResetPlayerPos()
    {
       
    }

    private IEnumerator WaitForTimer() {
        while (timer.Running()) {
            yield return new WaitForEndOfFrame();
        }
        NextCharacter();
        yield return new WaitForEndOfFrame();
    }

    public void ShootMessage(Message message) {
        deckManager.AddToDeck(message);
    }

    public void AffectionChange(float value) {
        affectionChange.Invoke(value); //send to UI
    }

    public void FlowChange(float value) {
        flowChange.Invoke(value); //send to UI
    }

    public void CharacterSpeak(Message message) {
        ChangeTopic(message.topic);
        chatBox.ReceiveMessage(message);
    }

    public void ReceiveMessage(Message message) {
        if (message.messageType != Message.MessageType.SmallTalk &&
        message.messageType != Message.MessageType.Feeling) ChangeTopic(message.topic);
        character.ReceiveMessage(message);
        chatBox.ReceiveMessage(message, false);
    }

    private void ChangeTopic(Character.Topic topic) {
        this.topic = topic;
        topicChange.Invoke(topic.ToString());
    }

    public void ChangeFlow(bool drop = false) {
        if (drop) flow = 0.1f;
        else flow = (0.5f > flow + flowAdd) ? flow + flowAdd : 0.25f;
    }
}
