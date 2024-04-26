using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddies
{
    public class ResultObject
    {
       public  List<MovieResult> Results { get; set; }
       public string Next {  get; set; }
        public ResultObject()
        {
            Results = new List<MovieResult>();
        }
    }
}
