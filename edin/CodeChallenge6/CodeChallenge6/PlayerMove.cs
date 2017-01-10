using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallenge6
{
    public class PlayerMove
    {
        public PlayerMove()
        {
        }

        public string PlayerName { get; set; }

        public List<CardPlay> CardPlays { get; set; }

        public void MergeUnknownCards(PlayerMove hintMove)
        {
            if (hintMove.CardPlays.Count != this.CardPlays.Where(x => x.CardPlayed.IsUnknown).Count())
            {
                throw new ArgumentException("Impossible move according to hint");
            }
            foreach (var hintPlay in hintMove.CardPlays)
            {
                var firstUnknownCard = this.CardPlays.Where(x => x.CardPlayed.IsUnknown).First();
                if (hintPlay.AnotherPlayer != firstUnknownCard.AnotherPlayer)
                {
                    throw new ArgumentException("Impossible move according to hint");
                }
                if (hintPlay.PlayType != firstUnknownCard.PlayType)
                {
                    throw new ArgumentException("Impossible move according to hint");
                }
                firstUnknownCard.CardPlayed.RevealCard(hintPlay.CardPlayed);
            }
        }

        public PlayerMove Copy()
        {
            var newMove = new PlayerMove();
            newMove.CardPlays = new List<CardPlay>();
            foreach(var play in this.CardPlays)
            {
                newMove.CardPlays.Add(play.Copy());
            }
            newMove.PlayerName = this.PlayerName;
            return newMove;
        }

        private void DumpPlayerMove()
        {
            Console.WriteLine("{0} turn:", this.PlayerName);

            foreach (var play in this.CardPlays)
            {
                Console.Write("- {0} {1}", play.PlayType, play.CardPlayed);
                if (!string.IsNullOrEmpty(play.AnotherPlayer))
                {
                    Console.Write(" to/from {0}", play.AnotherPlayer);
                }
                Console.WriteLine();
            }
        }
    }
}