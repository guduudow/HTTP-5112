using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _5112AssignmentOne.Controllers
{
    public class Q4Controller : ApiController
    {
        //<summary>Outputs the string where id is integer value</summary>
        //<example>GET api/Q4/{id}</example>
        //<returns>Greetings to [id] people!</example>
        public IEnumerable<string> Get(int id)
        {
            return new string[] { "Greetings to" + " " + id + " " + "people!" };
        }
    }
}
