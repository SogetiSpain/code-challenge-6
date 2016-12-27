using System;
using System.Collections.Generic;
using System.IO;

namespace CodeChallenge6
{
    internal class CardReader
    {
        private const char LINE_SEPARATOR = ' ';
        private const string HINT_LINE_PREFIX = "*";
        public CardReader()
        {
        }

        public void ProcessInputFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            foreach(var line in lines)
            {
                ProcessLine(line);
            }
        }

        private void ProcessLine(string line)
        {
            if(DoesLineBeginWithAsterisk(line))
            {
                ProcessHintLine(line);
            }
            else
            {
                ProcessPlayerLine(line);
            }
        }

        private void ProcessPlayerLine(string line)
        {
            Console.WriteLine("Player Line: {0}", line);

            var playerMove = ParsePlayerLine(line);
            DumpPlayerMove(playerMove);
        }

        private void DumpPlayerMove(PlayerMove playerMove)
        {
            Console.WriteLine("{0} turn:", playerMove.PlayerName);

            foreach(var play in playerMove.CardPlays)
            {
                Console.Write("- {0} {1}", play.PlayType, play.CardPlayed);
                if(!string.IsNullOrEmpty(play.AnotherPlayer))
                {
                    Console.Write(" to/from {0}", play.AnotherPlayer);
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

        private void ProcessHintLine(string line)
        {
            Console.WriteLine("Hint Line: {0}", line);
        }

        private bool DoesLineBeginWithAsterisk(string line)
        {
            return line.StartsWith(HINT_LINE_PREFIX);
        }
    }
}