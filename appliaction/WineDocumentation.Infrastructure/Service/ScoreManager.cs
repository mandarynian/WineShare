using System;
using System.Collections.Generic;
using WineDocumentation.Core.Domain;

namespace WineDocumentation.Infrastructure.Service
{
    public class ScoreManager : IScoreManager
    {
        public float GetAvgScores(Wine wine)
        {
            try 
            {
                if (wine == null)
                {
                    throw new Exception("This Wine has no refrence.");
                }

                if (wine.Scores.Count == 0)
                {
                    throw new Exception("This Wine has no scores.");
                }

                uint scoresValue = 0;
                foreach (var score in wine.Scores)
                {
                    scoresValue += score.ScoreValue;
                }

                return scoresValue / (float)wine.Scores.Count;

            }
            catch (Exception ex)
            {
                // Logger.GetExceptionToLogger(ex);
                return 0f;
            }
            
        }

        public List<Score> GetScores(Wine wine)
        {
            try 
            {
                if (wine == null)
                {
                    throw new Exception("This Wine has no refrence.");
                }

                return wine.Scores;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}