namespace CodeChallenge6
{
    public class CardPlay
    {
        public CardPlayType PlayType { get; private set; }
        public Card CardPlayed { get; private set; }
        public string AnotherPlayer { get; private set; }

        public CardPlay(string cardPlayString)
        {
            // +/- card : player/discard            
            var cardSeparatorIndex = cardPlayString.Length;
            this.AnotherPlayer = string.Empty;

            if (cardPlayString.Contains(":"))
            {
                cardSeparatorIndex = cardPlayString.IndexOf(":");
                this.AnotherPlayer = cardPlayString.Substring(cardSeparatorIndex+1);
            }
            var cardStringLength = cardSeparatorIndex - 1;

            var playTypeString = cardPlayString.Substring(0, 1);
            var cardString = cardPlayString.Substring(1, cardStringLength);
            this.CardPlayed = new Card(cardString);

            switch(playTypeString)
            {
                case "+":
                    this.PlayType = CardPlayType.ReceiveCard;
                    break;
                case "-":
                    this.PlayType = CardPlayType.GiveCard;
                    break;
            }
        }
    }
}