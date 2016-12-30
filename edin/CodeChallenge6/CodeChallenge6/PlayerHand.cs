using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge6
{
    public class PlayerHand
    {
        public List<Card> Cards { get; private set; }

        public PlayerHand()
        {
            Cards = new List<Card>();
        }


        public void AddCard(Card card)
        {
            this.Cards.Add(card);
        }

        public void RemoveCard(Card card)
        {
            if(!card.IsUnknown && this.Cards.Contains(card))
            {
                this.Cards.Remove(card);
                return;
            }

            if(card.IsUnknown && this.Cards.Contains(card))
            {
                var firstUnknownCard = this.Cards.Where(x => x.IsUnknown).First();
                this.Cards.Remove(firstUnknownCard);
                return;
            }

            if(!card.IsUnknown && this.Cards.Exists(x=>x.IsUnknown))
            {
                var firstUnknownCard = this.Cards.Where(x => x.IsUnknown).First();
                this.Cards.Remove(firstUnknownCard);
                return;
            }

            throw new ArgumentException(String.Format("The card {0} isn't in the hand.", card));
            
        }

        public PlayerHand Copy()
        {
            var newHand = new PlayerHand();
            foreach(var card in this.Cards)
            {
                newHand.AddCard(new Card(card.Suit, card.Value));
            }
            return newHand;
        }
    }
}
