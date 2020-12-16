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
    [SerializeField]
    private int characterAt = 0;
    public List<GameObject> characters = new List<GameObject>();

    [SerializeField]
    private int round = 0;

    [Range(0.1f, 0.5f)]
    public float flow = 0.1f;
    [Range(0.01f, 0.1f)]
    public float flowAdd = 0.05f;

    public Character.Topic topic = Character.Topic.None;
    
    public _UnityEventFloat affectionChange;
    public _UnityEventFloat flowChange;
    public _UnityEventString topicChange;

    private Timer timer;
    private DeckManager deckManager;


    void Start() {
        timer = GetComponent<Timer>();
        deckManager = GetComponent<DeckManager>();
    }

    public void StartPlay() {
        NextCharacter();
    }

    public void EndPlay() {
        Debug.Log("The End");
    }

    void Update() {
        if (character) AffectionChange(character.affection);
        if (flow > 0.1f) flow -= Time.deltaTime * 0.025f;
        if (flow < 0.1f) flow = 0.1f;
        FlowChange(flow);
    }

    [Button]
    private void NextCharacter() {
        if (character) character.Denitialize();
        flow = 0.1f;
        characterAt++;
        
        if (characterAt > characters.Count) {
            round++;
            characterAt = 1;
        }

        if (round == 3) {
            GameStateMachine.Instance.Next();
        } else {
            character = characters[characterAt - 1].GetComponent<Character>();
            character.Initialize();
            timer.Reset();
            timer.StartTimer();
            ChangeTopic(Character.Topic.None);
            chatBox.DeleteChatBoxes();
            StartCoroutine(WaitForTimer());
        }
    }

    [Button]
    public void RespondRandomly() {
        character.ReceiveMessage(new Message());
        flow = (0.5f > flow + flowAdd) ? flow + flowAdd : 0.5f;
    }

    private IEnumerator WaitForTimer() {
        while (timer.Running()) {
            yield return new WaitForEndOfFrame();
        }
        NextCharacter();
        yield return new WaitForEndOfFrame();
    }

    public void ShootMessage(Message message) {
        Debug.Log(message.sentence);
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
        ChangeTopic(message.topic);
        character.ReceiveMessage(message);
        chatBox.ReceiveMessage(message, false);
    }

    private void ChangeTopic(Character.Topic topic) {
        this.topic = topic;
        topicChange.Invoke(topic.ToString());
    }
}
