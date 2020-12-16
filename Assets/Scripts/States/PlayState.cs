using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : MonoBehaviour, StateInterface
{   
    GameplayManager gpm;
    [SerializeField]
    private int round = 1;

    public string Name() {
        return "Play";
    }
    public void Start()
    {
        gpm = GetComponent<GameplayManager>();   
    }

    void Update()
    {
        
    }

    public void StartState() {
        Debug.Log("Play state begun");
        GameplayManager.Instance.StartPlay();
    }
    public void EndState()
    {
        GameplayManager.Instance.EndPlay();
    }
}
