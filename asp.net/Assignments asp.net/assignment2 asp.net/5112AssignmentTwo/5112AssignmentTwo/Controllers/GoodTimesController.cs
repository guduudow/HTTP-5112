using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _5112AssignmentTwo.Controllers
{
    public class GoodTimesController : ApiController
    {
        ///2009 CCC J3 Question
        /// <summary>
        /////input time in Ottawa (i.e. eastern Time) output local time in each zone in 24 hour format
        ///</summary>
        ///<input>J3/GoodTimes/{time}
        ///issue: how to get time to change as in 1359 should become 1400 and not 1360. use modulus
        ///create a if statement that if the minute hand difference is greater than 30, add an hour to NL time.
        /// <param name="time"></param>
        /// <returns>
        /// time in ottawa
        /// time in toronto
        /// time in winnipeg
        /// time in vancouver
        /// time in halifax
        /// time in st. john's
        /// time in edmonton
        /// </returns>
        [Route("J3/GoodTimes/{time}")]
        [HttpGet]
        public IEnumerable<string> Get(int time)
        {



            
            int standardTime = time;
            int easternTime = standardTime;
            int centralTime = standardTime - 100;
            int pacificTime = standardTime - 300;
            int atlanticTime = standardTime + 100;
            int newfoundlandTime = standardTime + 130;
            int mountainTime = standardTime - 200;

            

            return new string[] {
                
                (easternTime + " in Ottawa "),
                (easternTime + " in Toronto "),
                (centralTime + " in Winnipeg "),
                (pacificTime + " in Vancouver "),
                (atlanticTime + " in Halifax "),
                (newfoundlandTime + " in St. John's "),
                (mountainTime + " in Edmonton ")
                };



    }
            
            
            
            
    }
}
