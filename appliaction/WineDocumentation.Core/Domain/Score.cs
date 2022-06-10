using System;
using System.Text.RegularExpressions;

namespace WineDocumentation.Core.Domain
{
    
    public class Score
    {
       
       public Guid Id {get;set;} 
       public Guid WineId {get;set;}
        public uint ScoreValue {get; protected set; }
        public string Comment {get; protected set; }
        public string Author {get; protected set; }

        protected Score()
        {
        }

        public Score(uint scoreValue, string comment, Guid wineId)
        {   
            Id = Guid.NewGuid();
            SetScoreValue(scoreValue);
            SetComment(comment);
            WineId = wineId;
        }

        public Score(uint scoreValue, string comment, string author, Guid wineId)
        {   
            Id = Guid.NewGuid();
            SetScoreValue(scoreValue);
            SetComment(comment);
            Author = author;
            WineId = wineId;
        }

        public void SetScoreValue(uint scoreValue)
        {
            if(scoreValue > 5)
            {
                throw new Exception("Score value is out of range.");
            }

            ScoreValue = scoreValue;
        }

        public void SetComment(string comment)
        {
            // if (!string.IsNullOrWhiteSpace(comment))
            // {
            //     throw new Exception("Comment has invalid character. Please make sure you didn't use any of special character.");
            // }

            Comment = comment;
        }

        // public static Score CreateSocre(uint scoreValue, string comment)
        // {
        //     return new Score(scoreValue, comment);
        // }
    }
}