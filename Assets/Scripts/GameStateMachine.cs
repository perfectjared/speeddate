using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[System.Serializable] public class _UnityEventInt:UnityEvent<int> {}
[System.Serializable] public class _UnityEventFloat:UnityEvent<float> {}

[System.Serializable] public class _UnityEventGameObject:UnityEvent<GameObject> {}
public class GameStateMachine : MonoBehaviour
{
    public List<StateInterface> states = new List<StateInterface>();

    [SerializeField]
    string stateString = "";
    int stateAt = 0;


    [SerializeField]
    GameObject shotObject = null;

    public _UnityEventFloat affectionChange;
    public _UnityEventFloat flowChange;

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

    public void AffectionChange(float value) {
        affectionChange.Invoke(value);
    }

    public void FlowChange(float value) {
        flowChange.Invoke(value);
    }
}
