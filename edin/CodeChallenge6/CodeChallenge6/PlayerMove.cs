using System.Collections.Generic;

namespace CodeChallenge6
{
    public class PlayerMove
    {
        public PlayerMove()
        {
        }

        public string PlayerName { get; set; }

        public List<CardPlay> CardPlays { get; set; }
    }
}