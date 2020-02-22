using System;

namespace Domain
{
    public class Points
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public string Source { get; set; }
        public DateTime DateEarned { get; set; }
    }
}