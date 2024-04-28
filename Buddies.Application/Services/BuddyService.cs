using Buddies.Domain.Entities;
using Buddies.Domain.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Buddies.Application.Services
{
    public class BuddyService : IBuddyService
    {
        private readonly HttpClient _httpClient;

        public BuddyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BuddyResponse>> GetMovieBuddies(string url)
        {
            var allResults = await GetAllResults(url);
            var buddyResponses = new List<BuddyResponse>();

            foreach (var person in allResults)
            {
                bool found = false;
                foreach(var buddies in buddyResponses)
                {
                    if(buddies.MovieResults.Any(x=>x.Name == person.Name))
                    {
                        found = true;
                        break;
                    }
                }
                if (found) continue;
                var charactersInFilms = FilterCharactersInFilms(allResults, person);

                if (charactersInFilms.Count > 1)
                    buddyResponses.Add(new BuddyResponse { MovieResults = charactersInFilms });
            }

            return buddyResponses;
        }

        private async Task<List<MovieResult>> GetAllResults(string url)
        {
            var allResults = new List<MovieResult>();
            ResultObject movieResult;

            do
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var resultObject = await response.Content.ReadAsStringAsync();
                movieResult = JsonConvert.DeserializeObject<ResultObject>(resultObject);

                allResults.AddRange(movieResult.Results);
                url = movieResult.Next;
            }
            while (!string.IsNullOrEmpty(movieResult.Next));

            return allResults;
        }

        private List<MovieResult> FilterCharactersInFilms(List<MovieResult> allResults, MovieResult person)
        {
            var charactersInFilms = new List<MovieResult> { person };

            foreach (var item in allResults.Where(x => !charactersInFilms.Contains(x)))
            {
                var characterFilms = person.Films;

                if (item.Films.All(characterFilms.Contains) && characterFilms.Count == item.Films.Count)
                {
                    charactersInFilms.Add(item);
                }
            }

            return charactersInFilms;
        }
    }
}
