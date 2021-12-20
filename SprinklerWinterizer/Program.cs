using SprinklerWinterizer.Models;
using SprinklerWinterizer.Services;
using System;

namespace SprinklerWinterizer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get your API token by logging into your Rachio account
            var apiAccessToken = "00000000-0000-0000-0000-000000000000";
            var logger = new ConsoleLogger();

            logger.Log("Sprinkler Closeout Automation");
            logger.Log("=============================");
            var api = new RachioApi(apiAccessToken);
            logger.Log("API Initialized");
            var workFlow = new SprinklerBlowoutWorkflow(
                api,
                logger);
            logger.Log("Workflow Initialized");
            var options = new BlowoutOptions(
                1,
                2,
                TimeSpan.FromSeconds(60),
                TimeSpan.FromSeconds(120));
            logger.Log("Options Initialized");
            logger.Log("Starting Zone Number............" + options.StartingZoneNumber);
            logger.Log("Number of Iterations Per Zone..." + options.NumberOfIterationsPerZone);
            logger.Log("Duration Per Iteration.........." + options.DurationPerIteration);
            logger.Log("Wait Time Between Iterations...." + options.WaitTimeBetweenIterations);
            logger.Log("=============================");
            logger.Log("Ready to start. Press any key to begin...");
            Console.ReadKey();
            workFlow.Start(options);
            logger.LogSeparator();
            logger.Log("Done");
            Console.ReadKey();
        }
    }
}
