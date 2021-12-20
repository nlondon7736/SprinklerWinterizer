using SprinklerWinterizer.Interfaces;
using SprinklerWinterizer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SprinklerWinterizer.Services
{
    public class SprinklerBlowoutWorkflow
    {

        private readonly ISprinklerApi _api;
        private readonly ILogger _logger;

        public SprinklerBlowoutWorkflow(
            ISprinklerApi api,
            ILogger logger)
        {
            _api = api;
            _logger = logger;
        }

        public void Start(BlowoutOptions options)
        {
            _logger.Log("Start");
            Log(options);
            var person = GetPerson();
            ProcessDevices(
                person.devices,
                options);
            _logger.Log("End");
        }

        private void ProcessDevices(
            Device[] devices,
            BlowoutOptions options)
        {
            _logger.Log("ProcessDevices");
            _logger.Log("NumberOfDevices|{0}", devices.Length);
            _logger.LogSeparator();
            foreach (var device in devices)
            {
                _logger.Log("DeviceId|{0}", device.id);
                var zones = GetZones(device.zones, options);
                
                ProcessZones(zones, options);
            }
        }

        private void ProcessZones(
            Zone[] zones,
            BlowoutOptions options)
        {
            _logger.Log("ProcessZones");
            _logger.Log("NumberOfZones|{0}", zones.Length);
            _logger.LogSeparator();
            foreach (var zone in zones)
            {
                ProcessZone(zone, options);
            }
        }

        private void ProcessZone(
            Zone zone,
            BlowoutOptions options)
        {
            _logger.Log("ProcessZone|{0}|{1}|{2}", zone.id, zone.zoneNumber, zone.name);
            for (int i = 0; i < options.NumberOfIterationsPerZone; i++)
            {
                _logger.Log("Iteration|{0}", i + 1);
                _api.Start(
                    zone,
                    options.DurationPerIteration);
                Wait(options.DurationPerIteration, "zone to finish");
                Wait(options.WaitTimeBetweenIterations, "compressor to recharge");
            }
            _logger.LogSeparator();
        }

        private Zone[] GetZones(
            Zone[] zones,
            BlowoutOptions options)
        {
            _logger.Log("GetZones");
            return zones
                .Where(d => d.enabled)
                .Where(z => z.zoneNumber >= options.StartingZoneNumber)
                .OrderBy(z => z.zoneNumber)
                .ToArray();
        }

        private Person GetPerson()
        {
            _logger.Log("GetPerson");
            var personId = _api.GetPerson();
            _logger.Log("PersonId|{0}", personId);
            var person = _api.GetPerson(personId);
            _logger.Log("Fullname|{0}", person.fullName);
            _logger.Log("Email|{0}", person.email);
            _logger.Log("Devices|{0}", person.devices.Length);
            _logger.LogSeparator();
            return person;

        }

        private void Log(BlowoutOptions options)
        {

            _logger.Log(
                "StartingZoneNumber|{0}",
                options.StartingZoneNumber);

            _logger.Log(
                "Iterations|{0}",
                options.NumberOfIterationsPerZone);

            _logger.Log(
                "DurationPerIteration|{0}",
                options.DurationPerIteration);

            _logger.Log(
                "WaitTimeBetweenIterations|{0}",
                options.WaitTimeBetweenIterations);

            _logger.LogSeparator();

        }

        private void Wait(
            TimeSpan duration,
            string description)
        {
            _logger.Log(
                "Waiting|{0} for {1}",
                duration,
                description);
            Task.Delay(duration).Wait() ;
        }


    }
}
