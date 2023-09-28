using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _5112AssignmentOne.Controllers
{
    public class NumberMachineController : ApiController
    {
        //<summary>multiples integer by five, adds ten, subtracts 4 and divides by eight</summary>
        //<example>GET api/NumberMachine/id</example>
        //<returns> id*5+10-4/8</returns>
        public int Get(int id)
        {
            int product = ((id * 5) + 10 - 4) / 8;
            return product;
        }
    }
}
