using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews.OODesign
{
    public enum Suit
    {
        Spades,
        Hearts,
        Diamonds,
        Clubs
    }

    public enum Denomination
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public abstract class Card
    {
        public Suit suit;
        public Denomination denomination;

        public abstract int Value { get; }
    }

    public class BlackJackCard : Card
    {
        public override int Value
        {
            get
            {
                switch (this.denomination)
                {
                    case Denomination.King:
                    case Denomination.Queen:
                    case Denomination.Jack:
                    case Denomination.Ten:
                        return 10;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }

    public enum GameType
    {
        Blackjack,
        Poker
    }

    public abstract class Hand
    {
        public List<Card> CurrentHand;

        public int MaxHandSize;

        public int MinHandSize;

        public abstract bool AddCardToHand(Card card);

        public abstract List<Card> PlayCardsFromHand(int count);

        public virtual int GetHandValue()
        {
            int totalValue = 0;

            foreach (var c in CurrentHand)
            {
                totalValue += c.Value;
            }

            return totalValue;
        }
    }

    public class BlackJackHand : Hand
    {
        public override bool AddCardToHand(Card card)
        {
            if (CurrentHand.Count < MaxHandSize)
            {
                CurrentHand.Add(card);
                return true;
            }

            return false;
        }

        public override List<Card> PlayCardsFromHand(int count)
        {
            return null;
        }

        public override int GetHandValue()
        {
            return 10;
        }

    }

    public class Deck
    {
        public List<Card> RemainingCards;

        public void Shuffle() { }

        public void ResetDeck() { }

        public List<Card> DrawCards(int count)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class CardGame
    {
        public CardGame()
        {
            Players = new List<Hand>();
        }

        //One hand for each player
        public List<Hand> Players;

        public Deck cardDeck;

        public abstract void StartGame();

        public abstract void PlayHand();
    }

    public class BlackJackGame : CardGame
    {
        public BlackJackGame() : base()
        {
            cardDeck = new Deck();
        }
        
        public override void StartGame()
        {
            cardDeck.RemainingCards.Clear();
            cardDeck.RemainingCards.Add(new BlackJackCard());
        }

        public override void PlayHand()
        {
            foreach(Hand player in Players)
            {
                player.AddCardToHand(cardDeck.DrawCards(1)[0]);
            }
        }
    }
}
