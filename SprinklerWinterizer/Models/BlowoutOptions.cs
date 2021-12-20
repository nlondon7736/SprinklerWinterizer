using System;

namespace SprinklerWinterizer.Models
{
    public class BlowoutOptions
    {

        public int StartingZoneNumber { get; private set; }
        public int NumberOfIterationsPerZone { get; private set; }
        public TimeSpan DurationPerIteration { get; private set; }
        public TimeSpan WaitTimeBetweenIterations { get; private set; }
        public BlowoutOptions(
            int startingZoneNumber,
            int numberOfIterationsPerZone,
            TimeSpan durationPerIteration,
            TimeSpan waitTimeBetweenIterations)
        {
            StartingZoneNumber = startingZoneNumber;
            NumberOfIterationsPerZone = numberOfIterationsPerZone;
            DurationPerIteration = durationPerIteration;
            WaitTimeBetweenIterations = waitTimeBetweenIterations;
        }
    }
}
