using System;
using System.Collections.Generic;
using WineDocumentation.Core.Domain;

namespace WineDocumentation.Infrastructure.DTO
{
    public class WineDto
    {
        public Guid Id { get; set; }
        public string Winename { get; set; }
        public string Brand { get; set; } 
        public Species Species { get; set; }
        public string Description { get; set; }
        public List<Score> Scores { get; set; }
    }
}