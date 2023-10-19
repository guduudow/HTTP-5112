using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Management;

namespace _5112AssignmentTwo.Controllers
{

    /// <summary>
    /// Gives the amount of ways each number rolled on a die can equal to 10
    /// </summary>
    /// <example>A die rolled twice gives the numbers 13 and 5</example>
    /// GET J2/DiceGame/{m}/{n} where m and n represent the numbers rolled
    /// 9 + 1, 8 + 2, 7 + 3, 6 + 4, 5 +5 are all different ways to add up to 10. Expected output should be 5.
    public class DiceGameController : ApiController
    {
        [Route("J2/DiceGame/{m}/{n}")]
        [HttpGet]

        public string DiceGame(int m, int n)
        {
            int g; //short for gets
            int i;
            int ways = 0;
            /* my original way
            for (g = 1; g <= m && g < 10; g = g + 1)
            {
                if (10 - g <= n)
                {
                    ways++;
                }
            }
            */
            for (g = 1; g <= m; g++)
            {
                for (i =1; i <= n; i++)
                {
                    if (g + i == 10)
                    {
                        ways++;
                    }
                }
            }
            
            return "There are " + ways + " way(s) to get the number 10.";
        }
    }
}
