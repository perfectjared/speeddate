using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : MonoBehaviour, StateInterface
{
    public List<Character> characters = new List<Character>();
    public Character currentCharacter;
    private int characterAt = 1;

    [SerializeField]
    private int round = 1;

    public string Name() {
        return "Play";
    }
    public void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartState() {
        Debug.Log("Play state begun");
    }
    public void EndState()
    {

    }
}
