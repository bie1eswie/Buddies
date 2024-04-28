using Buddies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddies.Domain.Interfaces
{
    public interface IBuddyService
    {
        Task<List<BuddyResponse>> GetMovieBuddies(string url);
    }
}
