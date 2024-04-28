using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddies
{
    public class MovieResult
    {
        public string Name { get;set; } = string.Empty;
        public List<string> Films { get; set; }
        public MovieResult()
        {
            Films = new List<string>();
        }

    }
}
