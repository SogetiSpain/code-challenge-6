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
            if(hintMove.CardPlays.Count != this.CardPlays.Where(x=>x.CardPlayed.IsUnknown).Count())
            {
                throw new ArgumentException("Impossible move according to hint");
            }
            foreach (var hintPlay in hintMove.CardPlays)
            {
                var firstUnknownCard = this.CardPlays.Where(x => x.CardPlayed.IsUnknown).First();
                if(hintPlay.AnotherPlayer != firstUnknownCard.AnotherPlayer)
                {
                    throw new ArgumentException("Impossible move according to hint");
                }
                if(hintPlay.PlayType != firstUnknownCard.PlayType)
                {
                    throw new ArgumentException("Impossible move according to hint");
                }
                firstUnknownCard.CardPlayed.RevealCard(hintPlay.CardPlayed);
            }            
        }

    }
}