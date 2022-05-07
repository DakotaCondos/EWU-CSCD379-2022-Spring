using System.Text.Json;

namespace Wordle.Api.Services
{
    public class LeaderBoardServiceMemory : ILeaderBoardService
    {

        private static readonly List<Score> _scores = new List<Score>();

        public LeaderBoardServiceMemory()
        {
            if (!_scores.Any())
            {
                _scores.Add(new Score("Ralph", 30, 3.4));
                _scores.Add(new Score("Gene", 50, 4.1));
                _scores.Add(new Score("Hildagaurd", 25, 2.6));
            }
        }

        public void AddScore(GameScore gameScore)
        {

            var score = _scores.FirstOrDefault(f => f.Name == gameScore.Name);
            if (score is not null)
            {
                score.AverageGuesses = ((score.NumberGames * score.AverageGuesses)
                    + gameScore.Score) / ++score.NumberGames;
            }
            //added 28-31
            else
            {
                _scores.Add(new Score(gameScore.Name, 1, gameScore.Score));
            }
            //end of added section
        }

        public IEnumerable<Score> GetScores()
        {
            return _scores;
        }

        // Each winning game is worth up to 6 points
        // Points earned are equal to 7 - number of turns taken to complete
        // Total score is equal to points earned * number of games
        public List<Score> GetTop10Scores()
        {
            var scores = new List<Score>();
            scores.AddRange(_scores);
            scores.OrderByDescending(f => (7 - f.AverageGuesses) * f.NumberGames);
            var top10 = new List<Score>();
            for (int i = 0; i < scores.Count && i < 10; i++)
            {
                top10.Add(scores[i]);
            }
            return scores;
        }

        public string GetTop10AsJson()
        {
            string jsonString = JsonSerializer.Serialize(GetTop10Scores());
            return jsonString;
        }
    }
}
