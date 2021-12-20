using Newtonsoft.Json;
using SprinklerWinterizer.Interfaces;
using SprinklerWinterizer.Models;
using System;
using System.Net;

namespace SprinklerWinterizer.Services
{
    public class RachioApi : ISprinklerApi
    {
        private const string _baseUri = "https://api.rach.io/1/public/";
        private readonly string _apiKey;

        public RachioApi(string apiKey)
        {
            _apiKey = apiKey;
        }

        public Guid GetPerson()
        {
            return Get<BaseItem>("person/info").id;
        }

        public Person GetPerson(Guid id)
        {
            return Get<Person>(string.Format("person/{0}", id));
        }

        public void Start(Zone zone, TimeSpan duration)
        {
            Put(
                "zone/start",
                string.Format("{0} 'id' : '{1}', 'duration' : {2} {3}", "{", zone.id, (int)duration.TotalSeconds, "}"));
        }

        public void Put(string path, string data)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                webClient.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + _apiKey);
                webClient.UploadString(
                    _baseUri + path,
                    "PUT",
                    data);
            }
        }

        public T Get<T>(string path)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                webClient.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + _apiKey);
                var data = webClient.DownloadString(_baseUri + path);
                return JsonConvert.DeserializeObject<T>(data);
            }
        }
    }
}
