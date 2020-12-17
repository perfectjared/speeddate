using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicTime : MonoBehaviour
{

  public float FADERDATA = 0.002f;

  public AudioSource[] speakers = new AudioSource[3];

  public int pointyNow;
  public int pointyLater;

  public bool firstMusic;
  public bool switched;
  public float maxVol = 0.8f;

  //characterAt indexes 1 == pierre 2 == aubrey 3 == amber

    // Start is called before the first frame update
    void Start()
    {
      firstMusic = true;
      switched = true;
      pointyNow = 0;
      speakers = GetComponentsInChildren<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
      if(!firstMusic){
        if(GameplayManager.Instance.topic == Character.Topic.None && !switched){
          Debug.Log(GameplayManager.Instance.characterAt);
          switch(GameplayManager.Instance.characterAt)
          {

            case 0:
              break;
            case 1:
              pointyLater = 0;
              StartCoroutine(crossfade());
              if(speakers[pointyNow].volume > 0.0f){
                speakers[pointyNow].volume = 0.0f;
              }
              pointyNow = pointyLater;
              switched = true;
              break;

            case 2:
              pointyLater = 1;
              StartCoroutine(crossfade());
              if(speakers[pointyNow].volume > 0.0f){
                speakers[pointyNow].volume = 0.0f;
              }
              pointyNow = pointyLater;
              switched = true;
              break;

            case 3:
              pointyLater = 2;
              StartCoroutine(crossfade());
              if(speakers[pointyNow].volume > 0.0f){
                speakers[pointyNow].volume = 0.0f;
              }
              pointyNow = pointyLater;
              switched = true;
              break;

          }
        }else if(GameplayManager.Instance.topic != Character.Topic.None && switched){
            switched = false;
          }
        }

      else{
        speakers[pointyNow].Play();
        firstMusic = false;
      }

    }

    IEnumerator crossfade(){
      speakers[pointyLater].volume = 0.0f;
      Debug.Log("Crossfade Called");
      firstMusic = true;
      while(speakers[pointyNow].volume > 0.0f){
        speakers[pointyNow].volume -= Time.deltaTime;
        if (speakers[pointyLater].volume < maxVol) {
          speakers[pointyLater].volume +=Time.deltaTime * 10;
        }
        if (speakers[pointyLater].volume > maxVol) speakers[pointyLater].volume = maxVol;
        yield return new WaitForEndOfFrame();
      }
      yield return new WaitForEndOfFrame();



    }
}
