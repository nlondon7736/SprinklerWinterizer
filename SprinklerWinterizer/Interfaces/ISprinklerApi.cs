using SprinklerWinterizer.Models;
using System;

namespace SprinklerWinterizer.Interfaces
{
    public interface ISprinklerApi
    {
        Guid GetPerson();
        Person GetPerson(Guid id);
        void Start(Zone zone, TimeSpan duration);
    }
}
