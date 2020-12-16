using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
  //declaring the three lists that repressent the deck, hand, and Discard
  public List<Message> Deck = new List<Message>();
  public List<Message> Hand = new List<Message>();
  public List<Message> Discard = new List<Message>();

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

  // the following two options look smilar  but one should happen when you refresh and the other should
  // happen when you use a card
  public void drawThree(){
    for(int i = 0; i < 3; i++){
      if(Deck.Count < 1){
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

  public void AddToDiscard(Message message) {
    Discard.Add(message);
    Debug.Log(message.sentence + " added to discard");
  }

  public void AddToDeck(Message message) {
    Deck.Add(message);
    Debug.Log(message.sentence + " added to deck");
  }

  // Start is called before the first frame update
  void Start()
  {
    Message message1 = new Message();
    message1.sentence = "Howdy.";
    message1.messageType = Message.MessageType.SmallTalk;

    Message message2 = new Message();
    message2.sentence = "Yee... Haw?";
    message2.messageType = Message.MessageType.SmallTalk;

    Message message3 = new Message();
    message3.sentence = "You sure are awful purty.";
    message3.messageType = Message.MessageType.SmallTalk;

    Message message4 = new Message();
    message4.messageType = Message.MessageType.Topic;
    message4.topic = Character.Topic.Cats;
    message4.sentence = "How's about uh... You like Cats?";

    Message message5 = new Message();
    message5.messageType = Message.MessageType.Feeling;
    message5.feeling = 2;
    message5.sentence = "I, uh, I like it. Yep.";

    Message message6 = new Message();
    message6.messageType = Message.MessageType.FeelingTopic;
    message6.feeling = 3;
    message6.topic = Character.Topic.Coffee;
    message6.sentence = "I sure do love coffee,... What do you think?";

    AddToDiscard(message1);
    AddToDiscard(message2);
    AddToDiscard(message3);
    AddToDiscard(message4);
    AddToDiscard(message5);
    AddToDiscard(message6);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
