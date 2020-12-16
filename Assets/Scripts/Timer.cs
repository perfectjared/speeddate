using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Timer : MonoBehaviour
{
    public int length = 10;
    [SerializeField]
    private int timeInt = 0;
    private int timeIntLast = 0;
    private float time = 0f;
    [SerializeField]
    [Range(0, 3f)]
    private float speed = 1f;
    private bool running = false;
    public _UnityEventInt onNumChange;
    
    [Button]
    public void StartTimer() {
        if (!running) 
        {
            running = true;
            StartCoroutine(Countdown());
        }
    }

    private IEnumerator Countdown() {
        time = length;
        while (time > 0f) {
            time -= Time.deltaTime * speed;
            timeInt = Mathf.RoundToInt(Mathf.Ceil(time));
            if (timeInt != timeIntLast) onNumChange.Invoke(timeInt);
            timeIntLast = timeInt;
            yield return new WaitForEndOfFrame();
        }
        
        running = false;
    }

    public void Stop() {
        time = 0f;
    }

    public void Reset() {
        time = length;
    }

    public bool Running() {
        return running;
    }
    
    public int TimeAt() {
        return timeInt;
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }

    public void SetLength(int length) {
        this.length = length;
        onNumChange.Invoke(length);
    }
}
