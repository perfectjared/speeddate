using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Timer : MonoBehaviour
{
    public int defaultTime = 99;
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
        time = defaultTime;
        while (time > 0f) {
            time -= Time.deltaTime * speed;
            timeInt = Mathf.RoundToInt(Mathf.Ceil(time));
            if (timeInt != timeIntLast) onNumChange.Invoke(timeInt);
            timeIntLast = timeInt;
            yield return new WaitForEndOfFrame();
        }
        
        running = false;
    }
    
    public int TimeAt() {
        return timeInt;
    }

    public void SetSpeed(float speed) {
        this.speed = speed;
    }
}
