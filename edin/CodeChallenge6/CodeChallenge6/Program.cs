using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge6
{
    class Program
    {
        static void Main(string[] args)
        {
            var cardReader = new CardReader();
            cardReader.ProcessInputFile("..\\..\\sampleinput2.txt");

            cardReader.BuildTree();

            cardReader.FindCorrectPath();

            Console.ReadLine();
        }
    }
}
