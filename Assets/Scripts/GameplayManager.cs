using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

enum State { Start, Play, Transition, Pause, Cutscene, End};
public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField]
    private Character character;
    [SerializeField]
    private int characterAt = 0;
    public List<GameObject> characters = new List<GameObject>();
    public ChatBox chatBox;

    [SerializeField]
    private int round = 0;

    [SerializeField]
    [Range(0.1f, 0.5f)]
    public float flow = 0.1f;
    [SerializeField]
    private GameObject shotObject;

    public Character.Topic topic = Character.Topic.None;
    
    public _UnityEventFloat affectionChange;
    public _UnityEventFloat flowChange;


    void Start() {

    }

    public void StartPlay() {
        NextCharacter();
    }

    void Update() {

    }

    [Button]
    private void NextCharacter() {
        if (character) character.Denitialize();
        characterAt++;
        
        if (characterAt > characters.Count - 1) {
            round++;
            characterAt = 1;
        }

        character = characters[characterAt - 1].GetComponent<Character>();
        character.Initialize();
    }

    public void ShotObject(GameObject shotObject) {
        this.shotObject = shotObject;
        //if own message, send to character, put in discard, remove from hand, draw new message
        //if character message, add to discard, make message not takeable anymore
    }

    public void AffectionChange(float value) {
        affectionChange.Invoke(value); //send to UI
    }

    public void FlowChange(float value) {
        flowChange.Invoke(value); //send to UI
    }

    public void CharacterSpeak(Message message) {
        this.topic = message.topic;
        chatBox.ReceiveMessage(message);
    }

    public void ReceiveMessage(Message message) {
        this.topic = message.topic;
        character.ReceiveMessage(message);
        chatBox.ReceiveMessage(message, false);
    }
}
