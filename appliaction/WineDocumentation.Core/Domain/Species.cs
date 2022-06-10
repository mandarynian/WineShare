using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace WineDocumentation.Core.Domain
{
    public class Species
    {
        
        public Guid Id { get; protected set; }
        public string Sepciesname { get; protected set; }
        public string Color { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
 
        public Species()
        {
        }

        public Species(Species species)
        {
            this.Id = species.Id;
            this.Sepciesname = species.Sepciesname;
            this.Description = species.Description;
            this.CreatedAt = species.CreatedAt;
            this.Color = species.Color;
        }


        public Species(string speciesname)
        {
            Id = Guid.NewGuid();
            Sepciesname = speciesname;
        }

        public Species(string speciesname, string color, string description)
        {
            Id = Guid.NewGuid();
            SetSpeciesName(speciesname);
            Color = color;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetSpeciesName(string speciesname)
        {
            if(string.IsNullOrWhiteSpace(speciesname))
            {
                throw new Exception("Speciesname is invalid.");
            }

            Sepciesname = speciesname;
        }

        public void SetColor(Color color)
        {
            if(color.IsEmpty || color == null)
            {
                throw new Exception("Color is invalid.");
            }

        }

        
        public static Species CreateSpecies(string speciesname, string color, string description)
            => new Species(speciesname,color, description);
    }
}