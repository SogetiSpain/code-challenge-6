using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge6
{
    public class GameTreeNode
    {
        private static int _lastId = 0;

        public GameTurn Turn { get; set; }
        public List<GameTreeNode> Children { get; set; }

        public int Id { get; set; }

        public GameTreeNode(GameTurn turn)
        {
            GameTreeNode._lastId++;
            this.Id = _lastId;
            this.Turn = turn;
            this.Children = new List<GameTreeNode>();
        }

        public void AddChild(GameTreeNode child)
        {
            this.Children.Add(child);
        }

        public void AddChildren(List<GameTurn> remainingTurns)
        {
            var copyRemainingTurns = CopyOfRemainingTurns(remainingTurns);

            if(copyRemainingTurns.Count == 0)
            {
                return;
            }

            var childTurn = copyRemainingTurns[0];
            copyRemainingTurns.Remove(childTurn);

            for (int i = 0; i<childTurn.Hints.Count;i++)
            {
                var turn = new GameTurn();
                turn.PlayerMoves = new List<PlayerMove>();
                foreach(var origMove in childTurn.PlayerMoves)
                {
                    var newMove = origMove.Copy();
                    turn.PlayerMoves.Add(newMove);
                }
                turn.Hints = new List<PlayerMove>() { childTurn.Hints[i] };

                var childNode = new GameTreeNode(turn);
                this.AddChild(childNode);                                
            }
            foreach(var childNode in this.Children)
            {
                childNode.AddChildren(copyRemainingTurns);
            }
        }

        public List<PlayerHand> ProcessTurn(Dictionary<string, PlayerHand> hands, List<Card> discards)
        {
            var copyOfHands = CopyHands(hands);
            var copyOfDiscards = CopyOfDiscards(discards);

            Console.WriteLine("Processing turn {0}", this.Id);
            try
            {
                if (this.Turn.Hints.Count == 1)
                {
                    Console.WriteLine("Merging Lil's unknown cards");
                    var lilPlay = this.Turn.PlayerMoves.Where(m => m.PlayerName == "Lil").First();
                    lilPlay.MergeUnknownCards(this.Turn.Hints[0]);
                }
                PlayAllPlayerHands(copyOfHands, copyOfDiscards);
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR {0}. Returning empty list", ex.Message);
                return new List<PlayerHand>();
            }

            // Lil's hand
            var lilsHandCopy = copyOfHands["Lil"].Copy();

            if (this.Children == null || this.Children.Count == 0)
            {
                Console.WriteLine("FINAL PATH FOUND!!!");
                return new List<PlayerHand>() { lilsHandCopy };
            }
            else
            {
                List<PlayerHand> childResult = new List<PlayerHand>();
                // Process child nodes
                foreach (var childNode in this.Children)
                {
                    Console.WriteLine("Processing  {1} child with ID {0}", childNode.Id, this.Id);
                    try
                    {
                        childResult = childNode.ProcessTurn(copyOfHands, copyOfDiscards);
                        if (childResult.Count != 0)
                        {
                            Console.WriteLine("One of the children has the result!");
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("One of the children has raised error. Continuing...");
                        continue;
                    }
                }
                Console.WriteLine("Returning result from the children");
                if(childResult.Count != 0)
                {
                    Console.WriteLine("Non-empty result");
                    childResult.InsertRange(0, new List<PlayerHand>() { lilsHandCopy });
                    return childResult;
                }
                else
                {
                    Console.WriteLine("Empty result");
                    return childResult;
                }

            }
        }


        private void PlayAllPlayerHands(Dictionary<string, PlayerHand> hands, List<Card> discards)
        {

            foreach (var playerHand in this.Turn.PlayerMoves)
            {
                Console.WriteLine("Playing {0} moves", playerHand.PlayerName);

                var currentHand = hands[playerHand.PlayerName];

                Console.WriteLine("Current {0} hand: ", playerHand.PlayerName);
                foreach(var cardInHand in currentHand.Cards) { Console.WriteLine(" - " + cardInHand);  };

                foreach (var playerCard in playerHand.CardPlays)
                {
                    // Juega la carta

                    if(!playerCard.CardPlayed.IsUnknown && discards.Contains(playerCard.CardPlayed))
                    {
                        throw new ArgumentException("The card that is played is already discarded.");
                    }

                    Console.WriteLine(" - {0} {1} to {2}", playerCard.PlayType, playerCard.CardPlayed, playerCard.AnotherPlayer);

                    if (playerCard.PlayType == CardPlayType.GiveCard)
                    {
                        currentHand.RemoveCard(playerCard.CardPlayed);
                        if (!String.IsNullOrEmpty(playerCard.AnotherPlayer))
                        {
                            if (playerCard.AnotherPlayer == "discard")
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
        }

        private Dictionary<string, PlayerHand> CopyHands(Dictionary<string, PlayerHand> hands)
        {
            var result = new Dictionary<string, PlayerHand>();

            foreach(var hand in hands)
            {
                result.Add(hand.Key, hand.Value.Copy());
            }
            return result;
        }

        private List<Card> CopyOfDiscards(List<Card> discards)
        {
            var result = new List<Card>();
            foreach(var card in discards)
            {
                result.Add(new Card(card.Suit, card.Value));
            }
            return result;
        }

        private List<GameTurn> CopyOfRemainingTurns(List<GameTurn> turns)
        {
            var result = new List<GameTurn>();
            foreach(var turn in turns)
            {
                result.Add(turn);
            }
            return result;
        }
    }
}
