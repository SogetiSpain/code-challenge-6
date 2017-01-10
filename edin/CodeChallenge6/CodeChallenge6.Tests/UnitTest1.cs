using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge6.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CardPlay_ReceiveQueenHearts_Correct()
        {
            var inputString = "+QH";

            var play = new CardPlay(inputString);

            Assert.AreEqual(play.PlayType, CardPlayType.ReceiveCard);
            Assert.AreEqual(play.CardPlayed.ToString(), "Queen of Hearts");
        }

        [TestMethod]
        public void CardPlay_ReceiveQueenHeartsFromJack_Correct()
        {
            var inputString = "+QH:Jack";

            var play = new CardPlay(inputString);

            Assert.AreEqual(play.PlayType, CardPlayType.ReceiveCard);
            Assert.AreEqual(play.CardPlayed.ToString(), "Queen of Hearts");
            Assert.AreEqual(play.AnotherPlayer, "Jack");

        }
    }
}
