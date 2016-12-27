using System;

namespace CodeChallenge6
{
    public class Card
    {
        public enum CardSuit
        {
            Hearts,
            Clubs,
            Spades,
            Diamonds
        }

        public enum CardValue
        {
            One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
        }

        public CardSuit Suit { get; private set; }
        public CardValue Value { get; private set; }
        public bool IsUnknown { get; private set; }

        public Card(string cardString)
        {
            if(cardString == "??")
            {
                this.IsUnknown = true;
            }
            else
            {
                ParseCardString(cardString);
            }
        }

        private void ParseCardString(string cardString)
        {
            var suitString = cardString.Substring(cardString.Length - 1, 1);
            if(cardString.Length == 3)
            {
                this.Value = CardValue.Ten;
            }
            else
            {
                switch(cardString.Substring(0,1))
                {
                    case "1":
                        this.Value = CardValue.One;
                        break;
                    case "2":
                        this.Value = CardValue.Two;
                        break;
                    case "3":
                        this.Value = CardValue.Three;
                        break;
                    case "4":
                        this.Value = CardValue.Four;
                        break;
                    case "5":
                        this.Value = CardValue.Five;
                        break;
                    case "6":
                        this.Value = CardValue.Six;
                        break;
                    case "7":
                        this.Value = CardValue.Seven;
                        break;
                    case "8":
                        this.Value = CardValue.Eight;
                        break;
                    case "9":
                        this.Value = CardValue.Nine;
                        break;
                    case "J":
                        this.Value = CardValue.Jack;
                        break;
                    case "Q":
                        this.Value = CardValue.Queen;
                        break;
                    case "K":
                        this.Value = CardValue.King;
                        break;
                    case "A":
                        this.Value = CardValue.Ace;
                        break;
                }
            }
            switch(suitString)
            {
                case "D":
                    this.Suit = CardSuit.Diamonds;
                    break;
                case "C":
                    this.Suit = CardSuit.Clubs;
                    break;
                case "S":
                    this.Suit = CardSuit.Spades;
                    break;
                case "H":
                    this.Suit = CardSuit.Hearts;
                    break;
            }
        }

        public override string ToString()
        {
            if(this.IsUnknown)
            {
                return "Unknown";
            }
            else
            {
                return String.Format("{0} of {1}", this.Value, this.Suit);
            }
        }
    }
}