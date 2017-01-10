using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge6
{
    public class GameTurn
    {
        public List<PlayerMove> PlayerMoves { get; set; }
        public List<PlayerMove> Hints { get; set; }

        public GameTurn()
        {
            this.PlayerMoves = new List<PlayerMove>();
            this.Hints = new List<PlayerMove>();
        }
    }
}
