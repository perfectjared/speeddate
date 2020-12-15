using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State { Start, Play, Transition, Pause, Cutscene, End};
public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField]
    private Character character;
    [SerializeField]
    private int characterAt = 0;
    private List<Character> characters = new List<Character>();

    [SerializeField]
    private int round = 0;

    [SerializeField]
    public float flow = 0f;
    [SerializeField]
    private GameObject shotObject;

    public Character.Topic topic;
    
    public _UnityEventFloat affectionChange;
    public _UnityEventFloat flowChange;


    void Start() {

    }

    public void StartPlay() {
        NextCharacter();
    }

    void Update() {

    }

    private void NextCharacter() {
        characterAt++;
        
        if (characterAt > characters.Count - 1) {
            round++;
            characterAt = 1;
        }

        character = characters[characterAt - 1];
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
}
