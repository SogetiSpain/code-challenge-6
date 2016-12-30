using System;
using System.Collections.Generic;
using System.IO;

namespace CodeChallenge6
{
    internal class CardReader
    {
        private const char LINE_SEPARATOR = ' ';
        private const string HINT_LINE_PREFIX = "*";

        private PlayerHand lilsHand;
        private List<GameTurn> turns;
        private GameTurn initialTurn;
        private GameTreeNode rootNode;
        

        public CardReader()
        {
            initialTurn = new GameTurn();
            turns = new List<GameTurn>();
            lilsHand = new PlayerHand();
        }

        public void FindCorrectPath()
        {
            var hands = new Dictionary<string, PlayerHand>();
            hands.Add("Shady", new PlayerHand());
            hands.Add("Rocky", new PlayerHand());
            hands.Add("Danny", new PlayerHand());
            hands.Add("Lil", new PlayerHand());

            var discards = new List<Card>();

            var lilsHands = rootNode.ProcessTurn(hands, discards);

            DumpLilHands(lilsHands);
        }


        public void BuildTree()
        {
            rootNode = new GameTreeNode(initialTurn);
            rootNode.AddChildren(turns);           
        }

        public void ProcessInputFile(string filePath)
        {
            int headerLineCount = 0;
            var lines = File.ReadAllLines(filePath);
            var isHint = false;
            var currentTurn = new GameTurn();

            foreach (var line in lines)
            {
                headerLineCount++;

                var play = ProcessPlayerLine(line);
                if(headerLineCount <= 4)
                {
                    initialTurn.PlayerMoves.Add(play);
                    continue;        
                }

                if (DoesLineBeginWithAsterisk(line))
                {
                    isHint = true;
                    currentTurn.Hints.Add(play);
                }
                else
                {
                    if(isHint)
                    {
                        turns.Add(currentTurn); 
                        currentTurn = new GameTurn();
                    }
                    currentTurn.PlayerMoves.Add(play);
                    isHint = false;
                }
            }
        }

  
        private PlayerMove  ProcessPlayerLine(string line)
        {
            //Console.WriteLine("Player Line: {0}", line);
            var playerMove = ParsePlayerLine(line);
            // DumpPlayerMove(playerMove);
            return playerMove;
        }

        private void DumpLilHands(List<PlayerHand> lilsHands)
        {
            Console.WriteLine("Lil's hands: ");
            foreach(var lilHand in lilsHands)
            {
                foreach(var lilCard in lilHand.Cards)
                {
                    Console.Write(lilCard);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        private PlayerMove ParsePlayerLine(string line)
        {
            var move = new PlayerMove();
            var lineParts = line.Split(LINE_SEPARATOR);

            move.PlayerName = lineParts[0];
            move.CardPlays = new List<CardPlay>();
            for(int i = 1; i<lineParts.Length; i++)
            {
                var cardPlay = new CardPlay(lineParts[i]);
                move.CardPlays.Add(cardPlay);
            }
            return move;
        }


        private bool DoesLineBeginWithAsterisk(string line)
        {
            return line.StartsWith(HINT_LINE_PREFIX);
        }
    }
}