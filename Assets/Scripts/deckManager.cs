using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deckManager : MonoBehaviour
{
  //declaring the three lists that repressent the deck, hand, and Discard
  public List<Message> Deck = new List<Message>();
  public List<Message> Hand = new List<Message>();
  public List<Message> Discard = new List<Message>();
  public const int HANDSIZE = 3;

  // these are helper variables that we are gonna declare
  public int discardHold;
  public int shuffleMax;


  //this is a public function to shuffle the deck. It is called when the deck is empty
  public void shuffle(){
    discardHold = Discard.Count;
    for(int i = 0; i < discardHold; i++){
      shuffleMax = Random.Range(0, Discard.Count);
      Deck.Add(Discard[shuffleMax]);
    }
  }

  public void initHand(){
    for(int i = 0; i < HANDSIZE; i++){
      Hand.Add(Deck[0]);
      Deck.RemoveAt(0);
    }
  }

  // the following two options look smilar  but one should happen when you refresh and the other should
  // happen when you use a card
  public void drawThree(){
    for(int i = 0; i < HANDSIZE; i++){
      Discard.Add(Hand[0]);
      Hand.RemoveAt(0);
      if(Deck.Count < 1){
        Debug.Log("shuffling");
        shuffle();
      }
      Hand.Add(Deck[0]);
      Deck.RemoveAt(0);
    }
  }

  public void drawOne(){
    if(Deck.Count < 1){
      shuffle();
    }
    Hand.Add(Deck[0]);
    Deck.RemoveAt(0);
  }



    // Start is called before the first frame update
    void Start()
    {
      for(int i = 0; i < 6; i++){
        Deck.Add(new Message());
        Debug.Log(Deck[i].sentence);
      }
      initHand();
      for(int i = 0; i < 3; i++){
        Debug.Log("Hand");
        Debug.Log(Hand[i].sentence);
        Debug.Log("Deck");
        Debug.Log(Deck[i].sentence);
      }
      drawThree();
      for(int i = 0; i < 3; i++){
        Debug.Log("Hand");
        Debug.Log(Hand[i].sentence);
        Debug.Log("Discard");
        Debug.Log(Discard[i].sentence);
        Debug.Log(Deck.Count);
        Debug.Log(Discard.Count);

      }
      drawThree();
      for(int i = 0; i < Deck.Count ; i++){
        Debug.Log(Deck[i].sentence);
      }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
