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
        ///<input>J3/GoodTimes/{hour}/{min}
        ///issue: how to get time to change as in 1359 should become 1400 and not 1360. use modulus
        ///create a if statement that if the minute hand difference is greater than 30, add an hour to NL time.
        /// <param name="hour" name="min"></param>
        /// <returns>
        /// time in ottawa
        /// time in toronto
        /// time in winnipeg
        /// time in vancouver
        /// time in halifax
        /// time in st. john's
        /// time in edmonton
        /// </returns>
        [Route("J3/GoodTimes/{hour}/{min}")]
        [HttpGet]
        public IEnumerable<string> Get(int hour, int min)
        {




            int torontoHour = hour;
            int torontoMin = min;
            int ottawaHour = hour;
            int ottawaMin = min;
            int halifaxHour = hour + 1;
            int halifaxMin = min;
            int edmontonHour = hour - 2;
            int edmontonMin = min;
            int victoriaHour = hour - 3;
            int victoriaMin = min;
            int winnipegHour = hour - 1;
            int winnipegMin = min;
            int nlHour = hour;
            int nlMin = min;

            if (nlMin >= 30) //newfoundland is ahead of eastern time by one hour and thirty minutes, if statement is needed if minute hand is greater than 30.
            {
                int sum = nlMin + 30;
                int remainder = sum - 60;
                nlMin = remainder;
                nlHour = nlHour + 2;
            }
            else
            {
                nlHour = nlHour + 1;
                nlMin = nlMin + 30;
            }



            return new string[] {

                ottawaHour.ToString() + ":" + ottawaMin.ToString() + " in Ottawa",
                torontoHour.ToString() + ":" + torontoMin.ToString() + " in Toronto",
                halifaxHour.ToString() + ":" + halifaxMin.ToString() + " in Halifax",
                edmontonHour.ToString() + ":" + edmontonMin.ToString() + " in Edmonton",
                victoriaHour.ToString() + ":" + victoriaMin.ToString() + " in Victoria",
                winnipegHour.ToString() + ":" + winnipegMin.ToString() + " in Winnipeg",
                nlHour.ToString() + ":" + nlMin.ToString() + " in St.John's"




            };




        }
    }
}
