using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge6
{
    public class GameTreeNode
    {
        public GameTurn Turn { get; set; }
        public List<GameTreeNode> Children { get; set; }

        public GameTreeNode(GameTurn turn)
        {
            this.Turn = turn;
            this.Children = new List<GameTreeNode>();
        }

        public void AddChild(GameTreeNode child)
        {
            this.Children.Add(child);
        }

        public void AddChildren(List<GameTurn> remainingTurns)
        {
            if(remainingTurns.Count == 0)
            {
                return;
            }

            var childTurn = remainingTurns[0];
            remainingTurns.Remove(childTurn);

            for (int i = 0; i<childTurn.Hints.Count;i++)
            {
                var turn = new GameTurn();
                turn.PlayerMoves = childTurn.PlayerMoves;
                turn.Hints = new List<PlayerMove>() { childTurn.Hints[i] };
                var childNode = new GameTreeNode(turn);
                this.AddChild(childNode);                
                childNode.AddChildren(remainingTurns);
            }

                       
        }

        public List<PlayerHand> ProcessTurn(Dictionary<string, PlayerHand> hands, List<Card> discards, List<PlayerHand> lilsHands)
        {
            if(this.Turn.Hints.Count == 1)
            {
                var lilPlay = this.Turn.PlayerMoves.Where(m => m.PlayerName == "Lil").First();
                lilPlay.MergeUnknownCards(this.Turn.Hints[0]);
            }

            foreach (var playerHand in this.Turn.PlayerMoves)
            {
                var currentHand = hands[playerHand.PlayerName];

                foreach (var playerCard in playerHand.CardPlays)
                {
                    // Juega la carta
                    if(playerCard.PlayType == CardPlayType.GiveCard)
                    {
                        currentHand.RemoveCard(playerCard.CardPlayed);
                        if (!String.IsNullOrEmpty(playerCard.AnotherPlayer))
                        {
                            if(playerCard.AnotherPlayer == "discard")
                            {
                                discards.Add(playerCard.CardPlayed);
                            }
                        }

                    }
                    else
                    {
                        currentHand.AddCard(playerCard.CardPlayed);
                    }

                }
            }

            // Lil's 
            var lilsHandCopy = hands["Lil"].Copy();
            lilsHands.Add(lilsHandCopy);

            if (this.Children == null || this.Children.Count == 0)
            {
                return lilsHands;
            }
            else
            {
                // Process child nodes
                foreach (var childNode in this.Children)
                {
                    try
                    {
                        var finalResult = childNode.ProcessTurn(hands, discards, lilsHands);
                        return finalResult;
                    }
                    catch
                    {
                    }                    
                }
                return null;
            }
        }

        
    }
}
