using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Wordle.Api.Services;

namespace Wordle.Api.Tests
{
    [TestClass]
    public class LeaderBoardServiceMemoryTests
    {
        [Ignore]
        [TestMethod]
        public void GetScores()
        {
            LeaderBoardServiceMemory sut = new LeaderBoardServiceMemory();
            Assert.AreEqual(4, sut.GetScores().Count());
        }
        
        [TestMethod]
        public void AddScore_AddsNewPlayer()
        {
            LeaderBoardServiceMemory sut = new LeaderBoardServiceMemory();
            int beforeAddingScore = sut.GetScores().Count();
            sut.AddScore(new GameScore(1, "test"));
            Assert.AreEqual(beforeAddingScore +1, sut.GetScores().Count());
        }
        
        [TestMethod]
        public void AddScore_UpdatesExistingPlayer()
        {
            LeaderBoardServiceMemory sut = new LeaderBoardServiceMemory();
            sut.AddScore(new GameScore(5, "Ralph"));
            Assert.AreEqual(31, sut.GetScores().First(x => x.Name == "Ralph").NumberGames);
        }


    }
}