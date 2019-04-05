﻿using System.Text;

namespace Evoq.Surfdude
{
    public class JourneyContext
    {
        public JourneyContext(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                throw new System.ArgumentException("The location is null or whitespace.", nameof(location));
            }

            this.StartingLocation = location;
        }

        public string StartingLocation { get; }

        public bool IgnoreBadResults { get; set; } = false;

        public Encoding DefaultEncoding { get; set; } = Encoding.UTF8;
    }
}