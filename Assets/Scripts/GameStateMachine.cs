using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[System.Serializable] public class _UnityEventInt:UnityEvent<int> {}
[System.Serializable] public class _UnityEventFloat:UnityEvent<float> {}
[System.Serializable] public class _UnityEventGameObject:UnityEvent<GameObject> {}
[System.Serializable] public class _UnityEventMessage:UnityEvent<Message> {}
[System.Serializable] public class _UnityEventString:UnityEvent<string>{}

public class GameStateMachine : Singleton<GameStateMachine>
{
    public List<StateInterface> states = new List<StateInterface>();

    [SerializeField]
    string stateString = "";
    int stateAt = 0;


    [SerializeField]
    GameObject shotObject = null;
    void Start() {
        states.Add(new StartingState());
        states.Add(new MenuState());
        states.Add(new PlayState());
        states.Add(new EndingState());

        StartState(true);
    }
    public void ShotObject(GameObject shotObject) {
        this.shotObject = shotObject;
    }

    [Button]    
    public void Next() {
        StartState(EndState());
    }

    public void PostMessage(Message msg) {
        //logic of adding a new message comoposed of msg to the UI
    }

    private void StartState(bool endState) {
        if (endState) {
            stateString = states[stateAt].Name();
            states[stateAt].StartState();
        }
    }
    
    private bool EndState() {
        if (stateAt <= states.Count - 1) {
            states[stateAt].EndState();
            stateAt++;
            if (stateAt >= states.Count) {
                stateString = "END";
                End();
                return false;
            }
            return true;
        }
        return false;
    }

    private void End() {

    }
}
