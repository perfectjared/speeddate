using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public enum State { Start, Menu, Intro, Play, Transition, Pause, Cutscene, End };
    #region 
    public List<Character> characters = new List<Character>();
    #endregion

    [SerializeField]
    State state = State.Start;
    [SerializeField]
    GameObject shotObject = null;

    public void ShotObject(GameObject shotObject) {
        this.shotObject = shotObject;
    }

    public void ChangeState(State state) {
        StartState(EndState(), state); //it's composed this way so that StartState always happens after EndState
    }

    private bool EndState() {
        //do whatever cleanup here
        return true;
    }

    private void StartState(bool endState, State state) {
        if (endState) {
            this.state = state;
        }
    }

    


}
