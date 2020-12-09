using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[System.Serializable] public class _UnityEventInt:UnityEvent<int> {}
[System.Serializable] public class _UnityEventGameObject:UnityEvent<GameObject> {}
public class GameStateMachine : MonoBehaviour
{
    public List<StateInterface> states = new List<StateInterface>();
    public List<Character> characters = new List<Character>();

    [SerializeField]
    string stateString = "";
    int stateAt = 0;


    [SerializeField]
    GameObject shotObject = null;

    void Start() {
        states.Add(new StartState());
        states.Add(new MenuState());
        states.Add(new PlayState());
        states.Add(new EndState());

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
            states[stateAt].Begin();
        }
    }
    
    private bool EndState() {
        if (stateAt <= states.Count - 1) {
            states[stateAt].End();
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
