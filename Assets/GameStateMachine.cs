using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    enum State { Start, Menu, Game, Transition, Pause, Cutscene, End };
    #region 
    public int handSize = 3;
    public int messagesOnScreen = 3;
    #endregion

    [SerializeField]
    State state = State.Start;
    [SerializeField]
    int round = 3;
    [SerializeField]
    int time = 99;
    [SerializeField]
    [Range(0, 6)]
    int ammo = 6;
    [SerializeField]
    GameObject shotObject = null;
    [SerializeField]
    [Range(0f, 100f)]
    float affection = 0f;
    [SerializeField]
    [Range(0f, 100f)]
    float flow = 0f;

    [SerializeField]
    Character character = null;
    List<Character> characters = new List<Character>();

    List<Message> deck = new List<Message>();
    List<Message> hand = new List<Message>();
    List<Message> discard = new List<Message>();

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Ammo(int ammo) {
        this.ammo = ammo;
    }

    public void Timer(int timer) {
        
    }

    public void ShotObject(GameObject shotObject) {
        this.shotObject = shotObject;
    }
}
