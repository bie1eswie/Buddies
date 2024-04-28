using Buddies.Domain.Interfaces;

namespace Buddies.Application.Services
{
    public class App(IBuddyService buddyService) : IApp
    {
        public async Task Execute()
        {
            Console.WriteLine("==============Thapelo Motubatse's Buddy finder==============");
            var result = await buddyService.GetMovieBuddies("https://swapi.dev/api/people");
            foreach(var resultItem in result)
            {
                Console.WriteLine("=========================================================");
                foreach(var item in resultItem.MovieResults)
                {
                    Console.WriteLine($"Name:  {item.Name}");
                }
            }
            Console.ReadLine();
        }
    }
}
