using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace WineDocumentation.Core.Domain
{
    [Table("Wine", Schema = "public")]
    public class Wine
    {
        [Key]
        public Guid Id { get; protected set; }
        public Guid SpeciesId { get; set; }
        public string Winename { get; protected set; }
        public string Brand { get; set; } 
        public Species Species { get;  set; }
        public string Description { get; protected set; }
        public List<Score> Scores {get; protected set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public float Scoreavg 
        {
            get 
            {
                return GetScoreAvg();
            }
        }

        protected Wine()
        {
            Scores = new List<Score>();
        }

        public Wine(Guid wineId, string winename, Species species, string brand,string description = "") 
        {
            Id = wineId;
            Winename = winename;
            Description = description;
            Brand = brand;
            Species = species;
            Scores = new List<Score>();
            CreatedAt = DateTime.Now;
        }

        public void AddScore(Score score)
        {
            Scores.Add(score);
        }

        public void AddScores(List<Score> scores)
        {
            Scores.AddRange(scores);
        }

        public float GetScoreAvg()
        {
            if (Scores.Count > 0)
            {
                float avg = 0.0f;

                foreach (var s in Scores)
                {
                    avg += s.ScoreValue;
                }

                return avg / Scores.Count;
            }
            else 
            {
                return 0.0f;
            }
        }
    }
}